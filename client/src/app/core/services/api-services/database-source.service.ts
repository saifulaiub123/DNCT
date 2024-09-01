import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';
import { ServerResponse } from '../../model/contract/server-response';
import { Observable } from 'rxjs';
import { ProcessDDLResponse } from '../../model/contract/process-ddl-response';

const controller = 'database-source';
@Injectable({
  providedIn: 'root'
})
export class DatabaseSourceService extends BaseApiService {

  public createNewObject(model: any): Observable<ServerResponse<any>> {
    var action: string = `${controller}/create-new-object`;

    return this.post(action, model);
  }

  public processDDL(model: any): Observable<ServerResponse<ProcessDDLResponse>> {
    var action: string = `${controller}/process-ddl`;

    return this.post(action, model);
  }
}
