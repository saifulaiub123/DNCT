import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ServerResponse } from "src/app/core/model/contract/server-response";
import { BaseApiService } from "src/app/core/services/api-services/base-api.service";
import { ApiEndpoints } from "src/app/core/shared/api-endpoints.constant";
import { Create, ILoadStrategy } from "./load-strategy.model";
const controller = 'table-col-config';
@Injectable()
export class TableConfigurationService extends BaseApiService{
    fetchAllUser(): Observable<ServerResponse<ILoadStrategy>>{
        const action =`${controller}/${ApiEndpoints.tableConfiguration.getAll}`;
       return  this.get(action);
    }
    createMulti(_model: Create): Observable<ServerResponse<Create>>{
        const action =`${controller}/${ApiEndpoints.tableConfiguration.createMulti}`;
       return  this.post(action,_model);
    }
}