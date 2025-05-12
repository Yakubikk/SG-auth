import {LoginPayload, LoginResponse, RegisterPayload, User} from "@/types";
import {api, clearTokens, getResponseData} from "@/services/api";

export const AuthApi = {
    register: async (formData: RegisterPayload): Promise<void> => {
        return getResponseData(await api.post('/register', formData));
    },

    login: async (formData: LoginPayload): Promise<LoginResponse | undefined> => {
        return getResponseData(await api.post('/login', formData));
    },

    logout: async (): Promise<void> => {
        await clearTokens();
        window.location.href = '/login';
    },

    checkAuth: async (): Promise<void> => {
        return getResponseData(await api.get('/user/check'));
    },

    getCurrentUser: async (): Promise<User | undefined> => {
        return getResponseData(await api.get('/user/me'));
    },

    getAllUsers: async (): Promise<User[] | undefined> => {
        return getResponseData(await api.get('/user/all'));
    },

    getUserById: async (id: string): Promise<User | undefined> => {
        return getResponseData(await api.get(`/user/${id}`));
    },

    updateUser: async (
        id: string,
        data: { username: string; email: string; phoneNumber: string }
    ): Promise<User | undefined> => {
        return getResponseData(await api.put(`/user/${id}`, data));
    },
};
