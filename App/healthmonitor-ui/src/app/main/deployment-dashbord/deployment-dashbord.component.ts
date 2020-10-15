import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';
import { DeploymentScaleHistoryReply } from 'app/proto/K8sHealthcheck_pb';
import { K8sDeploymentService } from 'app/shared/grpc-services/k8s-deployment.service';
import { DeploymentDashbordService } from './deployment-dashbord.service';


@Component({
  selector: 'app-deployment-dashbord',
  templateUrl: './deployment-dashbord.component.html',
  styleUrls: ['./deployment-dashbord.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class DeploymentDashbordComponent implements OnInit {
  dataStore: { dataset: any; };
  currentDeploymentName: any;
  currentNamespace: any;
  panelOpenState = true;
  deployment: any;
  deploymentHistory: DeploymentScaleHistoryReply[];

  constructor(
    private deploymentDashbordService: DeploymentDashbordService,
    private k8sDeploymentService: K8sDeploymentService) { }

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
