import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private apiService: ApiService) { }


  // Services for user order methods

  addUserOrder(order: any){
    return this.apiService.post("order/adduserorder/", order);
  }

  getAllusersOrder(userId: string){
    return this.apiService.get(`order/getalluserorder/${userId}`);
  }

  getUsersOrder(userId: string, orderIId:number){
    return this.apiService.get(`order/getuserorder/${userId}/${orderIId}`);
  }

  // Services for order for a product order

  addProductOrder(order: any) {
    return this.apiService.post("order/addproductorder/", order);
  }

  getProductOrderDetails(orderId: number){
    return this.apiService.get(`order/getorderproductdetails/${orderId}`);
  }

}
