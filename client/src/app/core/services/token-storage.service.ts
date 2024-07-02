import { Injectable } from '@angular/core';
import { TokenResponseModel } from '../model/dto/token-response-model';

const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root',
})
export class TokenStorageService {
  constructor() { }

  signOut(): void {
    localStorage.removeItem(USER_KEY);
    localStorage.clear();
  }

  public saveAccessToken(accessToken: string): void {
    var user = this.getUser();
    if (user == null) {
      return;
    }
    user.AccessToken = accessToken;
    this.saveUser(user);
  }

  public getAccessToken(): string | null {
    var user = this.getUser();
    if (user == null) {
      return null;
    }
    return user.AccessToken;
  }

  public saveRefreshToken(refreshToken: string): void {
    var user = this.getUser();
    if (user == null) {
      return;
    }
    user.RefreshToken = refreshToken;
    this.saveUser(user);
  }

  public getRefreshToken(): string | null {
    var user = this.getUser();
    if (user == null) {
      return null;
    }
    return user.RefreshToken;
  }

  public saveUser(tokenResponse: TokenResponseModel): void {
    localStorage.removeItem(USER_KEY);
    localStorage.setItem(USER_KEY, JSON.stringify(tokenResponse));
  }

  public getUser(): TokenResponseModel | null {
    const stringifyUser = localStorage.getItem(USER_KEY);
    if (stringifyUser) {
      return JSON.parse(stringifyUser);
    }
    return null;
  }
}
