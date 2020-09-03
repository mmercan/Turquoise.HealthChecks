import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NsDashboardRoutingModule } from './ns-dashboard-routing.module';
import { NsDashboardComponent } from './ns-dashboard.component';
import { FlexLayoutModule } from '@angular/flex-layout';

import { FuseSharedModule } from '@fuse/shared.module';
import { SharedModule } from 'app/shared/shared.module';
import { FuseWidgetModule } from '@fuse/components/widget/widget.module';
import { FuseSidebarModule } from '@fuse/components';


import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';


import { MatCardModule } from '@angular/material/card';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';

import { NgxChartsModule } from '@swimlane/ngx-charts';

import { NsDashboardService } from './ns-dashboard.service';
import { OverviewComponent } from './overview/overview.component';
import { DeploymentListComponent } from './deployment-list/deployment-list.component';
import { ServiceListComponent } from './service-list/service-list.component';



import { MatChipsModule } from '@angular/material/chips';
import { MatRippleModule } from '@angular/material/core';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { AgmCoreModule } from '@agm/core';



@NgModule({
  declarations: [NsDashboardComponent, OverviewComponent, DeploymentListComponent, ServiceListComponent],
  imports: [
    CommonModule,
    NsDashboardRoutingModule,

    MatTabsModule,
    MatCardModule,
    MatToolbarModule,
    MatButtonModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    FlexLayoutModule,
    SharedModule,
    FuseSharedModule,
    FuseWidgetModule,
    FuseSidebarModule,
    MatDividerModule,
    MatFormFieldModule,
    MatIconModule,
    MatMenuModule,
    MatSelectModule,
    NgxChartsModule,

    MatChipsModule,
    MatRippleModule,
    MatExpansionModule,
    MatInputModule,
    MatSnackBarModule,
    AgmCoreModule
  ],
  providers: [NsDashboardService]
})
export class NsDashboardModule { }
