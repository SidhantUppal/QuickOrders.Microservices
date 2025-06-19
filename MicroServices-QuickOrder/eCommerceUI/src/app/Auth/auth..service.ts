import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { environment } from '../../environments/environment';
import { LoginRequest, UserResponse } from '../Core/Services/Models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly baseUrl = `${environment.apigatewayUrl}/gateway/users`;

  constructor(private http: HttpClient) {}

  async login(request: LoginRequest): Promise<UserResponse> {
    try {
      return await firstValueFrom(
        this.http.post<UserResponse>(`${this.baseUrl}/login`, request)
      );
    } catch (error) {
      console.error('Login error:', error);
      throw error;
    }
  }

  async register(request: LoginRequest): Promise<UserResponse> {
    try {
      return await firstValueFrom(
        this.http.post<UserResponse>(`${this.baseUrl}/register`, request)
      );
    } catch (error) {
      console.error('Registration error:', error);
      throw error;
    }
  }

  isLoggedIn(): boolean {
  const user = sessionStorage.getItem('user');
  return !!user; // returns true if user exists, false otherwise
}


}
