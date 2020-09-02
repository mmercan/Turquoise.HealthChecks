import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';
import { NsDashboardService } from '../ns-dashboard.service';

import * as shape from 'd3-shape';
import { BehaviorSubject, Observable } from 'rxjs';
import { fuseAnimations } from '@fuse/animations';
import { DataSource } from '@angular/cdk/table';


@Component({
  selector: 'app-deployment-list',
  templateUrl: './deployment-list.component.html',
  styleUrls: ['./deployment-list.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class DeploymentListComponent implements OnInit {
  projects: any[];
  selectedProject: any;
  widgets: any;


  widget11: any = {};

  constructor(
    private fuseSidebarService: FuseSidebarService,
    private nsDashboardService: NsDashboardService
  ) {

  }

  ngOnInit(): void {

    this.projects = this.nsDashboardService.projects;
    this.selectedProject = this.projects[0];
    this.widgets = this.nsDashboardService.widgets;

    this.widget11.onContactsChanged = new BehaviorSubject({});
    this.widget11.onContactsChanged.next(this.widgets.widget11.table.rows);
    this.widget11.dataSource = new FilesDataSource(this.widget11);

  }


  toggleSidebar(name): void {
    this.fuseSidebarService.getSidebar(name).toggleOpen();
  }

}

export class FilesDataSource extends DataSource<any>
{

  constructor(private widget11) {
    super();
  }

  connect(): Observable<any[]> { return this.widget11.onContactsChanged; }

  disconnect(): void { }
}
