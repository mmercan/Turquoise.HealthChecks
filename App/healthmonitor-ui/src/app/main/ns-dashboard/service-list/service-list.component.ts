import { Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { DataSource } from '@angular/cdk/collections';
import { BehaviorSubject, fromEvent, merge, Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, map, mergeMap } from 'rxjs/operators';

import { fuseAnimations } from '@fuse/animations';
import { FuseUtils } from '@fuse/utils';

import { takeUntil } from 'rxjs/operators';
import { NsDashboardService } from '../ns-dashboard.service';

@Component({
  selector: 'app-service-list',
  templateUrl: './service-list.component.html',
  styleUrls: ['./service-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class ServiceListComponent implements OnInit {
  dataSource: FilesDataSource | null;
  displayedColumns = ['id', 'name', 'price', 'quantity', 'active'];

  @ViewChild(MatPaginator, { static: true })
  paginator: MatPaginator;

  @ViewChild(MatSort, { static: true })
  sort: MatSort;

  @ViewChild('filter', { static: true })
  filter: ElementRef;



  private unsubscribeAll: Subject<any>;

  constructor(
    private nsDashboardService: NsDashboardService
  ) {
    // Set the private defaults
    this.unsubscribeAll = new Subject();
  }


  ngOnInit(): void {
    this.dataSource = new FilesDataSource(this.nsDashboardService, this.paginator, this.sort);

    // fromEvent(this.filter.nativeElement, 'keyup')
    //   .pipe(
    //     takeUntil(this.unsubscribeAll),
    //     debounceTime(150),
    //     distinctUntilChanged()
    //   )
    //   .subscribe(() => {
    //     if (!this.dataSource) {
    //       return;
    //     }

    //     this.dataSource.filter = this.filter.nativeElement.value;
    //   });
  }

}



export class FilesDataSource extends DataSource<any>
{
  private filterChange = new BehaviorSubject('');
  private filteredDataChange = new BehaviorSubject('');
  private services;

  constructor(
    private nsDashboardService: NsDashboardService,
    private matPaginator: MatPaginator,
    private matSort: MatSort
  ) {
    super();

    // this.filteredData = this.nsDashboardService.products;

    this.nsDashboardService.dataset.subscribe(
      data => {
        this.filteredData = data.services;
        this.services = data.services;
      });
  }

  connect(): Observable<any[]> {
    const displayDataChanges = [
      this.nsDashboardService.dataset,
      this.nsDashboardService.onProductsChanged,
      this.matPaginator.page,
      this.filterChange,
      this.matSort.sortChange
    ];

    return merge(...displayDataChanges)
      .pipe(
        map((x, y) => {
          let data = this.nsDashboardService.services.slice();

          debugger;
          data = this.filterData(data);

          this.filteredData = [...data];

          data = this.sortData(data);

          // Grab the page's slice of data.
          const startIndex = this.matPaginator.pageIndex * this.matPaginator.pageSize;
          return data.splice(startIndex, this.matPaginator.pageSize);
        }
        ));
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Accessors
  // -----------------------------------------------------------------------------------------------------

  // Filtered data
  get filteredData(): any {
    return this.filteredDataChange.value;
  }

  set filteredData(value: any) {
    this.filteredDataChange.next(value);
  }

  // Filter
  get filter(): string {
    return this.filterChange.value;
  }

  set filter(filter: string) {
    this.filterChange.next(filter);
  }

  // -----------------------------------------------------------------------------------------------------
  // @ Public methods
  // -----------------------------------------------------------------------------------------------------

  filterData(data): any {
    if (!this.filter) {
      return data;
    }
    return FuseUtils.filterArrayByString(data, this.filter);
  }

  sortData(data): any[] {
    if (!this.matSort.active || this.matSort.direction === '') {
      return data;
    }

    return data.sort((a, b) => {
      let propertyA: number | string = '';
      let propertyB: number | string = '';

      switch (this.matSort.active) {
        case 'id':
          [propertyA, propertyB] = [a.id, b.id];
          break;
        case 'name':
          [propertyA, propertyB] = [a.name, b.name];
          break;
        case 'categories':
          [propertyA, propertyB] = [a.categories[0], b.categories[0]];
          break;
        case 'price':
          [propertyA, propertyB] = [a.priceTaxIncl, b.priceTaxIncl];
          break;
        case 'quantity':
          [propertyA, propertyB] = [a.quantity, b.quantity];
          break;
        case 'active':
          [propertyA, propertyB] = [a.active, b.active];
          break;
      }

      const valueA = isNaN(+propertyA) ? propertyA : +propertyA;
      const valueB = isNaN(+propertyB) ? propertyB : +propertyB;

      return (valueA < valueB ? -1 : 1) * (this.matSort.direction === 'asc' ? 1 : -1);
    });
  }

  disconnect(): void {
  }
}

