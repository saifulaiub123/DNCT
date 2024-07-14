// auth.guard.ts

import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, ActivatedRoute } from '@angular/router';
import { TokenStorageService } from './core/services/token-storage.service';



@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private authService: TokenStorageService,
    private route: ActivatedRoute,
    // public _signinService: SigninAuthenticationService,
    private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    if (this.authService.getUser()) {
      return true;
    } else {

      this.route.url.subscribe(e => { console.log(e) })

      console.log(next);
      console.log(state.url);
      // this._signinService.setRedirectURl(state.url)

      this.router.navigate(['/authentication/login']);
      return false;
    }
  }
}
