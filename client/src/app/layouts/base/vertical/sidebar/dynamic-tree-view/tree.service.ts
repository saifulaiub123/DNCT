export interface TreeNode {
  id: number;
  name: string;
  children?: TreeNode[];
}


import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { delay } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TreeService {

  private dataChange = new BehaviorSubject<TreeNode[]>([]);

  constructor() {
    this.initialize();
  }

  initialize() {
    // Simulate fetching data from a database or API
    const initialData: TreeNode[] = [
      { id: 1, name: 'Node 1' },
      { id: 2, name: 'Node 2' }
    ];

    this.dataChange.next(initialData);
  }

  getTreeData(): Observable<TreeNode[]> {
    return this.dataChange.asObservable();
  }

  loadChildren(nodeId: number): Observable<TreeNode[]> {
    // Simulate loading children for a specific node from a database or API
    const children: TreeNode[] = [
      { id: 11, name: 'Child 1 of Node ' + nodeId },
      { id: 12, name: 'Child 2 of Node ' + nodeId }
    ];

    // Replace with actual HTTP call to fetch data from API
    return of(children).pipe(delay(500)); // Simulate async delay
  }
}
