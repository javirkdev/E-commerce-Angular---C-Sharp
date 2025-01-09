import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Result } from '../models/result';
import { UserDto } from '../models/user-dto';
import { PassDto } from '../models/pass-dto';
import { AuthService } from './auth.service';
import { AuthResponse } from '../models/auth-response';
import Swal from 'sweetalert2';
import { EditDto } from '../models/edit-dto';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly TOKEN_KEY = 'token';
  decodedToken: any = null;

  constructor(private api: ApiService, private authService: AuthService) { }

  public async getUserOrder(): Promise<Result<UserDto>> {
    const userOrder = await this.api.get<UserDto>('User/UserOrder');
    return userOrder;
  }

  public async updatePass(passDto: PassDto): Promise<Result<AuthResponse>> {
    const result = await this.api.put<AuthResponse>('Auth/UpdatePass', passDto);
    
    return result;
  }

  public async editUser(editDto: EditDto): Promise<Result<AuthResponse>> {
    const result = await this.api.put<AuthResponse>('Auth/UserUpdate', editDto);
    if (result.success) {
      this.authService.handleSession(result.data.accessToken, false)
    }
    return result;
  }

  public async getEditUser(): Promise<Result<EditDto>>{
    const editUser = await this.api.get<EditDto>('Auth/GetEditUser');
    return editUser;
  }

  /* public async editUser(editDto: EditDto): Promise<Result<string>>{
    const result = await this.api.put<string>('Auth/UserUpdate', editDto);
    return result;
  } */
}
