import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { faGift } from '@fortawesome/free-solid-svg-icons';
import { NotifyService } from 'src/app/services/notification/notify.service';
import { ProductsService } from 'src/app/services/product/products.service';

@Component({
  selector: 'app-view-offers',
  templateUrl: './view-offers.component.html',
  styleUrls: ['./view-offers.component.css']
})
export class ViewOffersComponent implements OnInit {
  
  giftIcon = faGift;

  allOffers : any[] = [];

  constructor(private productsService: ProductsService, private notifyService: NotifyService){

  }
  
  ngOnInit(): void {
    this.fetchOffers(); 
  }

  fetchOffers(){
    this.productsService.getOffers().subscribe(
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

}
