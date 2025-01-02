import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ProcessDDLResponse } from '../../model/contract/process-ddl-response';
import { ServerResponse } from '../../model/contract/server-response';
import { BaseApiService } from './base-api.service';
import { UserQueriesResponse } from '../../model/contract/user-queries-response';

const controller = 'user-query';

@Injectable({
  providedIn: 'root'
})
export class UserQueryService extends BaseApiService {

  public getAll(): Observable<ServerResponse<UserQueriesResponse>> {
    var action: string = `${controller}/getAll`;

    return this.get(action);
  }
}

