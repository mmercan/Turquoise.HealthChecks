import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const appRoutes: Routes = [
  {
    path: 'ns/:nsname',
    loadChildren: () => import('./main/dashboard/dashboard.module').then((m) => m.DashboardModule),
  }, {
    path: 'sample',
    loadChildren: () => import('./main/sample/sample.module').then(m => m.SampleModule)
  },
  {
    path: 'id_token',
    loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule)
  },
  {
    path: '**',
    redirectTo: 'sample'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
