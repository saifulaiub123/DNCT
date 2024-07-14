import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';
import { Observable } from 'rxjs';
import { ServerResponse } from '../../model/contract/server-response';
import { SigninModel } from '../../model/request/singin-model';


const controller = 'tree-view';
@Injectable({
  providedIn: 'root'
})
export class TreeViewService extends BaseApiService {

  public getServers(): Observable<ServerResponse> {
    var action: string = `${controller}/GetAllServers`;

    return this.get(action);
  }
  public getDatabaseByServerId(serverId: number): Observable<ServerResponse> {
    var action: string = `${controller}/GetDatabasesByServerId`;

    return this.get(action,'',`id=${serverId.toString()}`);
  }
}
