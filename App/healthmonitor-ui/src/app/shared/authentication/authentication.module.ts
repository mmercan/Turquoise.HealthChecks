import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthService } from './auth.service';
import { JwtInterceptor } from './jwt.interceptor';

import { AdalGuard } from './adal/adal.guard';
import { AdalInterceptor } from './adal/adal.interceptor';
import { AdalService } from './adal/adal.service';
import { IfAuthenticatedDirective } from './if-authenticated/if-authenticated.directive';

import { UserAvatarComponent } from './user-avatar/user-avatar.component';


@NgModule({
  declarations: [IfAuthenticatedDirective, UserAvatarComponent],
  exports: [IfAuthenticatedDirective, UserAvatarComponent],
  imports: [
    CommonModule,
  ],
  providers: [AuthService,
    JwtInterceptor,
    AdalGuard,
    AdalInterceptor,
    AdalService
  ]

})
export class AuthenticationModule {
  static forRoot(): ModuleWithProviders<any> {
    return {
      ngModule: AuthenticationModule,
      providers: [AdalService, AuthService, AdalInterceptor, AdalGuard],
    };
  }

}
