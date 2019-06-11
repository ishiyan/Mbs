import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { D3tcBrushComponent } from './d3tc-brush.component';

describe('D3tcBrushComponent', () => {
  let component: D3tcBrushComponent;
  let fixture: ComponentFixture<D3tcBrushComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ D3tcBrushComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(D3tcBrushComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
