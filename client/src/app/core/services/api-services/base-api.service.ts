import { Token } from './../../model/dto/token-response-model';
import {
  HttpClient,
  HttpHeaders,
  HttpParamsOptions,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from 'src/app/environments/environment';
import { LoaderService } from '../loader.service';
import { NotificationService } from '../notification.service';
import { ServerResponse } from '../../model/contract/server-response';
import { TokenResponseModel } from '../../model/dto/token-response-model';
import { TokenStorageService } from '../token-storage.service';

@Injectable({
  providedIn: 'root',
})
export class BaseApiService {
  private _baseUrl: string = environment.apiUrl;
  private _apiVersion: string = environment.apiVersion;
  $currentUser: BehaviorSubject<TokenResponseModel | null>;


  constructor(
    private _httpClient: HttpClient,
    private _loaderService: LoaderService,
    private _notificationService: NotificationService,
    private tokenStorageService: TokenStorageService
  ) {
    this.$currentUser = new BehaviorSubject<TokenResponseModel | null>(
      this.tokenStorageService.getUser()
    );
  }

  getUser() {
    return this.$currentUser;
  }
  getHeaders(accessToken: string | undefined) {
    return accessToken !== undefined
      ? new HttpHeaders({
          'Content-Type': 'application/json',
          'Accept-Language': 'En-us',
          Authorization: 'Bearer ' + accessToken,
        })
      : new HttpHeaders({
          'Content-Type': 'application/json',
          'Accept-Language': 'En-us',
        });
  }



  protected get(
    action: string,
    paramter: string = '',
    showLoading: boolean = false,
    showWarning: boolean = false,
    noAuth: boolean = false
  ): Observable<ServerResponse> {
    if (showLoading) {
      this._loaderService.addToLoading(action);
    }
    const currentUser: TokenResponseModel|null = this.tokenStorageService.getUser();
    var option = {
      headers: this.getHeaders(currentUser?.token.accessToken),
    };
    return this._httpClient
      .get<ServerResponse>(`${this._baseUrl}/${this._apiVersion}/${action}/${paramter}`, option)
      .pipe(
        tap(
          (response) => {
            this._loaderService.removeFromLoading(action);
            if (!response.isSuccess && showWarning) {
              this._notificationService.showWarning(
                JSON.stringify(!response.isSuccess),
                action
              );
            }
          },
          (ex) => {
            this._loaderService.removeFromLoading(action);
            if (showWarning) {
              this._notificationService.showWarning(ex, '');
            }
          },
          () => {
            this._loaderService.removeFromLoading(action);
          }
        )
      );
  }

  protected post(
    action: string,
    model: any,
    showLoading: boolean = true,
    showNotification: boolean = false,
    noAuth: boolean = false
  ): Observable<ServerResponse> {
    if (showLoading) {
      this._loaderService.addToLoading(action);
    }

    const currentUser: TokenResponseModel|null = this.tokenStorageService.getUser();

    var option = {
      headers: this.getHeaders(currentUser?.token.accessToken),
    };
    return this._httpClient
      .post<ServerResponse>(`${this._baseUrl}/${this._apiVersion}/${action}`, model, option)
      .pipe(
        tap(
          (response) => {
            this._loaderService.removeFromLoading(action);
            if (showNotification) {
              if (response.isSuccess) {
                this._notificationService.ShowSuccess(action, 'Successful');
              } else {
                this._notificationService.showWarning(
                  JSON.stringify(!response.isSuccess),
                  action
                );
              }
            }
          },
          (ex) => {
            this._loaderService.removeFromLoading(action);
            if (showNotification) { this._notificationService.showWarning(ex, ''); }

            return ex;
          },
          () => {
            this._loaderService.removeFromLoading(action);
          }
        )
      );
  }

  protected put(
    action: string,
    model: any,
    parameters: string = '',
    showLoading: boolean = true,
    showNotification: boolean = false
  ): Observable<any> {
    if (showLoading) {
      this._loaderService.addToLoading(action);
    }

    var gAction = '';
    if (parameters == '') {
      gAction = `${this._baseUrl}/${this._apiVersion}/${action}`;
    } else {
      gAction = `${this._baseUrl}/${this._apiVersion}/${action}/${parameters}`;
    }

    const currentUser: TokenResponseModel|null = this.tokenStorageService.getUser();
    var option = {
      headers: this.getHeaders(currentUser?.token.accessToken),//this.getHeaders(this.$currentUser.value?.AccessToken),
    };
    return this._httpClient.put<ServerResponse>(gAction, model, option).pipe(
      tap(
        (response) => {
          this._loaderService.removeFromLoading(action);
          if (showNotification) {
            if (response.isSuccess) {
              this._notificationService.ShowSuccess(action, 'Successful');
            } else {
              this._notificationService.showWarning(
                JSON.stringify(!response.isSuccess),
                action
              );
            }
          }
        },
        (ex) => {
          this._loaderService.removeFromLoading(action);
          if (showNotification) {
            this._notificationService.showWarning(ex, '');
          }
        },
        () => {
          this._loaderService.removeFromLoading(action);
        }
      )
    );
  }

  protected delete(
    action: string,
    paramter: string,
    showLoading: boolean = true,
    showNotification: boolean = false
  ): Observable<any> {

    const currentUser: TokenResponseModel|null = this.tokenStorageService.getUser();
    var option = {
      headers: this.getHeaders(currentUser?.token.accessToken),//this.getHeaders(this.$currentUser.value?.AccessToken),
    };
    return this._httpClient
      .delete<ServerResponse>(`${this._baseUrl}/${this._apiVersion}/${action}/${paramter}`, option)
      .pipe(
        tap(
          (response) => {
            this._loaderService.removeFromLoading(action);
            if (showNotification) {
              if (response.isSuccess) {
                this._notificationService.ShowSuccess(action, 'Successful');
              } else {
                this._notificationService.showWarning(
                  JSON.stringify(!response.isSuccess),
                  action
                );
              }
            }
          },
          (ex) => {
            this._loaderService.removeFromLoading(action);
            if (showNotification) {
              this._notificationService.showWarning(ex, '');
            }
          },
          () => {
            this._loaderService.removeFromLoading(action);
          }
        )
      );
  }
}
