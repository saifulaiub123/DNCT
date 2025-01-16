import { Component, inject } from '@angular/core';
import { MaterialModule } from 'src/app/material.module';
import { RunTimeInstance, LoadStrategy, RunTimeParameter } from './load-strategy.model';
import { MatTableDataSource } from '@angular/material/table';
import { MockAPIClass } from '../user-query-table/user-query-table.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { TablerIconsModule } from 'angular-tabler-icons';
import { ToastrService } from 'ngx-toastr';
import { LoadStrategyService } from './load-strategy.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { catchError, EMPTY, first } from 'rxjs';
const loadStrategycontroller = 'load-strategy';
const runtimeParameterController = 'run-time-master'
const runtimeInstanceController = 'table-instance-run-time'
@Component({
  selector: 'app-load-strategy',
  standalone: true,
  imports: [MaterialModule, CommonModule, ReactiveFormsModule, TablerIconsModule],
  providers: [LoadStrategyService],
  templateUrl: './load-strategy.component.html',
  styleUrl: './load-strategy.component.scss',
})
export class LoadStrategyComponent extends MockAPIClass {
  loadStrategyDisplayedColumns: string[] = [
    'select', 'loadStrategy'
  ];
  parameterDisplayedColumns: string[] = [
    'parameter', 'value', 'action'
  ];
  overlapDisplayedColumns: string[] = [
    'select',
    'instance',
    'order',
    'overlap', 'action'
  ];

  //injectable
  private snackBar = inject(MatSnackBar);
  private fb = inject(FormBuilder);
  private toastrService = inject(ToastrService);
  private loadStrategyService = inject(LoadStrategyService);
  private ngxLoaderService = inject(NgxUiLoaderService);

  // table datasources
  loadStrategyDatasource = new MatTableDataSource<any>();
  instanceNameDatasource = new MatTableDataSource<any>();
  parameterDatasource = new MatTableDataSource<any>();

  private RunTimeParameterInitialFormState:RunTimeParameter[];
  private runtimeInstanceInitialFormState:RunTimeInstance[];
  // forms
  instanceNameForm: FormGroup;
  parameterForm: FormGroup;
  selectedLoadStragtegyRow: LoadStrategy;
  table_config_id = 1000;
  selectedInstanceNameRows: RunTimeInstance[] = [];

  // flags
  isParameterFormAlreadyEditing: boolean = false;
  isInstanceFormAlreadyEditing: boolean = false;
  ngOnInit(): void {
    this.loadStrategyFormInit();
    this.runtimeParameterFormInit(Parameter);
    this.InstanceNameFormInit(InstanceName);
  }
  loadStrategyFormInit(): void {
    this.ngxLoaderService.start();
    this.loadStrategyService.fetchAll(loadStrategycontroller).pipe(first(), catchError((err) => {
      this.ngxLoaderService.stop();
      this.toastrService.error(`error Occurred ${err}`, 'Error');
      return EMPTY;
    })).subscribe(res => {
      this.ngxLoaderService.stop();
      if (res.isSuccess) {this.loadStrategyDatasource.data = res.data.map((res:LoadStrategy) => {
        return {
          ...res, tblConfigId: 1, // dummy config id replace with actual
        }
      });
    }
    })
  }

  runtimeParameterFormInit(_parameters: RunTimeParameter[]): void {
    this.parameterForm = this.fb.group({
      parameters: this.fb.array(_parameters.map(row => this.createParameterForm(row)))
    })
    this.RunTimeParameterInitialFormState = (this.parameterForm.get('parameters') as FormArray)?.value; // to save initial form state
    this.parameterDatasource = new MatTableDataSource((this.parameterForm.get('parameters') as FormArray)?.controls);
  }
  loadRunTimeParameter():void{
    // this.ngxLoaderService.start();
    const tableConfigId = this.selectedLoadStragtegyRow?.tblConfigId;
    const parameter = tableConfigId ? `?TableConfigId=${tableConfigId}` : '';
    // this.loadStrategyService.fetchAll(runtimeParameterController, parameter).pipe(first(), catchError(err => {
    //   this.ngxLoaderService.stop();
    //   this.toastrService.error(`error Occurred ${err.message}`, err.statusText);
    //   return EMPTY;
    // })).subscribe(res => {
    //   this.ngxLoaderService.stop();
    //   if (res.isSuccess) {this.parameterDatasource = res.data; this.runtimeParameterFormInit(res.data);}
    // })
  }
  createParameterForm(parameter: RunTimeParameter): FormGroup {
    return this.fb.group({
      value: new FormControl(parameter.value),
      parameter: new FormControl(parameter.parameter),
      table_config_id: new FormControl(this.table_config_id++),
      rtm_parmtrs_mstr_id: new FormControl(this.table_config_id++),
      // for editable form
      action: new FormControl('existingRecord'),
      isEditable: new FormControl(false),
      isEditing: new FormControl(false),
    });
  }

  InstanceNameFormInit(_instance: RunTimeInstance[]): void {
    this.instanceNameForm = this.fb.group({
      instanceName: this.fb.array(_instance.map(row => this.createInstanceNameForm(row)))
    })
    this.runtimeInstanceInitialFormState = (this.instanceNameForm.get('instanceName') as FormArray)?.value;
    this.instanceNameDatasource = new MatTableDataSource((this.instanceNameForm.get('instanceName') as FormArray).controls);
  }
  loadRunTimeInstance():void{
    this.ngxLoaderService.start();
    const tableConfigId = this.selectedLoadStragtegyRow?.tblConfigId;
    const parameter = tableConfigId ? `?TableConfigId=${tableConfigId}` : ''; //
    this.loadStrategyService.fetchAll(runtimeInstanceController, parameter).pipe(first(), catchError(err => {
      this.ngxLoaderService.stop();
      this.toastrService.error(`error Occurred ${err.message}`, err.statusText);
      return EMPTY;
    })).subscribe(res => {
      this.ngxLoaderService.stop();
      if (res.isSuccess) {this.instanceNameDatasource = res.data; this.InstanceNameFormInit(res.data);}
    })
  }
  createInstanceNameForm(_row: RunTimeInstance): FormGroup {
    return this.fb.group({
      isSelected: new FormControl(_row.isSelected),
      instanceName: new FormControl(_row.instanceName),
      order: new FormControl(_row.order),
      overlap: new FormControl(_row.overlap),
      tbl_confgrtn_id: new FormControl(_row.tbl_confgrtn_id),
      action: new FormControl('existingRecord'),
      isEditable: new FormControl(false),
      isEditing: new FormControl(false),
    })
  }
  onShowCurrentScript(): void { }
  // what I understood there has a one button if it pressed all the three table values values should be save
  onSaveRuntime(): void {
    if (!this.selectedLoadStragtegyRow) {
      this.snackBar.open('Row from load strategy table should be selected!', 'Close', {
        duration: 3000,
        verticalPosition: 'top'
      });
      return
    }
    const loadStrategyPayload = {
      tblConfigId: this.selectedLoadStragtegyRow.tblConfigId,
      loadStrategyId: this.selectedLoadStragtegyRow.loadStrategyId,
    };

    //
    if (!this.saveParameterGridData()) return

    // saving load strategy selected row
    this.loadStrategyService.create(loadStrategycontroller, loadStrategyPayload).pipe(first(), catchError(err => {
      this.ngxLoaderService.stop();
      this.toastrService.error(`error Occurred ${err}`, 'Error');
      return EMPTY;
    })).subscribe(res => {
      this.ngxLoaderService.stop();
      if (res.isSuccess) { this.toastrService.success('Selected Row from load Strategy save successfully', res.message)};
    })
  }
  saveParameterGridData(): RunTimeParameter[] | undefined {
    const parametersArray = this.parameterForm.get('parameters') as FormArray;
    const hasEmptyValues = parametersArray.controls.some((row) => {
      const value = row.get('value')?.value;
      return value === null || value === undefined || value === '';
    });
    if (hasEmptyValues) {
      this.snackBar.open('Please fill in all values in the Value column.', 'Close', {
        duration: 3000,
      });
      return;
    }
    const parameterGridData: RunTimeParameter[] = parametersArray.controls.map(control => {
      return {
        value: control.value['value'],
        parameter: control.value['parameter'],
        table_config_id: control.value['table_config_id'],
        rtm_parmtrs_mstr_id: control.value['rtm_parmtrs_mstr_id'],
      }
    })
    return parameterGridData
  }
  onRefreshRuntime(): void {
    this.selectedInstanceNameRows = [];
    this.loadStrategyFormInit();
    this.InstanceNameFormInit(this.runtimeInstanceInitialFormState);
    this.runtimeParameterFormInit(this.RunTimeParameterInitialFormState);
  }
  onAnalyzeSpecsAndGenerateCode(): void { }
  onAnalyzeSpecs(): void { }
  onGenerateCode(): void { }
  onLoadStragtegyRowSelect(_event: MatCheckboxChange, _row: LoadStrategy): void {
    if(_event.checked){
      this.selectedLoadStragtegyRow = _row;
      this.loadStrategyDatasource.data.forEach((item) => {
        if (item !== _row) {
          item.isSelected = false;
        }
      });
      this.loadRunTimeParameter();
      this.loadRunTimeParameter();
    }
  }

  onInstanceNameRowSelect(_row: FormGroup, _event: MatCheckboxChange, _index: number): void {
    if (_row.get('order')?.value && _row.get('overlap')?.value) {
      _row.patchValue({ isSelected: _event.checked });
      if (_event.checked) {
        this.selectedInstanceNameRows.push(_row.value);
        this.snackBar.open('Row selected', 'Close', {
          duration: 1000
        });
      } else {
        this.selectedInstanceNameRows.splice(_index, 1);
        this.snackBar.open('Row unSelected', 'Close', {
          duration: 1000
        });
      }
    } else {
      _row.patchValue({ isSelected: false });
      this.snackBar.open('Columns order and overlap should have value', 'Close', {
        duration: 3000
      });
    }
    this.instanceNameDatasource._updateChangeSubscription();
  };

  saveParamter(form: any, i: number) {
    this.RunTimeParameterInitialFormState[i] = form.get('parameters').at(i).value;
    form.get('parameters').at(i).get('isEditable').patchValue(false);
    this.isParameterFormAlreadyEditing = false;
  }
  cancelParameter(form: any, i: number) {
    form.get('parameters').at(i).patchValue(this.RunTimeParameterInitialFormState[i]);
    form.get('parameters').at(i).get('isEditable').patchValue(false);
    this.isParameterFormAlreadyEditing = false;
  }
  editParameter(form: any, i: number) {
    if (this.isParameterFormAlreadyEditing) {
      this.snackBar.open('Already Editing. Complete it first.', 'Close', {
        duration: 2000,
        verticalPosition: 'top'
      });
      return;
    }
    form.get('parameters').at(i).get('isEditable').patchValue(true);
    this.isParameterFormAlreadyEditing = form.get('parameters').at(i).get('isEditable').value;
  }
  CancelInstanceNameEditing(form: any, i: number): void {
    form.get('instanceName').at(i).patchValue(this.runtimeInstanceInitialFormState[i]);
    form.get('instanceName').at(i).get('isEditable').patchValue(false);
    this.isInstanceFormAlreadyEditing = false;
  }
  SaveInstanceNameChanges(form: any, i: number): void {
    this.runtimeInstanceInitialFormState[i] = form.get('instanceName').at(i).value;
    form.get('instanceName').at(i).get('isEditable').patchValue(false);
    this.isInstanceFormAlreadyEditing = false;
  }
  EditInstanceNameRow(form: any, i: number): void {
    if (this.isInstanceFormAlreadyEditing) {
      this.snackBar.open('Already Editing. Complete it first.', 'Close', {
        duration: 2000,
        verticalPosition: 'top'
      });
      return;
    }
    form.get('instanceName').at(i).get('isEditable').patchValue(true);
    this.isInstanceFormAlreadyEditing = form.get('instanceName').at(i).get('isEditable').value;
  }
}
export const Parameter: RunTimeParameter[] = [
  {
    value: 0,
    parameter: 'processing',
    action: 'existingRecord',
    table_config_id: 1003,
    rtm_parmtrs_mstr_id: 1003,
    isEditable: false,
    isEditing: false
  },
  {
    value: 0,
    parameter: 'processing',
    action: 'existingRecord',
    table_config_id: 1003,
    rtm_parmtrs_mstr_id: 1003,
    isEditable: false,
    isEditing: false
  }
]
export const InstanceName: RunTimeInstance[] = [
  {
    isSelected: false,
    instanceName: 'string',
    order: 'order',
    overlap: 'overlap',
    tbl_confgrtn_id: 1000,
    action: 'existingRecord',
    isEditable: false,
    isEditing: false
  },
  {
    isSelected: false,
    instanceName: 'string',
    order: 'order',
    overlap: 'overlap',
    tbl_confgrtn_id: 1000,
    action: 'existingRecord',
    isEditable: false,
    isEditing: false
  },
]
