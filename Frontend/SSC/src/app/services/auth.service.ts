import { Injectable } from '@angular/core';
import { AppConfigService } from '../core/services/app-config.service';
import { HttpService } from '../core/services/http.service';
import { HR_TOKEN_KEY, HR_TOKEN_LAST_SAVE_KEY, POST_LOGIN, SSC_TOKEN_KEY } from '../shared/Defination';

/**
 * @author Thông Hoàng
 */
@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpService) { }

  /**
   * Login API Request
   * @param hrToken Refresh token from HR
   */
  login(hrToken: string) {
    this.hrToken = hrToken;
    this.http.post<string>(POST_LOGIN, {
      hrToken
    }).subscribe(response => {
      if (response.isSuccess) {
        this._token = response.data;
      }
    }, error => {
      console.log(error);
    });
  }

  /**
   * HR Refresh Token
   * @param hrToken HR token
   */
  public set hrToken(hrToken: string) {
    localStorage.setItem(HR_TOKEN_KEY, hrToken);
    localStorage.setItem(HR_TOKEN_LAST_SAVE_KEY, new Date().toJSON());
  }

  /**
   * HR Refresh Token
   */
  public get hrToken(): string {
    if (localStorage.getItem(HR_TOKEN_KEY) === null) {
      // Token is not setted. Return empty string.
      return "";
    }
    // From now to last save HR token is calculated in miliseconds. While config is in minute. So a minute equal with 60.000 miliseconds.
    if (new Date().valueOf() - new Date(localStorage.getItem(HR_TOKEN_LAST_SAVE_KEY)).valueOf() > AppConfigService.settings.hrTokenValidInMinutes * 60000) {
      // Out of token validate time.
      // Should clear token.
      localStorage.removeItem(HR_TOKEN_KEY);
      localStorage.removeItem(HR_TOKEN_LAST_SAVE_KEY);
    }
    return localStorage.getItem(HR_TOKEN_KEY);
  }


  /**
   * Authentication token from SSC
   */
  private get _token() {
    return localStorage.getItem(SSC_TOKEN_KEY);
  }

  /**
   * Authentication token from SSC
   * @param token SSC Token
   */
  private set _token(token: string) {
    localStorage.setItem(SSC_TOKEN_KEY, token);
  }

  /**
   * Token payload as object.
   * @returns Return token payload as object or null.
   */
  public get tokenObj(): any {
    if (this._token === null) {
      return null;
    }
    try {
      return JSON.parse(this._token.split(".")[1].b64Decode());
    }
    catch {
      // Token in localstorage was modified.
      // Should remove.
      localStorage.removeItem(SSC_TOKEN_KEY);
      return null;
    }
  }

  /**
   * SSC Token
   * @returns SSC Token which is current stored or null.
   */
  public get token(): string {
    // Since JWT store in token is presented as seconds, so to compare we have to convert to miliseconds. Formula: 1 second equal with 1000 miliseconds.
    if (this.tokenObj !== null && this.tokenObj.exp * 1000 < new Date().valueOf()) {
      // Token expired. Should remove it.
      localStorage.removeItem(SSC_TOKEN_KEY);
    }
    return this._token;
  }

  /**
   * Authenticated or not
   */
  public get isAuthenicated(): boolean {
    return this.token !== null;
  }
}
