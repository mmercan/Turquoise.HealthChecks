import { Injectable } from '@angular/core';
import { FuseNavigationService } from '@fuse/components/navigation/navigation.service';
import { AuthService } from 'app/shared/authentication/auth.service';

import { grpc } from '@improbable-eng/grpc-web';
import { DeploymentReply, NamespaceListReply, NamespaceReply } from '../../proto/K8sHealthcheck_pb';
import { NamespaceServiceClient } from '../../proto/K8sHealthcheck_pb_service';
import { NamespaceService } from '../../proto/K8sHealthcheck_pb_service';
import { Empty } from 'google-protobuf/google/protobuf/empty_pb';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NamespaceAppService {
  token: string;
  private unsubscribeAll: Subject<any>;

  constructor(
    private authService: AuthService,
    private fuseNavigation: FuseNavigationService
  ) {

    authService.getUserInfo().subscribe(
      (user) => { this.token = authService.getLocalToken(); }
    );


  }

  getNameSpaces(): Observable<NamespaceReply[]> {
    // debugger;
    const obs = Observable.create(observer => {

      const getNamespaceRequest = new Empty();

      const authmetadata = new grpc.Metadata();
      authmetadata.append('authorization', `Bearer ${this.token}`);

      grpc.unary(NamespaceService.GetNamespaces, {

        metadata: authmetadata,
        request: getNamespaceRequest,
        host: environment.grpc.url,

        onEnd: res => {
          const message = res.message as NamespaceListReply;
          const status = res.status;

          if (status === grpc.Code.OK && message) {
            observer.next(message.getNamespacesList());
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

  fillNamespaceNavigations(): void {
    this.fuseNavigation.getNavigationItem('namespace');
    const allokColor = '#09d261';
    const warningColor = '#FF6F00';
    const errorColor = '#F44336';
    this.getNameSpaces().subscribe(
      (data) => {
        data.forEach(element => {
          const ns = element.getNamespace();
          const nswarning = element.getWarning();
          const nserror = element.getErrors();
          const total = nswarning + nserror;
          const seconds = element.getCreationdate().toDate();
          let badgetext;
          if (this.isit24hoursold(seconds)) {
            badgetext = 'Hot';
          } else if (this.isit7daysold(seconds)) {
            badgetext = 'New';
          }

          const item = {
            id: ns,
            title: ns,
            type: 'item',
            icon: 'folder',
            url: '/ns/' + ns,
            badge: undefined
          };
          if (total > 0 || badgetext) {
            let bgc = '#09d261';
            if (nserror > 0) {
              bgc = errorColor;
              badgetext = total;
            } else if (nswarning > 0) {
              bgc = warningColor;
              badgetext = total;
            } else {
              bgc = allokColor;
            }

            const badge = {
              title: badgetext,
              bg: bgc,
              fg: '#FFFFFF'
            };
            item.badge = badge;
          }
          this.fuseNavigation.addNavigationItem(item, 'namespaces');
        });
      },
      (error) => {

      }
    );

  }

  isit24hoursold(nsdate: Date): boolean {
    const OneDay = new Date().getTime() - (1 * 24 * 60 * 60 * 1000);
    const saveddate = nsdate.getTime();
    if (saveddate > OneDay) {
      return true;
    }

  }

  isit7daysold(nsdate: Date): boolean {

    const SevenDays = new Date().getTime() - (30 * 24 * 60 * 60 * 1000);
    const saveddate = nsdate.getTime();
    if (saveddate > SevenDays) {
      return true;
    }


  }

}
