import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private apiService: ApiService) { }

  addNewProduct(product: any){
    return this.apiService.post('admin/addnewproduct', product);
  }

  updateProduct(product: any){
    return this.apiService.put('admin/updateproduct', product);
  }

  addProductImages(images: any, productId: any){
    return this.apiService.post(`admin/addproductimages/${productId}`, images);
  }

  deleteImageById(imageId: any){
    return this.apiService.delete(`admin/deleteimage/${imageId}`,);
  }

  deleteProductById(productId: any){
    return this.apiService.delete(`admin/deleteproduct/${productId}`,);
  }

  getAllProducts(){
    return this.apiService.get('admin/allproducts');
  }

  getAllUserOrders(){
    return this.apiService.get('admin/allorders');
  }

  getMostOrderProducts(month:any){
    return this.apiService.get(`admin/getmostorderproducts/${month}`);
  }
  
}
