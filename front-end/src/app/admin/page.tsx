import { LogoutButton } from '@/components/logout-button'
import Link from 'next/link'

export default function AdminDashboard() {
    return (
        <div className="min-h-screen bg-gray-50 p-8">
            <div className="max-w-4xl mx-auto">
                <div className="flex justify-between items-center mb-8">
                    <h1 className="text-2xl font-bold">Admin Dashboard</h1>
                    <LogoutButton />
                </div>

                <div className="bg-white p-6 rounded shadow-md space-y-6">
                    <div>
                        <h2 className="text-xl font-semibold mb-4">User Management</h2>
                        <Link
                            href="/admin/register"
                            className="inline-block px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                        >
                            Register New User
                        </Link>
                    </div>

                    <div>
                        <h2 className="text-xl font-semibold mb-4">Admin Tools</h2>
                        <div className="space-y-2">
                            <p className="text-gray-600">Other admin functionalities will be here...</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}