import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { K8sServiceService } from 'app/shared/grpc-services/k8s-service.service';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServiceDashboardService implements Resolve<any> {
  currentNamespace: any;
  currentServiceName: any;


  public serviceDataset: Observable<any>;
  private service_dataset: BehaviorSubject<any>;
  public serviceDataStore: {
    dataset: {
      currentNamespace: any,
      currentServiceName: any,
    },
    services?: any
  };




  public healthcheckDataset: Observable<any>;
  private healthcheck_dataset: BehaviorSubject<any>;
  public healthcheckDataStore: {
    dataset: any
  };
  services: any[];

  constructor(private k8sService: K8sServiceService) {

    this.serviceDataStore = { dataset: { currentNamespace: undefined, currentServiceName: undefined }, services: [] };
    this.service_dataset = new BehaviorSubject([]);
    this.serviceDataset = this.service_dataset.asObservable();



    this.healthcheckDataStore = { dataset: [] };
    this.healthcheck_dataset = new BehaviorSubject([]);
    this.healthcheckDataset = this.service_dataset.asObservable();


  }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {


    this.currentNamespace = route.params.nsname;
    this.currentServiceName = route.params.servicename;


    this.serviceDataStore = {
      dataset: {
        currentNamespace: route.params.nsname,
        currentServiceName: route.params.servicename
      }

    };

    const obs = Observable.create(observer => {

      observer.next(Object.assign({}, this.serviceDataStore).services);


      observer.complete();



    });
    return obs;
  }




  private updateServices(ns: string, servicename: string): void {

    this.k8sService.getServices(ns).subscribe(
      data => {
        this.services = data as any[];
        this.serviceDataStore.services = data as any[];
        this.service_dataset.next(Object.assign({}, this.serviceDataStore));
        // debugger;
      },
      error => { }
    );
  }
}
