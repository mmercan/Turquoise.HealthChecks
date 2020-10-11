import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { NodeReply } from 'app/proto/K8sHealthcheck_pb';
import { NodeDashboardService } from '../../node-dashboard.service';

@Component({
  selector: 'app-node-list',
  templateUrl: './node-list.component.html',
  styleUrls: ['./node-list.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class NodeListComponent implements OnInit {
  nodes: NodeReply[];

  constructor(private nodeDashboardService: NodeDashboardService) { }

  ngOnInit(): void {
    this.nodes = this.nodeDashboardService.nodes;
    debugger;
  }

}
