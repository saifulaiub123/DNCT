import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ServerResponse } from "src/app/core/model/contract/server-response";
import { BaseApiService } from "src/app/core/services/api-services/base-api.service";
import { ApiEndpoints } from "src/app/core/shared/api-endpoints.constant";
import { Create, LoadStrategy } from "./load-strategy.model";

@Injectable()
export class LoadStrategyService extends BaseApiService{
    fetchAll(_controller:string, _parameters?: string): Observable<ServerResponse<LoadStrategy>>{
        const action =`${_controller}/${ApiEndpoints.loadStrategy.getAll}`;
       return  this.get(action,_parameters);
    }
    create(_controller:string,_model: Create): Observable<ServerResponse<Create>>{
        const action =`${_controller}/${ApiEndpoints.loadStrategy.create}`;
       return  this.post(action,_model);
    }
}