import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ServerResponse } from "src/app/core/model/contract/server-response";
import { BaseApiService } from "src/app/core/services/api-services/base-api.service";
import { ApiEndpoints } from "src/app/core/shared/api-endpoints.constant";
import { AutoPopulate, CreateUpdateQuery, UserQuery, ValidateSyntax} from "./user-query-table.model";
import { _defaultParams } from "@angular/material/dialog";
const controller = 'user-query';
@Injectable()
export class UserQueryService extends BaseApiService{
    fetchAllUserQueries(): Observable<ServerResponse<UserQuery>>{
        const action =`${controller}/${ApiEndpoints.userQuery.getAll}`;
       return  this.get(action);
    }
    autoPopulate():Observable<ServerResponse<AutoPopulate>>{
        const action =`${controller}/${ApiEndpoints.userQuery.autoPopulate}?tableConfigId=100&queryId=50`;
       return  this.get(action);
    }
    validateSyntax():Observable<ServerResponse<ValidateSyntax>>{
        const action =`${controller}/${ApiEndpoints.userQuery.validateQuery}`;
       return  this.get(action);
    }
    createOrUpdateQuery(model: CreateUpdateQuery):Observable<ServerResponse<CreateUpdateQuery>>{
        const action =`${controller}/${ApiEndpoints.userQuery.createOrUpdate}`;
       return  this.post(action,model);
    }
    removeQuery(_parameters: {userQueryId:number, tableConfigId: number}):Observable<ServerResponse<CreateUpdateQuery>>{
        const action =`${controller}/${ApiEndpoints.userQuery.delete}`;
        const parameters = `userQueryId=${_parameters.userQueryId}&tableConfigId=${_parameters.tableConfigId}`
       return  this.delete(action,parameters);
    }
}
