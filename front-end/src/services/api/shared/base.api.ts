import axios, { type AxiosError, type AxiosResponse } from 'axios';
import {getCookie} from "cookies-next";

const baseURL = 'http://localhost:5189';

export const api = axios.create({ baseURL });

api.interceptors.request.use(async (config) => {
    const token = await getCookie('accessToken');
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
});

// api.interceptors.response.use(
//     (response) => response,
//     async (error) => {
//         const originalRequest = error.config;
//         if (error.response?.status === 401 && !originalRequest._retry) {
//             originalRequest._retry = true;
//
//             try {
//                 const refreshToken = await getCookie('refreshToken');
//                 const response = await axios.post(`${baseURL}/refresh`, { refreshToken });
//                 await setTokens(response.data);
//                 originalRequest.headers.Authorization = `Bearer ${response.data.accessToken}`;
//                 return api(originalRequest);
//             } catch (refreshError) {
//                 await deleteCookie('accessToken');
//                 await deleteCookie('refreshToken');
//                 return Promise.reject(refreshError);
//             }
//         }
//         return Promise.reject(error);
//     }
// );

export const getResponseData = <T>(response: AxiosResponse<T> | AxiosError): T | undefined => {
    return (response as AxiosResponse<T>)?.data;
};
