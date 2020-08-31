import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { FuseSharedModule } from '@fuse/shared.module';

import { FuseCountdownModule, FuseHighlightModule, FuseMaterialColorPickerModule, FuseWidgetModule } from '@fuse/components';
import { DocsComponentsCountdownComponent } from 'app/main-fuse/documentation/components/countdown/countdown.component';
import { DocsComponentsHighlightComponent } from 'app/main-fuse/documentation/components/highlight/highlight.component';
import { DocsComponentsMaterialColorPickerComponent } from 'app/main-fuse/documentation/components/material-color-picker/material-color-picker.component';
import { DocsComponentsNavigationComponent } from 'app/main-fuse/documentation/components/navigation/navigation.component';
import { DocsComponentsProgressBarComponent } from 'app/main-fuse/documentation/components/progress-bar/progress-bar.component';
import { DocsComponentsSearchBarComponent } from 'app/main-fuse/documentation/components/search-bar/search-bar.component';
import { DocsComponentsSidebarComponent } from 'app/main-fuse/documentation/components/sidebar/sidebar.component';
import { DocsComponentsShortcutsComponent } from 'app/main-fuse/documentation/components/shortcuts/shortcuts.component';
import { DocsComponentsWidgetComponent } from 'app/main-fuse/documentation/components/widget/widget.component';

const routes = [
    {
        path: 'countdown',
        component: DocsComponentsCountdownComponent
    },
    {
        path: 'highlight',
        component: DocsComponentsHighlightComponent
    },
    {
        path: 'material-color-picker',
        component: DocsComponentsMaterialColorPickerComponent
    },
    {
        path: 'navigation',
        component: DocsComponentsNavigationComponent
    },
    {
        path: 'progress-bar',
        component: DocsComponentsProgressBarComponent
    },
    {
        path: 'search-bar',
        component: DocsComponentsSearchBarComponent
    },
    {
        path: 'sidebar',
        component: DocsComponentsSidebarComponent
    },
    {
        path: 'shortcuts',
        component: DocsComponentsShortcutsComponent
    },
    {
        path: 'widget',
        component: DocsComponentsWidgetComponent
    }
];

@NgModule({
    declarations: [
        DocsComponentsCountdownComponent,
        DocsComponentsHighlightComponent,
        DocsComponentsMaterialColorPickerComponent,
        DocsComponentsNavigationComponent,
        DocsComponentsProgressBarComponent,
        DocsComponentsSearchBarComponent,
        DocsComponentsSidebarComponent,
        DocsComponentsShortcutsComponent,
        DocsComponentsWidgetComponent
    ],
    imports: [
        RouterModule.forChild(routes),

        MatButtonModule,
        MatIconModule,

        FuseSharedModule,

        FuseCountdownModule,
        FuseHighlightModule,
        FuseMaterialColorPickerModule,
        FuseWidgetModule
    ]
})
export class ComponentsModule {
}
