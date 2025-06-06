'use server';

import {ApiService} from '@/services/api-service';
import type {LoginPayload} from '@/types';

export const onSubmitForm = async (values: LoginPayload) => {
    try {
        return await ApiService.postLogin(values);
    } catch (error) {
        throw error;
    }
};
