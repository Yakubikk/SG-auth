import React from 'react';
import { LoginForm, LoginPhoneForm, Tabs, TabsContent, TabsList, TabsTrigger } from '@/components';

const LoginBlock = () => {
    return (
        <div className='flex flex-col gap-4 items-center'>
            <span className='text-2xl font-bold'>
                Вход
            </span>
            <Tabs defaultValue='email'>
                <TabsList>
                    <TabsTrigger
                        variant='contained'
                        value='email'
                        ripple
                    >
                        По почте
                    </TabsTrigger>
                    <TabsTrigger
                        variant='contained'
                        value='phone'
                        ripple
                    >
                        По номеру телефона
                    </TabsTrigger>
                </TabsList>
                <TabsContent value='phone'><LoginPhoneForm /></TabsContent>
                <TabsContent value='email'><LoginForm /></TabsContent>
            </Tabs>
        </div>
    );
};

export { LoginBlock };
