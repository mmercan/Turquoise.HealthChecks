import { TestBed } from '@angular/core/testing';

import { K8sNodeService } from './k8s-node.service';

describe('K8sNodeService', () => {
  let service: K8sNodeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(K8sNodeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
