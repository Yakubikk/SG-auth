import { NextResponse } from 'next/server';
import type { NextRequest } from 'next/server';
import {ApiService} from "@/services/api-service";

export async function middleware(request: NextRequest) {
    const token = request.cookies.get('.AspNetCore.Identity.Application')?.value;
    const { pathname } = request.nextUrl;

    const publicPaths = ['/login', '/register'];

    if (token && publicPaths.includes(pathname)) {
        return NextResponse.redirect(new URL('/', request.url));
    }

    if (publicPaths.includes(pathname)) {
        return NextResponse.next();
    }

    if (!token) {
        return NextResponse.redirect(new URL('/login', request.url));
    }

    try {
        try {
            const userId = await ApiService.checkAuth();
            if (!userId) {
                const response = NextResponse.redirect(new URL('/login', request.url));
                response.cookies.delete('.AspNetCore.Identity.Application');
                return response;
            }
            return NextResponse.next();
        } catch (error) {
            console.log(error);
            new Error(error as string);
        }
    } catch {
        const response = NextResponse.redirect(new URL('/login', request.url));
        response.cookies.delete('.AspNetCore.Identity.Application');
        return response;
    }
}

export const config = {
    matcher: [
        '/((?!api|_next/static|_next/image|favicon.ico).*)',
    ],
}
