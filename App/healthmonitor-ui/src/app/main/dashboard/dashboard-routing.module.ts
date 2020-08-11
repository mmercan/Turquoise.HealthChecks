import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NsDashboardComponent } from './ns-dashboard/ns-dashboard.component';


const routes: Routes = [
    {
        path: '',
        component: NsDashboardComponent,
        data: {
            heading: 'Program Dashboard',
        },
    }];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class DashboardRoutingModule { }
