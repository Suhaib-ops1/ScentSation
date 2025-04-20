import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FragranceAiComponent } from './fragrance-ai.component';

describe('FragranceAiComponent', () => {
  let component: FragranceAiComponent;
  let fixture: ComponentFixture<FragranceAiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FragranceAiComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FragranceAiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
