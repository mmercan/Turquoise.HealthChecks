import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NsDashboardComponent } from './ns-dashboard/ns-dashboard.component';
import { DashboardRoutingModule } from './dashboard-routing.module';

import { MatTabsModule } from '@angular/material/tabs';
import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';

import { FlexLayoutModule } from '@angular/flex-layout';
import { FuseSharedModule } from '@fuse/shared.module';
import { SharedModule } from 'app/shared/shared.module';



@NgModule({
  declarations: [NsDashboardComponent],
  imports: [
    CommonModule,
    MatTabsModule,
    MatCardModule,
    MatToolbarModule,
    MatButtonModule,
    MatTableModule,
    MatPaginatorModule,
    FlexLayoutModule,
    DashboardRoutingModule,
    SharedModule,
  ]
})
export class DashboardModule { }
