import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormsCollectionComponent } from './forms-collection.component';

describe('FormsCollectionComponent', () => {
  let component: FormsCollectionComponent;
  let fixture: ComponentFixture<FormsCollectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormsCollectionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormsCollectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
