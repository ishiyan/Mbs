import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { D3tcHorizonChartSingleComponent } from './d3tc-horizon-chart-single.component';

describe('D3tcHorizonChartSingleComponent', () => {
  let component: D3tcHorizonChartSingleComponent;
  let fixture: ComponentFixture<D3tcHorizonChartSingleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ D3tcHorizonChartSingleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(D3tcHorizonChartSingleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
