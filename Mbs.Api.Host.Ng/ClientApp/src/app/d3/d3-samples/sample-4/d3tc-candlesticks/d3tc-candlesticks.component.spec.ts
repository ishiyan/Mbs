import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { D3tcCandlesticksComponent } from './d3tc-candlesticks.component';

describe('D3tcCandlesticksComponent', () => {
  let component: D3tcCandlesticksComponent;
  let fixture: ComponentFixture<D3tcCandlesticksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ D3tcCandlesticksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(D3tcCandlesticksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
