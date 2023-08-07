import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { faIndianRupee, faTruckMoving } from '@fortawesome/free-solid-svg-icons';
import { Subscription } from 'rxjs';
import { OrderService } from 'src/app/services/myorder/order.service';
import { NotifyService } from 'src/app/services/notification/notify.service';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrls: ['./my-orders.component.css']
})
export class MyOrdersComponent implements OnInit {
  
  rupeeIcon = faIndianRupee;
  truckIcon = faTruckMoving;


  // activated routes
  routeSub: Subscription;

  //url details
  userId: string;

  // order details
  date = new Date();

  allOrders: any[] = [];

  selectedSortingOption ; string;

  allOrdersCount = 0;

  sortedData: any[];

  constructor(private route: ActivatedRoute,  private orderService: OrderService, private notifyService: NotifyService) {
      this.route.paramMap.subscribe(() => {
        this.ngOnInit();
    });
    this.selectedSortingOption = 'Default';
  }

  ngOnInit(): void {
    this.routeSub = this.route.params.subscribe(params => {
      this.userId = params['userId'];
    });
    this.fetchOrders();
  }

  returnPrice(price: number, discount: number): any {
    var res = price - price * discount / 100;
    if (res % 1 != 0) {
      return res.toFixed(2);
    }
    else return res;
  }

  fetchOrders(){
    this.orderService.getAllusersOrder(this.userId).subscribe(
      {
        next: (response: any) => {
          this.sortedData = response.orders;
          this.allOrders = response.orders;
        },
        error: (error: Error) => {
          console.log(error);
          this.notifyService.showError(`Error: ${error.message}`, "Failed");
        }
      }
    ); 
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
