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
  selector: 'app-view-all-orders',
  templateUrl: './view-all-orders.component.html',
  styleUrls: ['./view-all-orders.component.css']
})
export class ViewAllOrdersComponent implements OnInit {

  rupeeIcon = faIndianRupeeSign;
  starIcon = faStar;
  editIcon = faPenToSquare;
  deleteIcon = faTrashCan;

  pagination: number = 1;

  allOrders: any[] = [];

  selectedSortingOption ; string;

  allOrdersCount = 0;

  sortedData: any[];

  constructor(private adminService: AdminService, private notifyService: NotifyService, private userService: UserService,
    private router: Router) {
      this.selectedSortingOption = 'Default';
  }

  ngOnInit(): void {  
    this.fetchOrders();
    
  }

  fetchOrders() {
    this.adminService.getAllUserOrders().subscribe(
      {
        next: (response: any) => {
         this.allOrders = response.orders;
         this.sortedData = response.orders;
         this.allOrdersCount = this.sortedData.length;
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
    this.fetchOrders();
  }

  // return price after discount upto 2 decimal values
  returnPrice(price: number, discount: number): any {
    var res = price - price * discount / 100;
    if (res % 1 != 0) {
      return res.toFixed(2);
    }
    else return res;
  }


  applySorting(option1: string, option2 = '') {

    this.selectedSortingOption = option1;
    var option = option1 + option2;

    switch (option) {
      case 'Default':
        this.sortedData = this.allOrders;
        break;
      case 'PriceUp':
        this.sortedData = this.sortedData.slice().sort((a, b) => a.finalAmount - b.finalAmount);
          
        break;
      case 'PriceDown':
        this.sortedData = this.sortedData.slice().sort((a, b) => b.finalAmount - a.finalAmount);
        break;
      default:
        break;
    }

  }

}
