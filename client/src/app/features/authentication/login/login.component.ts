import { Component, OnInit } from '@angular/core';
import { CoreService } from 'src/app/core/services/core.service';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { MaterialModule } from '../../../material.module';
import { AuthService } from 'src/app/core/services/api-services/auth.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { TokenStorageService } from 'src/app/core/services/token-storage.service';
import { BehaviorSubject } from 'rxjs';
import { TokenResponseModel } from 'src/app/core/model/contract/token-response-model';
import { AuthStateService } from '../auth-state.service';
import { ServerResponse } from 'src/app/core/model/contract/server-response';
import { MsalLoginComponent } from "../component/msal-login/msal-login.component";
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-boxed-login',
  standalone: true,
  imports: [RouterModule, MaterialModule, FormsModule, ReactiveFormsModule, MsalLoginComponent],
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit{

  $currentUser: BehaviorSubject<TokenResponseModel | null>;
  redirectUrl: string = '';

  options = this._settings.getOptions();

  constructor(
    private _settings: CoreService,
    private _router: Router,
    private _authService: AuthService,
    private _ngxService: NgxUiLoaderService,
    private _tokenStorageService: TokenStorageService,
    private _authStateService: AuthStateService,
    private _toastr: ToastrService,
  ) { }

  ngOnInit(): void {
    this.sharedSubscription();
  }

  loginForm = new UntypedFormGroup({
    email: new UntypedFormControl('', [Validators.required, Validators.email]),
    password: new UntypedFormControl('', [Validators.required]),
  });

  get f() {
    return this.loginForm.controls;
  }
  public setRedirectURl(redirectUrl: string) {
    this.redirectUrl = redirectUrl;
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
  login() {

    if(!this.loginForm.valid)
    {
      return;
    }
    this._ngxService.start();
    this._authService.login(this.loginForm.value).subscribe((res: ServerResponse<TokenResponseModel>)=> {

      if (res.isSuccess) {
        this._toastr.success("Successfully logged in","Success");

        this._tokenStorageService.saveUser(res.data);
        // this._authStateService._currentUser$.next(res.data);

        if (this.redirectUrl)
            this._router.navigate([this.redirectUrl]);
        else this._router.navigate(['/user/dashboard']);
      }

      this._ngxService.stop();
    },
    (ex) => {
      console.log(ex);
      this._toastr.error("Something went wrong","Error!");
      this._ngxService.stopAll();
    })

  }
  logout()
  {
    this._ngxService.start();
    this._tokenStorageService.logout();
    // this._toastr.success("Successfully logged out","Success");
    this._router.navigate(['/authentication/login']);
    this._ngxService.stop();
    this._authStateService._logOut$.next(false);
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
