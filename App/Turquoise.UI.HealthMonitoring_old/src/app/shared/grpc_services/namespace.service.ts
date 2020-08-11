import { Injectable } from '@angular/core';
import { grpc } from "@improbable-eng/grpc-web";
import { DeploymentReply, NamespaceListReply } from '../../proto/K8sHealthcheck_pb';
import { NamespaceServiceClient } from '../../proto/K8sHealthcheck_pb_service';
import { NamespaceService } from '../../proto/K8sHealthcheck_pb_service';
import { Empty } from 'google-protobuf/google/protobuf/empty_pb';
import { Observable } from 'rxjs';

import { AuthService } from '../authentication/auth.service';

@Injectable({
  providedIn: 'root'
})
export class NamespaceNgService {
  token = '';
  constructor(private authService: AuthService) {

    authService.getUserInfo().subscribe(

      (user) => { this.token = authService.getLocalToken(); }
    );
  }


  Getnamespace(): Observable<string[]> {
    // debugger;
    const obs = Observable.create(observer => {

      const getNamespaceRequest = new Empty();


      const authmetadata = new grpc.Metadata();
      authmetadata.append('authorization', `Bearer ${this.token}`);


      grpc.unary(NamespaceService.GetNamespaces, {

        metadata: authmetadata,
        request: getNamespaceRequest,
        host: 'http://localhost:80', //https://grpcwebdemo.azurewebsites.net (Windows App Service)
        onEnd: res => {
          const { status, message } = res;
          const nmspace: NamespaceListReply = message as NamespaceListReply;
          if (status === grpc.Code.OK && nmspace) {
            observer.next(nmspace.getNamespacesList());
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
