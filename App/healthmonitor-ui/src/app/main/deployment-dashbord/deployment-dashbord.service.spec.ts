import { TestBed } from '@angular/core/testing';

import { DeploymentDashbordService } from './deployment-dashbord.service';

describe('DeploymentDashbordService', () => {
  let service: DeploymentDashbordService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DeploymentDashbordService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
