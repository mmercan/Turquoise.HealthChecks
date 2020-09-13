import { Injectable } from '@angular/core';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { AuthService } from 'app/shared/authentication/auth.service';

import { grpc } from '@improbable-eng/grpc-web';
import { DeploymentReply, NamespaceListReply, NamespaceReply } from '../../proto/K8sHealthcheck_pb';
import { NamespaceServiceClient } from '../../proto/K8sHealthcheck_pb_service';
import { NamespaceService } from '../../proto/K8sHealthcheck_pb_service';
import { Empty } from 'google-protobuf/google/protobuf/empty_pb';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class K8sServiceService {
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



  getNameSpaces(): Observable<NamespaceReply[]> {
    // debugger;
    const obs = Observable.create(observer => {

      const getNamespaceRequest = new Empty();

      const authmetadata = new grpc.Metadata();
      authmetadata.append('authorization', `Bearer ${this.token}`);

      grpc.unary(NamespaceService.GetNamespaces, {

        metadata: authmetadata,
        request: getNamespaceRequest,
        host: environment.grpc.url,

        onEnd: res => {
          const message = res.message as NamespaceListReply;
          const status = res.status;

          if (status === grpc.Code.OK && message) {
            observer.next(message.getNamespacesList());
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