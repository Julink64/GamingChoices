import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { ApplicationPaths, QueryParameterNames } from 'src/api-authorization/api-authorization.constants';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Injectable({
  providedIn: 'root'
})
export class RedirectGMGuard implements CanActivate {
  constructor(private authorize: AuthorizeService, private router: Router) {
  }
  canActivate(
    _next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
       this.authorize.isAuthenticated().subscribe
        (isAuthenticated =>
          {
            if (isAuthenticated) {
            return this.router.navigate(['gaming-mood']) ;
          }});
          return true;
  }

}
