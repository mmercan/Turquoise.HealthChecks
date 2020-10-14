import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeploymentDashbordComponent } from './deployment-dashbord.component';

describe('DeploymentDashbordComponent', () => {
  let component: DeploymentDashbordComponent;
  let fixture: ComponentFixture<DeploymentDashbordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeploymentDashbordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeploymentDashbordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
