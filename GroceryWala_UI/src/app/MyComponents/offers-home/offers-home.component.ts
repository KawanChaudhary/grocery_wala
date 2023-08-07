import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { faGift } from '@fortawesome/free-solid-svg-icons';
import { ProductsService } from 'src/app/services/product/products.service';

@Component({
  selector: 'app-offers-home',
  templateUrl: './offers-home.component.html',
  styleUrls: ['./offers-home.component.css']
})
export class OffersHomeComponent implements OnInit {
  
  giftIcon = faGift;

  allOffers : any[] = [];

  constructor(private productsService: ProductsService){

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
          console.log(error);
        }
      }
    );
  }

}
