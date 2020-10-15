import { Component, OnInit } from '@angular/core';
import { DeploymentScaleHistoryReply } from 'app/proto/K8sHealthcheck_pb';
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
  deploymentHistory: DeploymentScaleHistoryReply[];
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

    this.deploymentDashbordService.getCurrentDeploymentHistoryList().subscribe(
      (data) => {
        this.deploymentHistory = data;
      },
      (error) => {

      }
    );

  }

}
