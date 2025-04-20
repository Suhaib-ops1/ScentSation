import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffappointmetsComponent } from './staffappointmets.component';

describe('StaffappointmetsComponent', () => {
  let component: StaffappointmetsComponent;
  let fixture: ComponentFixture<StaffappointmetsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StaffappointmetsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StaffappointmetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
