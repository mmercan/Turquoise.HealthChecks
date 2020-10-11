import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { NodeReply } from 'app/proto/K8sHealthcheck_pb';
import { Observable, Observer } from 'rxjs';
import { K8sNodeService } from '../../shared/grpc-services/k8s-node.service';

@Injectable({
  providedIn: 'root'
})
export class NodeDashboardService implements Resolve<any> {

  nodes: NodeReply[];

  constructor(private nodeservice: K8sNodeService) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {

    const obs = new Observable((observer) => {

      this.nodeservice.getNodes().subscribe(
        data => {
          this.nodes = data;
          observer.next(data);
          observer.complete();

        },
        error => { observer.error(error); }
      );
    });

    return obs;
  }

}
