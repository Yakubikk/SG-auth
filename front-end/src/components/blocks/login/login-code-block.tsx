'use client';

import React, { useEffect, useState } from 'react';
import { Loading, LoginCodeForm } from '@/components';
import { useSearchParams, useRouter } from 'next/navigation';

const LoginCodeBlock = () => {
    const router = useRouter();
    const searchParams = useSearchParams();
    const phoneParam = searchParams.get('phone');
    const [phone, setPhone] = useState('');

    // Перенаправляем на /login если phone отсутствует
    useEffect(() => {
        if (!phoneParam) {
            router.replace('/login');
        } else {
            setPhone(decodeURIComponent(phoneParam));
        }
    }, [phoneParam, router]);

    // Если phone нет, не рендерим форму (перенаправление произойдет в useEffect)
    if (!phoneParam) {
        return <Loading />;
    }

    return (
        <div className='flex flex-col h-full w-full items-center justify-center gap-6'>
            <span>
                {'enterCode'}
            </span>
            <LoginCodeForm phone={phone} />
        </div>
    );
};

export { LoginCodeBlock };
export default LoginCodeBlock;
