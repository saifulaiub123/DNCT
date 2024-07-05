import { Routes } from '@angular/router';
import { AppErrorComponent } from '../authentication/error/error.component';
import { AppBoxedLoginComponent } from '../authentication/login/login.component';
import { AppMaintenanceComponent } from '../authentication/maintenance/maintenance.component';
import { RegisterComponent } from '../authentication/register/register.component';


export const UserRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'dashboard',
        component: AppBoxedLoginComponent,
      },
      {
        path: 'register',
        component: RegisterComponent,
      },
      {
        path: 'error',
        component: AppErrorComponent,
      },
      {
        path: 'maintenance',
        component: AppMaintenanceComponent,
      },
    ],
  },
];
