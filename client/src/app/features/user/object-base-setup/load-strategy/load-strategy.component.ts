import { Component, inject } from '@angular/core';
import { MaterialModule } from 'src/app/material.module';
import { IInstanceName, ILoadStrategy, IParameter } from './load-strategy.model';
import { MatTableDataSource } from '@angular/material/table';
import { MockAPIClass } from '../user-query-table/user-query-table.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatCheckboxChange } from '@angular/material/checkbox';

@Component({
  selector: 'app-load-strategy',
  standalone: true,
  imports: [MaterialModule, CommonModule, ReactiveFormsModule],
  templateUrl: './load-strategy.component.html',
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
    'overlap','action'
  ];

  //injectable
  private snackBar = inject(MatSnackBar);
  private fb = inject(FormBuilder);

  // table datasources
  loadStrategyTabledataSource = new MatTableDataSource<ILoadStrategy>(Load_Strategy)
  instanceNameTabledataSource = new MatTableDataSource<any>()
  parameterTabledataSource = new MatTableDataSource<any>()


  // forms
  instanceNameForm: FormGroup;
  parameterForm: FormGroup;
  selectedLoadStragtegyRow: ILoadStrategy;
  table_config_id = 1000;
  ngOnInit(): void {
    this.initParameterForm();
    this.initInstanceNameForm();
  }
  initParameterForm(): void {
    this.parameterTabledataSource = new MatTableDataSource<IParameter>([...Parameter]);
    this.parameterForm = this.fb.group({
      parameters: this.fb.array(Parameter.map(row => this.createParameterForm(row)))
    })
    this.parameterTabledataSource = new MatTableDataSource((this.parameterForm.get('parameters') as FormArray).controls);
  }
  createParameterForm(parameter: IParameter): FormGroup {
    return this.fb.group({
      value: new FormControl(parameter.value),
      parameter: new FormControl(parameter.parameter),
      table_config_id: new FormControl(this.table_config_id++),
      rtm_parmtrs_mstr_id: new FormControl(this.table_config_id++),
      // for editable form
      action: new FormControl('existingRecord'),
      isEditable: new FormControl(false),
      isNewRow: new FormControl(false),
    });
  }

  initInstanceNameForm(): void {
    this.instanceNameTabledataSource = new MatTableDataSource<IInstanceName>([...InstanceName]);
    this.instanceNameForm = this.fb.group({
      instanceName: this.fb.array(InstanceName.map(row => this.createInstanceNameForm(row)))
    })
    this.instanceNameTabledataSource = new MatTableDataSource((this.instanceNameForm.get('instanceName') as FormArray).controls);
  }
  createInstanceNameForm(_row: IInstanceName): FormGroup {
    return this.fb.group({
      isSelected: new FormControl(_row.isSelected),
      instanceName: new FormControl(_row.instanceName),
      order: new FormControl(_row.order),
      overlap: new FormControl(_row.overlap),
      tbl_confgrtn_id: new FormControl(_row.tbl_confgrtn_id),
      action: new FormControl('existingRecord'),
      isEditable: new FormControl(false),
      isNewRow: new FormControl(false),
    })
  }
  onShowCurrentScript(): void { }
  onSaveRuntime(): void {
    if (!this.selectedLoadStragtegyRow) {
      this.snackBar.open('Row from load strategy table should be selected!', 'Close', {
        duration: 3000
      });
      return
    }
    const selectedRow = {
      table_config_id: this.selectedLoadStragtegyRow.table_config_id,
      load_strategy_id: this.selectedLoadStragtegyRow.load_stratgy_id,
    };
    if(!this.saveParameterGridData()) return
    
    this.autoPopulateColumnsAPI(selectedRow.table_config_id, selectedRow.load_strategy_id).subscribe({
      next: (response) => {
        if (response.success) {
          this.snackBar.open('Selected row and parameter grid data save successfully!', 'Close', {
            duration: 3000
          });
        }
      },
      error: () => {
        this.snackBar.open('Failed to save. Please try again.', 'Close', {
          duration: 3000
        });
      }
    })

  }
  saveParameterGridData(): IParameter[] | undefined {
    const parametersArray = this.parameterForm.get('parameters') as FormArray;
    const hasEmptyValues = parametersArray.controls.some((row) => { const value = row.get('value')?.value;
      return value === null || value === undefined || value === '';});
    if (hasEmptyValues) {
      this.snackBar.open('Please fill in all values in the Value column.', 'Close', {
        duration: 3000,
      });
      return;
    }
    const parameterGridData: IParameter[] = parametersArray.controls.map(control => {
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
    this.selectedInstanceNameRows=[];
    this.loadStrategyTabledataSource = new MatTableDataSource<ILoadStrategy>(Load_Strategy)
    this.initInstanceNameForm();
    this.initParameterForm();
   }
  onAnalyzeSpecsAndGenerateCode(): void { }
  onAnalyzeSpecs(): void { }
  onGenerateCode(): void { }
  onLoadStragtegyRowSelect(_row: ILoadStrategy): void {
    this.selectedLoadStragtegyRow = _row
    this.loadStrategyTabledataSource.data.forEach((item) => {
      if (item !== _row) {
        item.isSelected = false;
      }
    });
  }
  selectedInstanceNameRows: IInstanceName[] =[];
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
    this.instanceNameTabledataSource._updateChangeSubscription();
  };

  SaveVO(form: any, i: number) {
    form.get('parameters').at(i).get('isEditable').patchValue(false);
  }
  CancelSVO(form: any, i: number) {
    form.get('parameters').at(i).get('isEditable').patchValue(false);
  }
  EditSVO(form: any, i: number) {
    form.get('parameters').at(i).get('isEditable').patchValue(true);
  }
  CancelInstanceNameEditing(form: any, i: number): void {
    form.get('instanceName').at(i).get('isEditable').patchValue(false);
  }
  SaveInstanceNameChanges(form: any, i: number): void {
    form.get('instanceName').at(i).get('isEditable').patchValue(false);
  }
  EditInstanceNameRow(form: any, i: number): void {
    form.get('instanceName').at(i).get('isEditable').patchValue(true);
  }
}
export const Load_Strategy = [
  {
    isSelected: false,
    loadStrategy: 'processing',
    table_config_id: 1001,
    load_stratgy_id: 1001,
  },
  {
    isSelected: false,
    loadStrategy: 'Loading',
    table_config_id: 1002,
    load_stratgy_id: 1002,
  },
  {
    isSelected: false,
    loadStrategy: 'configurring',
    table_config_id: 1003,
    load_stratgy_id: 1003,
  },
]
export const Parameter: IParameter[] = [
  {
    value: 0,
    parameter: 'processing',
    action: 'existingRecord',
    table_config_id: 1003,
    rtm_parmtrs_mstr_id: 1003,
    isEditable: false,
    isNewRow: false
  },
  {
    value: 0,
    parameter: 'Loading',
    table_config_id: 1004,
    rtm_parmtrs_mstr_id: 1004,
    action: 'existingRecord',
    isEditable: false,
    isNewRow: false
  },
  {
    value: 0,
    parameter: 'configurring',
    action: 'existingRecord',
    table_config_id: 1005,
    rtm_parmtrs_mstr_id: 1005,
    isEditable: false,
    isNewRow: false
  },
]
export const InstanceName: IInstanceName[] = [
  {
    isSelected: false,
    instanceName: 'string',
    order: 'order',
    overlap: 'overlap',
    tbl_confgrtn_id: 1000,
    action: 'existingRecord',
    isEditable: false,
    isNewRow: false
  },
  {
    isSelected: false,
    instanceName: 'string',
    order: 'order',
    overlap: 'overlap',
    tbl_confgrtn_id: 1001,
    action: 'existingRecord',
    isEditable: false,
    isNewRow: false
  },
  {
    isSelected: false,
    instanceName: 'string',
    order: 'order',
    overlap: 'overlap',
    tbl_confgrtn_id: 1002,
    action: 'existingRecord',
    isEditable: false,
    isNewRow: false
  },
]
