import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';

import { NSDashboardResponse } from './ns-dashboard.model';

@Injectable({
  providedIn: 'root'
})
export class NsDashboardService implements Resolve<any> {


  products: any[];
  onProductsChanged: BehaviorSubject<any>;


  public projects: any[];
  public widgets: any[];


  public currentNamespace: string;



  public dataset: Observable<NSDashboardResponse>;
  // tslint:disable-next-line:variable-name
  private _dataset: BehaviorSubject<NSDashboardResponse>;

  private dataStore: {
    currentNamespace: string,
    events: any[],
    services: any[]
  };

  constructor(private httpClient: HttpClient) {

    this.dataStore = {
      currentNamespace: '',
      events: [],
      services: []
    };

    this._dataset = new BehaviorSubject(
      {
        currentNamespace: undefined,
        events: undefined,
        services: undefined
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

  private updateevents(ns: string): void {
    this.httpClient.get('api/k8sevents-events').subscribe(
      data => {
        this.dataStore.events = data as any[];
        this._dataset.next(Object.assign({}, this.dataStore));
      },
      error => { }
    );
  }


  private updateServices(ns: string): void {
    this.httpClient.get('api/k8sevents-services').subscribe(
      data => {
        this.dataStore.services = data as any[];
        this._dataset.next(Object.assign({}, this.dataStore));
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


  // getEvents(): Promise<any> {
  //   return new Promise((resolve, reject) => {
  //     this.httpClient.get('api/k8sevents-events')
  //       .subscribe((response: any) => {
  //         this.events = response;
  //         resolve(response);
  //       }, reject);
  //   });
  // }


}
