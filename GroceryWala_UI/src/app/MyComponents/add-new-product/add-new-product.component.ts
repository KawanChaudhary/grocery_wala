import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { faIndianRupeeSign, faPercentage, faPlus, faCubesStacked, faCirclePlus } from '@fortawesome/free-solid-svg-icons';
import { ProductModel } from 'src/app/Models/ProductModel';
import { UserService } from 'src/app/services/account/user.service';
import { AdminService } from 'src/app/services/admin/admin.service';
import { NotifyService } from 'src/app/services/notification/notify.service';
import { UserModel } from 'src/app/Models/UserModel';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';


@Component({
  selector: 'app-add-new-product',
  templateUrl: './add-new-product.component.html',
  styleUrls: ['./add-new-product.component.css']
})
export class AddNewProductComponent implements OnInit {
  // Icons
  rupeeIcon = faIndianRupeeSign;
  percentageIcon = faPercentage;
  stockIcon = faCubesStacked;
  quantityIcon = faPlus;
  addIcon = faCirclePlus;

  @ViewChild('inputFile') myInputVariable: any;

  @ViewChild('addProduct') productForm : any;
  @ViewChild('name') name: any;
  @ViewChild('description') description: any;
  @ViewChild('price') price: any;
  @ViewChild('quantity') quantity: any;
  @ViewChild('category') category: any;
  @ViewChild('stock') stock: any;
  @ViewChild('discount') discount: any;
  @ViewChild('sizeType') sizeType: any;
  @ViewChild('otherDetails') otherDetails: any;

  selectedFile: any[];

  product: ProductModel;

  productId: any;

  // If logged then user details ::
  user: UserModel = new UserModel('', '', '', '', '', false);

  // Other Specifications variables
  newKey: string;
  newValue: string;

  userDetails: UserModel;

  constructor(private adminService: AdminService, private notifyService: NotifyService, private userService: UserService,
    private router: Router) { }

  ngOnInit(): void {

    this.fetchuserDetails();
     
     
   }

  fetchuserDetails(){
    if (this.userService.isUserAuthenticated()) {      
      this.userService.getUserDetails().subscribe(
        {
        next: (response: any) => {
          if(response.response){
            this.user = response.user;
          }
          this.isAccessible();
        },
        error: (error: HttpErrorResponse) => {
          console.log(error.error);
        }
      }
      );
    }
    else{
      this.notifyService.showWarning(`You must first sign in as admin.`, "");
      this.router.navigate(['/signin'])
    }
  }


  private isAccessible()  {
    if(!this.user.isAdmin){
      this.notifyService.showWarning(`You must first sign in as admin.`, "");
      this.router.navigate(['/signin'])
    }
  }


  onFileSelected(event: any) {
    this.selectedFile = event.target.files;
  }

  private checkFileType() {

    for (let file of this.selectedFile) {
      if (file.type == 'image/jpeg' || file.type == 'image/jpg' || file.type == 'image/png') {
        continue;
      }
      else return false;
    }
    return false;
  }

  onSubmit(productForm: ProductModel) {
    /// create new product

    if (this.checkFileType()) {

      this.notifyService.showWarning("Images should be in .jpg, .jpeg or .png format", "");
    }
    else {

      this.product = productForm;
      
      this.adminService.addNewProduct(this.product).subscribe(
        {
          next: (response: any) => {
            this.productId = response.id;
            this.addProductImages();

            this.notifyService.showSuccess("Successfully added product", "")

          },
          error: (error: Error) => {
            console.log(error);
            this.notifyService.showError(`Error: ${error.message}`, "Failed");
          }
        }
      );

    }
  }


  private addProductImages() {

    // sending images to the database
    let imageData = new FormData();

    for (let file of this.selectedFile) {
      imageData.append('images', file, file.name);
    }

    this.adminService.addProductImages(imageData, this.productId).subscribe(
      {
        next: (response: any) => {
          this.productForm.reset();
          this.selectedFile = [];
          this.myInputVariable.nativeElement.value = '';
        },
        error: (error: Error) => {
          console.log(error);
        }
      }
    );
  }
}