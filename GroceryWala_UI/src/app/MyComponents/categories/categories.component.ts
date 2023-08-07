import { Component } from '@angular/core';
import {
  faAppleWhole, faCheese, faBurger, faIceCream,
  faMugHot, faPepperHot, faSoap, faSprayCanSparkles
} from '@fortawesome/free-solid-svg-icons';

import { CategoryEnum } from 'src/app/Models/CategoryEnum';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent {
  fruitIcon = faAppleWhole;
  dairyIcon = faCheese;
  snackIcon = faBurger;
  frozenIcon = faIceCream;
  beveragesIcon = faMugHot;
  condimentsIcon = faPepperHot;
  cleaningIcon = faSoap;
  personalIcon = faSprayCanSparkles;

  allIcons = [this.fruitIcon, this.dairyIcon, this.snackIcon, this.beveragesIcon, this.frozenIcon,  
              this.condimentsIcon, this.cleaningIcon, this.personalIcon];

  allCategory = CategoryEnum;

  constructor() { 
    
  }

  Number(value: any): number {
    return parseInt(value);
  }

}
