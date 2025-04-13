'use client';

import React, { useEffect, useState } from 'react';
import { useForm, FormProvider, Controller } from 'react-hook-form';
import { Button, InputOTP, InputOTPGroup, InputOTPSeparator, InputOTPSlot } from '@/components';

const LoginCodeForm: React.FC<{ phone: string }> = ({ phone }) => {
    const [timeLeft, setTimeLeft] = useState(15);
    const [showResendButton, setShowResendButton] = useState(false);

    const methods = useForm({
        defaultValues: {
            code: '',
        },
    });

    const {
        handleSubmit,
        control,
    } = methods;

    const onSubmit = async () => {
        // const response = await onSubmitCodeForm({ code: data.code, phone: phone });
        //
        // if (response) {
        //     console.log(response);
        //     router.replace('/');
        // } else {
        //     console.log('Ошибка');
        // }
    };

    const handleResendCode = () => {
        setTimeLeft(15);
        setShowResendButton(false);
    };

    const getResendText = () => {
        const lastDigit = timeLeft % 10;
        const lastTwoDigits = timeLeft % 100;

        if (lastTwoDigits >= 11 && lastTwoDigits <= 14) {
            return `Отправить повторно через ${timeLeft} секунд`;
        }

        switch (lastDigit) {
            case 1:
                return `Отправить повторно через ${timeLeft} секунду`;
            case 2:
            case 3:
            case 4:
                return `Отправить повторно через ${timeLeft} секунды`;
            default:
                return `Отправить повторно через ${timeLeft} секунд`;
        }
    };

    useEffect(() => {
        if (timeLeft > 0) {
            const timer = setTimeout(() => setTimeLeft(timeLeft - 1), 1000);
            return () => clearTimeout(timer);
        } else {
            setShowResendButton(true);
        }
    }, [timeLeft]);

    return (
        <FormProvider {...methods}>
            <form
                className='flex flex-col min-w-[400px] min-h-52 items-center justify-between'
                onSubmit={handleSubmit(onSubmit)}
            >
                <div className='flex flex-col gap-5 items-center'>
                    <Controller
                        name='code'
                        control={control}
                        rules={{
                            validate: (value) => {
                                if (value.length !== 6) {
                                    return 'Код должен содержать 6 цифр';
                                }
                                return true;
                            },
                        }}
                        render={({ field: { onChange, value }, fieldState: { error } }) => (
                            <div className='flex flex-col gap-2 items-center'>
                                <span className='text-sm'>
                                    {`Отправлено на номер ${phone}`}
                                </span>
                                <InputOTP
                                    maxLength={6}
                                    value={value}
                                    onChange={onChange}
                                    autoFocus
                                >
                                    <InputOTPGroup>
                                        <InputOTPSlot index={0} />
                                        <InputOTPSlot index={1} />
                                        <InputOTPSlot index={2} />
                                    </InputOTPGroup>
                                    <InputOTPSeparator />
                                    <InputOTPGroup>
                                        <InputOTPSlot index={3} />
                                        <InputOTPSlot index={4} />
                                        <InputOTPSlot index={5} />
                                    </InputOTPGroup>
                                </InputOTP>
                                {error && (
                                    <span className='text-sm text-red-500'>
                                        {error.message}
                                    </span>
                                )}
                            </div>
                        )}
                    />
                    <Button type='submit' ripple fullWidth>
                        Подтвердить
                    </Button>
                </div>
                {showResendButton ? (
                    <Button
                        type='button'
                        size='sm'
                        ripple
                        variant='outlined'
                        onClick={handleResendCode}
                    >
                        Отправить код повторно
                    </Button>
                ) : (
                    <span className='text-sm'>
                        {getResendText()}
                    </span>
                )}
            </form>
        </FormProvider>
    );
};

export { LoginCodeForm };
export default LoginCodeForm;
