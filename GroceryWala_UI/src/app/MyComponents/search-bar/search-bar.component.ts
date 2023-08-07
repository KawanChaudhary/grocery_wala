import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, OperatorFunction, debounceTime, distinctUntilChanged, map } from 'rxjs';
import { AdminService } from 'src/app/services/admin/admin.service';
import { NotifyService } from 'src/app/services/notification/notify.service';



@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

  public model: any;

  allProducts: any[] = [];

  constructor(private adminService: AdminService, private notifyService: NotifyService, private router: Router) { }

  ngOnInit(): void {
    this.fetchProducts();
  }

  fetchProducts() {
    this.adminService.getAllProducts().subscribe(
      {
        next: (response: any) => {
          this.allProducts = response.response;
        },
        error: (error: Error) => {
          console.log(error);
          this.notifyService.showError(`Error: ${error.message}`, "Failed");
        }
      }
    );
  }

  formatter = (result: { details: { name: string } }) => result ?  result.details.name : '';

  search: OperatorFunction<any, readonly {details:{name:string}}[]> = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(200),
      distinctUntilChanged(),
      map((term) =>
        term === '' ? [] : this.allProducts.filter((v) => v.details.name.toLowerCase().indexOf(term.toLowerCase()) > -1).slice(0, 10),
        ),
    );

    onSelect(event: any): void {  
      var product = event.item.details;
      this.router.navigate([`/viewproduct/${product.id}`]);
    } 


}
