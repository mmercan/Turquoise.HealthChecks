﻿import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';


// @Injectable()
export class CommonDataStoreService<T> {
    public dataset: Observable<T[]>;
    // public keyProperyName: string;
    // public baseUrl: string;
    private _dataset: BehaviorSubject<T[]>;

    private dataStore: {
        dataset: T[]
    };
    httpGetSubscription: Subscription;
    httpGetAllSubscription: Subscription;
    httpPostSubscription: Subscription;
    httpPutSubscription: Subscription;
    httpDeleteSubscription: Subscription;


    constructor(protected http: HttpClient, protected baseUrl: string, protected keyProperyName: string) {
        // this.baseUrl = 'http://56e05c3213da80110013eba3.mockapi.io/api';
        this.dataStore = { dataset: [] };
        this._dataset = <BehaviorSubject<T[]>>new BehaviorSubject([]);
        this.dataset = this._dataset.asObservable();
    }

    getAll() {
        // const obs = Observable.create(observer => {

        this.httpGetAllSubscription = this.http.get<T[]>(this.baseUrl).subscribe(data => {
            this.dataStore.dataset = data;
            // setTimeout(_ => this._dataset.next(Object.assign({}, this.dataStore).dataset));
            this._dataset.next(Object.assign({}, this.dataStore).dataset);

            console.log('Loading completed');
            // observer.next(Object.assign({}, this.dataStore).dataset);
        }, error => console.log('Could not load items.'));

        // });
        // return obs;
    }

    get(id: number | string) {
        this.httpGetSubscription = this.http.get<T>(`${this.baseUrl}/${id}`).subscribe(data => {
            let notFound = true;

            this.dataStore.dataset.forEach((item, index) => {
                if (this.getKeyField(item) === this.getKeyField(data)) {
                    this.dataStore.dataset[index] = data;
                    notFound = false;
                }
            });

            if (notFound) {
                this.dataStore.dataset.push(data);
            }

            this._dataset.next(Object.assign({}, this.dataStore).dataset);
        }, error => console.log(`Could not load item with the id. ${id}`));
    }

    private getKeyField(item: T): any {
        if (this.keyProperyName && item && item[this.keyProperyName]) {
            return item[this.keyProperyName];
        } else if (item && item['id']) {
            return item['id'];
        } else if (item && item['Id']) {
            return item['Id'];
        } else {
            return undefined;
        }
    }

    create(item: T) {
        this.httpPostSubscription = this.http.post<T>(this.baseUrl, JSON.stringify(item))
            .subscribe(data => {
                this.dataStore.dataset.push(data);
                this._dataset.next(Object.assign({}, this.dataStore).dataset);
            }, error => console.log('Could not create items.'));
    }


    update(item: T) {
        const id = this.getKeyField(item);
        this.httpPutSubscription = this.http.put<T>(this.baseUrl, JSON.stringify(item))
            .subscribe(data => {
                this.dataStore.dataset.forEach((t, i) => {
                    if (this.getKeyField(t) === this.getKeyField(data)) { this.dataStore.dataset[i] = data; }
                });

                this._dataset.next(Object.assign({}, this.dataStore).dataset);
            }, error => console.log('Could not update item.'));
    }

    remove(id: number | string) {
        this.httpDeleteSubscription = this.http.delete(`${this.baseUrl}/${id}`).subscribe(response => {
            this.dataStore.dataset.forEach((t, i) => {
                if (this.getKeyField(t) === id) { this.dataStore.dataset.splice(i, 1); }
            });

            this._dataset.next(Object.assign({}, this.dataStore).dataset);
        }, error => console.log('Could not delete item.'));
    }

    OnDestroy(): void {
        if (this.httpGetSubscription) { this.httpGetSubscription.unsubscribe(); }
        if (this.httpGetAllSubscription) { this.httpGetAllSubscription.unsubscribe(); }
        if (this.httpPostSubscription) { this.httpPostSubscription.unsubscribe(); }
        if (this.httpPutSubscription) { this.httpPutSubscription.unsubscribe(); }
        if (this.httpDeleteSubscription) { this.httpDeleteSubscription.unsubscribe(); }
    }

}
