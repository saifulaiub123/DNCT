import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ServerResponse } from "src/app/core/model/contract/server-response";
import { BaseApiService } from "src/app/core/services/api-services/base-api.service";
import { ApiEndpoints } from "src/app/core/shared/api-endpoints.constant";
import {  TableConfiguration } from "./column-detail.model";
const controller = 'table-col-config';
@Injectable()
export class TableConfigurationService extends BaseApiService{
    fetchAll(): Observable<ServerResponse<TableConfiguration>>{
        const action =`${controller}/${ApiEndpoints.tableConfiguration.getAll}`;
       return  this.get(action);
    }
    createMulti(_model: {data: TableConfiguration[]}): Observable<ServerResponse<TableConfiguration>>{
        const action =`${controller}/${ApiEndpoints.tableConfiguration.createMulti}`;
       return  this.post(action,_model);
    }
    remove(_params: {tbleColConfigId:number,tableConfigId:number}): Observable<ServerResponse<TableConfiguration>>{
        const action =`${controller}/${ApiEndpoints.tableConfiguration.delete}`;
        const params = `?tbleColConfigId=${_params.tbleColConfigId}&tableConfigId=${_params.tableConfigId}`
       return  this.delete(action,params);
    }
}