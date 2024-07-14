import { Component, Inject } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, RouterOutlet } from '@angular/router';
import { NgxUiLoaderModule } from 'ngx-ui-loader';
import { MSAL_GUARD_CONFIG, MsalGuardConfiguration, MsalService, MsalBroadcastService, MsalModule } from '@azure/msal-angular';
import { EventMessage, InteractionStatus, RedirectRequest, PopupRequest, AuthenticationResult, EventType } from '@azure/msal-browser';
import { Subject, filter, takeUntil } from 'rxjs';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { SharedModule } from 'src/app/core/shared/shared.module';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { AuthStateService } from '../../auth-state.service';
import { Token, TokenResponseModel } from 'src/app/core/model/contract/token-response-model';

@Component({
  selector: 'app-msal-login',
  templateUrl: './msal-login.component.html',
  styleUrl: './msal-login.component.scss',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterModule,
    SharedModule,
    NgxUiLoaderModule,
    ReactiveFormsModule,
    MatToolbarModule,
    MatButtonModule,
    MatMenuModule,
    MsalModule
  ],

})
export class MsalLoginComponent {
  isIframe = false;
  loginDisplay = false;
  private readonly _destroying$ = new Subject<void>();

  constructor(
    @Inject(MSAL_GUARD_CONFIG) private msalGuardConfig: MsalGuardConfiguration,
    private authService: MsalService,
    private msalBroadcastService: MsalBroadcastService,
    private _tokenStorageService: TokenStorageService,
    private _authStateService: AuthStateService
  ) {}

  ngOnInit(): void {
    this.authService.handleRedirectObservable().subscribe();
    this.isIframe = window !== window.parent && !window.opener; // Remove this line to use Angular Universal

    this.setLoginDisplay();

    this.authService.instance.enableAccountStorageEvents(); // Optional - This will enable ACCOUNT_ADDED and ACCOUNT_REMOVED events emitted when a user logs in or out of another tab or window
    this.msalBroadcastService.msalSubject$
      .pipe(
        filter(
          (msg: EventMessage) =>
            msg.eventType === EventType.ACCOUNT_ADDED ||
            msg.eventType === EventType.ACCOUNT_REMOVED
        )
      )
      .subscribe((result: EventMessage) => {
        if (this.authService.instance.getAllAccounts().length === 0) {
          window.location.pathname = '/';
        } else {
          this.setLoginDisplay();
        }
      });

    this.msalBroadcastService.inProgress$
      .pipe(
        filter(
          (status: InteractionStatus) => status === InteractionStatus.None
        ),
        takeUntil(this._destroying$)
      )
      .subscribe(() => {
        this.setLoginDisplay();
        this.checkAndSetActiveAccount();
      });
  }

  setLoginDisplay() {
    this.loginDisplay = this.authService.instance.getAllAccounts().length > 0;
  }

  checkAndSetActiveAccount() {
    /**
     * If no active account set but there are accounts signed in, sets first account to active account
     * To use active account set here, subscribe to inProgress$ first in your component
     * Note: Basic usage demonstrated. Your app may require more complicated account selection logic
     */
    let activeAccount = this.authService.instance.getActiveAccount();

    if (
      !activeAccount &&
      this.authService.instance.getAllAccounts().length > 0
    ) {
      let accounts = this.authService.instance.getAllAccounts();
      this.authService.instance.setActiveAccount(accounts[0]);
    }
  }

  loginRedirect() {
    if (this.msalGuardConfig.authRequest) {
      this.authService.loginRedirect({
        ...this.msalGuardConfig.authRequest,
      } as RedirectRequest);
    } else {
      this.authService.loginRedirect();
    }
  }

  loginPopup() {
    if (this.msalGuardConfig.authRequest) {
      this.authService
        .loginPopup({ ...this.msalGuardConfig.authRequest } as PopupRequest)
        .subscribe((res: AuthenticationResult) => {
          //todo::: call dnct api to create account with ms email. if email exist api will return token response else it will create user and return response.

          this.authService.instance.setActiveAccount(res.account);
          let tokenResponse = new TokenResponseModel
          (
            null,
            res.account.username,
            res.account.name,
            new Token
              (
                res.accessToken,
                '',
                res.tokenType,
                res.expiresOn?.getMinutes()
              )
            )
        this._tokenStorageService.saveUser(tokenResponse);
        // this._authStateService._currentUser$.next(tokenResponse);
        });
    } else {
      this.authService
        .loginPopup()
        .subscribe((response: AuthenticationResult) => {
          this.authService.instance.setActiveAccount(response.account);
        });
    }
  }

  logout(popup?: boolean) {
    if (popup) {
      this.authService.logoutPopup({
        mainWindowRedirectUri: '/',
      });
    } else {
      this.authService.logoutRedirect();
    }
  }

  ngOnDestroy(): void {
    this._destroying$.next(undefined);
    this._destroying$.complete();
  }
}
