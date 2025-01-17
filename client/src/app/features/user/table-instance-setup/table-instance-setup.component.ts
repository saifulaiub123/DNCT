import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTabsModule } from '@angular/material/tabs';
import { TablerIconsModule } from 'angular-tabler-icons';
import { TreeViewStateService } from 'src/app/core/shared/state-service/tree-view-state.service';
import { UserQueryTableComponent } from "./user-query-table/user-query-table.component";
import { ColumnDetailComponent } from "./column-detail/column-detail.component";
import { LoadStrategyComponent } from "./load-strategy/load-strategy.component";

interface TabData {
  title: string;
  tableConfigId: number;
}

@Component({
  selector: 'app-table-instance-setup',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule, TablerIconsModule, MatTabsModule, MatFormFieldModule, MatSlideToggleModule, MatSelectModule, MatInputModule, MatButtonModule, UserQueryTableComponent, ColumnDetailComponent, LoadStrategyComponent],
  templateUrl: './table-instance-setup.component.html',
  styleUrl: './table-instance-setup.component.scss'
})
export class TableInstanceSetupComponent {

  selectedIndex = 0;

    // tabs: string[] = [];
    tabs: TabData[] = [];
    ids: number[] = [];

    constructor(
      private _treeViewStateService: TreeViewStateService
    ) {}

    ngOnInit() {
      this.subscribeStateService();
    }

    subscribeStateService()
    {
      this._treeViewStateService.$isTableInstanceClicked.subscribe((res: any)=> {
        if(res !== null)
        {
          if(!this.tabs.includes(res.name))
          {
            this.tabs.push({title : res.name, tableConfigId: res.tableConfigId});
            this.ids.push(res.tableConfigId);
            this.selectedIndex = this.tabs.length - 1
          }
          else
          {
            this.selectedIndex = this.tabs.indexOf(res.name);
          }
          // this._treeViewStateService.clickTableInstanceReset();
        }
      })
    }
    removeTab(index: number) {
      this.tabs.splice(index, 1);
      // Optionally reset the selected tab index if needed
      if (this.selectedIndex >= this.tabs.length) {
        this.selectedIndex = this.tabs.length - 1;
      }
    }
}
