import { Component, inject } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MaterialModule } from 'src/app/material.module';
import { IUserQueryTable } from './user-query-table.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { delay, first, Observable, of } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { AppDialogOverviewComponent } from 'src/app/pages/ui-components/dialog/dialog.component';
export class MockAPIClass {
   simulateApiValidation(rows: { tbl_col_confrtn_id: number; transformSql: string }[]) {
    return new Promise<{ tbl_col_confrtn_id: number; validationResult: number }[]>(resolve => {
      const response = rows.map(row => ({
        tbl_col_confrtn_id: row.tbl_col_confrtn_id,
        validationResult: Math.random() < 0.5 ? 0 : 1 // Randomly return 0 or 1
      }));
      setTimeout(() => resolve(response), 1000); // Simulate network delay
    });
  }
  deleteRowFromAPI(
    tableConfigId: number,
    queryId: number
  ): Observable<{ success: boolean; message: string }> {
    console.log(
      `API Call: Deleting row with TableConfigId=${tableConfigId}, QueryId=${queryId}`
    );

    return of({
      success: true,
      message: `Row with TableConfigId=${tableConfigId} and QueryId=${queryId} deleted successfully.`,
    }).pipe(delay(1000));
  }

  autoPopulateColumnsAPI(tableConfigId: number, queryId: number): Observable<any> {
    const payload = { tableConfigId: tableConfigId, queryId: queryId };
    console.log(payload);
    return of({
      success: true,
      data: {
        fullQuery: 'SELECT * FROM new_table',
        seedQuery: 100,
        qtyOrder: 50,
      },
    }).pipe(delay(1000));
  }
  SaveQueryTableRow(_row: any): Observable<any> {
    console.log(_row);
    return of({
      success: true,
      message: 'Data save successfully'
    }).pipe(delay(1000));
  }
}
@Component({
  selector: 'app-user-query-table',
  standalone: true,
  imports: [MaterialModule],
  templateUrl: './user-query-table.component.html',
})
export class UserQueryTableComponent extends MockAPIClass {
  displayedColumns: string[] = [
    'Select',
    'Query Id',
    'Full Query',
    'Seed Query',
    'Qty Order',
    'Save',
    'Remove',
    'Validation Result',
  ];
  selectedQuery: string = '';
  queryId!: string
  dataSource = new MatTableDataSource<IUserQueryTable>(QueryTableDataSource)


  // injectables
  private snackBar = inject(MatSnackBar);
  private dialog = inject(MatDialog);

  saveRow(_row: IUserQueryTable) {
    this.SaveQueryTableRow(_row).pipe(first()).subscribe({
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
  }

  removeRow(row: any) {
    const { tableConfigId, queryId } = row;
    this.dialog.open(AppDialogOverviewComponent).afterClosed().pipe(first()).subscribe((res) => {
      if (res) {
        this.deleteRowFromAPI(tableConfigId, queryId).pipe(first()).subscribe({
          next: (response) => {
            if (response.success) {
              this.dataSource.data = this.dataSource.data.filter(
                (data) => data.tableConfigId !== tableConfigId
              );
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

  toggleAllRows(event: any) {
    const isChecked = event.checked;
    this.dataSource.data.forEach((row: any) => (row.isSelected = isChecked));
  }

  onRowSelect(row: any) {
    this.dataSource.data.forEach((item) => {
      if (item !== row) {
        item.isSelected = false;
      }
    });
    this.queryId = row.isSelected ? row.queryId : ''
    this.selectedQuery = row.isSelected ? row.fullQuery : '';
  }

  addNewQuery() {
    const existingRow = this.dataSource.data.find(row => row.queryId === -1);
    if (existingRow) {
      this.snackBar.open('A row with Query ID -1 already exists!', 'Close', {
        duration: 3000,
      });
    } else {
      const newRow = {
        isSelected: false,
        tableConfigId: this.dataSource.data[this.dataSource.data.length - 1].tableConfigId + 1,
        queryId: -1,
        fullQuery: '',
        seedQuery: 0,
        qtyOrder: 0,
      };
      this.dataSource.data = [...this.dataSource.data, newRow];
    }
  }

  refreshData() {
    this.dataSource.data = [];
    this.selectedQuery = '';
    this.queryId = '';
    this.dataSource.data = QueryTableDataSource;
  }
  autoPopulate() {
    const selectedRows = this.dataSource.data.filter((row) => row.isSelected);
    if (selectedRows.length === 0) {
      this.snackBar.open('Please select one row.', 'Close', {
        duration: 3000
      });
      return;
    }
    const selectedRow = selectedRows[0];
    this.autoPopulateColumnsAPI(selectedRow.tableConfigId, selectedRow.queryId).pipe(first()).subscribe({
      next: (response) => {
        if (response.success) {
          this.snackBar.open('Columns auto-populated successfully!', 'Close', {
            duration: 3000
          });
        }
      },
      error: () => {
        this.snackBar.open('Failed to auto-populate columns. Please try again.', 'Close', {
          duration: 3000
        });
      },
    });
  }
  validateQuery() {
    const selectedRows = this.dataSource.data.filter((row) => row.isSelected);
    if (selectedRows.length === 0) {
      this.snackBar.open('Please select one row.', 'Close', {
        duration: 3000
      });
      return;
    }
    const selectedRow = selectedRows[0];
    this.autoPopulateColumnsAPI(selectedRow.tableConfigId, selectedRow.queryId).pipe(first()).subscribe({
      next: (response) => {
        if (response.success) {
          console.log(response.body)
          this.snackBar.open('Query Validated successfully!', 'Close', {
            duration: 3000
          });
        }
      },
      error: () => {
        this.snackBar.open('Failed to validate query. Please try again.', 'Close', {
          duration: 3000
        });
      },
    });
  }

}

export const QueryTableDataSource: IUserQueryTable[] = [
  {
    isSelected: false,
    tableConfigId: 101,
    queryId: 23,
    fullQuery: 'CREATE TABLE ...',
    seedQuery: 0,
    qtyOrder: 0,
  },
  {
    isSelected: false,
    tableConfigId: 102,
    queryId: 24,
    fullQuery: 'SELECT * FROM ...',
    seedQuery: 0,
    qtyOrder: 0,
  },
  {
    isSelected: false,
    tableConfigId: 103,
    queryId: 26,
    fullQuery: 'SELECT TRIM(...)',
    seedQuery: 0,
    qtyOrder: 0,
  },
]


