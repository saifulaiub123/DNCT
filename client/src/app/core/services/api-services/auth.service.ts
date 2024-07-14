import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ServerResponse } from '../../model/contract/server-response';
import { BaseApiService } from './base-api.service';
import { SigninModel } from '../../model/request/singin-model';

const controller = 'identity';
@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseApiService {

  public register(model: any): Observable<ServerResponse<any>> {
    var action: string = `${controller}/register`;

    return this.post(action, model);
  }
  public login(model: SigninModel): Observable<ServerResponse<any>> {
    var action: string = `${controller}/token`;

    return this.post(action, model);
  }
  public logout(): Observable<ServerResponse<any>> {
    var action: string = `${controller}/logout`;

    return this.post(action,{});
  }
}
