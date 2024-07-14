// import {FlatTreeControl} from '@angular/cdk/tree';
// import {Component} from '@angular/core';
// import {MatTreeFlatDataSource, MatTreeFlattener, MatTreeModule} from '@angular/material/tree';
// import {MatIconModule} from '@angular/material/icon';
// import {MatButtonModule} from '@angular/material/button';
// import { TreeViewService } from 'src/app/core/services/api-services/tree-view.service';
// import { TreeViewResponse } from 'src/app/core/model/contract/tree-view-response';

// /**
//  * Food data with nested structure.
//  * Each node has a name and an optional list of children.
//  */
// interface FoodNode {
//   id: number;
//   // parentId: number;
//   name: string;
//   type: string;
//   children?: FoodNode[];
// }

// const TREE_DATA: FoodNode[] = []
// // = [
// //   {
// //     id: 1,
// //     name: 'Server',
// //     children: [
// //       {
// //         id: 1,
// //         name: '192.168.0.2',
// //         children: [
// //         {
// //           id: 1,
// //           name: 'Broccoli'
// //         },
// //         {
// //           id: 1,
// //           name: 'Brussels sprouts'
// //         }
// //       ],
// //       },
// //       {
// //         id: 1,
// //         name: '142.168.0.2',
// //         children: [
// //           {
// //             id: 1,
// //             name: 'Pumpkins'
// //           },
// //           {
// //             id: 1,
// //             name: 'Carrots'
// //           }
// //         ],
// //       },
// //     ],
// //   },
// // ];

// /** Flat node with expandable and level information */


// /**
//  * @title Tree with flat nodes
//  */
// @Component({
//   selector: 'app-dynamic-tree-view',
//   templateUrl: 'dynamic-tree-view.component.html',
//   standalone: true,
//   imports: [MatTreeModule, MatButtonModule, MatIconModule],
// })
// export class DynamicTreeViewComponent {
//   private _transformer = (node: TreeNode, level: number) => {
//     return {
//       expandable: true,
//       message: node.message,
//       id: node.id,
//       parentId: node.parentId,
//       input: node.input,
//       title: node.title,
//       messageArabic: node.messageArabic,
//       isDefault: node.isDefault,
//       nodeType: node.nodeType,
//       action: node.action,
//       level: level,
//     };
//   };

//   treeControl = new FlatTreeControl<ExampleFlatNode>(
//     (node) => node.level,
//     (node) => node.expandable
//   );

//   treeFlattener = new MatTreeFlattener(
//     this._transformer,
//     (node) => node.level,
//     (node) => node.expandable,
//     (node) => node.children
//   );

//   dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
//   hasChild = (_: number, node: ExampleFlatNode) => node.expandable;

//   constructor(private _treeViewService: TreeViewService) {

//   }
//   ngOnInit() {

//     this.getServers();
//   }
//   getServers() {
//     this._treeViewService.getServers().subscribe({
//       next: (results) => {
//         const transformedTree = this.transformApiToTree(results.data);
//         this.dataSource.data = transformedTree;
//       },
//       error: (error) => {

//       },
//     });
//   }


//   transformApiToTree(data: TreeViewResponse[]): TreeNode[] {
//     const map = new Map<number, TreeNode>();

//     // Create a map for quick lookup based on node id
//     data.forEach((item) => {
//       const {
//         id,
//         parentId,
//         title,
//         message,
//         messageArabic,
//         input,
//         nodeType,
//         action,
//         isDefault,
//       } = item;
//       const treeNode: TreeNode = {
//         id,
//         title,
//         message,
//         messageArabic,
//         input,
//         parentId,
//         nodeType,
//         action,
//         isDefault,
//       };

//       if (item.hasChild) {
//         treeNode.children = [];
//       }

//       map.set(id, treeNode);

//       if (parentId !== null) {
//         const parent = map.get(parentId);
//         if (parent) {
//           if (!parent.children) {
//             parent.children = [];
//           }
//           parent.children.push(treeNode);
//         }
//       }
//     });

//     // Find the root nodes (nodes without a parent)
//     const result: TreeNode[] = [];
//     map.forEach((node, id) => {
//       const parent = map.get(
//         data.find((item) => item.id === id)?.parentId || 0
//       );
//       if (!parent) {
//         result.push(node);
//       }
//     });

//     return result;
//   }

//   onToggle(node: TreeNode): void {
//     // if (this.treeControl.isExpanded(node) && node.hasChildren && (!node.children || node.children.length === 0)) {
//     //   this.loadChildren(node);
//     // }
//     let p = 10;
//     this.loadChildren(node);
//   }

//   loadChildren(node: TreeNode): void {
//     this._treeViewService.getDatabaseByServerId(node.id).subscribe(children => {
//       node.children = [{
//         id : 801,
//         title: 'AAAA',
//         message: '',
//         messageArabic: '',
//         input: '',
//         parentId: 100,
//         nodeType: 'Database',
//         action: 0,
//         isDefault: false,
//       }];
//       this.dataSource.data = this.dataSource.data; // Refresh the tree
//     });
//   }
//   selectNode(node: any)
//   {
//     let p = 18;
//   }
// }


// interface TreeNode {
//   id: number | 0;
//   parentId: number | 0;
//   title: string | null;
//   message: string | null;
//   messageArabic: string | null;
//   input: string | null;
//   nodeType: string | null;
//   action: number | 0;
//   isDefault: boolean;
//   children?: TreeNode[];
// }

// /** Flat node with expandable and level information */
// interface ExampleFlatNode {
//   expandable: boolean;
//   id: number | 0;
//   parentId: number | 0;
//   title: string | null;
//   message: string | null;
//   messageArabic: string | null;
//   input: string | null;
//   nodeType: string | null;
//   isDefault: boolean;
//   action: number | 0;
//   level: number;
// }

