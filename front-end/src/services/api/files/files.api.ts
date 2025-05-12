import { FileModel } from "@/types";
import { api, getResponseData } from "@/services/api";

export const FilesApi = {
    getAll: async (): Promise<FileModel[] | undefined> => {
        return getResponseData(await api.get('/Files'));
    },

    getById: async (id: string): Promise<FileModel | undefined> => {
        return getResponseData(await api.get(`/Files/${id}`));
    },

    getUserFiles: async (userId: string): Promise<FileModel[] | undefined> => {
        return getResponseData(await api.get(`/Files/User/${userId}`));
    },

    upload: async (userId: string, file: File): Promise<void> => {
        const formData = new FormData();
        formData.append('file', file);
        return getResponseData(
            await api.post(`/Files/User/${userId}`, formData, {
                headers: { 'Content-Type': 'multipart/form-data' },
            })
        );
    },

    delete: async (id: string): Promise<void> => {
        return getResponseData(await api.delete(`/Files/${id}`));
    },

    download: async (fileId: string): Promise<Blob> => {
        const response = await api.get(`/Files/${fileId}/download`, {
            responseType: 'blob',
        });
        return response.data;
    },
};
