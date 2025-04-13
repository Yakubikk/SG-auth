import axios, {
    type AxiosError,
    type AxiosResponse,
} from 'axios';
import type {
    RegisterPayload,
    LoginPayload,
    User,
} from '@/types';
import {deleteCookie} from "cookies-next";

const api = axios.create({
    baseURL: 'http://localhost:5003',
    headers: {
        'Content-Type': 'application/json',
    },
    withCredentials: true,
});

const getResponseData = <T>(
    response: AxiosError | AxiosResponse
): T | undefined => {
    return (response as AxiosResponse<T>)?.data;
};

const ApiService = {
    postRegister: async (
        formData: RegisterPayload
    ): Promise<void> => {
        await api.post(`/register`, {
            email: formData.email,
            password: formData.password,
        });
    },
    postLogin: async (formData: LoginPayload): Promise<void> => {
        await api.post(`/login?${formData.rememberMe ? 'useCookies=true' : 'useSessionCookies=true'}`, {
            email: formData.email,
            password: formData.password
        });
    },
    logout: async (): Promise<void> => {
        deleteCookie('.AspNetCore.Identity.Application');
    },
    checkAuth: async (): Promise<string | undefined> => {
        return getResponseData<string>(
            await api.get(`/user/check`)
        )
    },
    getUser: async (): Promise<User | undefined> => {
        return getResponseData<User>(
            await api.get(`/user/me`)
        );
    },
};

export { ApiService };
