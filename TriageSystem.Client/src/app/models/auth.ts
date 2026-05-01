
export interface LoginRequest {
    email: string;
    password: string;
}

export interface AuthResponse {
    email: string;
    firstName: string;
    token: string;
    roles: string[];
}