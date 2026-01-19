export interface AuthResponse {
  isSuccess: boolean;
  message: string;
  token: string;
  username: string;
}

export interface LoginDto {
  email: string;
  password: string;
}

export interface RegisterDto {
  email: string;
  password: string;
  confirmPassword: string;
}
