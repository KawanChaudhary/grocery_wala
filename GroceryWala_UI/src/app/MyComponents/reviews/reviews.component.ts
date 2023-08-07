import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { faStar, faCircleCheck } from '@fortawesome/free-solid-svg-icons';
import { Subscription } from 'rxjs';
import { CommentModel } from 'src/app/Models/CommentModel';
import { RatingModel } from 'src/app/Models/RatingModel';
import { UserModel } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/services/account/user.service';
import { NotifyService } from 'src/app/services/notification/notify.service';
import { ProductsService } from 'src/app/services/product/products.service';

@Component({
  selector: 'app-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css']
})
export class ReviewsComponent implements OnInit {

  starIcon = faStar;
  checkIcon = faCircleCheck;

  // User Details
  user: UserModel = new UserModel('', '', '', '', '', false);

  productId = null;


  currentRate = 0;

  // activated routes
  routeSub: Subscription;

  // Comments
  allComments: any[] = [];

  constructor(private userService: UserService, private notifyService: NotifyService,
    private productService: ProductsService, private route: ActivatedRoute, private router: Router) {
    this.route.paramMap.subscribe(() => {
      this.ngOnInit();
    });
  }

  ngOnInit(): void {

    this.routeSub = this.route.params.subscribe(params => {
      this.productId = params['productid'];
    });

    this.fetchUserDetails();
  }

  // Fetch details of logged user
  fetchUserDetails(): void {
    if (this.userService.isUserAuthenticated()) {
      this.userService.getUserDetails().subscribe(
        {
          next: (response: any) => {

            if (response.response) {
              this.user = response.user;
              this.fetchAllComments();
            }
          },
          error: (error: Error) => {
            console.log(error);
            this.notifyService.showError(`Error: ${error.message}`, "Failed");
          }
        }
      );
    }
    else this.fetchAllComments();
  }

  addNewComment(commentId: any): void {
    var comment = commentId.value;

    if (comment == '') return;

    if (this.userService.isUserAuthenticated()) {

      // new Comment
      var newComment = new CommentModel(this.user.id, this.user.firstName, this.productId, comment, null, this.user.lastName);
      console.log(newComment);

      this.productService.addNewComment(newComment).subscribe(
        {
          next: (response: any) => {

            if (response.response) {
              this.notifyService.showSuccess("New Comment Added", "");
              this.fetchAllComments();
              commentId.value = '';
            }
          },
          error: (error: Error) => {
            console.log(error);
            this.notifyService.showError(`Error: ${error.message}`, "Failed");
          }
        }
      );
    }
  }

  fetchAllComments() {

    if (this.userService.isUserAuthenticated()) {
      this.productService.getReviewOfProduct(this.productId, this.user.id).subscribe(
        {
          next: (response: any) => {
            if (response.response) {
              this.allComments = response.comments;              
              this.currentRate = response.rating.rating;
            }
          },
          error: (error: Error) => {
            console.log(error);
            this.notifyService.showError(`Error: ${error.message}`, "Failed");
          }
        }
      );
    }
    else {

      this.productService.getCommentByProduct(this.productId).subscribe(
        {
          next: (response: any) => {

            this.allComments = response.comments;
          },
          error: (error: Error) => {
            console.log(error);
            this.notifyService.showError(`Error: ${error.message}`, "Failed");
          }
        }
      );

    }
  }

  updateRating() {
    if(this.userService.isUserAuthenticated()){

      var rating = new RatingModel(this.user.id, this.productId, this.currentRate);

      this.productService.addRating(rating).subscribe(
        {
          next: (response: any) => {
            console.log(response);

            if (response.response) {
              this.fetchAllComments();
              this.notifyService.showSuccess("Rating updated successfully", "");
            }
          },
          error: (error: Error) => {
            console.log(error);
            this.notifyService.showError(`Error: ${error.message}`, "Failed");
          }
        }
      );

    }
    else{
      this.router.navigate(['/signin'])
    }
  }

}
