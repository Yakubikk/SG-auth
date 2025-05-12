'use server';

import type {LoginPayload} from '@/types';
import ApiService from "@/services/api";

export const onSubmitForm = async (values: LoginPayload) => {
    try {
        return await ApiService.auth.login(values);
    } catch (error) {
        throw error;
    }
};
