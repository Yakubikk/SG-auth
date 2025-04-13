'use client';

import React from 'react';
import { useForm, FormProvider } from 'react-hook-form';
import { Button, Checkbox } from '@/components';
import { TextField } from '@/components/inputs/text-field';
import { cn } from '@/lib/utils';
import type {LoginPayload} from '@/types';
import { useRouter } from 'next/navigation';
import toast from 'react-hot-toast';
import {onSubmitForm} from "@/components/blocks/login/login-form.helper";

export interface LoginFormValues {
    email: string;
    password: string;
    rememberMe: boolean;
}

const LoginForm: React.FC = () => {
    const router = useRouter();

    const methods = useForm<LoginFormValues>({
        criteriaMode: 'all',
        defaultValues: {
            email: '',
            password: '',
            rememberMe: false,
        },
    });

    const {
        handleSubmit,
        register,
        formState: { errors, isSubmitting },
    } = methods;

    const onSubmit = async (values: LoginPayload) => {
        try {
            await onSubmitForm(values);
            toast.success('Вход выполнен успешно!');
            router.push('/');
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
                        {...register('password', { required: 'Обязательное поле' })}
                        error={errors.password}
                        placeholder={'Введите пароль'}
                    />
                    <Checkbox
                        {...register('rememberMe')}
                        label={'Запомнить меня'}
                    />
                </div>
                <Button
                    type='submit'
                    ripple
                    fullWidth
                    disabled={isSubmitting}
                >
                    {isSubmitting ? 'Вход...' : 'Войти'}
                </Button>
            </form>
        </FormProvider>
    );
};

export {LoginForm};
export default LoginForm;
