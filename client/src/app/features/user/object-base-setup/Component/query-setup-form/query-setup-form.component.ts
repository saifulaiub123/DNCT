import { CommonModule, DatePipe } from '@angular/common';
import { Component, Inject, Input, OnInit, Optional, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MAT_DIALOG_DATA, MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatTable, MatTableDataSource, MatTableModule } from '@angular/material/table';
import { Router } from '@angular/router';
import { TablerIconsModule } from 'angular-tabler-icons';
import { ToastrService } from 'ngx-toastr';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { DbNameObjectSetupResponse } from 'src/app/core/model/contract/db-source-object-setup-response';
import { ServerResponse } from 'src/app/core/model/contract/server-response';
import { UserQueriesResponse } from 'src/app/core/model/contract/user-queries-response';
import { ServerDto } from 'src/app/core/model/dto/server-dto';
import { CommonService } from 'src/app/core/services/api-services/common.service';
import { DatabaseSourceService } from 'src/app/core/services/api-services/database-source.service';
import { UserQueryService } from 'src/app/core/services/api-services/user-query.service';
import { MaterialModule } from 'src/app/material.module';
import { Employee } from 'src/app/pages/apps/employee/employee.component';
import { AppAddKichenSinkComponent } from 'src/app/pages/datatable/kichen-sink/add/add.component';

const employees = [
  {
    id: 1,
    Name: 'Johnathan Deo',
    Position: 'Seo Expert',
    Email: 'r@gmail.com',
    Mobile: 9786838,
    DateOfJoining: new Date('01-2-2020'),
    Salary: 12000,
    Projects: 10,
    imagePath: 'assets/images/profile/user-2.jpg',
  },
  {
    id: 2,
    Name: 'Mark Zukerburg',
    Position: 'Web Developer',
    Email: 'mark@gmail.com',
    Mobile: 8786838,
    DateOfJoining: new Date('04-2-2020'),
    Salary: 12000,
    Projects: 10,
    imagePath: 'assets/images/profile/user-3.jpg',
  },
  {
    id: 3,
    Name: 'Sam smith',
    Position: 'Web Designer',
    Email: 'sam@gmail.com',
    Mobile: 7788838,
    DateOfJoining: new Date('02-2-2020'),
    Salary: 12000,
    Projects: 10,
    imagePath: 'assets/images/profile/user-4.jpg',
  },
  {
    id: 4,
    Name: 'John Deo',
    Position: 'Tester',
    Email: 'john@gmail.com',
    Mobile: 8786838,
    DateOfJoining: new Date('03-2-2020'),
    Salary: 12000,
    Projects: 11,
    imagePath: 'assets/images/profile/user-5.jpg',
  },
  {
    id: 5,
    Name: 'Genilia',
    Position: 'Actor',
    Email: 'genilia@gmail.com',
    Mobile: 8786838,
    DateOfJoining: new Date('05-2-2020'),
    Salary: 12000,
    Projects: 19,
    imagePath: 'assets/images/profile/user-6.jpg',
  },
  {
    id: 6,
    Name: 'Jack Sparrow',
    Position: 'Content Writer',
    Email: 'jac@gmail.com',
    Mobile: 8786838,
    DateOfJoining: new Date('05-21-2020'),
    Salary: 12000,
    Projects: 5,
    imagePath: 'assets/images/profile/user-7.jpg',
  },
  {
    id: 7,
    Name: 'Tom Cruise',
    Position: 'Actor',
    Email: 'tom@gmail.com',
    Mobile: 8786838,
    DateOfJoining: new Date('02-15-2019'),
    Salary: 12000,
    Projects: 9,
    imagePath: 'assets/images/profile/user-3.jpg',
  },
  {
    id: 8,
    Name: 'Hary Porter',
    Position: 'Actor',
    Email: 'hary@gmail.com',
    Mobile: 8786838,
    DateOfJoining: new Date('07-3-2019'),
    Salary: 12000,
    Projects: 7,
    imagePath: 'assets/images/profile/user-6.jpg',
  },
  {
    id: 9,
    Name: 'Kristen Ronaldo',
    Position: 'Player',
    Email: 'kristen@gmail.com',
    Mobile: 8786838,
    DateOfJoining: new Date('01-15-2019'),
    Salary: 12000,
    Projects: 1,
    imagePath: 'assets/images/profile/user-5.jpg',
  },
];


@Component({
  selector: 'app-query-setup-form',
  standalone: true,
  imports: [MaterialModule, TablerIconsModule, MatFormFieldModule, MatInputModule, MatRadioModule, MatCheckboxModule, MatDatepickerModule, MatTableModule, CommonModule, ReactiveFormsModule],
  providers: [DatePipe],
  templateUrl: './query-setup-form.component.html',
  styleUrl: './query-setup-form.component.scss'
})
export class QuerySetupFormComponent implements OnInit {

@ViewChild(MatTable, { static: true }) table: MatTable<any> =
    Object.create(null);
userQueryData: UserQueriesResponse[] = [];
  searchText: any;
  displayedColumns: string[] = [
    'select',
    'queryId',
    'fullQuery',
    'seadQuery',
    'qryOrder',
    'validationResult',
    'action'
  ];
  dataSource = new MatTableDataSource(employees);

  constructor(
    public dialog: MatDialog,
    public datePipe: DatePipe,
  private _userQueryService: UserQueryService) {}


  ngOnInit(): void {
    this.initialize();
  }
  initialize() {

    this._userQueryService.getAll().subscribe((res: ServerResponse<UserQueriesResponse>)=> {
      this.userQueryData = res.data;
      // this.form.patchValue({
      //     databaseSourceId : this.databaseSourceId,
      //     databaseName: res.data.length > 0 ? res.data[0].databaseName : '',
      //     truncateBeforeLoad: 'Y'
      //   })

    })
  }

  submit()
  {
    // if(!this.form.valid)
    // {
    //   return;
    // }
    // this._ngxService.start();
    // this._databaseSourceService.createNewObject(this.form.value).subscribe((res: ServerResponse<any>) => {
    //   this._ngxService.stop();
    //   this.form.reset();
    //   this.connections = [];
    //   this.initialize();
    //   this._toastr.success("Object created successfully");
    // },
    // (ex) => {
    //   console.log(ex);
    //   this._toastr.error("Something went wrong","Error!");
    //   this._ngxService.stopAll();
    // })
  }


  openDialog(action: string, obj: any): void {
      obj.action = action;
      const dialogRef = this.dialog.open(AppKichenSinkDialogContentComponent, {
        data: obj,
      });
      dialogRef.afterClosed().subscribe((result) => {
        if (result.event === 'Add') {
          this.addRowData(result.data);
        } else if (result.event === 'Update') {
          this.updateRowData(result.data);
        } else if (result.event === 'Delete') {
          //this.deleteRowData(result.data);
        }
      });
    }

    addRowData(row_obj: Employee): void {
        this.dataSource.data.unshift({
          id: employees.length + 1,
          Name: row_obj.Name,
          Position: row_obj.Position,
          Email: row_obj.Email,
          Mobile: row_obj.Mobile,

          DateOfJoining: new Date(),
          Salary: row_obj.Salary,
          Projects: row_obj.Projects,
          imagePath: row_obj.imagePath,
        });
        this.dialog.open(AppAddKichenSinkComponent);
        this.table.renderRows();
      }

      // tslint:disable-next-line - Disables all
      updateRowData(row_obj: Employee): boolean | any {
        this.dataSource.data = this.dataSource.data.filter((value: any) => {
          if (value.id === row_obj.id) {
            value.Name = row_obj.Name;
            value.Position = row_obj.Position;
            value.Email = row_obj.Email;
            value.Mobile = row_obj.Mobile;
            value.DateOfJoining = row_obj.DateOfJoining;
            value.Salary = row_obj.Salary;
            value.Projects = row_obj.Projects;
            value.imagePath = row_obj.imagePath;
          }
          return true;
        });
      }
}


@Component({
  // tslint:disable-next-line: component-selector
  selector: 'app-dialog-content',
  standalone: true,
  imports: [MatDialogModule, FormsModule, MaterialModule],
  providers: [DatePipe],
  templateUrl: './kichen-sink-dialog-content.html',
})
// tslint:disable-next-line: component-class-suffix
export class AppKichenSinkDialogContentComponent {
  action: string;
  // tslint:disable-next-line - Disables all
  local_data: any;
  selectedImage: any = '';
  joiningDate: any = '';

  constructor(
    public datePipe: DatePipe,
    public dialogRef: MatDialogRef<AppKichenSinkDialogContentComponent>,
    // @Optional() is used to prevent error if no data is passed
    @Optional() @Inject(MAT_DIALOG_DATA) public data: Employee
  ) {
    this.local_data = { ...data };
    this.action = this.local_data.action;
    if (this.local_data.DateOfJoining !== undefined) {
      this.joiningDate = this.datePipe.transform(
        new Date(this.local_data.DateOfJoining),
        'yyyy-MM-dd'
      );
    }
    if (this.local_data.imagePath === undefined) {
      this.local_data.imagePath = 'assets/images/profile/user-1.jpg';
    }
  }

  doAction(): void {
    this.dialogRef.close({ event: this.action, data: this.local_data });
  }
  closeDialog(): void {
    this.dialogRef.close({ event: 'Cancel' });
  }

  selectFile(event: any): void {
    if (!event.target.files[0] || event.target.files[0].length === 0) {
      // this.msg = 'You must select an image';
      return;
    }
    const mimeType = event.target.files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      // this.msg = "Only images are supported";
      return;
    }
    // tslint:disable-next-line - Disables all
    const reader = new FileReader();
    reader.readAsDataURL(event.target.files[0]);
    // tslint:disable-next-line - Disables all
    reader.onload = (_event) => {
      // tslint:disable-next-line - Disables all
      this.local_data.imagePath = reader.result;
    };
  }
}
