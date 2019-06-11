import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { D3tcHorizonChartInteractiveComponent } from './d3tc-horizon-chart-interactive.component';

describe('D3tcHorizonChartDemoComponent', () => {
  let component: D3tcHorizonChartInteractiveComponent;
  let fixture: ComponentFixture<D3tcHorizonChartInteractiveComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ D3tcHorizonChartInteractiveComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(D3tcHorizonChartInteractiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
