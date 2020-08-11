import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NamespaceAppService } from './grpc-services/namespace.service';
import { DeploymentAppService } from './grpc-services/deployment-app.service';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [NamespaceAppService, DeploymentAppService],
})
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [NamespaceAppService, DeploymentAppService],
    };
  }
}
