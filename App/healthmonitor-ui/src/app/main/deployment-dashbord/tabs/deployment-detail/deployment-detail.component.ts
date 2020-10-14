import { Component, OnInit } from '@angular/core';
import { DeploymentDashbordService } from '../../deployment-dashbord.service';

@Component({
  selector: 'app-deployment-detail',
  templateUrl: './deployment-detail.component.html',
  styleUrls: ['./deployment-detail.component.scss']
})
export class DeploymentDetailComponent implements OnInit {
  dataStore: { dataset: any; };
  currentDeploymentName: any;
  currentNamespace: any;
  panelOpenState = true;
  deployment: any;
  scaleHistory: [] = [];
  constructor(private deploymentDashbordService: DeploymentDashbordService) { }

  ngOnInit(): void {

    this.currentNamespace = this.deploymentDashbordService.currentNamespace;
    this.currentDeploymentName = this.deploymentDashbordService.currentDeploymentName;


    this.deploymentDashbordService.deploymentDataset.subscribe(
      (data) => {
        this.deployment = data.deployment;
      },
      (error) => {

      }
    );


  }

}
