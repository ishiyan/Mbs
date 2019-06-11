import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClickToSelectAllComponent } from './click-to-select-all.component';

describe('ClickToSelectAllComponent', () => {
  let component: ClickToSelectAllComponent;
  let fixture: ComponentFixture<ClickToSelectAllComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClickToSelectAllComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClickToSelectAllComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
