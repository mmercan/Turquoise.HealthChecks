import { TestBed } from '@angular/core/testing';

import { K8sEventService } from './k8s-event.service';

describe('K8sEventService', () => {
  let service: K8sEventService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(K8sEventService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
