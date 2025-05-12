import { WagonType } from "@/types";
import { api, getResponseData } from "@/services/api";

export const WagonTypesApi = {
    getAll: async (): Promise<WagonType[] | undefined> => {
        return getResponseData<WagonType[]>(
            await api.get('/wagon-types')
        );
    },

    getById: async (id: string): Promise<WagonType | undefined> => {
        return getResponseData<WagonType>(
            await api.get(`/wagon-types/${id}`)
        );
    },

    create: async (data: Omit<WagonType, 'id'>): Promise<WagonType | undefined> => {
        return getResponseData<WagonType>(
            await api.post('/wagon-types', data)
        );
    },

    update: async (id: string, data: Partial<WagonType>): Promise<WagonType | undefined> => {
        return getResponseData<WagonType>(
            await api.put(`/wagon-types/${id}`, data)
        );
    },

    delete: async (id: string): Promise<void> => {
        return getResponseData<void>(
            await api.delete(`/wagon-types/${id}`)
        );
    },
}
