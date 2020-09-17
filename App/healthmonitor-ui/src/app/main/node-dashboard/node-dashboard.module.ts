import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NodeDashboardRoutingModule } from './node-dashboard-routing.module';
import { NodeDashboardComponent } from './node-dashboard.component';


@NgModule({
  declarations: [NodeDashboardComponent],
  imports: [
    CommonModule,
    NodeDashboardRoutingModule
  ]
})
export class NodeDashboardModule { }
