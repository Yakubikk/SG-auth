'use server';

import ApiService from '@/services/api';
import type {RegisterPayload} from '@/types';

export const onSubmitForm = async (
    values: RegisterPayload
) => {
    try {
        await ApiService.auth.register(values);
    } catch (error) {
        throw error;
    }
};
