import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CategoryEnum } from 'src/app/Models/CategoryEnum';

@Component({
  selector: 'app-category-carousel',
  templateUrl: './category-carousel.component.html',
  styleUrls: ['./category-carousel.component.css']
})
export class CategoryCarouselComponent {

  allCategory = CategoryEnum;

  off = [24, 25, 42, 36, 37, 30, 50, 40]

  constructor( private cdref: ChangeDetectorRef ) {
    
  }  

  //return category image for a particular category
  fetchCategoryImage(val: any): void {
    if (val == 0) {
      document.documentElement.style.setProperty('--img0', "url(./../../../assets/category/fruits&vegetables.jpg)");
    }
    else if (val == 1) {
      document.documentElement.style.setProperty('--img1', "url(./../../../assets/category/dairy&bakery.png)");
    }
    else if (val == 2) {
      document.documentElement.style.setProperty('--img2', "url(./../../../assets/category/snacks.png)");
    }
    else if (val == 4) {
      document.documentElement.style.setProperty('--img3', "url(./../../../assets/category/frozenfood.jpg)");
    }
    else if (val == 3) {
      document.documentElement.style.setProperty('--img4', "url(./../../../assets/category/beverages.jpg)");
    }
    else if (val == 5) {
      document.documentElement.style.setProperty('--img5', "url(./../../../assets/category/condiments.jpg)");
    }
    else if (val == 6) {
      document.documentElement.style.setProperty('--img6', "url(./../../../assets/category/cleaningsupplies.jpg)");
    }
    else if (val == 7) {
      document.documentElement.style.setProperty('--img7', "url(./../../../assets/category/personalcare.jpg)");
    }
    else {
      document.documentElement.style.setProperty('--img0', "url(./../../../assets/category/allproducts2.jpg)");
    }
  } 

}
