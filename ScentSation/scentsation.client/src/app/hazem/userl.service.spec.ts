import { TestBed } from '@angular/core/testing';

import { UserlService } from './userl.service';

describe('UserlService', () => {
  let service: UserlService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserlService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
