import axios, {
    type AxiosError,
    type AxiosResponse,
} from 'axios';
import type {
    RegisterPayload,
    LoginPayload,
    LoginResponse,
    User,
    FileModel,
} from '@/types';
import {deleteCookie, getCookie, setCookie} from 'cookies-next';

const api = axios.create({
    baseURL: 'http://kaftp.online:5000',
});

const authApi = axios.create({
    baseURL: 'http://localhost:5000',
});

api.interceptors.request.use(async (config) => {
    const token = await getCookie('accessToken');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

api.interceptors.response.use(
    (response) => response,
    async (error) => {
        const originalRequest = error.config;
        if (error.response.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;

            try {
                const refreshToken = await getCookie('refreshToken');
                const response = await axios.post('http://kaftp.online:5000/refresh', { refreshToken: refreshToken });
                await setTokens(response.data.accessToken, response.data.refreshToken, response.data.expiresIn);

                originalRequest.headers.Authorization = `Bearer ${response.data.accessToken}`;
                return api(originalRequest);
            } catch (refreshError) {
                console.error("Refresh token failed:", refreshError);
                await deleteCookie('accessToken');
                await deleteCookie('refreshToken');
            }
        }
        return Promise.reject(error);
    }
);

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
    checkAuth: async (): Promise<void> => {
        return getResponseData<void>(
            await api.get('/user/check')
        );
    },
    getUser: async (): Promise<User | undefined> => {
        return getResponseData<User>(
            await api.get('/user/me')
        );
    },
    getAllUsers: async (): Promise<User[] | undefined> => {
        return getResponseData<User[]>(
            await api.get('/user/all')
        );
    },
    getUserById: async (
        id: string
    ): Promise<User | undefined> => {
        return getResponseData<User>(
            await api.get(`/user/${id}`)
        );
    },
    updateUser: async (
        id: string,
        data: {
            username: string;
            email: string;
            phoneNumber: string;
        },
    ): Promise<User | undefined> => {
        return getResponseData<User>(
            await api.put(`/user/${id}`, data)
        );
    },

    // Files API
    getAllFiles: async (): Promise<FileModel[] | undefined> => {
        return getResponseData<FileModel[]>(
            await api.get('/Files')
        );
    },

    getFileById: async (id: string): Promise<FileModel | undefined> => {
        return getResponseData<FileModel>(
            await api.get(`/Files/${id}`)
        );
    },

    getUserFiles: async (userId: string): Promise<FileModel[] | undefined> => {
        return getResponseData<FileModel[]>(
            await api.get(`/Files/User/${userId}`)
        );
    },

    uploadFile: async (userId: string, file: File): Promise<FileModel | undefined> => {
        const formData = new FormData();
        formData.append('file', file);

        return getResponseData<FileModel>(
            await api.post(`/Files/User/${userId}`, formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
        );
    },

    deleteFile: async (id: string): Promise<void> => {
        return getResponseData<void>(
            await api.delete(`/Files/${id}`)
        );
    },

    downloadFile: async (fileId: string): Promise<Blob> => {
        const response = await api.get(`/Files/${fileId}/download`, {
            responseType: 'blob'
        });
        return response.data;
    }
};

export { ApiService, setTokens };
export default ApiService;
