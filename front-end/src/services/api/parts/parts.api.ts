import type {Part, PartType} from "@/types";
import { api, getResponseData } from "@/services/api";

export const PartsApi = {
    getAll: async (): Promise<Part[] | undefined> => {
        return getResponseData<Part[]>(
            await api.get('/parts')
        );
    },

    getById: async (id: string): Promise<Part | undefined> => {
        return getResponseData<Part>(
            await api.get(`/parts/${id}`)
        );
    },

    create: async (data: Omit<Part, 'part_id' | 'created_at'>): Promise<Part | undefined> => {
        return getResponseData<Part>(
            await api.post('/parts', data)
        );
    },

    update: async (id: string, data: Partial<Part>): Promise<Part | undefined> => {
        return getResponseData<Part>(
            await api.put(`/parts/${id}`, data)
        );
    },

    delete: async (id: string): Promise<void> => {
        return getResponseData<void>(
            await api.delete(`/parts/${id}`)
        );
    },

    getByType: async (type: PartType): Promise<Part[] | undefined> => {
        return getResponseData<Part[]>(
            await api.get(`/parts/type/${type}`)
        );
    },

    getByWagon: async (wagonId: string): Promise<Part[] | undefined> => {
        return getResponseData<Part[]>(
            await api.get(`/parts/wagon/${wagonId}`)
        );
    }
};
