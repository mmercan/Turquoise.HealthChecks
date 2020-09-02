import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';

import { NsDashboardService } from './ns-dashboard.service';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';

@Component({
  selector: 'app-ns-dashboard',
  templateUrl: './ns-dashboard.component.html',
  styleUrls: ['./ns-dashboard.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class NsDashboardComponent implements OnInit {
  currentNamespace: string;

  constructor(
    private nsDashboardService: NsDashboardService
  ) { }

  ngOnInit(): void {

    this.nsDashboardService.dataset.subscribe(
      data => { },
      error => { }
    );

    this.currentNamespace = this.nsDashboardService.currentNamespace;
  }
}



