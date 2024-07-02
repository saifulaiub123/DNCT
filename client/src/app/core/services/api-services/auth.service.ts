import { Injectable } from '@angular/core';
import { RegistrationModel } from '../../model/request/registration-model';
import { Observable } from 'rxjs';
import { ServerResponse } from '../../model/contract/server-response';
import { BaseApiService } from './base-api.service';

const controller = 'Identity';
@Injectable({
  providedIn: 'root'
})
export class AuthService extends BaseApiService {

  public register(model: any): Observable<ServerResponse> {
    var action: string = `${controller}/Register`;

    return this.post(action, model);
  }

}
