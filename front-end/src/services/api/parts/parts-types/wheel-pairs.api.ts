import type {WheelPair} from "@/types";
import { api, getResponseData } from "@/services/api";

export const WheelPairsApi = {
    getWheelPair: async (partId: string): Promise<WheelPair | undefined> => {
        return getResponseData<WheelPair>(
            await api.get(`/parts/wheel-pairs/${partId}`)
        );
    },

    createWheelPair: async (data: WheelPair): Promise<WheelPair | undefined> => {
        return getResponseData<WheelPair>(
            await api.post('/parts/wheel-pairs', data)
        );
    },
}