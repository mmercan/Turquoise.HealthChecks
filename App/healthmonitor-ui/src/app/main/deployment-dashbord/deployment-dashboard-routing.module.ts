import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DeploymentDashbordComponent } from './deployment-dashbord.component';
import { DeploymentDashbordService } from './deployment-dashbord.service';

const routes: Routes = [{
    path: '',
    component: DeploymentDashbordComponent,
    resolve: {
        data: DeploymentDashbordService
    }
}];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class DeploymentDashboardRoutingModule { }
