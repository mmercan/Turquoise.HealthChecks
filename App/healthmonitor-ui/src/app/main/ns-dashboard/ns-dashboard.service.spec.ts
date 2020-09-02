import { TestBed } from '@angular/core/testing';

import { NsDashboardService } from './ns-dashboard.service';

describe('NsDashboardService', () => {
  let service: NsDashboardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NsDashboardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
