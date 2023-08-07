import { Component } from '@angular/core';
import { faStar, faCameraRetro, faPlane, faAppleWhole, faThumbsUp, faWarehouse } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-why-choose-us',
  templateUrl: './why-choose-us.component.html',
  styleUrls: ['./why-choose-us.component.css']
})
export class WhyChooseUsComponent {

  /// Icon
  starIcon = faStar;
  awardSign = faCameraRetro;
  planeSign = faPlane;
  peopleIcon = faAppleWhole;
  likeIcon = faThumbsUp;
  wareHouseIcon = faWarehouse;

}
