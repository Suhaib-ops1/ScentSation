import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PerfumeBuilderComponent } from './perfume-builder.component';

describe('PerfumeBuilderComponent', () => {
  let component: PerfumeBuilderComponent;
  let fixture: ComponentFixture<PerfumeBuilderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PerfumeBuilderComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PerfumeBuilderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
