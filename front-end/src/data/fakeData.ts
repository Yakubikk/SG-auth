import { Department, User, UserRole } from "@/types";

export const departments: Department[] = [
    {
        id: '1',
        name: 'IT',
        description: 'IT department'
    },
    {
        id: '2',
        name: 'HR',
        description: 'HR department'
    }
];

export const roles: UserRole[] = [
    { 
        id: '1', 
        name: 'Admin', 
        description: 'Admin role', 
        permissions: { all: true } 
    },
    { 
        id: '2', 
        name: 'User', 
        description: 'User role', 
        permissions: { common: true } 
    },
];

export const users: User[] = [
    { 
        id: '1', 
        userName: 'Admin', 
        role: roles.find(role => role.name === 'Admin')!, 
        fullName: 'Admin User', 
        email: 'admin@example.com', 
        department: departments[0], 
        isActive: false, 
        lastLogin: '2023-05-01T10:00:00Z', 
        createdAt: '2023-05-01T10:00:00Z', 
        updatedAt: '2023-05-01T10:00:00Z'   
    },
    { 
        id: '2', 
        userName: 'Common', 
        role: roles.find(role => role.name === 'User')!, 
        fullName: 'Common User', 
        email: 'user@example.com', 
        department: departments[1], 
        isActive: false, 
        lastLogin: '2023-05-01T10:00:00Z', 
        createdAt: '2023-05-01T10:00:00Z', 
        updatedAt: '2023-05-01T10:00:00Z'   
    }
];