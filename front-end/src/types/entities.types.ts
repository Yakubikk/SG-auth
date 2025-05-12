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

export interface Manufacturer {
    id: string;
    name: string;
    country: string;
}

export interface WagonType {
    id: string;
    name: string;
}

export interface Registrar {
    id: number;
    name: string;
}

export interface RailwayCistern {
    id: string;
    manufacturer_id: string;
    build_date: Date;
    tare_weight: number;
    load_capacity: number;
    length: number;
    axle_count: number;
    volume: number;
    filling_volume?: number;
    initial_tare_weight?: number;
    type_id: string;
    model: string;
    serial_number: string;
    registration_number: string;
    registration_date: Date;
    registrar_id?: number;
    notes?: string;
    created_at: Date;
    updated_at: Date;
}

export interface Vessel {
    id: string;
    railway_cisterns_id: string;
    vessel_serial_number?: string;
    vessel_build_date?: Date;
}

export type PartType = 'wheel_pair' | 'side_frame' | 'bolster' | 'coupler' | 'shock_absorber';
export type PartStatus = 'active' | 'decommissioned' | 'extended';
export type LocationType = 'warehouse' | 'wagon' | 'repair_shop' | 'scrap_yard' | 'other';

export interface Part {
    part_id: string;
    part_type: PartType;
    stamp_number: string;
    serial_number?: string;
    manufacture_year?: number;
    current_location?: string;
    status: PartStatus;
    notes?: string;
    created_at: Date;
}

export interface WheelPair {
    part_id: string;
    thickness_left?: number;
    thickness_right?: number;
    wheel_type?: string;
}

export interface SideFrame {
    part_id: string;
    service_life_years?: number;
    extended_until?: Date;
}

export interface Bolster {
    part_id: string;
    service_life_years?: number;
    extended_until?: Date;
}

export interface CouplerModel {
    id: string;
    name: string;
}

export interface Coupler {
    part_id: string;
    model_id?: string;
}

export interface ShockAbsorber {
    part_id: string;
    model?: string;
    manufacturer_code?: string;
    next_repair_date?: Date;
    service_life_years?: number;
}

export interface Location {
    location_id: string;
    name: string;
    type: LocationType;
    description?: string;
}

export interface PartInstallation {
    installation_id: string;
    part_id: string;
    wagon_id?: string;
    installed_at: Date;
    installed_by?: string;
    removed_at?: Date;
    removed_by?: string;
    from_location_id?: string;
    to_location_id: string;
    notes?: string;
}

// Extended interfaces that combine base Part with specific part type data
export interface WheelPairWithDetails extends Part, WheelPair {}
export interface SideFrameWithDetails extends Part, SideFrame {}
export interface BolsterWithDetails extends Part, Bolster {}
export interface CouplerWithDetails extends Part, Coupler {}
export interface ShockAbsorberWithDetails extends Part, ShockAbsorber {}
