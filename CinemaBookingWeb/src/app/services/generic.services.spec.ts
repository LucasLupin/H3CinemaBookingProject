import { TestBed } from '@angular/core/testing';

import { GenericService } from './generic.services';

describe('GenericService', () => {
  let service: GenericService<any>;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GenericService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
