import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AppErrorComponent } from '../error/error.component';
import { AppMaintenanceComponent } from '../maintenance/maintenance.component';
import { RegisterComponent } from './register/register.component';

export const AuthenticationRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'login',
        component: LoginComponent,
      },
      {
        path: 'register',
        component: RegisterComponent,
      },
    ],
  },
];
