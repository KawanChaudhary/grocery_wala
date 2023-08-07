import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MostSellProductsComponent } from './most-sell-products.component';

describe('MostSellProductsComponent', () => {
  let component: MostSellProductsComponent;
  let fixture: ComponentFixture<MostSellProductsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MostSellProductsComponent]
    });
    fixture = TestBed.createComponent(MostSellProductsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
