import { Component } from '@angular/core';
import { NotifyService } from 'src/app/services/notification/notify.service';

@Component({
  selector: 'app-news-letter',
  templateUrl: './news-letter.component.html',
  styleUrls: ['./news-letter.component.css']
})
export class NewsLetterComponent {


  constructor(private notifyService: NotifyService){}

  subscribe(email: string){

    if(email != '') this.notifyService.showSuccess("You have successfully susbcribed to our newsletter.", "");
  }

}
