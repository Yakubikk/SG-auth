'use client';

import {useAuthStore} from "@/stores/useAuth";

const UserTitle = () => {
    const {user} = useAuthStore();
    return (
            <span
                className="text-xl font-semibold mb-4"
            >
                {`Welcome, ${user ? user.userName + '!' : ''}`}
            </span>
    );
};

export { UserTitle };
export default UserTitle;
