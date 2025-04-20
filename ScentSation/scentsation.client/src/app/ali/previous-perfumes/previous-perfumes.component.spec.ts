import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreviousPerfumesComponent } from './previous-perfumes.component';

describe('PreviousPerfumesComponent', () => {
  let component: PreviousPerfumesComponent;
  let fixture: ComponentFixture<PreviousPerfumesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PreviousPerfumesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PreviousPerfumesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
