import { EventEmitter, Injectable } from '@angular/core';
import { CartModel } from 'src/app/Models/CartModel';
import { ApiService } from '../api.service';
import { Subscription } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class CartService {


    invokeCartIcon = new EventEmitter();
    subsVar: Subscription;

    constructor(private apiService: ApiService) {
    }

    AddToCart(cartItem: CartModel) {
        return this.apiService.post('user/addtocart', cartItem);
    }

    DeleteItem(itemId: string) {
        return this.apiService.delete(`user/deletecartitem/${itemId}`);
    }

    GetCartItems(userId: string) {
        return this.apiService.get(`user/getcartitems/${userId}`)
    }

    GetItem(itemId: string) {
        return this.apiService.get(`user/getcartitem/${itemId}`)
    }

    UpdateCartItem(cartItem: any) {
        return this.apiService.put('user/updatecartitem', cartItem);
    }

    onAddToCartInvokeCartIcon(userId: string) {    
        this.invokeCartIcon.emit(userId);    
      }  

}
