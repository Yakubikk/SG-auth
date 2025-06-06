'use client'

import {ApiService} from "@/services/api-service";

export function LogoutButton() {

    const handleLogout = async () => {
        try {
            await ApiService.logout();
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
