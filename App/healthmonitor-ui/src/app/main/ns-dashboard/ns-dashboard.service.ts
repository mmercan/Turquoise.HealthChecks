import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable, BehaviorSubject } from 'rxjs';

import { NSDashboardResponse } from './ns-dashboard.model';

@Injectable({
  providedIn: 'root'
})
export class NsDashboardService implements Resolve<any> {



  // private _todos = new BehaviorSubject<any[]>([]);
  // readonly todos = this._todos.asObservable();
  // get todos() {
  //   return this._todos.asObservable();
  // }

  // private datastore:
  //   {
  //     currentNamespace: string,
  //     events: any[],
  //     services: any[]
  //   };

  public projects: any[];
  public widgets: any[];
  public events: any[];

  public currentNamespace: string;



  public dataset: Observable<any>;
  private _dataset: BehaviorSubject<any>;

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

    this._dataset = new BehaviorSubject({});
    this.dataset = this._dataset.asObservable();
  }


  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {


    return new Promise((resolve, reject) => {

      // this.deploymentService.getDeployments(this.currentNamespace).subscribe(
      //   (data) => {
      //     console.log(data);
      //   },
      //   (error) => { }
      // );

      this.currentNamespace = route.params.nsname;


      Promise.all([
        this.getProjects(),
        this.getWidgets(),
        this.getEvents()
      ]).then(
        () => {

          resolve();
          this.updatens(route.params.nsname);
        },
        reject
      );
    });
  }


  private updatens(ns: string): void {
    this.dataStore.currentNamespace = ns;
    this._dataset.next(Object.assign({}, this.dataStore));

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


  getEvents(): Promise<any> {
    return new Promise((resolve, reject) => {
      this.httpClient.get('api/k8sevents')
        .subscribe((response: any) => {
          this.events = response;
          resolve(response);
        }, reject);
    });
  }


}
