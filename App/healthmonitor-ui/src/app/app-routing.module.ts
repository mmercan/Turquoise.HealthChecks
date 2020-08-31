import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const appRoutes: Routes = [
  {
    path: 'id_token',
    loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule)
  },
  {
    path: 'ns/:nsname',
    loadChildren: () => import('./main/ns-dashboard/ns-dashboard.module').then((m) => m.NsDashboardModule),
  },
  {
    path: 'apps',
    loadChildren: () => import('./main-fuse/apps/apps.module').then(m => m.AppsModule)
  },
  {
    path: 'pages',
    loadChildren: () => import('./main-fuse/pages/pages.module').then(m => m.PagesModule)
  },
  {
    path: 'ui',
    loadChildren: () => import('./main-fuse/ui/ui.module').then(m => m.UIModule)
  },
  {
    path: 'documentation',
    loadChildren: () => import('./main-fuse/documentation/documentation.module').then(m => m.DocumentationModule)
  },
  {
    path: '**',
    redirectTo: 'apps/dashboards/analytics'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
