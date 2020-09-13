import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NamespaceAppService } from './grpc-services/namespace.service';
import { DeploymentAppService } from './grpc-services/deployment-app.service';
import { K8sServiceService } from './grpc-services/k8s-service.service';

import { IfOnlineDirective } from './offline/if-online.directive';
import { IfSignalrDirective } from './offline/if-signalr.directive';

import { SignalRService } from './signal-r/signal-r.service';
import { OfflineNotificationService } from './offline/offline-notification.service';

@NgModule({
  declarations: [IfOnlineDirective, IfSignalrDirective],
  imports: [
    CommonModule
  ], exports: [
    IfOnlineDirective,
    IfSignalrDirective
  ],
  providers: [NamespaceAppService, DeploymentAppService, K8sServiceService, SignalRService, OfflineNotificationService],
})
export class SharedModule {
  static forRoot(): ModuleWithProviders<any> {
    return {
      ngModule: SharedModule,
      providers: [NamespaceAppService, DeploymentAppService, K8sServiceService, SignalRService, OfflineNotificationService],
    };
  }
}
