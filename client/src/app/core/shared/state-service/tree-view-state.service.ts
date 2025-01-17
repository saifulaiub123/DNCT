import { ClickTableInstanceStateModel } from './../../model/state-service/click-table-instance.model';
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import {  map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TreeViewStateService {


  //Table State
  private isTableNodeClicked = new BehaviorSubject<any>(null);
  public $isTableNodeClicked = this.isTableNodeClicked.asObservable();

  public clickTableNode(id: number, name: string) {
    this.isTableNodeClicked.next({id : id, name: name});
  }
  public clickTableNodeReset() {
    this.isTableNodeClicked.next(null);
  }



  //TableInstance State
  private isTableInstanceClicked = new BehaviorSubject<any>(null);
  public $isTableInstanceClicked = this.isTableInstanceClicked.asObservable();

  public clickTableInstance(id: number, name: string) {
    this.isTableInstanceClicked.next({tableConfigId : id, name: name});
  }
  public clickTableInstanceReset() {
    this.isTableInstanceClicked.next(null);
  }




}
