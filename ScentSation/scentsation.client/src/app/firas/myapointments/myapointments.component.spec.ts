import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyapointmentsComponent } from './myapointments.component';

describe('MyapointmentsComponent', () => {
  let component: MyapointmentsComponent;
  let fixture: ComponentFixture<MyapointmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MyapointmentsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MyapointmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
