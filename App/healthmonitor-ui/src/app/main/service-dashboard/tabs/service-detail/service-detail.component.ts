import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { ServiceDashboardService } from '../../service-dashboard.service';

@Component({
  selector: 'app-service-detail',
  templateUrl: './service-detail.component.html',
  styleUrls: ['./service-detail.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class ServiceDetailComponent implements OnInit {
  service: any;
  healthcheckresult: any;
  currentNamespace: any;
  currentDeploymentName: any;

  constructor(
    private serviceDashboardService: ServiceDashboardService) { }

  ngOnInit(): void {
    this.serviceDashboardService.serviceDataset.subscribe(
      (data) => {
        this.service = data.service;
        // debugger;
      },
      (error) => {

      }
    );



    this.serviceDashboardService.healthcheckDataset.subscribe(
      (data) => {
        this.healthcheckresult = data.healthCheckResult;
        // debugger;
      },
      (error) => {

      }
    );

  }

}
