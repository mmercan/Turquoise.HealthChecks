import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { K8sDeploymentService } from 'app/shared/grpc-services/k8s-deployment.service';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DeploymentDashbordService implements Resolve<any> {


  public currentNamespace: any;
  public currentDeploymentName: any;
  public deploymentDataset: Observable<any>;
  private deployment_dataset: BehaviorSubject<any>;
  deploymentDataStore: { dataset: { currentNamespace: any; currentDeploymentName: any; }; deployment: {}, scaleHistory: [] };


  constructor(private k8sDeployment: K8sDeploymentService) {

    this.deploymentDataStore = {
      dataset: { currentNamespace: undefined, currentDeploymentName: undefined },
      deployment: {}, scaleHistory: []
    };
    this.deployment_dataset = new BehaviorSubject([]);
    this.deploymentDataset = this.deployment_dataset.asObservable();


  }


  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {


    this.currentNamespace = route.params.nsname;
    this.currentDeploymentName = route.params.deploymentname;


    this.deploymentDataStore = {
      dataset: {
        currentNamespace: route.params.nsname,
        currentDeploymentName: route.params.deploymentname
      },
      deployment: {},
      scaleHistory: []
    };

    const obs = Observable.create(observer => {
      this.k8sDeployment.getDeployment(
        this.deploymentDataStore.dataset.currentNamespace,
        this.deploymentDataStore.dataset.currentDeploymentName).subscribe(
          data => {

            this.deploymentDataStore.deployment = data as any;
            this.deployment_dataset.next(Object.assign({}, this.deploymentDataStore));

            observer.next(Object.assign({}, this.deploymentDataStore).deployment);
            observer.complete();

            // debugger;
          },
          error => { }
        );

      // .getService(ns, servicename).subscribe(
      //   data => {

      //     this.deploymentDataStore.deployment = data as any;
      //     this.service_dataset.next(Object.assign({}, this.serviceDataStore));
      //     // debugger;
      //   },
      //   error => { }
      // );





    });
    return obs;
  }

}
