import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  constructor(private apiService: ApiService) { }

  // addNewProduct(product: any){
  //   return this.apiService.post('home/addnewproduct', product);
  // }
  
  // updateProduct(product: any){
  //   return this.apiService.put('home/updateproduct', product);
  // }

  // addProductImages(images: any){
  //   return this.apiService.post('home/addproductimages', images);
  // }

  getProductByCategory(category: any){
    return this.apiService.get(`home/category/${category}`);
  }

  getProductById(productId: any){
    return this.apiService.get(`home/product/${productId}`);
  }

  // Comments Calls

  getCommentByProduct(productId: any){
    return this.apiService.get(`user/getcommentsbyproduct/${productId}`);
  }

  addNewComment(comment: any){
    return this.apiService.post('user/addcomment/', comment);
  }

  addRating(rating: any){
    return this.apiService.post('user/addrating/', rating);
  }

  getRatingOfProductByUser(productId: any, userId: any){
    return this.apiService.get(`user/getratingofproduct/${productId}/${userId}`);
  }
  
  // Get review an rating together
  getReviewOfProduct(productId: any, userId: any){
    return this.apiService.get(`user/getreviewofproduct/${productId}/${userId}`);
  }

  // Get Offers

  getOffers(){
    return this.apiService.get(`home/getoffers`);
  }

}
