import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewAllOrdersComponent } from './view-all-orders.component';

describe('ViewAllOrdersComponent', () => {
  let component: ViewAllOrdersComponent;
  let fixture: ComponentFixture<ViewAllOrdersComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewAllOrdersComponent]
    });
    fixture = TestBed.createComponent(ViewAllOrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
