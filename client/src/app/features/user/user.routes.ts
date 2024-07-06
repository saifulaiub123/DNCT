import { Routes } from '@angular/router';
import { AppErrorComponent } from '../authentication/error/error.component';
import { AppMaintenanceComponent } from '../authentication/maintenance/maintenance.component';
import { RegisterComponent } from '../authentication/register/register.component';
import { AppDashboard1Component } from 'src/app/pages/dashboards/dashboard1/dashboard1.component';


export const UserRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'dashboard',
        component: AppDashboard1Component,
      },
    ],
  },
];
