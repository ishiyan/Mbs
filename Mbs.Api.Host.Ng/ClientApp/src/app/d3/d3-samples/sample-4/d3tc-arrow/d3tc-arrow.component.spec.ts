import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { D3tcArrowComponent } from './d3tc-arrow.component';

describe('D3tcArrowComponent', () => {
  let component: D3tcArrowComponent;
  let fixture: ComponentFixture<D3tcArrowComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ D3tcArrowComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(D3tcArrowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
