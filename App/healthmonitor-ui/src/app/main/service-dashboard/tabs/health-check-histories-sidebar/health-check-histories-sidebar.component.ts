import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ServiceDashboardService } from '../../service-dashboard.service';

@Component({
  selector: 'app-health-check-histories-sidebar',
  templateUrl: './health-check-histories-sidebar.component.html',
  styleUrls: ['./health-check-histories-sidebar.component.scss']
})
export class HealthCheckHistoriesSidebarComponent implements OnInit, OnDestroy {
  private unsubscribeAll: Subject<any>;
  healthcheckresult: any;


  constructor(private serviceDashboardService: ServiceDashboardService) {
    this.unsubscribeAll = new Subject();
  }

  ngOnInit(): void {
    this.serviceDashboardService.onHealthHistorySelected
      .pipe(takeUntil(this.unsubscribeAll))
      .subscribe(data => {

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
        this.healthcheckresult = data;
        this.healthcheckresult.result = result;


        //  debugger;


      });

  }


  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this.unsubscribeAll.next();
    this.unsubscribeAll.complete();
  }
}
