import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthCheckHistoriesSidebarComponent } from './health-check-histories-sidebar.component';

describe('HealthCheckHistoriesSidebarComponent', () => {
  let component: HealthCheckHistoriesSidebarComponent;
  let fixture: ComponentFixture<HealthCheckHistoriesSidebarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HealthCheckHistoriesSidebarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthCheckHistoriesSidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
