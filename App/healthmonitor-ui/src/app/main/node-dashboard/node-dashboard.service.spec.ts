import { TestBed } from '@angular/core/testing';

import { NodeDashboardService } from './node-dashboard.service';

describe('NodeDashboardService', () => {
  let service: NodeDashboardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NodeDashboardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
