import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { faIndianRupeeSign, faStar, faPenToSquare, faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { CategoryEnum } from 'src/app/Models/CategoryEnum';
import { SizeTypeEnum } from 'src/app/Models/SizeTypeEnum';
import { UserModel } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/services/account/user.service';
import { AdminService } from 'src/app/services/admin/admin.service';
import { NotifyService } from 'src/app/services/notification/notify.service';

@Component({
  selector: 'app-view-all-products',
  templateUrl: './view-all-products.component.html',
  styleUrls: ['./view-all-products.component.css']
})
export class ViewAllProductsComponent implements OnInit {

  rupeeIcon = faIndianRupeeSign;
  starIcon = faStar;
  editIcon = faPenToSquare;
  deleteIcon = faTrashCan;

  // If logged then user details ::
  user: UserModel = new UserModel('', '', '', '', '', false);

  pagination: number = 1;

  allProducts: any;

  allProductsCount: number = 0;

  allCategory = CategoryEnum;

  selectedCategory: number;

  // sorting variables
  chooseCategory = -1;
  selectedSortingOption = 'Default';

  sortedData: any[];

  constructor(private adminService: AdminService, private notifyService: NotifyService, private userService: UserService,
    private router: Router) {
  }

  ngOnInit(): void {

   this.fetchuserDetails();
    
    
  }

  fetchuserDetails(){
    if (this.userService.isUserAuthenticated()) {      
      this.userService.getUserDetails().subscribe(
        {
        next: (response: any) => {
          if(response.response){
            this.user = response.user;
          }
          this.isAccessible();
        },
        error: (error: HttpErrorResponse) => {
          console.log(error.error);
        }
      }
      );
    }
    else{
      this.notifyService.showWarning(`You must first sign in as admin.`, "");
      this.router.navigate(['/signin'])
    }
  }

  isAccessible()  {
    if(this.user.isAdmin){
      this.fetchProducts();
    }
    else{
      this.notifyService.showWarning(`You must first sign in as admin.`, "");
      this.router.navigate(['/signin'])
    }
  }

  fetchProducts() {
    this.adminService.getAllProducts().subscribe(
      {
        next: (response: any) => {
          this.allProducts = response.response;
          this.allProductsCount = this.allProducts.length;
          this.sortedData = this.allProducts
        },
        error: (error: Error) => {
          console.log(error);
          this.notifyService.showError(`Error: ${error.message}`, "Failed");
        }
      }
    );
  }

  renderPage(event: number) {
    this.pagination = event;
    this.fetchProducts();
  }

  returnCategoryType(val: number): string {
    return CategoryEnum[val];
  }

  returnSizeType(val: number): string {
    return SizeTypeEnum[val];
  }

  // return price after discount upto 2 decimal values
  returnPrice(price: number, discount: number): number {
    return price - price * discount / 100;
  }

  sortByCategory(val: any) {
    this.chooseCategory = val;
    if (val < 0) {
      this.applySorting(this.selectedSortingOption);
    }
    else {
      this.sortedData = this.sortedData.filter(e => e.details.category == val);
    }
    this.allProductsCount = this.sortedData.length;
  }

  applySorting(option1: string, option2 = '') {

    this.selectedSortingOption = option1;
    var option = option1 + option2;

    switch (option) {
      case 'Default':
        this.sortedData = this.allProducts;
        break;
      case 'PriceUp':
        this.sortedData = this.allProducts.slice().sort((a, b) =>
          this.returnPrice(a.details.price, a.details.discount) - this.returnPrice(b.details.price, b.details.discount));
        break;
      case 'PriceDown':
        this.sortedData = this.allProducts.slice().sort((a, b) =>
          this.returnPrice(b.details.price, b.details.discount) - this.returnPrice(a.details.price, a.details.discount));
        break;
      case 'Discount':
        this.sortedData = this.allProducts.slice().sort((a, b) => b.details.discount - a.details.discount);
        break;
      case 'Stock':
        this.sortedData = this.allProducts.slice().sort((a, b) => b.details.stock - a.details.stock);
        break;
      default:
        break;
    }

    if(this.chooseCategory > -1) this.sortByCategory(this.chooseCategory);

  }

  deleteProductById(id: number, event:Event){
    this.adminService.deleteProductById(id).subscribe(
      {
        next: (response: any) => {
          this.fetchProducts();
        },
        error: (error: Error) => {
          console.log(error);
          this.notifyService.showError(`Error: ${error.message}`, "Failed");
        }
      }
    );

    event.stopPropagation();
    event.preventDefault();
    
  }

}
