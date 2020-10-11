import { Injectable } from '@angular/core';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { grpc } from '@improbable-eng/grpc-web';
import { NodeListReply, NodeReply } from 'app/proto/K8sHealthcheck_pb';
import { NamespaceService } from 'app/proto/K8sHealthcheck_pb_service';
import { environment } from 'environments/environment';
import { Empty } from 'google-protobuf/google/protobuf/empty_pb';
import { Observable, Subject } from 'rxjs';
import { AuthService } from '../authentication/auth.service';

@Injectable({
  providedIn: 'root'
})
export class K8sNodeService {

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

  getNodes(): Observable<NodeReply[]> {
    // debugger;
    const obs = Observable.create(observer => {

      const getEmptyRequest = new Empty();

      const authmetadata = new grpc.Metadata();
      authmetadata.append('authorization', `Bearer ${this.token}`);

      grpc.unary(NamespaceService.GetNodes, {

        metadata: authmetadata,
        request: getEmptyRequest,
        host: environment.grpc.url,

        onEnd: res => {
          const message = res.message as NodeListReply;
          const status = res.status;

          if (status === grpc.Code.OK && message) {
            observer.next(message.toObject().nodesList);
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
