import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormRegistrationWaterComponent } from './form-registration-water.component';

describe('FormRegistrationWaterComponent', () => {
  let component: FormRegistrationWaterComponent;
  let fixture: ComponentFixture<FormRegistrationWaterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormRegistrationWaterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormRegistrationWaterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
