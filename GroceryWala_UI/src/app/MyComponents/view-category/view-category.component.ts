import { HttpErrorResponse } from '@angular/common/http';
import { Component, HostBinding, OnChanges, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { faIndianRupeeSign, faStar } from '@fortawesome/free-solid-svg-icons';
import { Subscription, filter } from 'rxjs';
import { CartModel } from 'src/app/Models/CartModel';
import { CategoryEnum } from 'src/app/Models/CategoryEnum';
import { SizeTypeEnum } from 'src/app/Models/SizeTypeEnum';
import { UserModel } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/services/account/user.service';
import { CartService } from 'src/app/services/global/cart.service';
import { NotifyService } from 'src/app/services/notification/notify.service';
import { ProductsService } from 'src/app/services/product/products.service';

@Component({
  selector: 'app-view-category',
  templateUrl: './view-category.component.html',
  styleUrls: ['./view-category.component.css']
})
export class ViewCategoryComponent implements OnInit, OnDestroy {


  // Icons
  rupeeIcon = faIndianRupeeSign;
  starIcon = faStar;

  // add to cart variable
  cartItem = new CartModel('', '', null);
  user: UserModel = new UserModel('', '', '', '', '', false);

  // Cart ProductInfo variables
  cartItems: any[] = [];


  // category items variables:

  category: any = 0;

  pagination: number = 1;

  allProducts: any;

  allProductsCount: number = 0;

  private routeSub: Subscription;


  // Constructor
  constructor(private productService: ProductsService, private notifyService: NotifyService,
    private route: ActivatedRoute, private cartService: CartService, private userService: UserService,
    private router: Router) {

    this.route.paramMap.subscribe(() => {
      this.ngOnInit();
    });

  }

  // Start on initialization
  ngOnInit(): void {
    this.routeSub = this.route.params.subscribe(params => {
      this.category = params['category'];
      this.fetchProducts();
    });
    this.fetchCategoryImage(this.category);
    this.fetchUserDetails();
    this.fetchProducts();
  }

  // Fetch details of logged user
  fetchUserDetails(): void {
    if (this.userService.isUserAuthenticated()) {
      this.userService.getUserDetails().subscribe(
        {
          next: (response: any) => {

            if (response.response) {
              this.user = response.user;
              this.fetchCartProducts();
              // console.log(this.user);
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

  // destroy the subscription (routeSub)

  ngOnDestroy(): void {
    if (this.routeSub) {
      this.routeSub.unsubscribe();
    }
  }

  // fetch all products of a category
  fetchProducts() {
    this.productService.getProductByCategory(this.category).subscribe(
      {
        next: (response: any) => {
          this.allProducts = response.response;
          this.allProductsCount = this.allProducts.length;
        },
        error: (error: Error) => {
          console.log(error);
          this.notifyService.showError(`Error: ${error.message}`, "Failed");
        }
      }
    );
  }

  // render products for pagination (1 page max 12 products per category)
  renderPage(event: number) {
    this.pagination = event;
    this.fetchProducts();
  }

  // it return category details from categoryEnum
  returnCategoryType(val: number): string {
    return CategoryEnum[val];
  }

  // it return product size (kg/g/L/ml)
  returnSizeType(val: number): string {
    return SizeTypeEnum[val];
  }

  //return category image for a particular category
  fetchCategoryImage(val: number): void {
    if (val == 0) {
      document.documentElement.style.setProperty('--img', "url(./../../../assets/category/fruits&vegetables.jpg)");
    }
    else if (val == 1) {
      document.documentElement.style.setProperty('--img', "url(./../../../assets/category/dairy&bakery.png)");
    }
    else if (val == 2) {
      document.documentElement.style.setProperty('--img', "url(./../../../assets/category/snacks.png)");
    }
    else if (val == 4) {
      document.documentElement.style.setProperty('--img', "url(./../../../assets/category/frozenfood.jpg)");
    }
    else if (val == 3) {
      document.documentElement.style.setProperty('--img', "url(./../../../assets/category/beverages.jpg)");
    }
    else if (val == 5) {
      document.documentElement.style.setProperty('--img', "url(./../../../assets/category/condiments.jpg)");
    }
    else if (val == 6) {
      document.documentElement.style.setProperty('--img', "url(./../../../assets/category/cleaningsupplies.jpg)");
    }
    else if (val == 7) {
      document.documentElement.style.setProperty('--img', "url(./../../../assets/category/personalcare.jpg)");
    }
    else {
      document.documentElement.style.setProperty('--img', "url(./../../../assets/category/allproducts2.jpg)");
    }
  }

  // return price after discount upto 2 decimal values
  returnPrice(price: number, discount: number): string {
    var res = price - price * discount / 100;
    if (res % 1 != 0) {
      return res.toFixed(2);
    }
    else return res.toString();
  }

  // add to cart fucntionality from category component
  addToCart(product: any, index: number) {
    var idx = index + (this.pagination - 1) * 12;
    if (this.userService.isUserAuthenticated()) {

      // console.log(product);

      var productId = product.id;
      var userId = this.user.id;
      var quantity = 1;
      this.cartItem = new CartModel(userId, productId, quantity);
      this.cartService.AddToCart(this.cartItem).subscribe(
        {
          next: (response: any) => {
            if (response.response == false) {
              this.notifyService.showError(`Somehting wen wrong. Please try again.`, "");
            }
            this.allProducts[idx].isInCart = true;
            this.cartService.onAddToCartInvokeCartIcon(this.user.id)
          },
          error: (error: Error) => {
            console.log(error);
            this.notifyService.showError(`Error: ${error.message}`, "Failed");
          }
        }
      );

    }
    else {
      this.notifyService.showInfo(`To add items to your basket, you must first sign in.`, "");
      this.router.navigate(['/signin'])
    }
  }

  // fetch all cart items from the server
  fetchCartProducts() {
    if (this.userService.isUserAuthenticated()) {
      this.cartService.GetCartItems(this.user.id).subscribe(
        {
          next: (response: any) => {
            if (response.response) {
              this.cartItems = response.items;

              for (let i = 0; i < this.cartItems.length; i++) {
                var item = this.cartItems[i];
                for (var product of this.allProducts) {
                  if (product.details.id == item.details.id) {
                    product.isInCart = true;
                  }

                }
              }
              
            }
            else {
              this.cartItems = [];
            }
          },
          error: (error: HttpErrorResponse) => {
            console.log(error);
            this.notifyService.showError(`Error: ${error.error}`, "Failed");
          }
        }
      );
    }
  }

  checkItemInCart(productId: any): boolean {
    if (this.userService.isUserAuthenticated() && this.cartItems.length > 0) {
      for (let item of this.cartItems) {
        if (item.item.productId == productId) {
          return true;
        }
      }
    }
    return false;
  }
}
