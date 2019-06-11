import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BrushHandles2Component } from './brush-handles-2.component';

describe('BrushHandles2Component', () => {
  let component: BrushHandles2Component;
  let fixture: ComponentFixture<BrushHandles2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BrushHandles2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BrushHandles2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
