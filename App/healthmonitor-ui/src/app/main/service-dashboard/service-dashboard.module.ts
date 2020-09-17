import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatExpansionModule } from '@angular/material/expansion';

import { ServiceDashboardRoutingModule } from './service-dashboard-routing.module';
import { ServiceDashboardComponent } from './service-dashboard.component';

import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { FuseSharedModule } from '@fuse/shared.module';

import { ServiceDashboardService } from './service-dashboard.service';

@NgModule({
  declarations: [ServiceDashboardComponent],
  imports: [
    CommonModule,
    MatExpansionModule,
    ServiceDashboardRoutingModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
    MatTabsModule,

    FuseSharedModule
  ],
  providers: [ServiceDashboardService]
})
export class ServiceDashboardModule { }
