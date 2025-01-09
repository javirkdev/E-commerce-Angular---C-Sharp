import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Result } from '../models/result';
import { AuthResponse } from '../models/auth-response';
import { AuthDto } from '../models/auth-dto';
import { RegisterDto } from '../models/register-dto';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly TOKEN_KEY = 'token';
  decodedToken: any = null;

  constructor(private api: ApiService, private router: Router) {
    this.loadTokenFromStorage();
  }

  private loadTokenFromStorage(): void {
    const token =
      sessionStorage.getItem(this.TOKEN_KEY) ||
      localStorage.getItem(this.TOKEN_KEY);
    if (token) {
      this.api.jwt = token;
      this.decodedToken = this.decodeJwt(token);
    }
  }

  async login(
    authData: AuthDto,
    remember: boolean
  ): Promise<Result<AuthResponse>> {
    const result = await this.api.post<AuthResponse>('Auth/Login', authData);

    if (result.success) {
      this.setSession(result.data.accessToken, remember);
    } else {
      this.handleError('El usuario o la contraseña son incorrectos.');
    }

    return result;
  }

  async register(authData: RegisterDto): Promise<Result<AuthResponse>> {
    const result = await this.api.post<AuthResponse>('Auth/Register', authData);

    if (result.success) {
      this.setSession(result.data.accessToken, true);
    } else {
      this.handleError('Ha habido un problema al registrar el usuario.');
    }

    return result;
  }

  private setSession(token: string, remember: boolean): void {
    this.api.jwt = token;
    this.decodedToken = this.decodeJwt(token);

    if (remember) {
      localStorage.setItem(this.TOKEN_KEY, token);
    } else {
      sessionStorage.setItem(this.TOKEN_KEY, token);
    }
  }

  private decodeJwt(token: string): any {
    try {
      return jwtDecode(token);
    } catch (error) {
      console.error('Error decodificando el token JWT:', error);
      return null;
    }
  }

  private handleError(message: string): void {
    Swal.fire({
      icon: 'error',
      text: 'Login Incorrecto',
      showConfirmButton: true,
    });
  }

  get isLoggedIn(): boolean {
    return !!this.decodedToken;
  }

  logout(): void {
    this.api.jwt = '';
    this.decodedToken = null;
    localStorage.removeItem(this.TOKEN_KEY);
    sessionStorage.removeItem(this.TOKEN_KEY);
    this.router.navigate(['/login']);
  }

  async getSecretMessage(): Promise<Result<string>> {
    return await this.api.get<string>('Auth');
  }

  public handleSession(token: string, remember: boolean): void {
    this.clearSession();
    this.setSession(token, remember);
  }

  private clearSession(): void {
    this.api.jwt = null;
    this.decodedToken = null;
    localStorage.removeItem(this.TOKEN_KEY);
    sessionStorage.removeItem(this.TOKEN_KEY);
  }
}
