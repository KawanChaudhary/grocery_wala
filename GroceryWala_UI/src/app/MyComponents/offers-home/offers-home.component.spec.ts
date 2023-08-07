import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OffersHomeComponent } from './offers-home.component';

describe('OffersHomeComponent', () => {
  let component: OffersHomeComponent;
  let fixture: ComponentFixture<OffersHomeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [OffersHomeComponent]
    });
    fixture = TestBed.createComponent(OffersHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
