import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { NgxUiLoaderModule, NgxUiLoaderService } from 'ngx-ui-loader';
import { SharedModule } from './core/shared/shared.module';
import { AuthStateService } from './features/authentication/auth-state.service';
import { ToastrService } from 'ngx-toastr';
import { ServerResponse } from './core/model/contract/server-response';
import { TokenResponseModel } from './core/model/contract/token-response-model';
import { AuthService } from './core/services/api-services/auth.service';
import { CoreService } from './core/services/core.service';
import { TokenStorageService } from './core/services/token-storage.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    RouterModule,
    SharedModule,
    NgxUiLoaderModule,
    ReactiveFormsModule,
  ],
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  title = 'DNCT Server Management';

  constructor(
    private _settings: CoreService,
    private _router: Router,
    private _authService: AuthService,
    private _ngxService: NgxUiLoaderService,
    private _tokenStorageService: TokenStorageService,
    private _authStateService: AuthStateService,
    private _toastr: ToastrService,

  ) {}

  ngOnInit(): void {
    if(this._tokenStorageService.getUser())
    {
      this._authStateService.setCurrentUser(this._tokenStorageService.getUser()!);
    }

    this.sharedSubscription();
  }

  sharedSubscription()
  {
    this._authStateService._logOut$.subscribe(res=>{
      if(res)
      {
        this.logout();
      }
    })
  }

  logout()
  {
    this._ngxService.start();

    this._tokenStorageService.logout();
    this._toastr.success("Successfully logged out","Success");
    this._router.navigate(['/authentication/login']);
    this._ngxService.stop();
    // this._authService.logout().subscribe((res: ServerResponse<TokenResponseModel>)=> {
    //   this._tokenStorageService.logout();
    //   this._toastr.success("Successfully logged out","Success");
    //   this._router.navigate(['/authentication/login']);
    // },
    // (ex) => {
    //   console.log(ex);
    //   this._toastr.error("Something went wrong","Error!");
    //   this._ngxService.stopAll();
    // })
  }
}
