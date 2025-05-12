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
import {useAuthStore} from '@/stores/useAuth';
import ApiService, {setTokens} from '@/services/api';

export interface LoginFormValues {
    email: string;
    password: string;
    rememberMe: boolean;
}

const LoginForm: React.FC = () => {
    const router = useRouter();
    const {setUser} = useAuthStore();

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
            const response = await onSubmitForm(values);
            if (response) {
                await setTokens(response);
                const userResponse = await ApiService.auth.getCurrentUser();
                if (userResponse) {
                    console.log(userResponse);
                    setUser(userResponse);
                }
            }
            toast.success('Вход выполнен успешно!');
            router.push('/');
        } catch (error) {
            console.error((error as Error).message);
            toast.error('Неправильные учетные данные');
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
