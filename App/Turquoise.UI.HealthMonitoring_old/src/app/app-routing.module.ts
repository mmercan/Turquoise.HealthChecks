import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';



const routes: Routes = [
  {
    path: 'checks/:programname/:envname',
    loadChildren: () => import('./env-dashboard/env-dashboard.module').then((m) => m.EnvDashboardModule),
  },
  {
    path: 'apps',
    loadChildren: () => import('./main/apps/apps.module').then(m => m.AppsModule)
  },
  {
    path: 'pages',
    loadChildren: () => import('./main/pages/pages.module').then(m => m.PagesModule)
  },
  {
    path: 'ui',
    loadChildren: () => import('./main/ui/ui.module').then(m => m.UIModule)
  },
  {
    path: 'documentation',
    loadChildren: () => import('./main/documentation/documentation.module').then(m => m.DocumentationModule)
  }, {
    path: 'sample',
    loadChildren: () => import('./main/sample/sample.module').then(m => m.SampleModule)
  }, {
    path: 'id_token',
    loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule)
  },
  {
    path: '**',
    redirectTo: 'sample'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
