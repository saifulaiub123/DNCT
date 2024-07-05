import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
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

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [RouterModule, MaterialModule, ReactiveFormsModule, SharedModule],
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
    this._toastr.error("Error","Error");
  }

  form = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.minLength(3)]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });

  get f() {
    return this.form.controls;
  }

  submit() {
    this._ngxService.start();
    // console.log(this.form.value);
    this._authService.register(this.form.value).subscribe(
      (response) => {
        if (response.isSuccess) {
          //this.goVerify(response.Data.VerificationToken);
        }
      },
      (ex) => {
        this._ngxService.stop();
        // this._notificationService.showError("Error","Error");
        // this.showErrors = true;
        // console.log(ex);
        // if (ex.includes('already exist!')) {
        //   this.userAlreadyExists = true;
        // } else {
        //   this.hasbackendErrors = true;
        //   this.backendErrors = ex;
        // }
      }
    );
    //this.router.navigate(['/dashboards/dashboard1']);
  }
}
