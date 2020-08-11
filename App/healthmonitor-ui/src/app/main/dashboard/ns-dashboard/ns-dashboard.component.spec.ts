import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NsDashboardComponent } from './ns-dashboard.component';

describe('NsDashboardComponent', () => {
  let component: NsDashboardComponent;
  let fixture: ComponentFixture<NsDashboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NsDashboardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
