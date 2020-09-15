import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

@Pipe({ name: 'momentAge' })
export class MomentAgePipe implements PipeTransform {

    transform(value: any, args: any[] = []): string {
        if (value && value.seconds) {
            const day = moment.unix(value.seconds).fromNow(); // .utc();
            return day;  // value ? String(value).replace(/([A-Z])/g, (g) => `-${g[0].toLowerCase()}`) : '';
        }
    }
}
