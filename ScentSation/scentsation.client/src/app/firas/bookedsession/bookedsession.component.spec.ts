import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookedsessionComponent } from './bookedsession.component';

describe('BookedsessionComponent', () => {
  let component: BookedsessionComponent;
  let fixture: ComponentFixture<BookedsessionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BookedsessionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookedsessionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
