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

import { MatRippleModule } from '@angular/material/core';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatTableModule } from '@angular/material/table';

import { ServiceDashboardService } from './service-dashboard.service';
import { ServiceDetailComponent } from './tabs/service-detail/service-detail.component';
import { HealthCheckHistoriesComponent } from './tabs/health-check-histories/health-check-histories.component';
import { HealthCheckHistoriesListComponent } from './tabs/health-check-histories-list/health-check-histories-list.component';
import { HealthCheckHistoriesSidebarComponent } from './tabs/health-check-histories-sidebar/health-check-histories-sidebar.component';
import { FuseSidebarModule } from '@fuse/components';

@NgModule({
  declarations: [ServiceDashboardComponent,
    ServiceDetailComponent, HealthCheckHistoriesComponent, HealthCheckHistoriesListComponent, HealthCheckHistoriesSidebarComponent],
  imports: [
    CommonModule,
    MatExpansionModule,
    ServiceDashboardRoutingModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
    MatTabsModule,
    FuseSidebarModule,
    FuseSharedModule,
    MatRippleModule,
    MatSlideToggleModule,
    MatTableModule
  ],
  providers: [ServiceDashboardService]
})
export class ServiceDashboardModule { }
