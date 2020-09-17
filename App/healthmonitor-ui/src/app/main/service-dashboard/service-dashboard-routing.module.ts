import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ServiceDashboardComponent } from './service-dashboard.component';
import { ServiceDashboardService } from './service-dashboard.service';

const routes: Routes = [{
  path: '',
  component: ServiceDashboardComponent,
  resolve: {
    data: ServiceDashboardService
  }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ServiceDashboardRoutingModule { }
