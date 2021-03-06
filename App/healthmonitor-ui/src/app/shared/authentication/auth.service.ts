import { Injectable, OnDestroy } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Subscription, Observable, Subject } from 'rxjs';
// import { Notification, NotificationService } from '../notification/notification.service';

import { environment } from 'environments/environment';

import { AdalService } from './adal/adal.service';
import { LocalAuthService } from './local-auth/local-auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements OnDestroy {
  private tokeyKey = 'token';
  private internal: any;
  private token: string;
  private isLoggedinValue: boolean;
  private status = new Subject<boolean>();
  getUserSubscription: Subscription;
  httpGetSubscription: Subscription;
  httpDeleteSubscription: Subscription;
  httpPutSubscription: Subscription;
  httpPostSubscription: Subscription;

  // public user: Observable<any>;



  constructor(

    private http: HttpClient,
    // private notificationService: NotificationService,
    private adalService: AdalService,
    private localAuthService: LocalAuthService
  ) {

  }

  private handleError(error: any, observer: any, errorMessage: string) {
    console.error('An error occurred', error);
    if (error && error['_body']) { // check response here.
      const message = error.json();
    }
    // this.notificationService.showError(errorMessage, <string>error, true);
    observer.error(error.message || error);
  }

  checkLogin(): Observable<boolean> {
    if (environment.authenticationType === 'Adal') {
      const obs = Observable.create(observer => {
        console.log('checkLogin : ' + this.adalService.userInfo.authenticated);
        observer.next(this.adalService.userInfo.authenticated);
      });
      return obs;
    } else {
      return this.localAuthService.isLoggedin();
    }
  }


  login(userName?: string, password?: string): Observable<any> {
    if (environment.authenticationType === 'Adal') {
      this.adalService.login();
      const obs = Observable.create(observer => {
      });
      return obs;
    } else {
      return this.localAuthService.login(userName, password);
    }
  }


  logout() {
    if (environment.authenticationType === 'Adal') {
      this.adalService.logOut();

    } else {
      this.localAuthService.logout();
    }
  }

  authenticated(): boolean {
    if (environment.authenticationType === 'Adal') {
      return this.adalService.userInfo.authenticated;
    } else {
      return this.localAuthService.checkLogin();
    }
  }

  getUserInfo(): Observable<adal.User> {
    const obs = Observable.create(observer => {
      if (environment.authenticationType === 'Adal') {
        this.getUserSubscription = this.adalService.getUser.subscribe(
          data => { observer.next(data); },
          error => { observer.error(error); });
      } else {
        this.getUserSubscription = this.localAuthService.user.subscribe(
          data => { observer.next(data); },
          error => { observer.error(error); });
      }
    });
    return obs;
  }

  getLocalToken(): string {
    if (environment.authenticationType === 'Adal') {
      return this.adalService.getCachedToken(environment.adalConfig.clientId);
    } else {
      return this.localAuthService.getLocalToken();
    }
  }

  handleWindowCallback() {
    if (environment.authenticationType === 'Adal') {
      return this.adalService.handleWindowCallback();
    } else {
      console.log('authentication is local no need for handleWindowCallback');
    }
  }

  authGet(url): Observable<any> {
    const headers = this.initAuthHeaders();
    const obs = Observable.create(observer => {
      // let result = null;
      this.httpGetSubscription = this.http.get(url, { headers: headers, observe: 'response' })
        .subscribe(
          response => {
            // if (response.json) {
            //   result = response.json();
            // } else if (response.text) {
            //   result = response.text();
            // }
            observer.next(response.body);
          },
          error => { this.handleError(error, observer, 'Failed Get on' + url); }
        );

    });
    return obs;
  }

  authPost(url: string, body: any): Observable<any> {
    const obs = Observable.create(observer => {
      const headers = this.initAuthHeaders();
      //  let result = null;
      this.httpPostSubscription = this.http.post(url, body, { headers: headers, observe: 'response' })
        .subscribe(
          response => {
            if (response.ok) {
              // if (response.text() === '') {
              observer.next(response.body);
              // } else if (response.json) {
              //   result = response.json();
              // } else if (response.text) {
              //   result = response.text();
              // }
            }
            observer.next(response.body);
          },
          error => { this.handleError(error, observer, 'Failed Post on ' + url); }
        );
    });
    return obs;
  }

  authPut(url: string, body: any): Observable<any> {
    const obs = Observable.create(observer => {
      const headers = this.initAuthHeaders();
      // let result = null;
      this.httpPutSubscription = this.http.put(url, body, { headers: headers, observe: 'response' })
        .subscribe(
          response => {
            if (response.ok) {
              // if (response.text() === '') {
              //   observer.next(result);
              // } else if (response.json) {
              //   result = response.json();
              // } else if (response.text) {
              //   result = response.text();
              // }
              observer.next(response.body);
            }

            observer.next(response.body);
          },
          error => { this.handleError(error, observer, 'Failed Put on ' + url); }
        );
    });
    return obs;
  }

  authDelete(url): Observable<any> {
    const headers = this.initAuthHeaders();
    const obs = Observable.create(observer => {
      // let result = null;
      this.httpDeleteSubscription = this.http.delete(url, { headers: headers, observe: 'response' })
        .subscribe(
          response => {
            // if (response.json) {
            //   result = response.json();
            // } else if (response.text) {
            //   result = response.text();
            // }
            observer.next(response.body);
          },
          error => { this.handleError(error, observer, 'Failed Delete on ' + url); }
        );

    });
    return obs;
  }


  private initAuthHeaders(): HttpHeaders {
    const token = this.getLocalToken();
    if (token == null) { throw new Error('No token'); }

    const headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    headers.append('Authorization', 'Bearer ' + token);
    return headers;
  }

  ngOnDestroy(): void {
    if (this.getUserSubscription) { this.getUserSubscription.unsubscribe(); }
    if (this.httpGetSubscription) { this.httpGetSubscription.unsubscribe(); }
    if (this.httpDeleteSubscription) { this.httpDeleteSubscription.unsubscribe(); }
    if (this.httpPutSubscription) { this.httpPutSubscription.unsubscribe(); }
    if (this.httpPostSubscription) { this.httpPostSubscription.unsubscribe(); }
  }
}
