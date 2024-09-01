import { Routes } from '@angular/router';
import { NewObjectSetupComponent } from './new-object-setup/new-object-setup.component';

export const ObjectBaseRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'new-object-setup',
        component: NewObjectSetupComponent,
      },
    ],
  },
];
