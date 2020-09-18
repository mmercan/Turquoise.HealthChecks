import { Injectable } from '@angular/core';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { AuthService } from 'app/shared/authentication/auth.service';

import { grpc } from '@improbable-eng/grpc-web';
import { NamespaceServiceClient } from '../../proto/K8sHealthcheck_pb_service';
import { NamespaceService } from '../../proto/K8sHealthcheck_pb_service';
import { Empty } from 'google-protobuf/google/protobuf/empty_pb';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { environment } from 'environments/environment';
import { HealthCheckResultReply, HealthCheckResultRequest, HealthCheckStatsReply, HealthCheckStatsRequest } from 'app/proto/K8sHealthcheck_pb';

@Injectable({
  providedIn: 'root'
})
export class K8sHealthcheckService {

  token: string;
  private unsubscribeAll: Subject<any>;

  constructor(
    private authService: AuthService,
    private fuseNavigation: FuseNavigationService
  ) {

    authService.getUserInfo().subscribe(
      (user) => { this.token = authService.getLocalToken(); }
    );


  }


  getLastHealthCheckResult(namespaceparam: string, serviceName: string): Observable<HealthCheckResultReply.AsObject> {

    const obs = Observable.create(observer => {

      const getRequest = new HealthCheckResultRequest();
      getRequest.setNamespaceparam(namespaceparam);
      getRequest.setServicename(serviceName);

      const authmetadata = new grpc.Metadata();
      authmetadata.append('authorization', `Bearer ${this.token}`);

      grpc.unary(NamespaceService.GetLastHealthCheckResult, {

        metadata: authmetadata,
        request: getRequest,
        host: environment.grpc.url,

        onEnd: res => {
          const message = res.message as HealthCheckResultReply;
          const status = res.status;

          if (status === grpc.Code.OK && message) {
            observer.next(message.toObject());
          } else if (status === grpc.Code.Unauthenticated) {
            this.authService.login();
          } else {
            observer.error(status);
          }
        }
      });
    });
    return obs;

  }

  getStats(namespaceparam: string): Observable<HealthCheckStatsReply[]> {
    // debugger;
    const obs = Observable.create(observer => {

      const geteventRequest = new HealthCheckStatsRequest();
      geteventRequest.setNamespaceparam(namespaceparam);

      const authmetadata = new grpc.Metadata();
      authmetadata.append('authorization', `Bearer ${this.token}`);

      grpc.unary(NamespaceService.GetHealthCheckStats, {

        metadata: authmetadata,
        request: geteventRequest,
        host: environment.grpc.url,

        onEnd: res => {
          const message = res.message as HealthCheckStatsReply;
          const status = res.status;

          if (status === grpc.Code.OK && message) {
            observer.next(message.toObject());
          } else if (status === grpc.Code.Unauthenticated) {
            this.authService.login();
          } else {
            observer.error(status);
          }
        }
      });
    });
    return obs;
  }
}
