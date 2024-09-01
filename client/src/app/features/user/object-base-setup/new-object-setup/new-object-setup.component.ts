import { CommonModule, DatePipe } from '@angular/common';
import { Component, ComponentFactoryResolver, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { TablerIconsModule } from 'angular-tabler-icons';
import { MaterialModule } from 'src/app/material.module';
import { ObjectSetupFormComponent } from '../Component/object-setup-form/object-setup-form.component';
import { DynamicTabDirective } from 'src/app/core/shared/directives/dynamic-tab.directive';


export interface productsData {
  id: number;
  columnName: string;
  type2: boolean;
  pkColumn: boolean;
}
const PRODUCT_DATA: productsData[] = [
  {
    id: 1,
    columnName: 'Name',
    type2: true,
    pkColumn: false,
  },
  {
    id: 1,
    columnName: 'Age',
    type2: false,
    pkColumn: true,
  },
  {
    id: 1,
    columnName: 'Department',
    type2: true,
    pkColumn: false,
  },
  {
    id: 1,
    columnName: 'Salary',
    type2: true,
    pkColumn: false,
  },
];


@Component({
  selector: 'app-new-object-setup',
  standalone: true,
  imports: [ObjectSetupFormComponent, MaterialModule, TablerIconsModule, MatFormFieldModule, MatInputModule, MatRadioModule, MatCheckboxModule, MatDatepickerModule, MatTableModule, CommonModule,],
  templateUrl: './new-object-setup.component.html',
  styleUrl: './new-object-setup.component.scss'
})
export class NewObjectSetupComponent implements OnInit {

  @ViewChild(DynamicTabDirective, { static: true }) dynamicTab!: DynamicTabDirective;

  selectedIndex = 1;

  tabs = [
    { label: '01', component: ObjectSetupFormComponent},
    { label: '02', component: ObjectSetupFormComponent},
    { label: '03', component: ObjectSetupFormComponent},
    { label: '04', component: ObjectSetupFormComponent},
  ];

  constructor(private componentFactoryResolver: ComponentFactoryResolver) {}

  ngOnInit() {

  }

  removeTab(index: number) {
    this.tabs.splice(index, 1);
    // Optionally reset the selected tab index if needed
    if (this.selectedIndex >= this.tabs.length) {
      this.selectedIndex = this.tabs.length - 1;
    }
  }
}


