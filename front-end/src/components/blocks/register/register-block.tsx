import React from 'react';
import RegisterForm from './register-form';

const RegisterBlock = () => {
    return (
        <div className='flex flex-col gap-4 items-center'>
            <span className='text-2xl font-bold'>
                Регистрация
            </span>
            <RegisterForm />
        </div>
    );
};

export { RegisterBlock };
