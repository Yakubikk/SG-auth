import { Registrar } from "@/types";
import { api, getResponseData } from "@/services/api";

export const RegistrarsApi = {
    getAll: async (): Promise<Registrar[] | undefined> => {
        return getResponseData<Registrar[]>(
            await api.get('/registrars')
        );
    },

    getById: async (id: number): Promise<Registrar | undefined> => {
        return getResponseData<Registrar>(
            await api.get(`/registrars/${id}`)
        );
    },

    create: async (data: Omit<Registrar, 'id'>): Promise<Registrar | undefined> => {
        return getResponseData<Registrar>(
            await api.post('/registrars', data)
        );
    },

    update: async (id: number, data: Partial<Registrar>): Promise<Registrar | undefined> => {
        return getResponseData<Registrar>(
            await api.put(`/registrars/${id}`, data)
        );
    },

    delete: async (id: number): Promise<void> => {
        return getResponseData<void>(
            await api.delete(`/registrars/${id}`)
        );
    },
}