import type {PartInstallation} from "@/types";
import { api, getResponseData } from "@/services/api";

export const InstallationsApi = {
    getAll: async (): Promise<PartInstallation[] | undefined> => {
        return getResponseData<PartInstallation[]>(
            await api.get('/part-installations')
        );
    },

    getById: async (id: string): Promise<PartInstallation | undefined> => {
        return getResponseData<PartInstallation>(
            await api.get(`/part-installations/${id}`)
        );
    },

    create: async (data: Omit<PartInstallation, 'installation_id'>): Promise<PartInstallation | undefined> => {
        return getResponseData<PartInstallation>(
            await api.post('/part-installations', data)
        );
    },

    update: async (id: string, data: Partial<PartInstallation>): Promise<PartInstallation | undefined> => {
        return getResponseData<PartInstallation>(
            await api.put(`/part-installations/${id}`, data)
        );
    },

    delete: async (id: string): Promise<void> => {
        return getResponseData<void>(
            await api.delete(`/part-installations/${id}`)
        );
    },

    getByPart: async (partId: string): Promise<PartInstallation[] | undefined> => {
        return getResponseData<PartInstallation[]>(
            await api.get(`/part-installations/part/${partId}`)
        );
    },
};