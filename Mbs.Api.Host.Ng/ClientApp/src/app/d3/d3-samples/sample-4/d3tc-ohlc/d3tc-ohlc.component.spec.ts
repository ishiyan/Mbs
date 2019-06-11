import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { D3tcOhlcComponent } from './d3tc-ohlc.component';

describe('D3tcOhlcComponent', () => {
  let component: D3tcOhlcComponent;
  let fixture: ComponentFixture<D3tcOhlcComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ D3tcOhlcComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(D3tcOhlcComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
