import { Component } from '@angular/core';
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

@Component({
  selector: 'app-boxed-login',
  standalone: true,
  imports: [RouterModule, MaterialModule, FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
})
export class LoginComponent {

  $currentUser: BehaviorSubject<TokenResponseModel | null>;
  redirectUrl: string = '';

  options = this._settings.getOptions();

  constructor(
    private _settings: CoreService,
    private _router: Router,
    private _authService: AuthService,
    private _ngxService: NgxUiLoaderService,
    private _tokenStorageService: TokenStorageService,
  ) { }

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

  signin() {

    if(!this.loginForm.valid)
    {
      return;
    }
    this._ngxService.start();
    this._authService.signin(this.loginForm.value).subscribe((res: any)=> {

      if (res.isSuccess) {
        this._tokenStorageService.saveUser(res.data);

        if (this.redirectUrl)
            this._router.navigate([this.redirectUrl]);
        else this._router.navigate(['/user/dashboard']);
      }
      this._ngxService.stop();
    },
    (ex) => {
      console.log(ex);
      this._ngxService.stop();
    })

  }
}
