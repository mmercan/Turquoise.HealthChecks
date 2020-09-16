import { TestBed } from '@angular/core/testing';

import { K8sHealthcheckstatsService } from './k8s-healthcheckstats.service';

describe('K8sHealthcheckstatsService', () => {
  let service: K8sHealthcheckstatsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(K8sHealthcheckstatsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
