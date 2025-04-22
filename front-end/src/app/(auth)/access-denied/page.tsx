'use client'

import Link from 'next/link';
import ApiService from "@/services/api-service";

export default function AccessDenied() {
    const handleLogout = async () => {
        await ApiService.logout();
    }

    return (
        <div className="min-h-screen flex flex-col items-center justify-center bg-gray-50 p-4">
            <div className="w-full max-w-md bg-white rounded-lg shadow-md p-8 text-center">
                <div className="flex justify-center mb-6">
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        className="h-16 w-16 text-red-500"
                        fill="none"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                    >
                        <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            strokeWidth={2}
                            d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z"
                        />
                    </svg>
                </div>

                <h1 className="text-2xl font-bold text-gray-800 mb-4">Доступ запрещен</h1>

                <p className="text-gray-600 mb-6">
                    У вас недостаточно прав для просмотра этой страницы.
                    Пожалуйста, обратитесь к администратору системы.
                </p>

                <div className="flex flex-col space-y-3">
                    <Link
                        href="/"
                        className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors text-center"
                    >
                        Вернуться на главную
                    </Link>

                    <button
                        type='button'
                        onClick={handleLogout}
                        className="px-4 py-2 border border-gray-300 text-gray-700 rounded-md hover:bg-gray-50 transition-colors text-center cursor-pointer"
                    >
                        Войти под другой учетной записью
                    </button>
                </div>
            </div>
        </div>
    );
}
