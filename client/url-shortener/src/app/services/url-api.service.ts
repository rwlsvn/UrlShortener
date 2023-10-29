import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ShortenedUrlModel } from '../models/urls/shortened-url.model';
import { ShortenedUrlDetailsModel } from '../models/urls/shortened-url-details.model';
import { AddUrlModel } from '../models/urls/add-url.model';
import { DeleteUrlResponse } from '../models/urls/delete-url-response';
import { AddUrlResponse } from '../models/urls/add-url-response';

@Injectable({
  providedIn: 'root'
})
export class UrlApiService {
  private urlApiUrl = 'https://localhost:7061/api/'

  constructor(private http: HttpClient) { }

  getAllUrls(){
    return this.http.get<ShortenedUrlModel[]>(`${this.urlApiUrl}user/shortenerurl/all`);
  }

  getDetailedUrl(id: string){
    return this.http.get<ShortenedUrlDetailsModel>(`${this.urlApiUrl}user/shortenerurl/get/${id}`);
  }

  addUrl(addUrlDto: AddUrlModel){
    return this.http.post<AddUrlResponse>(`${this.urlApiUrl}user/shortenerurl/add`, addUrlDto);
  }

  deleteUserUrl(id: string){
    return this.http.delete<DeleteUrlResponse>(`${this.urlApiUrl}user/shortenerurl/delete/${id}`);
  }

  deleteAdminUrl(id: string){
    return this.http.delete<DeleteUrlResponse>(`${this.urlApiUrl}admin/shortenerurl/delete/${id}`);
  }

  getAboutText(){
    return this.http.get('https://localhost:7061/about.txt', { responseType: 'text' });
  }

  updateAboutText(text: string){
    const request = {
      message: text
    };
    return this.http.post(`${this.urlApiUrl}about`, request, { responseType: 'text' });
  }
}
