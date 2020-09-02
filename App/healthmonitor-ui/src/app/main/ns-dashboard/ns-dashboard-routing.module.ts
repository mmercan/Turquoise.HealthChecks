import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { NsDashboardComponent } from './ns-dashboard.component';
import { NsDashboardService } from './ns-dashboard.service';
const routes: Routes = [{
  path: '',
  component: NsDashboardComponent,
  resolve: {
    data: NsDashboardService
  }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NsDashboardRoutingModule { }
