import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { HttpHeaders } from '@angular/common/http';
import { AuthGuard } from './auth-guard.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject, Observable } from 'rxjs';
import { UserModel } from 'src/app/Models/UserModel';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private apiService: ApiService, private authGuard: AuthGuard, private jwtHelper: JwtHelperService) { 
    
  }

  

  isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    return false;
  }

  signUp(payload: any) {
    return this.apiService.post('account/signup', payload);
  }

  signIn(payload: any) {
    return this.apiService.post('account/signin', payload);
  }

  getUserDetails(){
    var t = this.authGuard.getToken();
    
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${t}`
    });

  const requestOptions = { headers: headers };
    return this.apiService.getUserDetails('user/getuserdetails', requestOptions);
  }

  signOut(){
    localStorage.removeItem("jwt"); 
    return this.apiService.get('account/signout');
  }

}
