import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './MyComponents/header/header.component';
import { HomeComponent } from './MyComponents/home/home.component';
import { CarouselComponent } from './MyComponents/carousel/carousel.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { SignUpComponent } from './MyComponents/sign-up/sign-up.component';
import { SignInComponent } from './MyComponents/sign-in/sign-in.component';
import { AddNewProductComponent } from './MyComponents/add-new-product/add-new-product.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ViewAllProductsComponent } from './MyComponents/view-all-products/view-all-products.component';
import { CategoriesComponent } from './MyComponents/categories/categories.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { ViewCategoryComponent } from './MyComponents/view-category/view-category.component';
import { ViewProductComponent } from './MyComponents/view-product/view-product.component';
import { NgbModule, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { ReviewsComponent } from './MyComponents/reviews/reviews.component';
import { ViewOffersComponent } from './MyComponents/view-offers/view-offers.component';
import { CartIconComponent } from './MyComponents/cart-icon/cart-icon.component';
import { CategoryCarouselComponent } from './MyComponents/category-carousel/category-carousel.component';
import { JwtModule } from '@auth0/angular-jwt';
import { ViewCartComponent } from './MyComponents/view-cart/view-cart.component';
import { TimeAgoPipe } from './time-ago.pipe';
import { ViewOrderComponent } from './MyComponents/view-order/view-order.component';
import { OrderConfirmedComponent } from './MyComponents/order-confirmed/order-confirmed.component';
import { LottieModule } from 'ngx-lottie';
import { WhyChooseUsComponent } from './MyComponents/why-choose-us/why-choose-us.component';
import { FooterComponent } from './MyComponents/footer/footer.component';
import { MyOrdersComponent } from './MyComponents/my-orders/my-orders.component';
import { EmptyCartComponent } from './MyComponents/empty-cart/empty-cart.component';
import { OffersHomeComponent } from './MyComponents/offers-home/offers-home.component';
import { NewsLetterComponent } from './MyComponents/news-letter/news-letter.component';
import { EditProductComponent } from './MyComponents/edit-product/edit-product.component';
import { ViewAllOrdersComponent } from './MyComponents/view-all-orders/view-all-orders.component';
import { MostSellProductsComponent } from './MyComponents/most-sell-products/most-sell-products.component';
import { SearchBarComponent } from './MyComponents/search-bar/search-bar.component';
import { JsonPipe } from '@angular/common';

export function tokenGetter() { 
  return localStorage.getItem("jwt"); 
}

export function playerFactory() {
  return import(/* webpackChunkName: 'lottie-web' */ 'lottie-web');
}

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    CarouselComponent,
    SignUpComponent,
    SignInComponent,
    AddNewProductComponent,
    ViewAllProductsComponent,
    CategoriesComponent,
    ViewCategoryComponent,
    ViewProductComponent,
    ReviewsComponent,
    ViewOffersComponent,
    CartIconComponent,
    CategoryCarouselComponent,
    ViewCartComponent,
    TimeAgoPipe,
    ViewOrderComponent,
    OrderConfirmedComponent,
    WhyChooseUsComponent,
    FooterComponent,
    MyOrdersComponent,
    EmptyCartComponent,
    OffersHomeComponent,
    NewsLetterComponent,
    EditProductComponent,
    ViewAllOrdersComponent,
    MostSellProductsComponent,
    SearchBarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    HttpClientModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),    
    NgxPaginationModule, NgbModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:3000"],
        disallowedRoutes: []
      }
    }),
    LottieModule.forRoot({ player: playerFactory }),
    NgbTypeaheadModule, FormsModule, JsonPipe
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
