import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OathCallbackComponent } from './oath-callback/oath-callback.component';
import { OAuthCallbackHandler } from './oauth-callback-guard';
import { AuthenticationRoutesModule } from './authentication.routing';

@NgModule({
  declarations: [OathCallbackComponent],
  imports: [
    CommonModule,
    AuthenticationRoutesModule
  ],
  providers: [OAuthCallbackHandler]
})
export class AuthenticationModule { }
