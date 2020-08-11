import { TestBed } from '@angular/core/testing';

import { DeploymentAppService } from './deployment-app.service';

describe('DeploymentAppService', () => {
  let service: DeploymentAppService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DeploymentAppService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
