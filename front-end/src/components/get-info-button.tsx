'use client'

import {ApiService} from "@/services/api-service";
import toast from "react-hot-toast";
import {useState} from "react";

export function GetInfoButton() {
    const handleGetUser = async () => {
        try {
            const response = await ApiService.getUser();
            console.log(response);
        } catch (error) {
            toast.error(error as string);
        }
    };
    const handleGetAllUsers = async () => {
        try {
            const response = await ApiService.getAllUsers();
            console.log(response);
        } catch (error) {
            toast.error(error as string);
        }
    };

    const [id, setId] = useState('');
    const handleGetUserById = async () => {
        try {
            const response = await ApiService.getUserById(id);
            console.log(response);
        } catch (error) {
            toast.error(error as string);
        }
    };

    return (
        <div>
            <button
                onClick={handleGetUser}
                className="text-indigo-600 hover:text-indigo-800 border border-indigo-600 hover:border-indigo-800 rounded-md px-4 py-2 cursor-pointer"
            >
                Get Info
            </button>
            <button
                onClick={handleGetAllUsers}
                className="text-indigo-600 hover:text-indigo-800 border border-indigo-600 hover:border-indigo-800 rounded-md px-4 py-2 cursor-pointer"
            >
                Get All Users
            </button>
            <input
                type="text"
                placeholder="User ID"
                value={id}
                onChange={(e) => setId(e.target.value)}
                className="border border-gray-300 rounded-md px-4 py-2"
            />
            <button
                onClick={handleGetUserById}
                className="text-indigo-600 hover:text-indigo-800 border border-indigo-600 hover:border-indigo-800 rounded-md px-4 py-2 cursor-pointer"
            >
                Get User By ID
            </button>
        </div>
    )
}
