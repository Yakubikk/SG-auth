type JSONValue =
    | null
    | string
    | number
    | boolean
    | { [x: string]: JSONValue }
    | Array<JSONValue>;

export interface UserRole {
    id: string;
    name: string;
    description: string;
    permissions: JSONValue;
}

export interface Department {
    id: string;
    name: string;
    description: string;
}

export interface User {
    id: string;
    userName: string;
    role: UserRole;
    fullName: string;
    email: string;
    phone?: string;
    department: Department;
    isActive: boolean;
    lastLogin: string;
    createdAt: string;
    updatedAt: string;
}

export interface SystemAction {
    id: string;
    name: string;
    description: string;
    entityType: string;
}

export interface ActionLog {
    id: string;
    user: User;
    action: SystemAction;
    entityType: string;
    entityId: string;
    actionDate: string;
    ipAddress: string;
    userAgent: string;
    oldValues: JSONValue;
    newValues: JSONValue;
    status: string;
}

export interface RegisterPayload {
    userName: string;
    fullName: string;
    email: string;
    password: string;
    phone?: string;
}

export interface RegisterResponse {
    id: string;
}

export interface LoginPayload {
    email: string;
    password: string;
    rememberMe: boolean;
}

export interface LoginPhonePayload {
    phone: string;
}

export interface LoginResponse {
    id?: string;
    token?: string;
}

export interface LoginPhoneResponse {
    code: string;
}

export interface LoginCodePayload {
    code: string;
    phone: string;
}

export interface LoginCodeResponse {
    id: string;
}
