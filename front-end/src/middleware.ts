import { NextResponse } from 'next/server';
import type { NextRequest } from 'next/server';
// import {useAuthStore} from "@/stores/useAuth";
// import axios from "axios";
// import ApiService from "@/services/api";
// import axios from "axios";

export async function middleware(request: NextRequest) {
    const token = request.cookies.get('accessToken')?.value;
    const { pathname } = request.nextUrl;

    const publicPaths = ['/login', '/register'];
    // const protectedPaths = ['/admin'];
    // const ACCESS_DENIED_PATH = '/access-denied';
    // const ALLOW_ACCESS_COOKIE = 'allowed-access';
    //
    // // Блокируем прямой доступ к /access-denied
    // if (pathname === ACCESS_DENIED_PATH) {
    //     const allowed = request.cookies.get(ALLOW_ACCESS_COOKIE)?.value === 'true';
    //     if (!allowed) {
    //         return NextResponse.redirect(new URL('/', request.url));
    //     }
    //
    //     // Удаляем куку после успешной проверки
    //     const response = NextResponse.next();
    //     response.cookies.delete(ALLOW_ACCESS_COOKIE);
    //     return response;
    // }

    // Редирект авторизованных пользователей с публичных страниц
    if (token && publicPaths.includes(pathname)) {
        return NextResponse.redirect(new URL('/', request.url));
    }

    // Проверка защищенных маршрутов
    // if (protectedPaths.some(path => pathname.startsWith(path))) {
    //     if (!token) {
    //         console.log('No token');
    //         return NextResponse.redirect(new URL('/login', request.url));
    //     }
    //
    //     const user = await axios.get('http://localhost:5189/user/me', {
    //         headers: {
    //             Authorization: `Bearer ${token}`
    //         }
    //     });
    //
    //     try {
    //         if (!user.data.roles?.includes('Admin')) {
    //             const response = NextResponse.redirect(new URL(ACCESS_DENIED_PATH, request.url));
    //             response.cookies.set(ALLOW_ACCESS_COOKIE, 'true', {
    //                 maxAge: 5, // Срок жизни 5 секунд
    //                 httpOnly: true,
    //                 sameSite: 'strict'
    //             });
    //             return response;
    //         }
    //     } catch (error) {
    //         console.error(error);
    //         return NextResponse.redirect(new URL('/login', request.url));
    //     }
    // }

    return NextResponse.next();
}

export const config = {
    matcher: [
        '/((?!api|_next/static|_next/image|favicon.ico).*)',
    ],
}
