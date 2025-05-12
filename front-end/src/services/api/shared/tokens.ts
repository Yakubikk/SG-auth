import { setCookie, deleteCookie } from 'cookies-next';

export const setTokens = async (
    {
        accessToken,
        refreshToken,
        expiresIn,
    }: {
    accessToken: string;
    refreshToken: string;
    expiresIn: number;
}) => {
    await setCookie('accessToken', accessToken, {
        maxAge: expiresIn,
        path: '/',
        secure: false,
        sameSite: 'strict',
    });
    await setCookie('refreshToken', refreshToken, {
        maxAge: 604800,
        path: '/',
        secure: false,
        sameSite: 'strict',
    });
};

export const clearTokens = async () => {
    await deleteCookie('accessToken');
    await deleteCookie('refreshToken');
};
