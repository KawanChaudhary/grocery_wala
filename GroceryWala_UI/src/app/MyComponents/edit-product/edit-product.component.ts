import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faCirclePlus, faCubesStacked, faIndianRupeeSign, faPercentage, faPlus } from '@fortawesome/free-solid-svg-icons';
import { Subscription } from 'rxjs';
import { ProductModel } from 'src/app/Models/ProductModel';
import { UserModel } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/services/account/user.service';
import { AdminService } from 'src/app/services/admin/admin.service';
import { NotifyService } from 'src/app/services/notification/notify.service';
import { ProductsService } from 'src/app/services/product/products.service';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent implements OnInit {
  // Icons
  rupeeIcon = faIndianRupeeSign;
  percentageIcon = faPercentage;
  stockIcon = faCubesStacked;
  quantityIcon = faPlus;
  addIcon = faCirclePlus;


  // activated routes
  routeSub: Subscription;

  //url details
  userId: string;
  productId: number = 0;

  // If logged then user details ::
  user: UserModel = new UserModel('', '', '', '', '', false);


  @ViewChild('inputFile') myInputVariable: any;

  selectedFile: any[] = [];

  product: ProductModel = new ProductModel('', 0, '', 0, 0, 0, 0, 0, '', 0, 0, 0, 0, false);
  images: any[] = [];

  // Other Specifications variables
  newKey: string;
  newValue: string;

  userDetails: UserModel;

  constructor(private productService: ProductsService, private notifyService: NotifyService,
    private route: ActivatedRoute, private adminService: AdminService, private router: Router,
    private userService: UserService ) { }

  ngOnInit(): void {

    this.routeSub = this.route.params.subscribe(params => {
      this.userId = params['userId'];
      this.productId = params['productId'];
    });   
    this.fetchProductDetails();
  }  

  fetchProductDetails() {
    this.productService.getProductById(this.productId).subscribe(
      {
        next: (response: any) => {
          this.product = response.product;
          this.images = response.images;
        },
        error: (error: Error) => {
          console.log(error);
          this.notifyService.showError(`Error: ${error.message}`, "Failed");
        }
      }
    );

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

  onSubmit() {
    /// create new product

    if (this.selectedFile.length > 0 && this.checkFileType()) {

      this.notifyService.showWarning("Images should be in .jpg, .jpeg or .png format", "");
    }
    else {


      this.adminService.updateProduct(this.product).subscribe(
        {
          next: (response: any) => {
            this.productId = response.id;
            if(this.selectedFile.length > 0) this.addProductImages();

            this.notifyService.showSuccess("Successfully updated product", "")
            

          },
          error: (error: Error) => {
            console.log(error);
            this.notifyService.showError(`Error: ${error.message}`, "Failed");
          }
        }
      );

      this.router.navigate([`/allproducts/${this.userId}/true`])

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
          this.selectedFile = [];
          this.myInputVariable.nativeElement.value = null;
        },
        error: (error: Error) => {
          console.log(error);
        }
      }
    );
  }

  deleteImage(imageId: number) {
    if (this.images.length > 1) {

      this.adminService.deleteImageById(imageId).subscribe(
        {
          next: (response: any) => {
            if (response.response) {
              this.notifyService.showSuccess("Image deleted successfully", "");
              this.fetchProductDetails();
            }
          },
          error: (error: Error) => {
            console.log(error);
          }
        }
      );
    }
    else {
      this.notifyService.showWarning("You should have atleast 1 image", "");
    }
  }

}