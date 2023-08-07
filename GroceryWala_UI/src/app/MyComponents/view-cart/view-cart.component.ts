import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faIndianRupeeSign, faPlus, faMinus, faShoppingBasket, faTruck, } from '@fortawesome/free-solid-svg-icons';
import { UserModel } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/services/account/user.service';
import { CartService } from 'src/app/services/global/cart.service';
import { NotifyService } from 'src/app/services/notification/notify.service';
import { ProductsService } from 'src/app/services/product/products.service';
import { CategoryEnum } from 'src/app/Models/CategoryEnum';
import { SizeTypeEnum } from 'src/app/Models/SizeTypeEnum';
import { CartModel } from 'src/app/Models/CartModel';
import { UserOrderModel } from 'src/app/Models/UserOrderModel';
import { OrderService } from 'src/app/services/myorder/order.service';
import { OrderModel } from 'src/app/Models/OrderModel';
import { AnimationOptions } from 'ngx-lottie';
import { AdminService } from 'src/app/services/admin/admin.service';

@Component({
  selector: 'app-view-cart',
  templateUrl: './view-cart.component.html',
  styleUrls: ['./view-cart.component.css']
})
export class ViewCartComponent implements OnInit {


  // Icons
  rupeeIcon = faIndianRupeeSign;
  minusIcon = faMinus;
  plusIcon = faPlus;
  shoppingBasketIcon = faShoppingBasket;
  truckIcon = faTruck;

  // User Detials variables
  user: UserModel = new UserModel('', '', '', '', '', false);

  // Cart ProductInfo variables
  cartItems: any[] = [];

  //allOffers
  allOffers: any[] = [];

  // final amount variable
  order = new UserOrderModel(0, 0, 0, "");
  coupon: string = '';

  constructor(private productService: ProductsService, private notifyService: NotifyService,
    private route: ActivatedRoute, private cartService: CartService, private userService: UserService,
    private orderService: OrderService, private router: Router, private adminService: AdminService) {

  }

  ngOnInit(): void {
    this.fetchUserDetails();
    this.fetchOffers();
  }

  fetchUserDetails() {
    if (this.userService.isUserAuthenticated()) {

      this.userService.getUserDetails().subscribe(
        {
          next: (response: any) => {
            if (response.response) {
              this.user = response.user;

              // get all cart products
              this.fetchCartProducts();

            }
          },
          error: (error: HttpErrorResponse) => {
            console.log(error.error);
          }
        }
      );
    }
  }

  fetchCartProducts() {
    this.cartService.GetCartItems(this.user.id).subscribe(
      {
        next: (response: any) => {
          if (response.response) {
            this.cartItems = response.items;
            this.finalizeAmount();
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

  returnCategoryType(val: number): string {
    return CategoryEnum[val];
  }

  returnSizeType(val: number): string {
    return SizeTypeEnum[val];
  }

  returnPrice(price: number, discount: number): any {
    var res = price - price * discount / 100;
    if (res % 1 != 0) {
      return res.toFixed(2);
    }
    else return res;
  }

  changeQuantity(item: CartModel) {
    if (this.userService.isUserAuthenticated()) {
      this.cartService.UpdateCartItem(item).subscribe(
        {
          next: (response: any) => {
            if (response.response == false) {
              this.notifyService.showError(`Somehting went wrong. Please try again.`, "");
            }
            this.finalizeAmount();
          },
          error: (error: HttpErrorResponse) => {
            console.log(error);
            this.notifyService.showError(`Error: ${error.error}`, "Failed");
          }
        }
      );
    }
  }

  deleteItemFromCart(id: any) {
    if (this.userService.isUserAuthenticated()) {
      this.cartService.DeleteItem(id).subscribe(
        {
          next: (response: any) => {
            if (response.response == false) {
              this.notifyService.showError(`Somehting went wrong. Please try again.`, "");
              this.finalizeAmount();
            }
            else {
              this.cartService.onAddToCartInvokeCartIcon(this.user.id);
              this.fetchCartProducts();
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

  fetchOffers() {
    this.productService.getOffers().subscribe(
      {
        next: (response: any) => {
          this.allOffers = response.offers;
        },
        error: (error: HttpErrorResponse) => {
          this.notifyService.showError(error.error, "");
          console.log(error);
        }
      }
    );
  }

  finalizeAmount() {
    this.order = new UserOrderModel(0, 0, 0, "");

    if (this.cartItems.length > 0) {

      for (let item of this.cartItems) {
        this.order.totalMRP += item.details.price * item.item.quantity;
        this.order.discountOnMRP += (item.details.price * item.details.discount / 100 * item.item.quantity);
      }
      this.order.finalAmount = this.order.totalMRP - this.order.discountOnMRP + 100;
    }

    if (this.coupon != '') {
      this.applyCoupon();
    }
  }

  applyCoupon() {
    if (this.coupon == this.order.couponCode) {
      this.notifyService.showWarning(`Coupon already applied`, "");
      return;
    }


    for (let offer of this.allOffers) {
      if (offer.offerCode == this.coupon) {
        if (this.order.finalAmount >= offer.price) {

          // if the user has already paid the coupon
          if (this.order.couponCode != '') {
            this.order.finalAmount += this.order.extraDiscount;
          }

          this.order.finalAmount -= offer.offPrice;
          this.order.couponCode = offer.offerCode;
          this.order.extraDiscount = offer.offPrice;
          this.notifyService.showSuccess(`Extra discount of Rs ${offer.offPrice}`, "Coupon applied");
          return;
        }
        else {
          this.notifyService.showWarning(`${offer.description}`, "Coupon not applied");
          return;
        }
      }
    }
    this.notifyService.showError(`Invalid Coupon Code`, "");
  }

  makePurchase() {
    // console.log(this.order);
    // console.log(this.cartItems);

    if (this.userService.isUserAuthenticated()) {
      this.addOrderDetails();
    }
    else {
      this.notifyService.showInfo(`To add items to your basket, you must first sign in.`, "");
      this.router.navigate(['/signin'])
    }
  }

  private addOrderDetails() {
    this.order.userId = this.user.id;
    this.orderService.addUserOrder(this.order).subscribe(
      {
        next: (response: any) => {
          this.order.orderId = response.order.orderId;
          this.addProductOrders();
        },
        error: (error: HttpErrorResponse) => {
          this.notifyService.showError(error.error, "");
          console.log(error);
        }
      }
    );
  }

  private addProductOrders() {

    for (let item of this.cartItems) {

      // ading values to orderModel.order
      var orderModel = new OrderModel(this.order.orderId, this.user.id, item.details.name, item.details.id,
        item.item.quantity, item.details.price, item.details.discount);

      this.orderService.addProductOrder(orderModel).subscribe(
        {
          next: (response: any) => {

            this.router.navigate([`/orderconfirm/${this.user.id}/${this.order.orderId}`])
            this.deleteItemFromCart(item.item.id);
            item.details.stock = item.details.stock - item.item.quantity;
            this.UpdateProduct(item.details)
          },
          error: (error: HttpErrorResponse) => {
            this.notifyService.showError(error.error, "");
            console.log(error);
          }
        }
      );
    }
  }
  private UpdateProduct(product: any){
    this.adminService.updateProduct(product).subscribe(
      {
        next: (response: any) => {
        },
        error: (error: HttpErrorResponse) => {
          this.notifyService.showError(error.error, "");
          console.log(error);
        }
      }
    );
  }

}
