import { Component, inject } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MaterialModule } from 'src/app/material.module';
import { TableConfiguration } from './column-detail.model';
import { MockAPIClass } from '../user-query-table/user-query-table.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, EMPTY, first } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { AppDialogOverviewComponent } from 'src/app/pages/ui-components/dialog/dialog.component';
import { FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TableConfigurationService } from './column-detail.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { ServerResponse } from 'src/app/core/model/contract/server-response';
import { TablerIconsModule } from 'angular-tabler-icons';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-column-detail',
  standalone: true,
  imports: [MaterialModule, ReactiveFormsModule, CommonModule, TablerIconsModule],
  providers: [TableConfigurationService],
  templateUrl: './column-detail.component.html',
  styleUrl: './column-details.component.scss',
})
export class ColumnDetailComponent extends MockAPIClass {
  displayedColumns: string[] = [
    'columnId',
    'columnName',
    'dataType',
    'colmnTrnsfrmtnStep1',
    'idGenrtnStratgyId',
    'type2StartInd',
    'type2EndInd',
    'curActiveInd',
    'pattern1',
    'pattern2',
    'pattern3',
    'loadInd',
    'joinDupInd', 'action'
  ];
  private initialFormState: TableConfiguration[];
  dataSource = new MatTableDataSource<any>();
  selectedRow: FormGroup | null = null;
  selectedRows: FormGroup | null = null;
  tableConfigFormGroup: FormGroup;
  isValidatedSyntax: boolean = false;
  validatedData: any[] = [];
  //injectors
  private snackBar = inject(MatSnackBar);
  private _toaster = inject(ToastrService);
  private dialog = inject(MatDialog);
  private fb = inject(FormBuilder);
  private _ngxService = inject(NgxUiLoaderService);
  private tableConfigService = inject(TableConfigurationService);

  ngOnInit(): void {
    this.tableConfigFormGroup = this.fb.group({
      tableConfig: this.fb.array([])
    });
    this.fetchAllTableConfigs();
  }
  formInit(_formData: TableConfiguration[]): void {
    this.tableConfigFormGroup = this.fb.group({
      tableConfig: this.fb.array(_formData.map(row => this.createRowForm(row)))
    })
    this.initialFormState = (this.tableConfigFormGroup.get('tableConfig') as FormArray).value;
    this.dataSource = new MatTableDataSource((this.tableConfigFormGroup.get('tableConfig') as FormArray).controls);
  }
  fetchAllTableConfigs(): void {
    this._ngxService.start();
    this.tableConfigService.fetchAll().pipe(first(), catchError((err) => {
      this._toaster.error('Error Occured' + err, 'Error')
      return EMPTY;
    })).subscribe((res: ServerResponse<TableConfiguration>) => {
      if (res.isSuccess) {
        this.formInit(res.data);
        this._ngxService.stop();
      }
    })
  }
  getFormArrayControl(_controlName: string, _index: number): FormControl | undefined {
    return ((this.tableConfigFormGroup.get('tableConfig') as FormArray).controls[_index] as FormGroup).controls[_controlName] as FormControl
  }

  createRowForm(row: TableConfiguration): FormGroup {
    return this.fb.group({
      tblColConfgrtnId: new FormControl(row.tblColConfgrtnId),
      tblConfgrtnId: new FormControl(row.tblConfgrtnId),
      colmnName: new FormControl(row.colmnName, [Validators.maxLength(100)]),
      dataType: new FormControl(row.dataType),
      colmnTrnsfrmtnStep1: new FormControl(row.colmnTrnsfrmtnStep1),
      genrtIdInd: new FormControl(row.genrtIdInd, Validators.maxLength(1)),
      idGenrtnStratgyId: new FormControl(row.idGenrtnStratgyId),
      type2StartInd: new FormControl(row.type2StartInd),
      type2EndInd: new FormControl(row.type2EndInd),
      currRowInd: new FormControl(row.currRowInd),
      pattern1: new FormControl(row.pattern1, [Validators.maxLength(1000)]),
      pattern2: new FormControl(row.pattern2, [Validators.maxLength(1000)]),
      pattern3: new FormControl(row.pattern3, Validators.maxLength(1)),
      ladInd: new FormControl(row.ladInd),
      joinDupsInd: new FormControl(row.joinDupsInd),
      confgrtnEffStartTs: new FormControl(row.confgrtnEffStartTs),
      confgrtnEffEndTs: new FormControl(row.confgrtnEffEndTs),
      // Additional fields for editable form purposes
      status: new FormControl('unchanged'),
      action: new FormControl('existingRecord'),
      isEditable: new FormControl(false),
    });
  }
  addRow(): void {
    // create new row with default values
    const newRow = this.fb.group({
      tblColConfgrtnId: new FormControl(-1),
      tblConfgrtnId: new FormControl(this.dataSource.data.length + 1),
      colmnName: new FormControl(null, [Validators.maxLength(100)]),
      dataType: new FormControl(null),
      colmnTrnsfrmtnStep1: new FormControl(null),
      genrtIdInd: new FormControl(null, Validators.maxLength(1)),
      idGenrtnStratgyId: new FormControl(null),
      type2StartInd: new FormControl(null),
      type2EndInd: new FormControl(null),
      currRowInd: new FormControl(null),
      pattern1: new FormControl(null, Validators.maxLength(1000)),
      pattern2: new FormControl(null, Validators.maxLength(1000)),
      pattern3: new FormControl(null, Validators.maxLength(1)),
      ladInd: new FormControl(null),
      joinDupsInd: new FormControl(null),
      confgrtnEffStartTs: new FormControl(new Date()),
      confgrtnEffEndTs: new FormControl(new Date()),
      // Additional fields for editable form purposes
      status: new FormControl('changed'),
      action: new FormControl('existingRecord'),
      isEditable: new FormControl(false),
    });

    this.initialFormState.push(newRow.value as TableConfiguration);
    const currentControl = this.tableConfigFormGroup.get('tableConfig') as FormArray;
    currentControl.push(newRow);
    this.dataSource = new MatTableDataSource(currentControl.controls);
  }
  updateTableConfig(_form: any, _index: number) {
    if (this.tableConfigFormGroup.valid) {
      this.isAlreadyEditing = false;
      this.initialFormState[_index] = _form.get('tableConfig').at(_index).value;  // update initial state  to check where table state is changed
      _form.get('tableConfig').at(_index).get('isEditable').patchValue(false);
      _form.get('tableConfig').at(_index).get('status').patchValue('changed');
    }
    else this._toaster.error('Fill all mandatory fields', 'Error')
  }
  Cancel(_form: any, _index: number) {
    this.isAlreadyEditing = false;
    _form.get('tableConfig').at(_index).patchValue(this.initialFormState[_index]);
    _form.get('tableConfig').at(_index).get('isEditable').patchValue(false);
  }
  isAlreadyEditing: boolean = false;
  EditTableConfig(form: any, i: number) {
    if (this.isAlreadyEditing) {
      this.snackBar.open('Already Editing. Complete it first.', 'Close', {
        duration: 2000,
        verticalPosition: 'top'
      });
      return
    }
    form.get('tableConfig').at(i).get('isEditable').patchValue(true);
    this.isAlreadyEditing = form.get('tableConfig').at(i).get('isEditable').value;
  }
  refreshRow(): void {
    this.fetchAllTableConfigs();
  }
  selectRow(_row: FormGroup): void {
    this.selectedRow = _row;
  }
  saveRows(): void {
    const rows = (this.tableConfigFormGroup.get('tableConfig') as FormArray).value;
    const mappedRows: TableConfiguration[] = rows.map((row: TableConfiguration) => {
      return {
        tblColConfgrtnId: row.tblColConfgrtnId,
        tblConfgrtnId: row.tblConfgrtnId,
        colmnName: row.colmnName,
        dataType: row.dataType,
        colmnTrnsfrmtnStep1: row.colmnTrnsfrmtnStep1,
        genrtIdInd: row.genrtIdInd,
        idGenrtnStratgyId: row.idGenrtnStratgyId,
        type2StartInd: row.type2StartInd,
        type2EndInd: row.type2EndInd,
        currRowInd: row.currRowInd,
        pattern1: row.pattern1,
        pattern2: row.pattern2,
        pattern3: row.pattern3,
        ladInd: row.ladInd,
        joinDupsInd: row.joinDupsInd,
        confgrtnEffStartTs: row.confgrtnEffStartTs,
        confgrtnEffEndTs: row.confgrtnEffEndTs
      }
    });
    const payload: { data: TableConfiguration[] } = {
      data: mappedRows,
    }
    // save when changes happened into the table.
    const isChange = rows.find((row: TableConfiguration) => row.status === 'new' || row.status === 'changed');
    if (isChange) {
      this.tableConfigService.createMulti(payload).pipe(first()).subscribe(res => {
        if (res.isSuccess) {
          if (res.data.errorMessage) {
            this._toaster.error(res.data.errorMessage, 'Error');
          } else {
            this._toaster.success(res.message, 'Success');
          }
        }
      })
    } else {
      this._toaster.error('Can save when any change happened in table', 'Error');
    }
    this.fetchAllTableConfigs();
  }
  removeSelectedRow(): void {
    if (!this.selectedRow) {
      this.snackBar.open('Please select a row first! Just click on row', 'Close', {
        duration: 2000,
        verticalPosition: 'top'
      });
      return
    }
    const params = {
      tableConfigId: this.selectedRow.controls['tblConfgrtnId'].value,
      tbleColConfigId: this.selectedRow.controls['tblColConfgrtnId'].value
    }
    this.dialog.open(AppDialogOverviewComponent).afterClosed().pipe(first()).subscribe((res) => {
      if (res) {
        this.tableConfigService.remove(params).pipe(first()).subscribe(res => {
          if (res.isSuccess) {
            const index = (this.tableConfigFormGroup.get('tableConfig') as FormArray).controls.
              findIndex((control: any) => control.controls['tblConfgrtnId'].value === this.selectedRow?.controls['tblConfgrtnId'].value);
            if (index !== -1)
              (this.tableConfigFormGroup.get('tableConfig') as FormArray).controls.splice(index, 1);
            this.dataSource = new MatTableDataSource((this.tableConfigFormGroup.get('tableConfig') as FormArray).controls);
            this._toaster.success('Column Configuration delete successfully', 'Delete');
          }
        })
      }
    })
  }

  copyToCells(): void {
    if (!this.selectedRow) {
      this.snackBar.open('Please select a row first!', 'Close', {
        duration: 2000,
        verticalPosition: 'top'
      });
      return;
    }
    const transformSqlToCopy = this.selectedRow.controls['colmnTrnsfrmtnStep1'].value;
    if (!transformSqlToCopy) {
      this._toaster.error('The selected row has an empty Transformation SQL value.', 'Error');
      return;
    }
    this.dataSource.data = this.dataSource.data.map((row: FormGroup) => {
      if (!row.controls['colmnTrnsfrmtnStep1'].value) {
        row.controls['colmnTrnsfrmtnStep1'].setValue(transformSqlToCopy);
        row.controls['status'].setValue('changed');
      }
      return row;
    });
    this.dataSource._updateChangeSubscription();
  }

  validateSyntax(): void {
    // if (this.selectedRows.length === 0) {
    //   this.snackBar.open('Select at least one row!','Close',{
    //     duration: 2000
    //  verticalPosition: 'top'
    //   })
    //   return;
    // }

    // const rowsForValidation = this.selectedRows.map(row => ({
    //   tbl_col_confrtn_id: row.tbl_confrtn_id,
    //   transformSql: row.transformSql
    // }));
    // this.simulateApiValidation(rowsForValidation).then(response => {
    //   this.dataSource.data = this.dataSource.data.map(row => {
    //     const validationResult = response.find(
    //       res => res.tbl_col_confrtn_id === row.tbl_confrtn_id
    //     );
    //     if (validationResult) {
    //       row.validationResult = validationResult.validationResult;
    //     }
    //     return row;
    //   });
    //   this.dataSource._updateChangeSubscription();
    // });


    const rows = (this.tableConfigFormGroup.get('tableConfig') as FormArray).value;
    const mappedRows: TableConfiguration[] = rows.map((row: TableConfiguration) => {
      return {
        tblColConfgrtnId: row.tblColConfgrtnId,
        tblConfgrtnId: row.tblConfgrtnId,
        colmnName: row.colmnName,
        dataType: row.dataType,
        colmnTrnsfrmtnStep1: row.colmnTrnsfrmtnStep1,
        genrtIdInd: row.genrtIdInd,
        idGenrtnStratgyId: row.idGenrtnStratgyId,
        type2StartInd: row.type2StartInd,
        type2EndInd: row.type2EndInd,
        currRowInd: row.currRowInd,
        pattern1: row.pattern1,
        pattern2: row.pattern2,
        pattern3: row.pattern3,
        ladInd: row.ladInd,
        joinDupsInd: row.joinDupsInd,
        confgrtnEffStartTs: row.confgrtnEffStartTs,
        confgrtnEffEndTs: row.confgrtnEffEndTs
      }
    });
    const payload: { data: TableConfiguration[] } = {
      data: mappedRows,
    }

    this.tableConfigService.validateSystax(payload).pipe(first()).subscribe(res => {
      if (res.isSuccess) {
        if (res.data.errorMessage) {
          this._toaster.error(res.data.errorMessage, 'Error');
        } else {
          this.isValidatedSyntax = true;
          this._toaster.success(res.message, 'Success');
        }
      }
    })
  }
  getRowColor(tblColConfgrtnId: number): string {
    const validation = this.validatedData.find((v: any) => v.key === tblColConfgrtnId);
    if (validation) {
      return validation.value === 1 ? 'green' : 'red';
    }
    return 'transparent'; // Default background color
  }
  clearAllTransformations(): void {
    this.dataSource.data.forEach((element: FormGroup) => {
      element.controls['colmnTrnsfrmtnStep1'].setValue('');
    });
  }
}
