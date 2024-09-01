import { ClickTableInstanceStateModel } from './../../model/state-service/click-table-instance.model';
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import {  map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TreeViewStateService {

  private isTableInstanceClicked = new BehaviorSubject<any>(null);
  public $isTableInstanceClicked = this.isTableInstanceClicked.asObservable();

  public clickTableInstance(id: number, name: string) {
    this.isTableInstanceClicked.next({id : id, name: name});
  }
  public clickTableInstanceReset() {
    this.isTableInstanceClicked.next(null);
  }
}
