import { Component, inject } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MaterialModule } from 'src/app/material.module';
import { IColumnDetails } from './column-detail.model';
import { MockAPIClass } from '../user-query-table/user-query-table.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { first } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { AppDialogOverviewComponent } from 'src/app/pages/ui-components/dialog/dialog.component';
import {  FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-column-detail',
  standalone: true,
  imports: [MaterialModule, ReactiveFormsModule],
  templateUrl: './column-detail.component.html',
})
export class ColumnDetailComponent extends MockAPIClass {
  displayedColumns: string[] = [
    'columnId',
    'columnName',
    'dataType',
    'transformSql',
    'generateSk',
    'type2StartInd',
    'type2EndInd',
    'curActiveInd',
    'pattern1',
    'pattern2',
    'pattern3',
    'loadInd',
    'joinDupInd','action'
  ];
  dataSource = new MatTableDataSource<any>();
  initialData: IColumnDetails[] = ColumnDetails;
  selectedRow: FormGroup | null = null;
  selectedRows: IColumnDetails[] = [];
  ColumnDetailFormGroup: FormGroup;
  private snackBar = inject(MatSnackBar);
  private dialog = inject(MatDialog);
  private fb = inject(FormBuilder);
  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<IColumnDetails>([...this.initialData]);
    this.ColumnDetailFormGroup = this.fb.group({
      ColumnDetail: this.fb.array([])
    });
    this.ColumnDetailFormGroup = this.fb.group({
      ColumnDetail: this.fb.array(ColumnDetails.map(row => this.createRowForm(row)))
    })
    this.dataSource = new MatTableDataSource((this.ColumnDetailFormGroup.get('ColumnDetail') as FormArray).controls);
  }

  createRowForm(row: IColumnDetails): FormGroup {
    return this.fb.group({
      columnId: new FormControl(row.columnId),
      columnName: new FormControl(row.columnName),
      tbl_confrtn_id: new FormControl(row.tbl_confrtn_id),
      dataType: new FormControl(row.dataType),
      transformSql: new FormControl(row.transformSql),
      generateSk: new FormControl(row.generateSk),
      type2StartInd: new FormControl(row.type2StartInd),
      type2EndInd: new FormControl(row.type2EndInd),
      curActiveInd: new FormControl(row.curActiveInd),
      pattern1: new FormControl(row.pattern1),
      pattern2: new FormControl(row.pattern2),
      pattern3: new FormControl(row.pattern3),
      loadInd: new FormControl(row.loadInd),
      joinDupInd: new FormControl(row.joinDupInd),
      status: new FormControl('unchanged'),
      // for editable form purposes
      action: new FormControl('existingRecord'),
      isEditable: new FormControl(false),
      isNewRow: new FormControl(false)
    });
  }
  addRow(): void {
    const newRow = this.fb.group({
      columnId: new FormControl(-1),
      columnName: new FormControl(''),
      tbl_confrtn_id: new FormControl(this.dataSource.data.length + 1),
      dataType: new FormControl(''),
      transformSql: new FormControl(''),
      generateSk: new FormControl(0),
      type2StartInd: new FormControl(0),
      type2EndInd: new FormControl(0),
      curActiveInd: new FormControl(0),
      pattern1: new FormControl(''),
      pattern2: new FormControl(''),
      pattern3: new FormControl(''),
      loadInd: new FormControl(0),
      joinDupInd: new FormControl(0),
      status: new FormControl('changed'),
      // for editable form purposes
      action: new FormControl('existingRecord'),
      isEditable: new FormControl(false),
      isNewRow: new FormControl(false)
    });
    const currentControl = this.ColumnDetailFormGroup.get('ColumnDetail') as FormArray;
    currentControl.push(newRow);
    this.dataSource = new  MatTableDataSource(currentControl.controls);
  }
  SaveVO(form:any, i:number) {
    form.get('ColumnDetail').at(i).get('isEditable').patchValue(false);
  }
   CancelSVO(form:any, i:number) {
    form.get('ColumnDetail').at(i).get('isEditable').patchValue(false);
  }
  EditSVO(form:any, i:number) {
    form.get('ColumnDetail').at(i).get('isEditable').patchValue(true);
  }
  refreshRow(): void {
    this.selectedRow = null;
    this.ColumnDetailFormGroup = this.fb.group({
      ColumnDetail: this.fb.array(this.getInitialColumnDetails().map(row => this.createRowForm(row)))
    });
    this.dataSource = new MatTableDataSource((this.ColumnDetailFormGroup.get('ColumnDetail') as FormArray).controls)
  }
  
  getInitialColumnDetails(): IColumnDetails[] {
    return JSON.parse(JSON.stringify(ColumnDetails));
  }
  selectRow(_row: FormGroup): void {
    // if (this.selectedRows?.includes(_row)) {
    //   this.selectedRows = this.selectedRows.filter(selected => selected !== _row);
    // } else {
    //   this.selectedRows?.push(_row);
    // }
    this.selectedRow = _row;
  }
  saveRows(): void {
    const rows = this.dataSource.data;
    const isChange = rows.find(row => row.controls['status'].value === 'new' || row.controls['status'].value === 'changed');
    if (isChange) {
      this.SaveQueryTableRow(rows).pipe(first()).subscribe({
        next: (response) => {
          if (response.success) {
            this.snackBar.open(response.message, 'Close', {
              duration: 3000,
            });
          }
        },
        error: () => {
          this.snackBar.open('Failed to save the row. Please try again.');
        },
      });
    }else{
      this.snackBar.open('Can save when changes happen in table');
    }
  }
  removeSelectedRow(): void {
    if (!this.selectedRow) {
      this.snackBar.open('Please select a row first! Just click on row', 'Close', {
        duration: 3000,
      });
      return
    }
    const Ids = {
      tbl_confrtn_id: this.selectedRow.controls['tbl_confrtn_id'].value,
      tbl_col_confrtn_id: this.selectedRow.controls['tbl_confrtn_id'].value
    }
    this.dialog.open(AppDialogOverviewComponent).afterClosed().pipe(first()).subscribe((res) => {
      if (res) {
        this.deleteRowFromAPI(Ids.tbl_confrtn_id, Ids.tbl_col_confrtn_id).pipe(first()).subscribe({
          next: (response) => {
            if (response.success) {
              const index = (this.ColumnDetailFormGroup.get('ColumnDetail') as FormArray).controls.
              findIndex((control:any) => control.controls['tbl_confrtn_id'].value === this.selectedRow?.controls['tbl_confrtn_id'].value);
              if( index !== -1)
              (this.ColumnDetailFormGroup.get('ColumnDetail') as FormArray).controls.splice(index, 1);
            this.dataSource = new MatTableDataSource((this.ColumnDetailFormGroup.get('ColumnDetail') as FormArray).controls);
              this.snackBar.open(response.message, 'Close', {
                duration: 3000,
              });
            }
          },
          error: () => {
            this.snackBar.open('Failed to delete the row. Please try again.');
          },
        });
      }
    })

  }

  copyToCells(): void {
    if (!this.selectedRow) {
      this.snackBar.open('Please select a row first!', 'Close', {
        duration: 3000,
      });
      return;
    }
    const transformSqlToCopy = this.selectedRow.controls['transformSql'].value;
    if (!transformSqlToCopy) {
      this.snackBar.open('The selected row has an empty Transformation SQL value.', 'Close', {
        duration: 3000,
      });
      return;
    }
    this.dataSource.data = this.dataSource.data.map((row:FormGroup) => {
      if (!row.controls['transformSql'].value) {
        row.controls['transformSql'].setValue(transformSqlToCopy);
        row.controls['status'].setValue('changed');
      }
      return row;
    });
    this.dataSource._updateChangeSubscription();
  }

  validateSyntax(): void {
    // if (this.selectedRows.length === 0) {
    //   this.snackBar.open('Select at least one row!','Close',{
    //     duration: 3000
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
  }
  clearAllTransformations(): void {
   this.dataSource.data.forEach((element:FormGroup) => {
      element.controls['transformSql'].setValue('');
    });
  }
}

export const ColumnDetails: IColumnDetails[] = [{
  columnId: 1001,
  tbl_confrtn_id: 1002,
  columnName: '',
  dataType: '',
  transformSql: '',
  generateSk: 0,
  type2StartInd: 0,
  type2EndInd: 0,
  curActiveInd: 0,
  pattern1: '',
  pattern2: '',
  pattern3: '',
  loadInd: 0,
  joinDupInd: 0,
},
{
  columnId: 1002,
  tbl_confrtn_id: 1003,
  columnName: '',
  dataType: '',
  transformSql: '',
  generateSk: 0,
  type2StartInd: 0,
  type2EndInd: 0,
  curActiveInd: 0,
  pattern1: '',
  pattern2: '',
  pattern3: '',
  loadInd: 0,
  joinDupInd: 0,
},
{
  columnId: 1003,
  tbl_confrtn_id: 1004,
  columnName: '',
  dataType: '',
  transformSql: 'transformsql',
  generateSk: 0,
  type2StartInd: 0,
  type2EndInd: 0,
  curActiveInd: 0,
  pattern1: '',
  pattern2: '',
  pattern3: '',
  loadInd: 0,
  joinDupInd: 0,
},
]
