import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NotifyService } from '../notification/notify.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardAdminService implements CanActivate  {
  constructor(private router:Router, private jwtHelper: JwtHelperService, private notifyService: NotifyService){}
  
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)){      
      let decodedJWT = this.jwtHelper.decodeToken(token);
      var role = decodedJWT['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      if(role != 'Admin'){
        this.router.navigate(["/"]);
        this.notifyService.showWarning("Please sign in to your account as admin.", "");
        return false;
      }

      return true;
    }    
    localStorage.removeItem("jwt"); 
    this.router.navigate(["/signin"]);
    this.notifyService.showWarning("Please sign in to your account as admin.", "");
    return false;
  }
}
