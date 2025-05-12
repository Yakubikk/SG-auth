import { Vessel } from "@/types";
import { api, getResponseData } from "@/services/api";

export const VesselsApi = {
    getByCisternId: async (cisternId: string): Promise<Vessel | undefined> => {
        return getResponseData<Vessel>(
            await api.get(`/vessels/${cisternId}`)
        );
    },

    create: async (data: Omit<Vessel, 'id'>): Promise<Vessel | undefined> => {
        return getResponseData<Vessel>(
            await api.post('/vessels', data)
        );
    },

    update: async (cisternId: string, data: Partial<Vessel>): Promise<Vessel | undefined> => {
        return getResponseData<Vessel>(
            await api.put(`/vessels/${cisternId}`, data)
        );
    },

    delete: async (cisternId: string): Promise<void> => {
        return getResponseData<void>(
            await api.delete(`/vessels/${cisternId}`)
        );
    },
}