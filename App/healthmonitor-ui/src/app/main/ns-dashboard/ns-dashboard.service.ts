import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';

import { NSDashboardResponse } from './ns-dashboard.model';
import { K8sServiceService } from 'app/shared/grpc-services/k8s-service.service';
import { K8sEventService } from 'app/shared/grpc-services/k8s-event.service';
import { K8sHealthcheckService } from 'app/shared/grpc-services/k8s-healthcheck.service';
import { K8sDeploymentService } from 'app/shared/grpc-services/k8s-deployment.service';
import { DeploymentReply } from 'app/proto/K8sHealthcheck_pb';

@Injectable({
  providedIn: 'root'
})
export class NsDashboardService implements Resolve<any> {


  products: any[];
  onProductsChanged: BehaviorSubject<any>;


  public projects: any[];
  public widgets: any[];
  public services: any[] = [];
  public events: any[] = [];
  public stats = {};
  public currentNamespace: string;
  public deployments: DeploymentReply[] = [];


  public dataset: Observable<NSDashboardResponse>;
  // tslint:disable-next-line:variable-name
  private _dataset: BehaviorSubject<NSDashboardResponse>;

  private dataStore: {
    currentNamespace: string,
    events: any[],
    services: any[]
    stats: any,
    deployments: DeploymentReply[]
  };

  constructor(
    private httpClient: HttpClient,
    private k8sService: K8sServiceService,
    private k8sEventService: K8sEventService,
    private k8sHealthcheckService: K8sHealthcheckService,
    private k8sDeploymentService: K8sDeploymentService) {

    this.dataStore = {
      currentNamespace: '',
      events: [],
      services: [],
      stats: {},
      deployments: []
    };

    this._dataset = new BehaviorSubject(
      {
        currentNamespace: undefined,
        events: undefined,
        services: undefined,
        stats: undefined,
        deployments: undefined
      }
    );

    this.dataset = this._dataset.asObservable();

    this.onProductsChanged = new BehaviorSubject({});
  }


  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {


    return new Promise((resolve, reject) => {
      this.currentNamespace = route.params.nsname;

      this.updatens(route.params.nsname);
      this.updateevents(route.params.nsname);
      this.updateServices(route.params.nsname);
      this.updateStats(route.params.nsname);
      this.updateDeployments(route.params.nsname);
      Promise.all([
        this.getProjects(),
        this.getWidgets(),
        this.getProducts()
        // this.getEvents()
      ]).then(
        () => {
          resolve();
        },
        reject
      );
    });
  }


  private updatens(ns: string): void {
    this.dataStore.currentNamespace = ns;
    this._dataset.next(Object.assign({}, this.dataStore));

  }


  private updateStats(ns: string): void {
    this.stats = {};
    this.dataStore.stats = {} as any;
    this._dataset.next(Object.assign({}, this.dataStore));

    this.k8sHealthcheckService.getStats(ns).subscribe(
      data => {
        this.stats = data as any;
        this.dataStore.stats = data as any;
        this._dataset.next(Object.assign({}, this.dataStore));
      },
      error => { }
    );

  }

  private updateevents(ns: string): void {

    this.k8sEventService.getEvents(ns).subscribe(
      data => {
        this.events = data as any[];
        this.dataStore.events = data as any[];
        this._dataset.next(Object.assign({}, this.dataStore));
      },
      error => { }
    );


    // this.httpClient.get('api/k8sevents-events').subscribe(
    //   data => {
    //     this.dataStore.events = data as any[];
    //     this._dataset.next(Object.assign({}, this.dataStore));
    //   },
    //   error => { }
    // );
  }


  private updateServices(ns: string): void {

    this.k8sService.getServices(ns).subscribe(
      data => {
        this.services = data as any[];
        this.dataStore.services = data as any[];
        this._dataset.next(Object.assign({}, this.dataStore));
        // debugger;
      },
      error => { }
    );

    // this.httpClient.get('api/k8sevents-services').subscribe(
    //   data => {
    //     this.services = data as any[];
    //     this.dataStore.services = data as any[];
    //     this._dataset.next(Object.assign({}, this.dataStore));
    //   },
    //   error => { }
    // );
  }

  private updateDeployments(ns: string): void {
    this.k8sDeploymentService.getDeployments(ns).subscribe(
      data => {
        this.deployments = data as any[];
        this.dataStore.deployments = data as any[];
        this._dataset.next(Object.assign({}, this.dataStore));
        // debugger;
      },
      error => { }
    );
  }

  getProjects(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.httpClient.get('api/project-dashboard-projects')
        .subscribe((response: any) => {
          this.projects = response;
          resolve(response);
        }, reject);
    });
  }


  getWidgets(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.httpClient.get('api/project-dashboard-widgets')
        .subscribe((response: any) => {
          this.widgets = response;
          resolve(response);
        }, reject);
    });
  }


  getProducts(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.httpClient.get('api/e-commerce-products')
        .subscribe((response: any) => {
          this.products = response;
          this.onProductsChanged.next(this.products);
          resolve(response);
        }, reject);
    });
  }

}
