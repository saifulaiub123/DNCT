import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatTableModule } from '@angular/material/table';
import { Router } from '@angular/router';
import { TablerIconsModule } from 'angular-tabler-icons';
import { ToastrService } from 'ngx-toastr';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { DbNameObjectSetupResponse } from 'src/app/core/model/contract/db-source-object-setup-response';
import { ServerResponse } from 'src/app/core/model/contract/server-response';
import { ServerDto } from 'src/app/core/model/dto/server-dto';
import { CommonService } from 'src/app/core/services/api-services/common.service';
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
  imports: [MaterialModule, TablerIconsModule, MatFormFieldModule, MatInputModule, MatRadioModule, MatCheckboxModule, MatDatepickerModule, MatTableModule, CommonModule, ReactiveFormsModule],
  templateUrl: './object-setup-form.component.html',
  styleUrl: './object-setup-form.component.scss'
})
export class ObjectSetupFormComponent implements OnInit {
  @Input() databaseSourceId: number = 0;

  displayedColumns1: string[] = ['columnName', 'type2', 'pkColumn'];
  dataSource1 = PRODUCT_DATA;
  submitted: boolean = false;
  connections : ServerDto[] = [];
  monthsOfHistoryList : number[] = [6,12,18,24,30,36,42,48,54,60,66,72,78,84]

  form = new FormGroup({
    databaseSourceId: new FormControl<number>(0, [Validators.required]),
    connectionName: new FormControl('', [Validators.required]),
    databaseName: new FormControl('', [Validators.required]),
    objectName: new FormControl('', [Validators.required]),
    objectKind: new FormControl('', []),
    sqlToUse: new FormControl('', []),
    deltaRowsIdentificationLogic: new FormControl('', []),
    queryBand: new FormControl('', []),
    truncateBeforeLoad: new FormControl('', []),
    etlFramework: new FormControl('', []),
    dedupLogic: new FormControl('', []),
    estimatedTableSize: new FormControl<number>(0, []),
    alias: new FormControl('', []),
    additionalWhereCondition: new FormControl('', []),
    objectNature: new FormControl('', []),
    partitioningClause: new FormControl('', []),
    monthsOfHistory: new FormControl<number>(0, []),
    targetDatabaseConnection: new FormControl('', []),
  });

  get f() {
    return this.form.controls;
  }

  constructor(
    private _commonService: CommonService,
    private _router: Router,
    private _ngxService: NgxUiLoaderService,
    private _toastr: ToastrService,
  ){}

  ngOnInit(): void {
    this.initialize();
  }
  initialize() {

    this._commonService.getDatabaseSourceById(this.databaseSourceId).subscribe((res: ServerResponse<DbNameObjectSetupResponse>)=> {
      this.form.patchValue({
          databaseSourceId : this.databaseSourceId,
          databaseName: res.data.length > 0 ? res.data[0].databaseName : ''
        })

    })
    this._commonService.getServers().subscribe(res=> {
      res.data.forEach((item: any) => {
        this.connections.push(new ServerDto(item.id, item.name))
      });
    })

  }

  submit()
  {
    if(!this.form.valid)
      {
        return;
      }
      this._ngxService.start();
  }

}
