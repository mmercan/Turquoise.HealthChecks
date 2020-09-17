import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { NodeDashboardComponent } from './node-dashboard.component';
import { NodeDashboardService } from './node-dashboard.service';
const routes: Routes = [{
  path: '',
  component: NodeDashboardComponent,
  resolve: {
    data: NodeDashboardService
  }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NodeDashboardRoutingModule { }
