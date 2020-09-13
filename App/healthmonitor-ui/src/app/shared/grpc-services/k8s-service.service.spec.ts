import { TestBed } from '@angular/core/testing';

import { K8sServiceService } from './k8s-service.service';

describe('K8sServiceService', () => {
  let service: K8sServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(K8sServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
