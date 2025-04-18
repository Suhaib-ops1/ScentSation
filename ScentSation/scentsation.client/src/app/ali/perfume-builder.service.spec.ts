import { TestBed } from '@angular/core/testing';

import { PerfumeBuilderService } from './perfume-builder.service';

describe('PerfumeBuilderService', () => {
  let service: PerfumeBuilderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PerfumeBuilderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
