import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServiceDashboardService implements Resolve<any> {
  currentNamespace: any;
  currentServiceName: any;


  public dataset: Observable<any>;
  private _dataset: BehaviorSubject<any>;

  public dataStore: {
    dataset: any
  };

  constructor() { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {


    this.currentNamespace = route.params.nsname;
    this.currentServiceName = route.params.servicename;


    this.dataStore = {
      dataset: {
        currentNamespace: route.params.nsname,
        currentServiceName: route.params.servicename
      }
    };

    const obs = Observable.create(observer => {

      observer.next(Object.assign({}, this.dataStore).dataset);


      observer.complete();



    });
    return obs;
  }
}
