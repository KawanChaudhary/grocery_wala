import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NotifyService } from '../notification/notify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate  {
  constructor(private router:Router, private jwtHelper: JwtHelperService, private notifyService: NotifyService){}
  
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)){            
      return true;
    }    
    localStorage.removeItem("jwt"); 
    this.router.navigate(["/signin"]);
    this.notifyService.showWarning("Please sign in to your account.", "");
    return false;
  }

  getToken(){
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)){
      return token;
    }
    return null;
  }
}
