import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { TokenResponseModel } from 'src/app/core/model/contract/token-response-model';


@Injectable({
  providedIn: 'root',
})
export class AuthStateService {

  private tokenResponse: TokenResponseModel = new TokenResponseModel();
  _currentUser$ = new BehaviorSubject<TokenResponseModel>(this.tokenResponse);

  _logOut$ = new BehaviorSubject<boolean>(false);

  constructor() {}

  setCurrentUser(tokenRes: TokenResponseModel)
  {
    this.tokenResponse = tokenRes;
    this._currentUser$.next(this.tokenResponse);
  }
  getCurrentUser()
  {
    return this.tokenResponse;
  }

}
