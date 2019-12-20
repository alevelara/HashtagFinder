import { TestBed } from '@angular/core/testing';

import { HashtagsService } from './hashtags.service';

describe('HashtagsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HashtagsService = TestBed.get(HashtagsService);
    expect(service).toBeTruthy();
  });
});
