import { HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, HostBinding, HostListener, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faIndianRupeeSign, faStar, faPlus, faMinus, faShoppingBasket } from '@fortawesome/free-solid-svg-icons';
import { Subscription } from 'rxjs';
import { CartModel } from 'src/app/Models/CartModel';
import { CategoryEnum } from 'src/app/Models/CategoryEnum';
import { SizeTypeEnum } from 'src/app/Models/SizeTypeEnum';
import { UserModel } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/services/account/user.service';
import { CartService } from 'src/app/services/global/cart.service';
import { NotifyService } from 'src/app/services/notification/notify.service';
import { ProductsService } from 'src/app/services/product/products.service';
@Component({
  selector: 'app-view-product',
  templateUrl: './view-product.component.html',
  styleUrls: ['./view-product.component.css']
})
export class ViewProductComponent implements OnInit, OnDestroy {

  //Icon
  starIcon = faStar;
  rupeeIcon = faIndianRupeeSign;
  minusIcon = faMinus;
  plusIcon = faPlus;
  shoppingBasketIcon = faShoppingBasket;

  // activated routes
  routeSub: Subscription;

  //products variables
  productId: any = 0;
  product = {
    id: 0,
    name: "Apples",
    price: 100,
    description: "Apples",
    quantity: 1,
    category: 0,
    stock: 10,
    discount: 0,
    sizeType: SizeTypeEnum.kg,
    rating: 0,
    totalRatings: 0,
    otherDetails: "",
    reviewCount: 0,
  }
  images: any[] = [{
    id: 1,
    productId: '1',
    imageAddress: 'http://127.0.0.1:8080/images/8\\266577-2_4-kissan-mixed-fruit-jam.jpg'
  }
  ];

  // no. of items to buy
  totalItems = 1;

  // image index to show
  currentImageIndex: number = 0;

  // which nav pill activate
  active = 1;

  // product rating index
  rating  = 3.8;
  
  // similar items list item
  similarItems: any[] = [];

  // Cart
  cartItem = new CartModel('', '', 1);
  isInCart = false;

  // User Details
  user: UserModel = new UserModel('', '', '', '', '', false);


  constructor(private productService: ProductsService, private notifyService: NotifyService,
    private route: ActivatedRoute, private cartService: CartService, private userService: UserService, 
    private router: Router) {

      this.route.paramMap.subscribe(() => {
        this.ngOnInit();
    });

  }

  ngOnInit(): void {
    this.routeSub = this.route.params.subscribe(params => {
      this.productId = params['productid'];
    });
    this.fetchProductDetails();
    this.fetchUserDetails();
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe();
  }

  //fetchUserDetails()

  // Fetch details of logged user
  fetchUserDetails(): void {
    if (this.userService.isUserAuthenticated()) {
      this.userService.getUserDetails().subscribe(
        {
          next: (response: any) => {

            if (response.response) {
              this.user = response.user;
              // console.log(this.user);
              this.fetchCartProducts();
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

  fetchProductDetails() {
    this.productService.getProductById(this.productId).subscribe(
      {
        next: (response: any) => {
          this.product = response.product;
          this.images = response.images;

          this.fetchSimilarItems();
        },
        error: (error: Error) => {
          console.log(error);
          this.notifyService.showError(`Error: ${error.message}`, "Failed");
        }
      }
    );    

  }

  fetchSimilarItems(){
    this.productService.getProductByCategory(this.product.category).subscribe(
      {
        next: (response: any) => {     
          this.similarItems = [];
          if (response.response.length > 3) {
            for(let item of response.response){
              if(item.details.id != this.product.id){
                this.similarItems.push(item);
              }
              if(this.similarItems.length == 3) break;
            }
          }
          else {
            this.similarItems = response.response;
          }
        },
        error: (error: Error) => {
          console.log(error);
          this.notifyService.showError(`Error: ${error.message}`, "Failed");
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

  changeImage(index: number): void {
    this.currentImageIndex = index;
  }

  changeQuantity(item: any){
    console.log(item);
    if(this.userService.isUserAuthenticated() && this.isInCart) {
      this.cartService.UpdateCartItem(item).subscribe(
        {
        next: (response: any) => {          
          if(response.response == false){
            this.notifyService.showError(`Somehting went wrong. Please try again.`, "");
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

  ariaValueText(current: number, max: number) {
		return `${current} out of ${max} hearts`;
	}

  addToCart(product: any) {
    console.log(product);
    if (this.userService.isUserAuthenticated()) {


      var productId = product.id;
      var userId = this.user.id;
      var quantity = this.cartItem.quantity;
      this.cartItem = new CartModel(userId, productId, quantity);
      this.cartService.AddToCart(this.cartItem).subscribe(
        {
          next: (response: any) => {
            if (response.response == false) {
              this.notifyService.showError(`Somehting went wrong. Please try again.`, "");
            }
            this.cartService.onAddToCartInvokeCartIcon(this.user.id)
            this.cartItem.id = response.itemId;
            this.isInCart = true;
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
              var cartItems = response.items;
              
              
              for(var item of cartItems) {
                
                if(item.details.id == this.product.id){
                  this.isInCart = true;
                  this.cartItem = item.item;
                  break;
                }
              }
              
            }
            else {
              this.isInCart = false;
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

  returnRating(rating:number, totalRatings:number):number{
    if(rating == 0 || totalRatings == 0)
    return 5;
    return rating/totalRatings;
  }

}
