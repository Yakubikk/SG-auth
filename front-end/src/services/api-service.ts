import axios, {
    type AxiosError,
    type AxiosResponse,
} from 'axios';
import type {
    RegisterPayload,
    LoginPayload,
    LoginResponse, User,
} from '@/types';
import {deleteCookie, getCookie, setCookie} from 'cookies-next';

const api = axios.create({
    baseURL: 'http://localhost:5189',
});

const authApi = axios.create({
    baseURL: 'http://localhost:5189',
});

api.interceptors.request.use((config) => {
    const token = getCookie('accessToken');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

// api.interceptors.response.use(
//     (response) => response,
//     async (error) => {
//         const originalRequest = error.config;
//         if (error.response.status === 401 && !originalRequest._retry) {
//             originalRequest._retry = true;
//
//             try {
//                 const refreshToken = getCookie('refreshToken');
//                 const response = await axios.post('http://localhost:5189/refresh', { refreshToken });
//
//                 setCookie('accessToken', response.data.accessToken, {
//                     maxAge: response.data.expiresIn,
//                     path: '/',
//                     secure: true,
//                     sameSite: 'strict',
//                 });
//                 originalRequest.headers.Authorization = `Bearer ${response.data.accessToken}`;
//                 return api(originalRequest);
//             } catch (refreshError) {
//                 console.error("Refresh token failed:", refreshError);
//                 document.cookie = 'accessToken=; path=/; max-age=0';
//                 document.cookie = 'refreshToken=; path=/; max-age=0';
//
//                 window.location.href = '/login';
//             }
//         }
//         return Promise.reject(error);
//     }
// );

const setTokens = async (
    accessToken: string,
    refreshToken: string,
    expiresIn: number
) => {
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
}

const getResponseData = <T>(
    response: AxiosError | AxiosResponse
): T | undefined => {
    return (response as AxiosResponse<T>)?.data;
};

const ApiService = {
    postRegister: async (
        formData: RegisterPayload
    ): Promise<void> => {
        console.log(formData);
        return getResponseData<void>(
            await authApi.post('/register', {
                email: formData.email,
                password: formData.password,
            })
        );
    },
    postLogin: async (
        formData: LoginPayload
    ): Promise<LoginResponse | undefined> => {
        return getResponseData<LoginResponse>(
            await authApi.post('/login', {
                email: formData.email,
                password: formData.password,
            })
        );
    },
    logout: async (): Promise<void> => {
        await deleteCookie('accessToken');
        await deleteCookie('refreshToken');
        window.location.href = '/login';
    },
    getUser: async (): Promise<User | undefined> => {
        return getResponseData<User>(
            await api.get('/user/me')
        );
    },
};

export { ApiService, api, setTokens };
