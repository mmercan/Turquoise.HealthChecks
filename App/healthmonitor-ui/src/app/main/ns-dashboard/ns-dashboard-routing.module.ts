import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { NsDashboardComponent } from './ns-dashboard.component';

const routes: Routes = [{
  path: '',
  component: NsDashboardComponent
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NsDashboardRoutingModule { }
