import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AnimationOptions } from 'ngx-lottie';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-order-confirmed',
  templateUrl: './order-confirmed.component.html',
  styleUrls: ['./order-confirmed.component.css']
})
export class OrderConfirmedComponent implements OnInit {

  options: AnimationOptions = {
    path: '../../../assets/animation/orderConfirm.json',
    loop: false,
  };

  // activated routes
  routeSub: Subscription;

  //url details
  userId: string;
  orderId: string;

  constructor(private route: ActivatedRoute) {
      this.route.paramMap.subscribe(() => {
        this.ngOnInit();
    });

  }

  ngOnInit(): void {
    this.routeSub = this.route.params.subscribe(params => {
      this.userId = params['userId'];
      this.orderId = params['orderId'];
    });
  }

}
