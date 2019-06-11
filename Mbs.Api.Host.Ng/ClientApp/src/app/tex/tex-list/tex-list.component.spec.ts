import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TexListComponent } from './tex-list.component';

describe('TexEditorComponent', () => {
  let component: TexListComponent;
  let fixture: ComponentFixture<TexListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TexListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TexListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
