import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginModel } from '../models/auth/login.model';
import { Router } from '@angular/router';
import { TokenModel } from '../models/auth/token.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { RegisterResponse } from '../models/auth/register-response';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  authUrl = 'https://localhost:7135/api/'

  private userPayload: any;

  constructor(private http: HttpClient, private rout: Router) 
  { 
    this.userPayload = this.decodedToken();
  }

  login(loginModel: LoginModel){
    return this.http.post<any>(`${this.authUrl}identity/login`, loginModel);
  }

  refreshToken(tokenModel: TokenModel){
    return this.http.post<any>(`${this.authUrl}identity/refresh`, tokenModel);
  }

  register(loginModel: LoginModel){
    return this.http.post<RegisterResponse>(`${this.authUrl}user/register`, loginModel);
  }

  signOut(){
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    this.rout.navigate(['login']);
  }

  getToken(){
    return localStorage.getItem('token');
  }

  setToken(tokenValue: string){
    localStorage.setItem('token', tokenValue);
    this.userPayload = this.decodedToken();
  }

  getRefreshToken(){
    return localStorage.getItem('refreshToken');
  }

  setRefreshToken(refreshTokenValue: string){
    localStorage.setItem('refreshToken', refreshTokenValue);
  }

  isLoggedIn(): boolean{
    return !!localStorage.getItem('token');
  }

  decodedToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    return jwtHelper.decodeToken(token);
  }

  getIdFromToken(){
    if(this.userPayload)
      return this.userPayload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"]
  }

  getUsernameFromToken(){
    if(this.userPayload)
      return this.userPayload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
  }

  getRolesFromToken(): string[]{
    if(this.userPayload)
      return this.userPayload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ?? [];
    return [];
  }
}
