import {FlatTreeControl} from '@angular/cdk/tree';
import {Component} from '@angular/core';
import {MatTreeFlatDataSource, MatTreeFlattener, MatTreeModule} from '@angular/material/tree';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';

/**
 * Food data with nested structure.
 * Each node has a name and an optional list of children.
 */
interface FoodNode {
  id: number;
  // parentId: number;
  name: string;
  type: string;
  children?: FoodNode[];
}

const TREE_DATA: FoodNode[] = []
// = [
//   {
//     id: 1,
//     name: 'Server',
//     children: [
//       {
//         id: 1,
//         name: '192.168.0.2',
//         children: [
//         {
//           id: 1,
//           name: 'Broccoli'
//         },
//         {
//           id: 1,
//           name: 'Brussels sprouts'
//         }
//       ],
//       },
//       {
//         id: 1,
//         name: '142.168.0.2',
//         children: [
//           {
//             id: 1,
//             name: 'Pumpkins'
//           },
//           {
//             id: 1,
//             name: 'Carrots'
//           }
//         ],
//       },
//     ],
//   },
// ];

/** Flat node with expandable and level information */
interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
}

/**
 * @title Tree with flat nodes
 */
@Component({
  selector: 'app-dynamic-tree-view',
  templateUrl: 'dynamic-tree-view.component.html',
  standalone: true,
  imports: [MatTreeModule, MatButtonModule, MatIconModule],
})
export class DynamicTreeViewComponent {
  private _transformer = (node: FoodNode, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
      level: level,
    };
  };

  treeControl = new FlatTreeControl<ExampleFlatNode>(
    node => node.level,
    node => node.expandable,
  );

  treeFlattener = new MatTreeFlattener(
    this._transformer,
    node => node.level,
    node => node.expandable,
    node => node.children,
  );

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  constructor() {
    this.dataSource.data = TREE_DATA;
  }

  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;

  selectNode(node: any)
  {
    let p = 18;
  }
}
