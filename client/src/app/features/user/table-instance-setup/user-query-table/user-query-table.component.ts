import { Component, inject, Input } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MaterialModule } from 'src/app/material.module';
import { AutoPopulate, CreateUpdateQuery, UserQuery, ValidateSyntax } from './user-query-table.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { catchError,EMPTY, first, } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { ServerResponse } from 'src/app/core/model/contract/server-response';
import { UserQueryService } from './user-query.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { ToastrService } from 'ngx-toastr';
import { AppDialogOverviewComponent } from 'src/app/pages/ui-components/dialog/dialog.component';
@Component({
  selector: 'app-user-query-table',
  standalone: true,
  imports: [MaterialModule],
  providers: [UserQueryService],
  templateUrl: './user-query-table.component.html',
  styles: `::ng-deep{
    .mat-mdc-form-field .mat-mdc-text-field-wrapper{
      width:100% !important;
      height:200px!important;
    }
  }`
})
export class UserQueryTableComponent {

  @Input() tableConfigId : number = 0;

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
    this.userQueryService.fetchAllUserQueries(this.tableConfigId).pipe(first()).subscribe((res: ServerResponse<UserQuery>) => {
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
      this._toastr.error('Row Should be selected.', 'Error');
      return;
    }
    const payload: CreateUpdateQuery = {
      userQueryId: _row.userQueryId,
      tableConfigId: this.tableConfigId,
      userQuery: this.selectedQuery,
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
      if (res) {
        this._toastr.success('Saved successfully', 'Success');
        console.log('create update query ===>>>', res);
        this.refreshData();
      }
    })
  }

  removeRow(row: UserQuery) {
    const existingRow = this.dataSource.data.find(row => row.userQueryId === -1);
    if (existingRow) {
      this.dataSource.data = this.dataSource.data.filter(row => row.userQueryId !== -1);
      this._toastr.error('Row removed successfully', 'Removed');
      return;
    } 
    const parameters:{userQueryId:number, tableConfigId: number} = {
      tableConfigId:row.tableConfigId, userQueryId:row.userQueryId
    }
    this.dialog.open(AppDialogOverviewComponent).afterClosed().pipe(first()).subscribe(res => {
      if(res){
        this.userQueryService.removeQuery(parameters).pipe(first(), catchError(() => {
          this._ngxService.stop();
          this._toastr.error('Failed to delete the row. Please try again.', 'Error');
          return EMPTY
        })).subscribe((res: ServerResponse<any>) => {
          this._ngxService.stop();
          if (res) {
            this._toastr.success('Removed successfully', 'Success');
            this.fetchAllUserQueries();
          }
        })
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
      this._toastr.error('Select atleast one row', 'Error');
      return;
    }
    this._ngxService.start();
    this.userQueryService.autoPopulate(this.tableConfigId, this.queryId).pipe(first()).subscribe((res: ServerResponse<AutoPopulate>) => {
      if (res) {this._toastr.success('Auto populated successfully', 'Success');
        this._ngxService.stop();
      }
    });
  }
  validateQuery() {
    const selectedRows = this.dataSource.data.filter((row) => row.isSelected);
    if (selectedRows.length === 0) {
      this._toastr.error('Select atleast one row.', 'Error');
      return;
    };
    this._ngxService.start();
    this.userQueryService.validateSyntax(this.tableConfigId, this.queryId).pipe(first()).subscribe((res: ServerResponse<ValidateSyntax>) => {
      if (res) {this._toastr.success('Validate syntax successfully', 'Success');
        this._ngxService.stop();
      }
    });
  }

}



