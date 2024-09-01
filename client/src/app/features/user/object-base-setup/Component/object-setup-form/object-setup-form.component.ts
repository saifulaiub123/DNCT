import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatTableModule } from '@angular/material/table';
import { TablerIconsModule } from 'angular-tabler-icons';
import { MaterialModule } from 'src/app/material.module';


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
  selector: 'app-object-setup-form',
  standalone: true,
  imports: [MaterialModule, TablerIconsModule, MatFormFieldModule, MatInputModule, MatRadioModule, MatCheckboxModule, MatDatepickerModule, MatTableModule, CommonModule,],
  templateUrl: './object-setup-form.component.html',
  styleUrl: './object-setup-form.component.scss'
})
export class ObjectSetupFormComponent {

  displayedColumns1: string[] = ['columnName', 'type2', 'pkColumn'];
  dataSource1 = PRODUCT_DATA;

  removeTab(event: PointerEvent, index: number) {
    console.log(event);
    event.stopPropagation();
    // this.tabs.splice(index, 1);
  }
}
