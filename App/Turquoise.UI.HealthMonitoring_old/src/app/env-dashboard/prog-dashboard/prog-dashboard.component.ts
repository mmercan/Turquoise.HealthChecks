import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-prog-dashboard',
  templateUrl: './prog-dashboard.component.html',
  styleUrls: ['./prog-dashboard.component.scss']
})
export class ProgDashboardComponent implements OnInit {
  programname: any;
  envname: any;
  routeparamsSubscription: Subscription;
  currentNamespace: string;
  constructor(
    private route: ActivatedRoute) {
  }

  ngOnInit(): void {

    this.routeparamsSubscription = this.route.params.subscribe((params: { nsname: string }) => {
      // this.programname = params['programname'];
      // this.envname = params['envname'];
      this.currentNamespace = params.nsname;
    });
  }

}
