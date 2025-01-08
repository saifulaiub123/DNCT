import { Component, inject } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MaterialModule } from 'src/app/material.module';
import { AutoPopulate, CreateUpdateQuery, UserQuery, ValidateSyntax } from './user-query-table.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError, delay, EMPTY, EmptyError, first, Observable, of } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { ServerResponse } from 'src/app/core/model/contract/server-response';
import { UserQueryService } from './user-query.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { ToastrService } from 'ngx-toastr';
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
  providers: [UserQueryService],
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
  queryId!: number;
  dataSource = new MatTableDataSource<UserQuery>()
  // injectables
  private snackBar = inject(MatSnackBar);
  private dialog = inject(MatDialog);
  private userQueryService = inject(UserQueryService);
  private _ngxService = inject(NgxUiLoaderService);
  private _toastr = inject(ToastrService);
  ngOnInit(): void {
    this.fetchAllUserQueries();
  }
  fetchAllUserQueries(): void {
    this._ngxService.start();
    this.userQueryService.fetchAllUserQueries().pipe(first()).subscribe((res: ServerResponse<UserQuery>) => {
      if (res.isSuccess) {
        this.dataSource.data = res.data.map((userQuery: UserQuery) => {
          return {
            ...userQuery, isSelected: false
          }
        });
        this._ngxService.stop();
      }
    })
  }
  saveRow(_row: UserQuery): void {
    const selectedRows = this.dataSource.data.filter((row) => row.isSelected);
    if (selectedRows.length === 0) {
      this._toastr.error('Row Should be checked.', 'Error');
      return;
    }
    const payload: CreateUpdateQuery = {
      userQueryId: _row.userQueryId,
      tableConfigId: _row.tableConfigId,
      userQuery: _row.userQuery,
      baseQueryIndicator: _row.baseQueryIndicator,
      queryOrderIndicator: _row.queryOrderIndicator,
    }
    this._ngxService.start();
    this.userQueryService.createOrUpdateQuery(payload).pipe(first(), catchError(error => {
      this._ngxService.stop();
      this._toastr.error('Error occured', 'Error');
      return EMPTY
    })).subscribe((res: ServerResponse<CreateUpdateQuery>) => {
      this._ngxService.stop();
      if (res) {console.log('create update query ===>>>', res); this.fetchAllUserQueries();}
    })
  }

  removeRow(row: UserQuery) {
    const parameters:{userQueryId:number, tableConfigId: number} = {
      tableConfigId:row.tableConfigId, userQueryId:row.userQueryId
    }
    this.userQueryService.removeQuery(parameters).pipe(first(), catchError(() => {
      this._ngxService.stop();
      this._toastr.error('Failed to delete the row. Please try again.', 'Error');
      return EMPTY
    })).subscribe((res: ServerResponse<any>) => {
      this._ngxService.stop();
      if (res) {console.log('delete query ===>>>', res)
        this.fetchAllUserQueries();
      }
    })
  }

  onRowSelect(row: UserQuery) {
    this.dataSource.data.forEach((item) => {
      if (item !== row) {
        item.isSelected = false;
      }
    });
    this.queryId = row.isSelected ? row.userQueryId : 0
    this.selectedQuery = row.isSelected ? row.userQuery : '';
  }

  addNewQuery() {
    const existingRow = this.dataSource.data.find(row => row.userQueryId === -1);
    if (existingRow) {
      this._toastr.error('A row with Query ID -1 already exists!', 'Error');
    } else {
      const newRow: UserQuery = {
        isSelected: false,
        userQueryId: -1,
        tableConfigId: 0,
        userQuery: '',
        baseQueryIndicator: 0,
        queryOrderIndicator: 0,
        rowInsertTimestamp: null,
      };
      this.dataSource.data = [...this.dataSource.data, newRow];
    }
  }

  refreshData() {
    this.selectedQuery = '';
    this.queryId = 0;
    this.fetchAllUserQueries();
  }
  autoPopulate() {
    const selectedRows = this.dataSource.data.filter((row) => row.isSelected);
    if (selectedRows.length === 0) {
      this._toastr.error('Please select one row.', 'Error');
      return;
    }
    this._ngxService.start();
    this.userQueryService.autoPopulate().pipe(first()).subscribe((res: ServerResponse<AutoPopulate>) => {
      if (res) {
        console.log('auto populate api calll======>>>>', res.data);
        this._ngxService.stop();
      }
    });
  }
  validateQuery() {
    const selectedRows = this.dataSource.data.filter((row) => row.isSelected);
    if (selectedRows.length === 0) {
      this._toastr.error('Please select one row.', 'Error');
      return;
    };
    this._ngxService.start();
    this.userQueryService.validateSyntax().pipe(first()).subscribe((res: ServerResponse<ValidateSyntax>) => {
      if (res) {
        console.log('validate syntac api called ======>>>>', res.data);
        this._ngxService.stop();
      }
    });
  }

}



