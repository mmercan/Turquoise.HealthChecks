import { NgModule } from '@angular/core';

import { UIAngularMaterialModule } from 'app/main-fuse/ui/angular-material/angular-material.module';
import { UICardsModule } from 'app/main-fuse/ui/cards/cards.module';
import { UIFormsModule } from 'app/main-fuse/ui/forms/forms.module';
import { UIIconsModule } from 'app/main-fuse/ui/icons/icons.module';
import { UITypographyModule } from 'app/main-fuse/ui/typography/typography.module';
import { UIHelperClassesModule } from 'app/main-fuse/ui/helper-classes/helper-classes.module';
import { UIPageLayoutsModule } from 'app/main-fuse/ui/page-layouts/page-layouts.module';
import { UIColorsModule } from 'app/main-fuse/ui/colors/colors.module';

@NgModule({
    imports: [
        UIAngularMaterialModule,
        UICardsModule,
        UIFormsModule,
        UIIconsModule,
        UITypographyModule,
        UIHelperClassesModule,
        UIPageLayoutsModule,
        UIColorsModule
    ]
})
export class UIModule {
}
