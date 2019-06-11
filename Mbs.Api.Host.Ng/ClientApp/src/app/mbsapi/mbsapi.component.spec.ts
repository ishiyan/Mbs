import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MbsApiComponent } from './mbsapi.component';

describe('MbsApiComponent', () => {
  let component: MbsApiComponent;
  let fixture: ComponentFixture<MbsApiComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MbsApiComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MbsApiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
