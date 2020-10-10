import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NodeDashboardRoutingModule } from './node-dashboard-routing.module';
import { NodeDashboardComponent } from './node-dashboard.component';

import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { NodeListComponent } from './tabs/node-list/node-list.component';

import { FuseWidgetModule } from '@fuse/components/widget/widget.module';

@NgModule({
  declarations: [NodeDashboardComponent, NodeListComponent],
  imports: [
    CommonModule,
    NodeDashboardRoutingModule,
    MatTableModule,
    MatTabsModule,
    FuseWidgetModule
  ]
})
export class NodeDashboardModule { }
