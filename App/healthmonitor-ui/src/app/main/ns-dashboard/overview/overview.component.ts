import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';
import { NsDashboardService } from '../ns-dashboard.service';
import { BehaviorSubject } from 'rxjs';

import * as shape from 'd3-shape';
import { fuseAnimations } from '@fuse/animations';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class OverviewComponent implements OnInit {
  events = {
    columns: ['message', 'count', 'lastTimestamp', 'kind'],
    rows: []
  };

  projects: any[];

  constructor(
    private fuseSidebarService: FuseSidebarService,
    private nsDashboardService: NsDashboardService
  ) {

  }

  ngOnInit(): void {
    this.events.rows = this.nsDashboardService.events;
  }
}