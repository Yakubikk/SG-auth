import { RegisterBlock } from '@/components'
import Link from 'next/link'
import withAuth from "@/services/with-auth";

function RegisterPage() {
    return (
        <div className='min-h-screen bg-gray-50 p-8'>
            <div className='max-w-4xl mx-auto'>
                <div className='flex justify-between items-center mb-8'>
                    <h1 className='text-2xl font-bold'>Зарегистрировать нового пользователя</h1>
                    <Link href='/admin' className='text-indigo-600 hover:text-indigo-800'>
                        Вернуться
                    </Link>
                </div>

                <RegisterBlock />
            </div>
        </div>
    )
}

export default withAuth(RegisterPage, {
    requiredRoles: ['Admin']
});