import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProgDashboardComponent } from './prog-dashboard/prog-dashboard.component';
import { EnvDashboardRoutingModule } from './env-dashboard-routing.module';



@NgModule({
  declarations: [ProgDashboardComponent],
  imports: [
    CommonModule,
    EnvDashboardRoutingModule,
  ]
})
export class EnvDashboardModule { }
