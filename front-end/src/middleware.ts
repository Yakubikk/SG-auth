import { NextResponse } from 'next/server';
import type { NextRequest } from 'next/server';
import {ApiService} from "@/services/api-service";

export async function middleware(request: NextRequest) {
    const token = request.cookies.get('accessToken')?.value;
    const { pathname } = request.nextUrl;

    const publicPaths = ['/login', '/register'];

    if (token && publicPaths.includes(pathname)) {
        return NextResponse.redirect(new URL('/', request.url));
    }

    if (publicPaths.includes(pathname)) {
        return NextResponse.next();
    }

    if (!token) {
        try {
            await ApiService.checkAuth();
            return NextResponse.next();
        } catch (error) {
            console.error(error);
            return NextResponse.redirect(new URL('/login', request.url));
        }

    }
}

export const config = {
    matcher: [
        '/((?!api|_next/static|_next/image|favicon.ico).*)',
    ],
}
