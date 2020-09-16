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
import { ServiceListReply, ServiceReply, GetEventListRequest, EventListReply } from 'app/proto/K8sHealthcheck_pb';

@Injectable({
  providedIn: 'root'
})
export class K8sEventService {

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


  getEvents(namespaceparam: string): Observable<ServiceReply[]> {
    // debugger;
    const obs = Observable.create(observer => {

      const geteventRequest = new GetEventListRequest();
      geteventRequest.setNamespaceparam(namespaceparam);

      const authmetadata = new grpc.Metadata();
      authmetadata.append('authorization', `Bearer ${this.token}`);

      grpc.unary(NamespaceService.GetEvents, {

        metadata: authmetadata,
        request: geteventRequest,
        host: environment.grpc.url,

        onEnd: res => {
          const message = res.message as EventListReply;
          const status = res.status;

          if (status === grpc.Code.OK && message) {
            observer.next(message.toObject().eventsList);
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
