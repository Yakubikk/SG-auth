'use server';

import {ApiService} from '@/services/api-service';
import type {RegisterPayload} from '@/types';

export const onSubmitForm = async (
    values: RegisterPayload
) => {
    try {
        await ApiService.postRegister(values);
    } catch (error) {
        throw error;
    }
};
