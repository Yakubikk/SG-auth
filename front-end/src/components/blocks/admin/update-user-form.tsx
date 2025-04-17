'use client';

import React, {useEffect} from 'react';
import { useForm, FormProvider } from 'react-hook-form';
import { Button } from '@/components';
import { TextField } from '@/components/inputs/text-field';
import { cn } from '@/lib/utils';
import ApiService from "@/services/api-service";
import {useAuthStore} from "@/stores/useAuth";

export interface UserFormValues {
    username: string;
    email: string;
    phoneNumber: string;
}

const UpdateUserForm: React.FC = () => {
    const {user, setUser} = useAuthStore();

    const methods = useForm<UserFormValues>({
        criteriaMode: 'all',
        defaultValues: {
            username: user?.userName || '',
            email: user?.email || '',
            phoneNumber: user?.phoneNumber || '',
        },
    });

    const {
        handleSubmit,
        register,
        reset,
        formState: { errors, isSubmitting },
    } = methods;

    useEffect(() => {
        if (user) {
            reset({
                username: user.userName,
                email: user.email,
                phoneNumber: user.phoneNumber,
            });
        }
    }, [user, reset]);

    const onSubmit = async (values: UserFormValues) => {
        if (user) {
            const response = await ApiService.updateUser(user.id, values);
            if (response) {
                setUser(response);
            }
        }
    };

    return (
        <FormProvider {...methods}>
            <form
                className={cn(
                    'flex w-[400px] max-w-[400px] flex-col gap-10'
                )}
                onSubmit={handleSubmit(onSubmit)}
                autoComplete='off'
            >
                <div className='mt-10 flex flex-col gap-10 desktop:mt-2'>
                    <TextField
                        clearable
                        type='text'
                        {...register('username', {
                            required: 'Обязательное поле',
                        })}
                        error={errors.username}
                        placeholder={'Введите имя пользователя'}
                    />
                    <TextField
                        type='email'
                        {...register('email', {
                            required: 'Обязательное поле',
                            pattern: {
                                value: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
                                message: 'Неверный формат email',
                            },
                        })}
                        error={errors.email}
                        placeholder={'Введите email'}
                    />
                    <TextField
                        type='phone'
                        {...register('phoneNumber')}
                        error={errors.phoneNumber}
                    />
                </div>
                <Button
                    type='submit'
                    ripple
                    fullWidth
                    disabled={isSubmitting}
                >
                    {isSubmitting ? 'Обновляется...' : 'Обновить'}
                </Button>
            </form>
        </FormProvider>
    );
};

export {UpdateUserForm};
export default UpdateUserForm;
