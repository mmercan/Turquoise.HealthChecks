import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';

import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';

@Component({
  selector: 'app-node-dashboard',
  templateUrl: './node-dashboard.component.html',
  styleUrls: ['./node-dashboard.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class NodeDashboardComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
