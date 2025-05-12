'use client'

import ApiService from "@/services/api";
import {useAuthStore} from "@/stores/useAuth";

export function LogoutButton() {
    const { clearUser } = useAuthStore();
    const handleLogout = async () => {
        try {
            clearUser();
            await ApiService.auth.logout();
        } catch (error) {
            console.error(error);
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
