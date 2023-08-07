import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { faSearch, faCartArrowDown, faRightFromBracket } from '@fortawesome/free-solid-svg-icons';
import { CategoryEnum } from 'src/app/Models/CategoryEnum';
import { UserModel } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/services/account/user.service';
import { CartService } from 'src/app/services/global/cart.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  // Icons
  searchIcon = faSearch;
  cartIcon = faCartArrowDown;
  signOutIcon = faRightFromBracket;

  allCategory = CategoryEnum;

  anyItem: number = 0;

  // If logged then user details ::
  user: UserModel = new UserModel('', '', '', '', '', false);

  isLogged = false;

  constructor(private userService: UserService, public cartService: CartService,) { }

  ngOnInit(): void {

    if (this.userService.isUserAuthenticated()) {

      this.userService.getUserDetails().subscribe(
        {
          next: (response: any) => {
            if(response.response){
              this.user = response.user;
            }
          },
          error: (error: HttpErrorResponse) => {
            console.log(error.error);
          }
        }
      );
    }
    
  }

  anyUserAvailable(): boolean {
    return this.userService.isUserAuthenticated();
  }

  Number(value: any): number {
    return parseInt(value);
  }

  signOut(){
    this.userService.signOut();
  }


}
