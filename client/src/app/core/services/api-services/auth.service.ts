import { Injectable } from '@angular/core';
import { RegistrationModel } from '../../model/request/registration-model';
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
  public signin(model: SigninModel): Observable<ServerResponse<any>> {
    var action: string = `${controller}/token`;

    return this.post(action, model);
  }
}
