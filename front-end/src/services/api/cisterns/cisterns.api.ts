import { RailwayCistern } from "@/types";
import { api, getResponseData } from "@/services/api";

export const CisternsApi = {
    getAll: async (): Promise<RailwayCistern[] | undefined> => {
        return getResponseData<RailwayCistern[]>(
            await api.get('/railway-cisterns')
        );
    },

    getById: async (id: string): Promise<RailwayCistern | undefined> => {
        return getResponseData<RailwayCistern>(
            await api.get(`/railway-cisterns/${id}`)
        );
    },

    create: async (data: Omit<RailwayCistern, 'id' | 'created_at' | 'updated_at'>): Promise<RailwayCistern | undefined> => {
        return getResponseData<RailwayCistern>(
            await api.post('/railway-cisterns', data)
        );
    },

    update: async (id: string, data: Partial<RailwayCistern>): Promise<RailwayCistern | undefined> => {
        return getResponseData<RailwayCistern>(
            await api.put(`/railway-cisterns/${id}`, data)
        );
    },

    delete: async (id: string): Promise<void> => {
        return getResponseData<void>(
            await api.delete(`/railway-cisterns/${id}`)
        );
    },
}