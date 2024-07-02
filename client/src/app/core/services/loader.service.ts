import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import {  map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  private loadingQueue = new BehaviorSubject<string[]>([]);
  public $loadingQueue = this.loadingQueue.asObservable();

  public addToLoading(action: string) {
    let list = this.loadingQueue.getValue();
    var ind = list.findIndex(l => l === action);
    if (ind === -1) {
      list.push(action);
      this.loadingQueue.next(list);
    }

  }

  public removeFromLoading(action: string) {
    let list = this.loadingQueue.getValue();
    var ind = list.findIndex(l => l === action);
    if (ind !== -1) {
      list.splice(ind, 1);
      this.loadingQueue.next(list);
    }
  }

  public showLoading(): Observable<boolean> {
    return this.$loadingQueue.pipe(map((resp)=>{
      return resp.length > 0;
    }));
  }

}
