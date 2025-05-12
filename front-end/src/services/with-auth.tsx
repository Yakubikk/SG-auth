import React, { ComponentType, Suspense } from "react";
import { User } from "@/types";
import { redirect } from "next/navigation";
import { Loading } from "@/components";
import { cookies } from "next/headers";
import axios from "axios";

export type WithAuthProps = {
    user: User;
};

type WithAuthOptions = {
    loadingComponent?: React.ComponentType;
    requiredRoles?: string[];
    customCheck?: (user: User) => boolean;
};

const withAuth = <P extends WithAuthProps>(
    WrappedComponent: ComponentType<P>,
    options?: WithAuthOptions
) => {
    return async function AuthenticatedComponent(
        props: Omit<P, keyof WithAuthProps>
    ) {
        const {
            loadingComponent: LoadingComponent = Loading,
            requiredRoles = [],
            customCheck
        } = options || {};

        try {
            const token = (await cookies()).get('accessToken')?.value;

            if (!token) {
                console.error('User is not authenticated');
                redirect('/login');
            }

            const response = await axios.get('http://localhost:5189/user/me', {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            // 1. Проверка аутентификации
            if (response.status === 401) {
                console.error('User is not authenticated');
                redirect('/login');
            }

            const user = response.data as User;

            // 2. Проверка ролей
            if (requiredRoles.length > 0) {
                const hasRequiredRole = requiredRoles.some(role =>
                    user.roles?.includes(role)
                );

                if (!hasRequiredRole) {
                    redirect('/access-denied');
                }
            }

            // 3. Кастомная проверка (если предоставлена)
            if (customCheck && !customCheck(user)) {
                redirect('/access-denied');
            }

            // 4. Рендер защищенного компонента
            return (
                <Suspense fallback={<LoadingComponent />}>
                    <WrappedComponent {...(props as P)} user={user}/>
                </Suspense>
            );

        } catch (error) {
            if (error instanceof Error && error.message.includes('NEXT_REDIRECT')) {
                throw error;
            }
            console.error('Authentication check failed:', error);
            redirect('/login');
        }
    };
};

export { withAuth };
export default withAuth;
