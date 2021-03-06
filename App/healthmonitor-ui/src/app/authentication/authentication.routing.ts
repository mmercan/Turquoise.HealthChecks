import { Routes, RouterModule } from '@angular/router';

// import { SigninComponent } from './signin/signin.component';
// import { SigninbComponent } from './signinb/signinb.component';
// import { SignupComponent } from './signup/signup.component';
// import { ForgotComponent } from './forgot/forgot.component';
// import { LockscreenComponent } from './lockscreen/lockscreen.component';
import { OathCallbackComponent } from './oath-callback/oath-callback.component';
import { OAuthCallbackHandler } from './oauth-callback-guard';
import { NgModule } from '@angular/core';

export const routes: Routes = [
    {
        //  path: '',
        //  children: [
        //     {
        //   path: 'signin',
        //   component: SigninComponent
        // }, {
        //   path: 'signup',
        //   component: SignupComponent
        // }, {
        //   path: 'forgot',
        //   component: ForgotComponent
        // }, {
        //   path: 'lockscreen',
        //   component: LockscreenComponent
        // },

        //   {
        path: '',
        component: OathCallbackComponent, canActivate: [OAuthCallbackHandler]
        //  }
        // ]
    }
];


@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AuthenticationRoutesModule { }
