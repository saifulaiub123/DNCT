import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CreateTableComponent } from './table/create-table/create-table.component';

export const UserRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'dashboard',
        component: DashboardComponent,
      },
      {
        path: 'create-table',
        component: CreateTableComponent,
      },
      {
        path: 'object-setup',
        loadChildren: () =>
          import('./object-base-setup/object-base.routes').then(
            (m) => m.ObjectBaseRoutes
          ),
      },
    ],
  },
];
