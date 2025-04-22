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
    email: string;
    phoneNumber?: string;
    roles?: string[];
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

export interface LoginPayload {
    email: string;
    password: string;
    rememberMe: boolean;
}

export interface LoginPhonePayload {
    phone: string;
}

export interface LoginResponse {
    accessToken: string;
    refreshToken: string;
    expiresIn: number;
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

export interface FileModel {
    fileId: string;
    fileName: string;
    contentType: string;
    filePath: string;
    size: number;
    isPublic: boolean;
    // Add any other properties you need
}
