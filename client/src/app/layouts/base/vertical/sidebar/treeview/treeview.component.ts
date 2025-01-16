import { TreeViewStateService } from './../../../../../core/shared/state-service/tree-view-state.service';
import { SelectionModel } from '@angular/cdk/collections';
import { FlatTreeControl } from '@angular/cdk/tree';
import { Component, Injectable, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import {
  MatTreeFlatDataSource,
  MatTreeFlattener,
  MatTreeModule,
} from '@angular/material/tree';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { ServerResponse } from 'src/app/core/model/contract/server-response';
import { TreeViewResponse } from 'src/app/core/model/contract/tree-view-response';
import { ClickTableInstanceStateModel } from 'src/app/core/model/state-service/click-table-instance.model';
import { CommonService } from 'src/app/core/services/api-services/common.service';

export class TreeNode {
  id: number;
  name: string;
  nodeType: string;
  hasChildren: boolean;
  children: TreeNode[];
}

/** Flat to-do item node with expandable and level information */
export class FlatNode {
  id: number;
  name: string;
  nodeType: string;
  level: number;
  expandable: boolean;
}

@Injectable()
export class ChecklistDatabase {
  dataChange = new BehaviorSubject<TreeNode[]>([]);



  constructor() {
    //this.initialize();
  }

  // initialize() {
  //   // Build the tree nodes from Json object. The result is a list of `TreeNode` with nested
  //   //     file node as children.
  //   const data = this.buildFileTree(TREE_DATA, 0);

  //   // Notify the change.
  //   this.dataChange.next(data);
  // }

  /**
   * Build the file structure tree. The `value` is the Json object, or a sub-tree of a Json object.
   * The return value is the list of `TreeNode`.
   */
  // buildFileTree(obj: { [key: string]: any }, level: number): TreeNode[] {
  //   return Object.keys(obj).reduce<TreeNode[]>((accumulator, key) => {
  //     const value = obj[key];
  //     const node = new TreeNode();
  //     node.name = key;

  //     if (value != null) {
  //       if (typeof value === 'object') {
  //         node.children = this.buildFileTree(value, level + 1);
  //       } else {
  //         node.name = value;
  //       }
  //     }

  //     return accumulator.concat(node);
  //   }, []);
  // }

  /** Add an item to to-do list */

}

@Component({
  selector: 'app-sidebar-treeview',
  standalone: true,
  imports:[MatIconModule, MatCheckboxModule, MatFormFieldModule, MatTreeModule, MatCardModule, MatButtonModule, MatFormFieldModule, MatInputModule],
  templateUrl: './treeview.component.html',
  providers: [ChecklistDatabase],
})
export class SidebarTreeviewComponent implements OnInit {

  dataChange = new BehaviorSubject<TreeNode[]>([]);
  get data(): TreeNode[] {
    return this.dataChange.value;
  }
  /** Map from flat node to nested node. This helps us finding the nested node to be modified */
  flatNodeMap = new Map<FlatNode, TreeNode>();

  /** Map from nested node to flattened node. This helps us to keep the same object for selection */
  nestedNodeMap = new Map<TreeNode, FlatNode>();

  /** A selected parent node to be inserted */
  selectedParent: FlatNode | null = null;

  /** The new item's name */
  newItemName = '';

  treeControl: FlatTreeControl<FlatNode>;

  treeFlattener: MatTreeFlattener<TreeNode, FlatNode>;

  dataSource: MatTreeFlatDataSource<TreeNode, FlatNode>;
  /** The selection for checklist */
  checklistSelection = new SelectionModel<FlatNode>(
    true /* multiple */
  );

  constructor(
    private _commonService: CommonService,
    private _router: Router,
    private _treeViewStateService: TreeViewStateService
  ) {

    this.treeFlattener = new MatTreeFlattener(
      this.transformer,
      this.getLevel,
      this.isExpandable,
      this.getChildren
    );
    this.treeControl = new FlatTreeControl<FlatNode>(
      this.getLevel,
      this.isExpandable
    );
    this.dataSource = new MatTreeFlatDataSource(
      this.treeControl,
      this.treeFlattener
    );

    this.dataChange.subscribe((data) => {
      this.dataSource.data = data;
    });
  }

  ngOnInit(): void {
    this.initialize();
  }
  initialize() {

    this._commonService.getServers().subscribe((res:any)=> {
      const data = this.buildFileTree(res.data, 0);
      this.dataChange.next(data);
    })

  }
  buildFileTree(response: TreeViewResponse[], level: number): TreeNode[] {
    let treeDatas: TreeNode[] = [];

    response.forEach(item=> {
      const node = new TreeNode();
      node.id = item.id,
      node.name = item.name,
      node.children = [],
      node.nodeType = item.nodeType,
      node.hasChildren = item.hasChildren

      return treeDatas.push(node);
    })
    return treeDatas;
  }

  buildFileTree1(obj: { [key: string]: any }, level: number): TreeNode[] {
    return Object.keys(obj).reduce<TreeNode[]>((accumulator, key) => {
      const value = obj[key];
      const node = new TreeNode();
      node.name = key;

      if (value != null) {
        if (typeof value === 'object') {
          node.children = this.buildFileTree(value, level + 1);
        } else {
          node.name = value;
        }
      }

      return accumulator.concat(node);
    }, []);
  }

  onToggleExpand(node: FlatNode)
  {
    if (this.treeControl.isExpanded(node)) {
      this.fetchChildren(node);

    } else {
      this.collapseNode(node);
    }

  }

  fetchChildren(node: FlatNode)
  {
    if(node.nodeType==='Server')
    {
      this._commonService.getDatabaseByServerId(node.id).subscribe((res: ServerResponse<TreeViewResponse>)=>{
        this.addChildrenToNodeTree(node, res);
      })
    }
    else if(node.nodeType==='Database')
    {
      this._commonService.GetTablesByDatabaseSourceId(node.id).subscribe((res: ServerResponse<TreeViewResponse>)=>{
        this.addChildrenToNodeTree(node, res);
      })
    }
    else if(node.nodeType==='Table')
    {
      this._commonService.GetTableInstanceByDatabaseSourceId(node.id).subscribe((res: ServerResponse<TreeViewResponse>)=>{
        this.addChildrenToNodeTree(node, res);

        let uniqueName = `${node.id}-${node.name.toLowerCase()}`;
        this._treeViewStateService.clickTableNode(node.id, uniqueName);
        this._router.navigate(['/user/object-setup/table-configurations']);

      })
    }
    else if(node.nodeType==='TableInstance')
    {
      let uniqueName = `${node.id}-${node.name.toLowerCase()}`;
      this._treeViewStateService.clickTableInstance(node.id, uniqueName);
      this._router.navigate(['/user/table-instance-setup']);
    }
  }
  addChildrenToNodeTree(node: FlatNode, res: ServerResponse<TreeViewResponse>)
  {
    let parentNode = this.flatNodeMap.get(node);
    parentNode!.children = [];
    if (res.data.length> 0) {
      parentNode!.children.push(...res.data);

      this.dataChange.next(this.data);
    }
    this.treeControl.expand(node);
  }
  collapseNode(node: FlatNode) {
    this.treeControl.collapse(node);
    // const parentNode = this.treeControl.dataNodes.find(n => n.name === node.name);
    // if (parentNode) {
    //   const index = this.treeControl.dataNodes.indexOf(parentNode);
    //   let count = 0;
    //   for (let i = index + 1; i < this.treeControl.dataNodes.length; i++) {
    //     if (this.treeControl.dataNodes[i].level <= parentNode.level) {
    //       break;
    //     }
    //     count++;
    //   }
    //   this.treeControl.dataNodes.splice(index + 1, count);
    // }
  }
  getLevel = (node: FlatNode) => node.level;

  isExpandable = (node: FlatNode) => node.expandable;

  getChildren = (node: TreeNode): TreeNode[] => node.children;

  hasChild = (_: number, _nodeData: FlatNode) => _nodeData.expandable;

  hasNoContent = (_: number, _nodeData: FlatNode) =>
    _nodeData.name === '';

  /**
   * Transformer to convert nested node to flat node. Record the nodes in maps for later use.
   */
  transformer = (node: TreeNode, level: number) => {
    const existingNode = this.nestedNodeMap.get(node);
    const flatNode =
      existingNode && existingNode.name === node.name
        ? existingNode
        : new FlatNode();
    flatNode.id = node.id;
    flatNode.name = node.name;
    flatNode.nodeType = node.nodeType;
    flatNode.level = level;
    flatNode.expandable = node.hasChildren ? true: false;
    this.flatNodeMap.set(flatNode, node);
    this.nestedNodeMap.set(node, flatNode);
    return flatNode;
  };

  /** Whether all the descendants of the node are selected. */
  descendantsAllSelected(node: FlatNode): boolean {
    const descendants = this.treeControl.getDescendants(node);
    const descAllSelected =
      descendants.length > 0 &&
      descendants.every((child) => {
        return this.checklistSelection.isSelected(child);
      });
    return descAllSelected;
  }

  /** Whether part of the descendants are selected */
  descendantsPartiallySelected(node: FlatNode): boolean {
    const descendants = this.treeControl.getDescendants(node);
    const result = descendants.some((child) =>
      this.checklistSelection.isSelected(child)
    );
    return result && !this.descendantsAllSelected(node);
  }

  /** Toggle the to-do item selection. Select/deselect all the descendants node */
  todoItemSelectionToggle(node: FlatNode): void {
    this.checklistSelection.toggle(node);
    const descendants = this.treeControl.getDescendants(node);
    this.checklistSelection.isSelected(node)
      ? this.checklistSelection.select(...descendants)
      : this.checklistSelection.deselect(...descendants);

    // Force update for the parent
    descendants.forEach((child) => this.checklistSelection.isSelected(child));
    this.checkAllParentsSelection(node);
  }

  /** Toggle a leaf to-do item selection. Check all the parents to see if they changed */
  todoLeafItemSelectionToggle(node: FlatNode): void {
    this.checklistSelection.toggle(node);
    this.checkAllParentsSelection(node);
  }

  /* Checks all the parents when a leaf node is selected/unselected */
  checkAllParentsSelection(node: FlatNode): void {
    let parent: FlatNode | null = this.getParentNode(node);
    while (parent) {
      this.checkRootNodeSelection(parent);
      parent = this.getParentNode(parent);
    }
  }

  /** Check root node checked state and change it accordingly */
  checkRootNodeSelection(node: FlatNode): void {
    const nodeSelected = this.checklistSelection.isSelected(node);
    const descendants = this.treeControl.getDescendants(node);
    const descAllSelected =
      descendants.length > 0 &&
      descendants.every((child) => {
        return this.checklistSelection.isSelected(child);
      });
    if (nodeSelected && !descAllSelected) {
      this.checklistSelection.deselect(node);
    } else if (!nodeSelected && descAllSelected) {
      this.checklistSelection.select(node);
    }
  }

  /* Get the parent node of a node */
  getParentNode(node: FlatNode): FlatNode | null {
    const currentLevel = this.getLevel(node);

    if (currentLevel < 1) {
      return null;
    }

    const startIndex = this.treeControl.dataNodes.indexOf(node) - 1;

    for (let i = startIndex; i >= 0; i--) {
      const currentNode = this.treeControl.dataNodes[i];

      if (this.getLevel(currentNode) < currentLevel) {
        return currentNode;
      }
    }
    return null;
  }

  /** Select the category so we can insert the new item. */
  addNewItem(node: FlatNode) {
    const parentNode = this.flatNodeMap.get(node);
    this.insertItem(parentNode!, '');
    this.treeControl.expand(node);
  }

  /** Save the node to database */
  saveNode(node: FlatNode, itemValue: string) {
    const nestedNode = this.flatNodeMap.get(node);
    this.updateItem(nestedNode!, itemValue);
  }

  insertItem(parent: TreeNode, name: string) {
    if (parent.children) {
      parent.children.push({ name: name } as TreeNode);
      this.dataChange.next(this.data);
    }
  }

  updateItem(node: TreeNode, name: string) {
    node.name = name;
    this.dataChange.next(this.data);
  }


  // DDL Operation
  addNewTable()
  {
    this._router.navigate(['/user/create-table']);
  }
}
