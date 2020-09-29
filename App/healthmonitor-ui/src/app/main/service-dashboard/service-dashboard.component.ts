import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { ServiceDashboardService } from './service-dashboard.service';

@Component({
  selector: 'app-service-dashboard',
  templateUrl: './service-dashboard.component.html',
  styleUrls: ['./service-dashboard.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class ServiceDashboardComponent implements OnInit {
  dataStore: { dataset: any; };
  currentServiceName: any;
  currentNamespace: any;
  panelOpenState = true;
  service: any;
  constructor(private serviceDashboardService: ServiceDashboardService) {





  }

  ngOnInit(): void {
    // this.dataStore = this.serviceDashboardService.dataStore;
    this.currentNamespace = this.serviceDashboardService.currentNamespace;
    this.currentServiceName = this.serviceDashboardService.currentServiceName;


    this.serviceDashboardService.serviceDataset.subscribe(
      (data) => {
        this.service = data.service;
        // debugger;
      },
      (error) => {

      }
    );
  }

}
