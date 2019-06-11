import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BrushAndZoomAreaChartComponent } from './brush-and-zoom-area-chart.component';

describe('BrushAndZoomAreaChartComponent', () => {
  let component: BrushAndZoomAreaChartComponent;
  let fixture: ComponentFixture<BrushAndZoomAreaChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BrushAndZoomAreaChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BrushAndZoomAreaChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
