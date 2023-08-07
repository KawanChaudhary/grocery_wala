import { Component } from '@angular/core';
import { faInstagram, faTwitter, faFacebook, faPinterest } from '@fortawesome/free-brands-svg-icons';
import { faCaretRight,faPhone, faEnvelope} from '@fortawesome/free-solid-svg-icons';
import { CategoryEnum } from 'src/app/Models/CategoryEnum';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent {

  submitIcon = faCaretRight;
  phoneIcon = faPhone;
  enevelopeIcon = faEnvelope;
  instaIcon = faInstagram;
  twitterIcon = faTwitter;
  facebookIcon = faFacebook;
  pinterestIcon = faPinterest;

  allCategory = CategoryEnum;

}
