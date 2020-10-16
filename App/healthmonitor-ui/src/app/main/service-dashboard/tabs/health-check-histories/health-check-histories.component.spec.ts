import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthCheckHistoriesComponent } from './health-check-histories.component';

describe('HealthCheckHistoriesComponent', () => {
  let component: HealthCheckHistoriesComponent;
  let fixture: ComponentFixture<HealthCheckHistoriesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HealthCheckHistoriesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthCheckHistoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
