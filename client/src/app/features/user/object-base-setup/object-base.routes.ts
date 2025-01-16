import { Routes } from '@angular/router';
import { UserQueryTableComponent } from '../table-instance-setup/user-query-table/user-query-table.component';
import { ColumnDetailComponent } from '../table-instance-setup/column-detail/column-detail.component';
import { LoadStrategyComponent } from '../table-instance-setup/load-strategy/load-strategy.component';
import { TableConfigurationComponent } from './table-configuration/table-configuration.component';

export const ObjectBaseRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'table-configurations',
        component: TableConfigurationComponent,
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
