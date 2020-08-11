import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DeploymentAppService } from 'app/shared/grpc-services/deployment-app.service';
import { Console } from 'console';
import { fuseAnimations } from '@fuse/animations';

@Component({
  selector: 'app-ns-dashboard',
  animations: fuseAnimations,
  templateUrl: './ns-dashboard.component.html',
  styleUrls: ['./ns-dashboard.component.scss']
})
export class NsDashboardComponent implements OnInit {
  routeparamsSubscription: any;
  currentNamespace: string;
  dataSource = {
    filteredData: []
  };

  constructor(private route: ActivatedRoute, private deploymentService: DeploymentAppService) { }

  ngOnInit(): void {

    this.routeparamsSubscription = this.route.params.subscribe((params: { nsname: string }) => {
      // this.programname = params['programname'];
      // this.envname = params['envname'];
      this.currentNamespace = params.nsname;

      this.deploymentService.getDeployments(this.currentNamespace).subscribe(
        (data) => {

          console.log(data);
        },
        (error) => { }
      );
    });
  }

}
