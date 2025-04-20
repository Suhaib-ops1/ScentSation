import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AvailablesessionsComponent } from './availablesessions.component';

describe('AvailablesessionsComponent', () => {
  let component: AvailablesessionsComponent;
  let fixture: ComponentFixture<AvailablesessionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AvailablesessionsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AvailablesessionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
