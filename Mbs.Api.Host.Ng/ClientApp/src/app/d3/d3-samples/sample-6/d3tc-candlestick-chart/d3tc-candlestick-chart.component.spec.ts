import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { D3tcCandlestickChartComponent } from './d3tc-candlestick-chart.component';

describe('D3tcCandlestickChartComponent', () => {
  let component: D3tcCandlestickChartComponent;
  let fixture: ComponentFixture<D3tcCandlestickChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ D3tcCandlestickChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(D3tcCandlestickChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
