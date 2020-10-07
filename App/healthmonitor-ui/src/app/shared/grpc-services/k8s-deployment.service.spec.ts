import { TestBed } from '@angular/core/testing';

import { K8sDeploymentService } from './k8s-deployment.service';

describe('K8sDeploymentService', () => {
  let service: K8sDeploymentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(K8sDeploymentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
