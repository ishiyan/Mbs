import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HilbertPathsComponent } from './hilbert-paths.component';

describe('HilbertPathsComponent', () => {
  let component: HilbertPathsComponent;
  let fixture: ComponentFixture<HilbertPathsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HilbertPathsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HilbertPathsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
