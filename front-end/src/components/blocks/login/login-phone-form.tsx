'use client';

import React from 'react';
import { useForm, FormProvider } from 'react-hook-form';
import { Button, Checkbox } from '@/components';
import { TextField } from '@/components/inputs/text-field';
import { cn } from '@/lib/utils';

export interface LoginPhoneFormValues {
    phone: string;
    rememberMe: boolean;
}

const LoginPhoneForm: React.FC = () => {

    const methods = useForm<LoginPhoneFormValues>({
        criteriaMode: 'all',
        defaultValues: {
            phone: '',
            rememberMe: false,
        }
    });

    const {
        handleSubmit,
        register,
        formState: { errors, isSubmitting },
    } = methods;

    const onSubmit = async () => {
        // const response = await onSubmitPhoneForm(values);
        //
        // if (response) {
        //     router.push(`/login/code?phone=${encodeURIComponent(values.phone)}`);
        // } else {
        //     toast.error('Ошибка при отправке кода');
        // }
    };

    const phoneRegex = /^(?:\+375 \(\d{2}\) \d{3}-\d{2}-\d{2}|\+7 \(\d{3}\) \d{3}-\d{2}-\d{2})$/;

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
                        type='phone'
                        helpText='На этот номер будет отправлен код'
                        {...register('phone', {
                            required: 'Обязательное поле',
                            pattern: {
                                value: phoneRegex,
                                message: 'Неверный формат телефона',
                            },
                        })}
                        error={errors.phone}
                        placeholder='Введите номер телефона'
                    />
                    <Checkbox
                        {...register('rememberMe')}
                        label='Запомнить меня'
                    />
                </div>
                <Button
                    type='submit'
                    ripple
                    fullWidth
                    disabled={isSubmitting}
                >
                    {isSubmitting ? 'Отправка...' : 'Отправить код'}
                </Button>
            </form>
        </FormProvider>
    );
};

export { LoginPhoneForm };
export default LoginPhoneForm;
