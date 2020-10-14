import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DeploymentDashboardRoutingModule } from './deployment-dashboard-routing.module';
import { DeploymentDashbordService } from './deployment-dashbord.service';

import { MatExpansionModule } from '@angular/material/expansion';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { FuseSharedModule } from '@fuse/shared.module';

import { DeploymentDetailComponent } from './tabs/deployment-detail/deployment-detail.component';
import { DeploymentDashbordComponent } from './deployment-dashbord.component';

@NgModule({
  declarations: [DeploymentDetailComponent, DeploymentDashbordComponent],
  imports: [
    CommonModule,
    DeploymentDashboardRoutingModule,
    MatExpansionModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
    MatTabsModule,
    FuseSharedModule
  ],
  providers: [DeploymentDashbordService]
})
export class DeploymentDashbordModule { }
