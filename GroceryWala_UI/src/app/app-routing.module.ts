import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './MyComponents/home/home.component';
import { SignUpComponent } from './MyComponents/sign-up/sign-up.component';
import { SignInComponent } from './MyComponents/sign-in/sign-in.component';
import { AddNewProductComponent } from './MyComponents/add-new-product/add-new-product.component';
import { ViewAllProductsComponent } from './MyComponents/view-all-products/view-all-products.component';
import { ViewCategoryComponent } from './MyComponents/view-category/view-category.component';
import { ViewProductComponent } from './MyComponents/view-product/view-product.component';
import { AuthGuard } from './services/account/auth-guard.service';
import { ViewCartComponent } from './MyComponents/view-cart/view-cart.component';
import { ViewOrderComponent } from './MyComponents/view-order/view-order.component';
import { OrderConfirmedComponent } from './MyComponents/order-confirmed/order-confirmed.component';
import { MyOrdersComponent } from './MyComponents/my-orders/my-orders.component';
import { EditProductComponent } from './MyComponents/edit-product/edit-product.component';
import { AuthGuardAdminService } from './services/admin/auth-guard-admin.service';
import { ViewAllOrdersComponent } from './MyComponents/view-all-orders/view-all-orders.component';
import { MostSellProductsComponent } from './MyComponents/most-sell-products/most-sell-products.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'signup', component: SignUpComponent },
  { path: 'signin', component: SignInComponent },
  { path: 'viewcategory/:category', component: ViewCategoryComponent },
  { path: 'viewproduct/:productid', component: ViewProductComponent },

  { path: 'addnewproduct/:userId/:isAdmin', component: AddNewProductComponent, canActivate: [AuthGuardAdminService]},
  { path: 'allproducts/:userId/:isAdmin', component: ViewAllProductsComponent, canActivate: [AuthGuardAdminService]  },
  { path: 'editproduct/:userId/:isAdmin/:productId', component: EditProductComponent, canActivate: [AuthGuardAdminService]  },
  { path: 'allorders/:userId/:isAdmin', component: ViewAllOrdersComponent, canActivate: [AuthGuardAdminService]  },
  { path: 'mostsellproducts/:userId/:isAdmin', component: MostSellProductsComponent, canActivate: [AuthGuardAdminService]  },

  
  { path: 'viewcart/:userId', component: ViewCartComponent, canActivate: [AuthGuard]  },
  { path: 'vieworder/:userId/:orderId', component: ViewOrderComponent, canActivate: [AuthGuard]  },
  { path: 'orderconfirm/:userId/:orderId', component: OrderConfirmedComponent, canActivate: [AuthGuard]  },
  { path: 'myorders/:userId', component: MyOrdersComponent, canActivate: [AuthGuard]  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{
    onSameUrlNavigation: 'reload',
    scrollPositionRestoration: 'enabled'
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
