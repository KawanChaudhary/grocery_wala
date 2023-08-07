import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faIndianRupee } from '@fortawesome/free-solid-svg-icons';
import { Subscription } from 'rxjs';
import { UserOrderModel } from 'src/app/Models/UserOrderModel';
import { OrderService } from 'src/app/services/myorder/order.service';
import { NotifyService } from 'src/app/services/notification/notify.service';

@Component({
  selector: 'app-view-order',
  templateUrl: './view-order.component.html',
  styleUrls: ['./view-order.component.css']
})
export class ViewOrderComponent implements OnInit {

  // Icon
  rupeeIcon = faIndianRupee;

  // activated routes
  routeSub: Subscription;

  //url details
  userId: string;
  orderId: number;

  // order details

  date = new Date();

  orderDetails : UserOrderModel = new UserOrderModel(0, 0, 0, "", this.date, 0, 0);
  productsOrders : any;

  constructor(private route: ActivatedRoute, private router: Router, private orderService: OrderService,
    private notifyService: NotifyService) {
      this.route.paramMap.subscribe(() => {
        this.ngOnInit();
    });
  }

  ngOnInit(): void {
    this.routeSub = this.route.params.subscribe(params => {
      this.userId = params['userId'];
      this.orderId = params['orderId'];
    });
    this.fetchOrder();
  }

  returnPrice(price: number, discount: number): any {
    var res = price - price * discount / 100;
    if (res % 1 != 0) {
      return res.toFixed(2);
    }
    else return res;
  }

  fetchOrder(){
    this.orderService.getUsersOrder(this.userId, this.orderId).subscribe(
      {
        next: (response: any) => {
          this.orderDetails = response.order;
          this.fetchProductsOrder();
        },
        error: (error: Error) => {
          console.log(error);
          this.notifyService.showError(`Error: ${error.message}`, "Failed");
        }
      }
    ); 
  }

  fetchProductsOrder(){
    this.orderService.getProductOrderDetails(this.orderId).subscribe(
      {
        next: (response: any) => {
          this.productsOrders = response.productOrders;
        },
        error: (error: Error) => {
          console.log(error);
          this.notifyService.showError(`Error: ${error.message}`, "Failed");
        }
      }
    ); 
  }

}
