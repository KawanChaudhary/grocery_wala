import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'GroceryWala_UI';

  currentRoute = null;

  constructor(private router: Router) {
    this.router.events.pipe(filter(event => event instanceof NavigationEnd)
    ).subscribe(event => {
      this.currentRoute = router.url;
    });
  }

  isHomeComponent():boolean { 
    if(this.currentRoute == '/vieworder/:userId/:orderId') return false;
    else return true;
   }
}
