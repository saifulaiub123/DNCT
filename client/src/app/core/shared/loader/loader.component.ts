import { Component, OnInit } from '@angular/core';
import { NgxUiLoaderService } from "ngx-ui-loader";
import { LoaderService } from '../../services/loader.service';
@Component({
  selector: 'app-loading',
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.css']
})
export class LoaderComponent implements OnInit {

  isLoading: boolean = false;
  loadingQueue: any[] = [];

 constructor(
    private ngxService: NgxUiLoaderService,private _loaderService: LoaderService) { }

  ngOnInit(): void {
  }

  subscribeLoading() {
    this._loaderService.showLoading().subscribe(
      (resp) => {
        if (resp !== this.isLoading) {
          this.isLoading = resp;
        }
      }
    );
  }

  subscribeTest() {
    this._loaderService.$loadingQueue.subscribe((data) => {
      this.loadingQueue = data;
    })
  }

  private delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

}
