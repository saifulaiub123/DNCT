import { Routes } from '@angular/router';
import { NewObjectSetupComponent } from './new-object-setup/new-object-setup.component';
import { UserQueryTableComponent } from '../table-instance-setup/user-query-table/user-query-table.component';
import { ColumnDetailComponent } from './column-detail/column-detail.component';
import { LoadStrategyComponent } from './load-strategy/load-strategy.component';

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
      {
        path: 'column-details',
        component: ColumnDetailComponent,
      },
      {
        path: 'load-strategy',
        component: LoadStrategyComponent,
      },
    ],
  },
];
