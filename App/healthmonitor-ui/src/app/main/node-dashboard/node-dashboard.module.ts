import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NodeDashboardRoutingModule } from './node-dashboard-routing.module';
import { NodeDashboardComponent } from './node-dashboard.component';

import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { NodeListComponent } from './tabs/node-list/node-list.component';

import { FuseWidgetModule } from '@fuse/components/widget/widget.module';
import { SharedModule } from '../../shared/shared.module';

import { MatExpansionModule } from '@angular/material/expansion';

import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { FuseSharedModule } from '@fuse/shared.module';

@NgModule({
  declarations: [NodeDashboardComponent, NodeListComponent],
  imports: [
    CommonModule,
    NodeDashboardRoutingModule,
    MatTableModule,
    MatTabsModule,
    FuseWidgetModule,
    SharedModule,
    MatExpansionModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
    FuseSharedModule
  ]
})
export class NodeDashboardModule { }
