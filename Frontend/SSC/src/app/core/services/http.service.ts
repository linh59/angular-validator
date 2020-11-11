import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../models/ResponseModel.interface';
import { AppConfigService } from './app-config.service';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  
  constructor(private http: HttpClient) {
  }

  get<T>(apiLink: string, 
    pathParams?: string[], 
    params?: HttpParams | {
    [param: string]: string | string[];
  }): Observable<ResponseModel<T>> {
    const pathParam = pathParams ? pathParams.join("/") : "";
    return this.http.get<ResponseModel<T>>(`${AppConfigService.settings.apiUrl}${apiLink}/${pathParam}`, {
      params: params
    });
  }

  put<T>(apiLink: string, body: any): Observable<ResponseModel<T>> {
    return this.http.put<ResponseModel<T>>(`${AppConfigService.settings.apiUrl}${apiLink}`, body);
  }

  post<T>(apiLink: string, body: any): Observable<ResponseModel<T>> {
    return this.http.post<ResponseModel<T>>(`${AppConfigService.settings.apiUrl}${apiLink}`, body);
  }

  delete<T>(apiLink: string, objId: any): Observable<ResponseModel<T>> {
    return this.http.delete<ResponseModel<T>>(`${AppConfigService.settings.apiUrl}${apiLink}/${objId}`)
  }
}
