import { Injectable } from '@angular/core';
import { BaseApiService } from './base-api.service';
import { Observable } from 'rxjs';
import { ServerResponse } from '../../model/contract/server-response';
import { SigninModel } from '../../model/request/singin-model';
import { TreeViewResponse } from '../../model/contract/tree-view-response';


const controller = 'tree-view';
@Injectable({
  providedIn: 'root'
})
export class CommonService extends BaseApiService {

  public getServers(): Observable<ServerResponse<TreeViewResponse>> {
    var action: string = `${controller}/GetAllServers`;

    return this.get(action);
  }
  public getDatabaseByServerId(serverId: number): Observable<ServerResponse<TreeViewResponse>> {
    var action: string = `${controller}/GetDatabasesByServerId`;

    return this.get(action,'',`id=${serverId.toString()}`);
  }
  public GetTablesByDatabaseSourceId(dbSourceId: number): Observable<ServerResponse<TreeViewResponse>> {
    var action: string = `${controller}/GetTablesByDatabaseSourceId`;

    return this.get(action,'',`id=${dbSourceId.toString()}`);
  }
  public GetTableInstanceByDatabaseSourceId(dbSourceId: number): Observable<ServerResponse<TreeViewResponse>> {
    var action: string = `${controller}/GetTableInstanceByDatabaseSourceId`;

    return this.get(action,'',`id=${dbSourceId.toString()}`);
  }
}
