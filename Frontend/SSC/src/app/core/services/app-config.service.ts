import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CLIENT_CONFIG_PATH } from '../Defination';
import { AppConfig } from '../models/AppConfig.interface';

@Injectable({
  providedIn: 'root'
})
export class AppConfigService {
  public static settings: AppConfig;
  
  constructor(private http: HttpClient) { }
  public load(): Promise<void> {

    return new Promise<void>((resolve, reject) => {
      this.http
        .get(CLIENT_CONFIG_PATH).toPromise().then((response: AppConfig) => {
          AppConfigService.settings = response;
          resolve();
        }).catch((response: any) => {
          reject(`Could not load file '${CLIENT_CONFIG_PATH}': ${JSON.stringify(response)}`);
        });
    });
  }
}
