// import { Component, OnInit } from '@angular/core';
// import { LoadingService } from './loader.service';
// import { NgxUiLoaderService } from "ngx-ui-loader";
// @Component({
//   selector: 'app-loading',
//   templateUrl: './loading.component.html',
//   styleUrls: ['./loading.component.css']
// })
// export class LoadingComponent implements OnInit {

//   isLoading: boolean = false;
//   loadingQueue: any[] = [];

//  constructor(
//     private ngxService: NgxUiLoaderService,private loadingService: LoadingService) { }

//   ngOnInit(): void {
//   }

//   subscribeLoading() {
//     this.loadingService.showLoading().subscribe(
//       (resp) => {
//         if (resp !== this.isLoading) {
//           this.isLoading = resp;
//         }
//       }
//     );
//   }

//   subscribeTest() {
//     this.loadingService.$loadingQueue.subscribe((data) => {
//       this.loadingQueue = data;
//     })
//   }

//   private delay(ms: number) {
//     return new Promise(resolve => setTimeout(resolve, ms));
//   }

// }
