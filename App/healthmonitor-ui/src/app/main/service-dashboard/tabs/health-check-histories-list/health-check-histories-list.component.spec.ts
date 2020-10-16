import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthCheckHistoriesListComponent } from './health-check-histories-list.component';

describe('HealthCheckHistoriesListComponent', () => {
  let component: HealthCheckHistoriesListComponent;
  let fixture: ComponentFixture<HealthCheckHistoriesListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HealthCheckHistoriesListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthCheckHistoriesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
