export interface LoginRequest {
  email: string;
  password: string;
}

export interface User {
  id: number;
  name: string;
  email: string;
  token: string;
}

export interface UserResponse {
  message: string;
  status: boolean;
  data: User | null;
}


export interface RegisterRequest  {
  username: string; // If you want to include username in registration
   email: string;
  password: string;

}
