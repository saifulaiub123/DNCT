import { Routes } from '@angular/router';
import { NewObjectSetupComponent } from './new-object-setup/new-object-setup.component';
import { UserQueryTableComponent } from './user-query-table/user-query-table.component';

export const ObjectBaseRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'new-object-setup',
        component: NewObjectSetupComponent,
      },
      {
        path: 'query-table',
        component: UserQueryTableComponent,
      },
    ],
  },
];
