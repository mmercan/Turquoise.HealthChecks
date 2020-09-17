import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ServiceDashboardRoutingModule } from './service-dashboard-routing.module';
import { ServiceDashboardComponent } from './service-dashboard.component';


@NgModule({
  declarations: [ServiceDashboardComponent],
  imports: [
    CommonModule,
    ServiceDashboardRoutingModule
  ]
})
export class ServiceDashboardModule { }
