import { Component, OnInit } from '@angular/core';

import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { NamespaceNgService } from '../../shared/grpc_services/namespace.service';
import { locale as english } from './i18n/en';
import { locale as turkish } from './i18n/tr';

@Component({
    selector: 'sample',
    templateUrl: './sample.component.html',
    styleUrls: ['./sample.component.scss']
})
export class SampleComponent implements OnInit {

    constructor(
        private fuseTranslationLoaderService: FuseTranslationLoaderService,
        private namespaceNgService: NamespaceNgService,
    ) {
        this.fuseTranslationLoaderService.loadTranslations(english, turkish);
        namespaceNgService.Getnamespace().subscribe(
            (ss) => {
                console.log(ss);
            },
            error => { console.log(error); }
        );
    }

    ngOnInit() {

    }
}
