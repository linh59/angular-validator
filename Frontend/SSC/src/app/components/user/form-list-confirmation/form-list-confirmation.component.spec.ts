import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormListConfirmationComponent } from './form-list-confirmation.component';

describe('FormListConfirmationComponent', () => {
  let component: FormListConfirmationComponent;
  let fixture: ComponentFixture<FormListConfirmationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormListConfirmationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormListConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
