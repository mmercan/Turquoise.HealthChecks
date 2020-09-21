import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { ServiceReply } from 'app/proto/K8sHealthcheck_pb';
import { K8sHealthcheckService } from 'app/shared/grpc-services/k8s-healthcheck.service';
import { K8sServiceService } from 'app/shared/grpc-services/k8s-service.service';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServiceDashboardService implements Resolve<any> {
  currentNamespace: any;
  currentServiceName: any;
  healthCheckResult: any;

  public serviceDataset: Observable<any>;
  private service_dataset: BehaviorSubject<any>;
  public serviceDataStore: {
    dataset: {
      currentNamespace: any,
      currentServiceName: any,
    },
    service?: any
  };




  public healthcheckDataset: Observable<any>;
  private healthcheck_dataset: BehaviorSubject<any>;
  public healthcheckDataStore: {
    dataset: any,
    healthCheckResult: any;
  };
  services: any[];
  service: ServiceReply;

  constructor(private k8sService: K8sServiceService, private k8sHealthcheckService: K8sHealthcheckService) {

    this.serviceDataStore = { dataset: { currentNamespace: undefined, currentServiceName: undefined }, service: {} };
    this.service_dataset = new BehaviorSubject([]);
    this.serviceDataset = this.service_dataset.asObservable();



    this.healthcheckDataStore = { dataset: [], healthCheckResult: {} };
    this.healthcheck_dataset = new BehaviorSubject([]);
    this.healthcheckDataset = this.healthcheck_dataset.asObservable();


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
      this.updateService(this.currentNamespace, this.currentServiceName);
      this.updateHealthcheckResult(this.currentNamespace, this.currentServiceName);

      observer.next(Object.assign({}, this.serviceDataStore).service);


      observer.complete();



    });
    return obs;
  }

  private updateHealthcheckResult(ns: string, servicename: string): void {
    this.k8sHealthcheckService.getLastHealthCheckResult(ns, servicename).subscribe(
      data => {
        let result = {} as any;
        if (data.stringresult) {
          try {
            result = JSON.parse(data.stringresult);
            data.stringresult = null;

            if (Array.isArray(result.results)) {
              result.results.forEach(p => {

                if (p.data && typeof p.data === 'object' && p.data !== null) {
                  p.dataisobject = true;
                }
              });
            }

          } catch (e) {
            result = { jsonParseError: true };
          }
        } else {
          result = { noJson: true };
        }
        this.healthCheckResult = data;
        this.healthCheckResult.result = result;
        this.healthcheckDataStore.healthCheckResult = data as any;
        this.healthcheck_dataset.next(Object.assign({}, this.healthcheckDataStore));
        //  debugger;
      },
      error => { }
    );
  }


  private updateService(ns: string, servicename: string): void {

    this.k8sService.getService(ns, servicename).subscribe(
      data => {
        this.service = data;
        this.serviceDataStore.service = data as any;
        this.service_dataset.next(Object.assign({}, this.serviceDataStore));
        // debugger;
      },
      error => { }
    );
  }
}
