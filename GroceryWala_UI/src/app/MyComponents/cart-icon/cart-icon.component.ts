import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { faCartPlus } from '@fortawesome/free-solid-svg-icons';
import { UserModel } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/services/account/user.service';
import { CartService } from 'src/app/services/global/cart.service';
import { NotifyService } from 'src/app/services/notification/notify.service';

@Component({
  selector: 'app-cart-icon',
  templateUrl: './cart-icon.component.html',
  styleUrls: ['./cart-icon.component.css']
})
export class CartIconComponent implements OnInit {

  // cartIcon
  cartIcon = faCartPlus;

  anyItem: number = 0;

  user: UserModel = new UserModel('', '', '', '', '', false);

  constructor(public cartService: CartService, private userService: UserService, private notifyService: NotifyService) {
  }
  ngOnInit(): void {
    if (this.cartService.subsVar==undefined) {    
      this.cartService.subsVar = this.cartService.    
      invokeCartIcon.subscribe((userId:string) => {    
        this.itemInCart(userId);    
      });    
    }
    this.fetchUserDetails();
  }

  fetchUserDetails(): void {
    if (this.userService.isUserAuthenticated()) {
      this.userService.getUserDetails().subscribe(
        {
          next: (response: any) => {

            if (response.response) {
              this.user = response.user;
              // console.log(this.user);
              this.cartService.onAddToCartInvokeCartIcon(this.user.id);
            }
          },
          error: (error: Error) => {
            console.log(error);
            this.notifyService.showError(`Error: ${error.message}`, "Failed");
          }
        }
      );
    }
  }


  itemInCart(userId:string) {
    this.cartService.GetCartItems(userId).subscribe(
      {
        next: (response: any) => {
          if(response.response){
            this.anyItem = response.items.length;
          }
          else{
            this.anyItem = 0;
          }
        },
        error: (error: HttpErrorResponse) => {
          console.log(error.error);
        }
      }
    );
  }

}
