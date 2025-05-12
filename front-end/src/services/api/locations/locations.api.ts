import { api, getResponseData } from "@/services/api";
import type { Location } from "@/types";

export const LocationsApi = {
    getAll: async (): Promise<Location[] | undefined> => {
        return getResponseData<Location[]>(
            await api.get('/locations')
        );
    },

    getById: async (id: string): Promise<Location | undefined> => {
        return getResponseData<Location>(
            await api.get(`/locations/${id}`)
        );
    },

    create: async (data: Omit<Location, 'location_id'>): Promise<Location | undefined> => {
        return getResponseData<Location>(
            await api.post('/locations', data)
        );
    },

    update: async (id: string, data: Partial<Location>): Promise<Location | undefined> => {
        return getResponseData<Location>(
            await api.put(`/locations/${id}`, data)
        );
    },

    delete: async (id: string): Promise<void> => {
        return getResponseData<void>(
            await api.delete(`/locations/${id}`)
        );
    },
}
