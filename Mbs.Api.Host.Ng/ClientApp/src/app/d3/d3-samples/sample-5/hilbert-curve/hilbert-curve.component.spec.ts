import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HilbertCurveComponent } from './hilbert-curve.component';

describe('HilbertCurveComponent', () => {
  let component: HilbertCurveComponent;
  let fixture: ComponentFixture<HilbertCurveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HilbertCurveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HilbertCurveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
