import { Injectable } from '@angular/core';
import { TokenResponseModel } from '../model/contract/token-response-model';
import { AuthStateService } from 'src/app/features/authentication/auth-state.service';

const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root',
})
export class TokenStorageService {
  constructor(
    private _authStateService: AuthStateService
  ) { }

  logout(): void {
    localStorage.removeItem(USER_KEY);
    localStorage.clear();
  }

  public saveAccessToken(accessToken: string): void {
    var user = this.getUser();
    if (user == null) {
      return;
    }
    user.token.accessToken = accessToken;
    this.saveUser(user);
  }

  public getAccessToken(): string | null {
    var user = this.getUser();
    if (user == null) {
      return null;
    }
    return user.token.accessToken;
  }

  public saveRefreshToken(refreshToken: string): void {
    var user = this.getUser();
    if (user == null) {
      return;
    }
    user.token.refreshToken = refreshToken;
    this.saveUser(user);
  }

  public getRefreshToken(): string | null {
    var user = this.getUser();
    if (user == null) {
      return null;
    }
    return user.token.refreshToken;
  }

  public saveUser(tokenResponse: TokenResponseModel): void {
    localStorage.removeItem(USER_KEY);
    localStorage.setItem(USER_KEY, JSON.stringify(tokenResponse));
    this._authStateService.setCurrentUser(tokenResponse);
  }

  public getUser(): TokenResponseModel | null {
    const stringifyUser = localStorage.getItem(USER_KEY);
    if (stringifyUser) {
      return JSON.parse(stringifyUser);
    }
    return null;
  }
}
