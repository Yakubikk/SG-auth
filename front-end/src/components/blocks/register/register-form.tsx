'use client';

import React from 'react';
import { useForm, FormProvider } from 'react-hook-form';
import { Button, TextField } from '@/components';
import { cn } from '@/lib/utils';
import { type RegisterPayload } from '@/types';
import toast from 'react-hot-toast';
import {useRouter} from "next/navigation";
import {onSubmitForm} from "@/components/blocks/register/register-form-helper";

export interface RegisterFormValues {
    userName: string;
    fullName: string;
    email: string;
    password: string;
    phone?: string;
}

const RegisterForm: React.FC = () => {
    const router = useRouter();

    const methods = useForm<RegisterFormValues>({
        criteriaMode: 'all',
        defaultValues: {
            userName: '',
            fullName: '',
            email: '',
            password: '',
            phone: '',
        },
    });

    const {
        handleSubmit,
        register,
        formState: { errors, isSubmitting },
    } = methods;

    const onSubmit = async (values: RegisterPayload) => {
        try {
            await onSubmitForm(values);
            toast.success('Регистрация выполнена успешно!');
            router.push('/login');
        } catch (error) {
            toast.error(error as string);
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
                        type='userName'
                        {...register('userName', {
                            required: 'Обязательное поле',
                        })}
                        error={errors.email}
                        placeholder={'Введите имя пользователя'}
                    />
                    <TextField
                        clearable
                        type='fullName'
                        {...register('fullName', {
                            required: 'Обязательное поле',
                        })}
                        error={errors.email}
                        placeholder={'Введите фио пользователя'}
                    />
                    <TextField
                        clearable
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
                        clearable
                        type='password'
                        {...register('password', {
                            required: 'Обязательное поле'
                        })}
                        error={errors.password}
                        placeholder={'Введите пароль'}
                    />
                    <TextField
                        clearable
                        type='phone'
                        {...register('phone')}
                        error={errors.password}
                        placeholder={'Введите телефон'}
                    />
                </div>
                <Button
                    type='submit'
                    ripple
                    fullWidth
                    disabled={isSubmitting}
                >
                    {isSubmitting ? 'Регистрируем...' : 'Зарегистрировать'}
                </Button>
            </form>
        </FormProvider>
    );
};

export { RegisterForm };
export default RegisterForm;
