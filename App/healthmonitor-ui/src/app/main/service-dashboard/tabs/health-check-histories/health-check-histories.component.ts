import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { fuseAnimations } from '@fuse/animations';

@Component({
  selector: 'app-health-check-histories',
  templateUrl: './health-check-histories.component.html',
  styleUrls: ['./health-check-histories.component.scss'],
  encapsulation: ViewEncapsulation.None,
  animations: fuseAnimations
})
export class HealthCheckHistoriesComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {

  }

}
