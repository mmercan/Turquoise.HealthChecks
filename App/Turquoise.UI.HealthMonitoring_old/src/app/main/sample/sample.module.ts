import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { MatGridListModule } from '@angular/material/grid-list';
import { FuseSharedModule } from '@fuse/fuse-shared.module';

import { SampleComponent } from './sample.component';
import { SharedModule } from '../../shared/shared.module';
const routes = [
    {
        path: '',
        component: SampleComponent
    }
];

@NgModule({
    declarations: [
        SampleComponent
    ],
    imports: [
        RouterModule.forChild(routes),
        MatGridListModule,
        TranslateModule,
        SharedModule,
        FuseSharedModule
    ],
    exports: [
        SampleComponent
    ]
})

export class SampleModule {
}