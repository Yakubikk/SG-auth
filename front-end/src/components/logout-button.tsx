'use client'

import { useRouter } from 'next/navigation'
import {ApiService} from "@/services/api-service";
import toast from "react-hot-toast";

export function LogoutButton() {
    const router = useRouter();

    const handleLogout = async () => {
        try {
            await ApiService.logout();
            router.replace('/login');
        } catch (error) {
            toast.error(error as string);
        }
    }

    return (
        <button
            onClick={handleLogout}
            className="text-indigo-600 hover:text-indigo-800 border border-indigo-600 hover:border-indigo-800 rounded-md px-4 py-2 cursor-pointer"
        >
            Выйти
        </button>
    )
}
