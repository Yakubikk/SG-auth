import { LogoutButton } from '@/components/logout-button'
import {GetInfoButton} from "@/components/get-info-button";
import UserTitle from "@/components/blocks/user/user-title";
import withAuth, { WithAuthProps } from "@/services/with-auth";
import Calculator from "@/components/Calculator";

function UserDashboard({user}: WithAuthProps) {
  return (
    <div className="min-h-screen bg-gray-50 p-8 flex flex-col gap-4">
      <div className="max-w-4xl mx-auto">
        <div className="flex justify-between items-center mb-8">
          <h1 className="text-2xl font-bold">User Dashboard</h1>
          <LogoutButton />
          <GetInfoButton />
        </div>
        
        <div className="bg-white p-6 rounded shadow-md">
          <UserTitle userName={user.userName} />
          <p className="text-gray-600">This is your personal dashboard with user-specific content.</p>
        </div>
      </div>
      <Calculator />
    </div>
  )
}

export default withAuth(UserDashboard);
