import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AdminService } from 'src/app/services/admin/admin.service';
import { NotifyService } from 'src/app/services/notification/notify.service';
import { CategoryEnum } from 'src/app/Models/CategoryEnum';
import { SizeTypeEnum } from 'src/app/Models/SizeTypeEnum';

@Component({
  selector: 'app-most-sell-products',
  templateUrl: './most-sell-products.component.html',
  styleUrls: ['./most-sell-products.component.css']
})
export class MostSellProductsComponent {


  pagination: number = 1;

  allProducts: any;

  allProductsCount: number = 0;

  allCategory = CategoryEnum;

  selectedCategory: number;

  // sorting variables
  chooseCategory = -1;
  selectedSortingOption = 'Default';
  newDate = new Date();
  month : number;
  sortedData: any[];

  constructor(private adminService: AdminService, private notifyService: NotifyService, private router: Router) {
    this.month = this.newDate.getMonth();
  }

  ngOnInit(): void {

    this.fetchProducts();    
  }

  fetchProducts() {
    this.adminService.getMostOrderProducts(this.month + 1).subscribe(
      {
        next: (response: any) => {
          this.allProducts = response.products;
          this.allProductsCount = this.allProducts.length;
          this.sortedData = this.allProducts
          this.applySorting('Qty');
        },
        error: (error: Error) => {
          console.log(error);
          this.notifyService.showError(`Error: ${error.message}`, "Failed");
        }
      }
    );
  }

  sortByDate(date: any) {    
    this.month =  new Date(date).getMonth();
    this.fetchProducts();
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
      
      this.sortedData = this.allProducts.filter(e => e.product.category == val);
    }
    this.allProductsCount = this.sortedData.length;
  }

  applySorting(option1: string, option2 = '') {

    this.selectedSortingOption = option1;
    var option = option1 + option2;

    switch (option) {
      case 'Default':
        this.sortedData = this.allProducts.slice().sort((a, b) => b.quantity - a.quantity);
        break;
      case 'PriceUp':
        this.sortedData = this.allProducts.slice().sort((a, b) => a.product.price - b.product.price);
        break;
      case 'PriceDown':
        this.sortedData = this.allProducts.slice().sort((a, b) => b.product.price - a.product.price);
        break;
      case 'Stock':
        this.sortedData = this.allProducts.slice().sort((a, b) => b.product.stock - a.product.stock);
        break;
      case 'Qty':
        this.sortedData = this.allProducts.slice().sort((a, b) => b.quantity - a.quantity);
        break;
      default:
        break;
    }

    if (this.chooseCategory > -1) this.sortByCategory(this.chooseCategory);

  }

}
