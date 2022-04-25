/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { VoyageRequestService } from './voyageRequest.service';

describe('Service: VoyageRequest', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [VoyageRequestService]
    });
  });

  it('should ...', inject([VoyageRequestService], (service: VoyageRequestService) => {
    expect(service).toBeTruthy();
  }));
});
