import { DataSource } from '@angular/cdk/table';
import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ServiceDashboardService } from '../../service-dashboard.service';

@Component({
  selector: 'app-health-check-histories-list',
  templateUrl: './health-check-histories-list.component.html',
  styleUrls: ['./health-check-histories-list.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class HealthCheckHistoriesListComponent implements OnInit, OnDestroy {
  histories: any;
  dataSource: any;
  displayedColumns = ['url', 'status', 'age'];
  selected: any;

  // Private
  private unsubscribeAll: Subject<any>;
  constructor(private serviceDashboardService: ServiceDashboardService) {
    this.unsubscribeAll = new Subject();
  }

  ngOnInit(): void {
    this.histories = [];
    // this.serviceDashboardService.onHealthHistorySelected.next(undefined);


    this.serviceDashboardService.GetHealthCheckResultList().subscribe(
      (data) => {

      },
      (error) => {

      }
    );


    this.dataSource = new HealthCheckHistoryDataSource(this.serviceDashboardService);

    this.serviceDashboardService.onHealthHistoriesChanged
      .pipe(takeUntil(this.unsubscribeAll))
      .subscribe(histories => {
        this.histories = histories;
        // debugger;
      });

    this.serviceDashboardService.onHealthHistorySelected
      .pipe(takeUntil(this.unsubscribeAll))
      .subscribe(selected => {
        this.selected = selected;
      });

  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this.unsubscribeAll.next();
    this.unsubscribeAll.complete();
  }

  onSelect(selected): void {
    this.serviceDashboardService.onHealthHistorySelected.next(selected);
  }





}


export class HealthCheckHistoryDataSource extends DataSource<any>
{

  constructor(
    private serviceDashboard: ServiceDashboardService
  ) {
    super();
  }


  connect(): Observable<any[]> {
    return this.serviceDashboard.onHealthHistoriesChanged;
  }

  /**
   * Disconnect
   */
  disconnect(): void {
  }
}

