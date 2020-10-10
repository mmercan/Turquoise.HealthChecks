import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NamespaceAppService } from './grpc-services/namespace.service';
import { K8sDeploymentService } from './grpc-services/k8s-deployment.service';
import { K8sServiceService } from './grpc-services/k8s-service.service';
import { K8sEventService } from './grpc-services/k8s-event.service';
import { K8sHealthcheckService } from './grpc-services/k8s-healthcheck.service';
import { K8sNodeService } from './grpc-services/k8s-node.service';

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
  providers: [NamespaceAppService, K8sDeploymentService, K8sServiceService, K8sEventService,
    K8sHealthcheckService, K8sNodeService, SignalRService, OfflineNotificationService],
})
export class SharedModule {
  static forRoot(): ModuleWithProviders<any> {
    return {
      ngModule: SharedModule,
      providers: [NamespaceAppService, K8sDeploymentService, K8sServiceService, K8sEventService,
        K8sHealthcheckService, K8sNodeService, SignalRService, OfflineNotificationService],
    };
  }
}
