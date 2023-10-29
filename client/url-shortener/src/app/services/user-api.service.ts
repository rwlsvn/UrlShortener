import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserApiService {

  authUrl = 'https://localhost:7135/api/'

  constructor(private http: HttpClient) { }

  getUserName(id: string){
    return this.http.get<any>(`${this.authUrl}user/get/username/${id}`);
  }
}
