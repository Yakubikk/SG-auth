import { Manufacturer } from "@/types";
import { api, getResponseData } from "@/services/api";

export const ManufacturersApi = {
    getAllManufacturers: async (): Promise<Manufacturer[] | undefined> => {
        return getResponseData<Manufacturer[]>(
            await api.get('/manufacturers')
        );
    },

    getManufacturerById: async (id: string): Promise<Manufacturer | undefined> => {
        return getResponseData<Manufacturer>(
            await api.get(`/manufacturers/${id}`)
        );
    },

    createManufacturer: async (data: Omit<Manufacturer, 'id'>): Promise<Manufacturer | undefined> => {
        return getResponseData<Manufacturer>(
            await api.post('/manufacturers', data)
        );
    },

    updateManufacturer: async (id: string, data: Partial<Manufacturer>): Promise<Manufacturer | undefined> => {
        return getResponseData<Manufacturer>(
            await api.put(`/manufacturers/${id}`, data)
        );
    },

    deleteManufacturer: async (id: string): Promise<void> => {
        return getResponseData<void>(
            await api.delete(`/manufacturers/${id}`)
        );
    },
}
