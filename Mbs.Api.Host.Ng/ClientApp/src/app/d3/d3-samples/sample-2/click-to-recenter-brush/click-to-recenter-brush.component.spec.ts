import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClickToRecenterBrushComponent } from './click-to-recenter-brush.component';

describe('ClickToRecenterBrushComponent', () => {
  let component: ClickToRecenterBrushComponent;
  let fixture: ComponentFixture<ClickToRecenterBrushComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClickToRecenterBrushComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClickToRecenterBrushComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
