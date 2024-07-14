import { FlatTreeControl } from '@angular/cdk/tree';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTreeFlattener, MatTreeFlatDataSource, MatTreeModule } from '@angular/material/tree';
import { TreeViewResponse } from 'src/app/core/model/contract/tree-view-response';
import { TreeViewService } from 'src/app/core/services/api-services/tree-view.service';


export interface TreeNode {
  name: string;
  hasChildren: boolean;
  children?: TreeNode[];
}

export class FlatNode {
  constructor(
    public expandable: boolean,
    public name: string,
    public level: number,
    public isLoading: boolean = false,
  ) {}
}


@Component({
  selector: 'app-tree-1',
  templateUrl: './dynamic-tree-view-1.component.html',
  styleUrls: ['./dynamic-tree-view-1.component.scss'],
  standalone: true,
  imports: [MatTreeModule, MatButtonModule, MatIconModule],
})

  export class TreeComponent implements OnInit {
    private transformer = (node: TreeNode, level: number) => {
      return new FlatNode(!!node.hasChildren, node.name, level);
    };

    treeControl = new FlatTreeControl<FlatNode>(
      node => node.level, node => node.expandable);

    treeFlattener = new MatTreeFlattener(
      this.transformer, node => node.level, node => node.expandable, node => node.children);

    dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

    constructor(private http: HttpClient) {}

    ngOnInit() {
      this.loadInitialData();
    }

    hasChild = (_: number, node: FlatNode) => node.expandable;

    loadInitialData() {
      this.http.get<TreeNode[]>('/api/parents').subscribe(data => {
        this.dataSource.data = data;
      });
    }

    fetchChildren(node: FlatNode) {
      if (node.isLoading) {
        return;
      }

      node.isLoading = true;
      this.http.get<TreeNode[]>(`/api/children/${node.name}`).subscribe(data => {
        node.isLoading = false;
        const parentNode = this.treeControl.dataNodes.find(n => n.name === node.name);
        if (parentNode) {
          const index = this.treeControl.dataNodes.indexOf(parentNode);
          const children = data.map(child => new FlatNode(!!child.hasChildren, child.name, parentNode.level + 1));
          this.treeControl.dataNodes.splice(index + 1, 0, ...children);
          this.treeControl.expand(parentNode);
        }
      });
    }

    // addChild(node: FlatNode) {
    //   const parentNode = this.treeControl.dataNodes.find(n => n.name === node.name);
    //   if (parentNode) {
    //     const newChild = { name: `${node.name} Child`, hasChildren: false };
    //     if (!parentNode.children) {
    //       parentNode.children = [];
    //     }
    //     parentNode.children.push(newChild);
    //     const index = this.treeControl.dataNodes.indexOf(parentNode);
    //     this.treeControl.dataNodes.splice(index + 1, 0, new FlatNode(false, newChild.name, node.level + 1));
    //     this.treeControl.expand(parentNode);
    //     this.treeControl.dataNodes = [...this.treeControl.dataNodes]; // Trigger change detection
    //   }
    // }
  }





// fetchChildren(node: FlatNode) {
//   if (node.isLoading) {
//     return;
//   }

//   node.isLoading = true;
//   this._treeViewService.getDatabaseByServerId(100).subscribe(res => {
//     node.isLoading = false;
//     const parentNode = this.treeControl.dataNodes.find(n => n.name === node.name);
//     if (parentNode) {
//       const index = this.treeControl.dataNodes.indexOf(parentNode);
//       const children = res.data.map((child: TreeViewResponse) => new FlatNode(!!child.hasChildren, child.name, parentNode.level + 1));
//       this.treeControl.dataNodes.splice(index + 1, 0, ...children);
//       this.treeControl.expand(parentNode);
//     }

//   });
// }

// addChild(node: TreeNode) {
//   const parentNode = this.treeControl.dataNodes.find(n => n.name === node.name);
//   if (parentNode) {
//     const newChild = { name: `${node.name} Child`, hasChildren: false };
//     if (!parentNode.children) {
//       parentNode.children = [];
//     }
//     parentNode.children.push(newChild);
//     const index = this.treeControl.dataNodes.indexOf(parentNode);
//     this.treeControl.dataNodes.splice(index + 1, 0, new FlatNode(false, newChild.name, node.level + 1));
//     this.treeControl.expand(parentNode);
//     this.treeControl.dataNodes = [...this.treeControl.dataNodes]; // Trigger change detection
//   }
// }
