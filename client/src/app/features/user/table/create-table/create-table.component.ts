import { Component, OnInit } from '@angular/core';
import { UntypedFormGroup, UntypedFormControl, Validators, FormControl, FormGroup, FormsModule } from '@angular/forms';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { TablerIconsModule } from 'angular-tabler-icons';
import { ProcessDDLResponse } from 'src/app/core/model/contract/process-ddl-response';
import { ServerResponse } from 'src/app/core/model/contract/server-response';
import { TreeViewResponse } from 'src/app/core/model/contract/tree-view-response';
import { ServerDto } from 'src/app/core/model/dto/server-dto';
import { TableService } from 'src/app/core/services/api-services/table.service';
import { CommonService } from 'src/app/core/services/api-services/tree-view.service';
import { PasswordMatchValidator } from 'src/app/features/authentication/register/register.component';
import { MaterialModule } from 'src/app/material.module';

@Component({
  selector: 'app-create-table',
  standalone: true,
  imports: [MaterialModule, TablerIconsModule, MatFormFieldModule, MatInputModule, MatRadioModule, MatCheckboxModule, MatDatepickerModule, FormsModule],
  templateUrl: './create-table.component.html',
  styleUrl: './create-table.component.scss'
})
export class CreateTableComponent implements OnInit {

  createTableForm: FormGroup;

  connectionList: any;
  ddlText: string;
  tableName: string;

  servers : ServerDto[] = [];
  selectedServer: any = "";

  constructor(
    private _commonService : CommonService,
    private _tableService: TableService
  ){

  }
  ngOnInit(): void {
    this.initializeCreateTableForm();
    this.loadConnection();
  }

  initializeCreateTableForm()
  {
    this.createTableForm = new FormGroup({
      ddlText: new FormControl('', [Validators.required, Validators.minLength(2)]),
      tableName: new FormControl('', [Validators.required, Validators.email]),
    }, {
      validators: PasswordMatchValidator('password', 'confirmPassword')
    })
  }

  loadConnection()
  {
    this._commonService.getServers().subscribe((res: ServerResponse<TreeViewResponse>)=> {
      res.data.forEach((item: any) => {
        this.servers.push(new ServerDto(item.id, item.name))
      });
    })
  }
  processDDL()
  {
    let model =
    {
      content: this.ddlText
    }
    this._tableService.processDDL(model).subscribe((res: ServerResponse<ProcessDDLResponse>)=> {
      this.tableName = res.data.tableName;
    })
  }
}
