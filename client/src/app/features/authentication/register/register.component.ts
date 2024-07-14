import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule, AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { MaterialModule } from '../../../material.module';
import { CoreService } from 'src/app/core/services/core.service';
import { AuthService } from 'src/app/core/services/api-services/auth.service';
import { SharedModule } from 'src/app/core/shared/shared.module';
import { CommonModule } from '@angular/common';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { NotificationService } from 'src/app/core/services/notification.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrService } from 'ngx-toastr';
import { MsalLoginComponent } from '../component/msal-login/msal-login.component';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterModule, MaterialModule, ReactiveFormsModule, SharedModule, MsalLoginComponent],
  templateUrl: './register.component.html',
})
export class RegisterComponent implements OnInit {
  options = this.settings.getOptions();

  constructor(
    private settings: CoreService,
    private router: Router,
    private _authService: AuthService,
    private _ngxService: NgxUiLoaderService,
    private _notificationService: NotificationService,
    private _toastr: ToastrService

  ) { }

  ngOnInit(): void {

  }

  form = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(2)]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(4)]),
    confirmPassword: new FormControl('', [Validators.required, Validators.minLength(4)]),
  }, {
    validators: PasswordMatchValidator('password', 'confirmPassword')
  })

  get f() {
    return this.form.controls;
  }

  submit() {
    if(!this.form.valid)
    {
      return;
    }
    this._ngxService.start();
    this._authService.register(this.form.value).subscribe(
      (response) => {
        if (response.isSuccess) {
          this._toastr.success("Successfully registered","Success");
          this._ngxService.stop();
          this.router.navigate(['/authentication/login']);
        }
      },
      (ex) => {
        this._ngxService.stop();
      }
    );
  }
}

export function PasswordMatchValidator(controlName: string, matchingControlName: string): ValidatorFn {
  return (formGroup: AbstractControl): ValidationErrors | null => {
    const control = formGroup.get(controlName);
    const matchingControl = formGroup.get(matchingControlName);

    if (!control || !matchingControl) {
      return null;
    }

    if (matchingControl.errors && !matchingControl.errors['passwordMismatch']) {
      // Return if another validator has already found an error on the matchingControl
      return null;
    }

    if (control.value !== matchingControl.value) {
      matchingControl.setErrors({ passwordMismatch: true });
      return { passwordMismatch: true };
    } else {
      matchingControl.setErrors(null);
      return null;
    }
  };
}
