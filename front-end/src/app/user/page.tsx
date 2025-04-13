import { LogoutButton } from '@/components/logout-button'

export default function UserDashboard() {
  return (
    <div className="min-h-screen bg-gray-50 p-8">
      <div className="max-w-4xl mx-auto">
        <div className="flex justify-between items-center mb-8">
          <h1 className="text-2xl font-bold">User Dashboard</h1>
          <LogoutButton />
        </div>
        
        <div className="bg-white p-6 rounded shadow-md">
          <h2 className="text-xl font-semibold mb-4">Welcome, User!</h2>
          <p className="text-gray-600">This is your personal dashboard with user-specific content.</p>
        </div>
      </div>
    </div>
  )
}