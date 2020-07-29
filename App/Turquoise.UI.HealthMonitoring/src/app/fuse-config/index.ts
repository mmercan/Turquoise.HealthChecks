import { FuseConfig } from '@fuse/types';

/**
 * Default Fuse Configuration
 *
 * You can edit these options to change the default options. All these options also can be
 * changed per component basis. See `app/main/pages/authentication/login/login.component.ts`
 * constructor method to learn more about changing these options per component basis.
 */

export const fuseConfig: FuseConfig = {
    // Color themes can be defined in src/app/app.theme.scss
    colorTheme: 'theme-blue-gray-dark', // 'theme-default',
    customScrollbars: true,
    layout: {
        style: 'vertical-layout-1',
        width: 'fullwidth',
        navbar: {
            primaryBackground: 'fuse-navy-700',
            secondaryBackground: 'fuse-navy-900',
            folded: false,
            hidden: false,
            position: 'left',
            variant: 'vertical-style-1'
        },
        toolbar: {
            customBackgroundColor: false,
            background: 'fuse-white-500',
            hidden: false,
            position: 'below-static'
        },
        footer: {
            customBackgroundColor: true,
            background: 'fuse-navy-900',
            hidden: true,
            position: 'below-fixed'
        },
        sidepanel: {
            hidden: true,
            position: 'right'
        }
    },

    adalConfig: {
        tenant: 'e1870496-eab8-42d0-8eb9-75fa94cfc3b8',
        clientId: '67d009b1-97fe-4963-84ff-3590b06df0da',
        redirectUri: window.location.origin + '/',
        postLogoutRedirectUri: window.location.origin + '/',
        cacheLocation: 'localStorage'
    },
    authenticationType: 'Adal',
    // debug: true,
    // logLevel: logLevel.debug,
    // Nofitication: {
    //   publicKey: 'BCbYNxjxYPOcv3Hn8xZH1bB2kJLFLeO9Fx68U0C2FOZ7wFmG_yxGdiiNIWrFRHY6X1NL6egRgzZGAC_A_6fcigA',
    //   subscriptionRepoUrl: window.location.hostname === 'localhost'
    //     ? 'http://localhost:5000/api/PushNotification' : 'https://decima.azurewebsites.net/api/PushNotification'
    // },
    login: {
        loginUrl: window.location.hostname === 'localhost' ? 'http://localhost:5000/api/Token' : 'https://auth.myrcan.com/api/Token',
        bearerToken: ''
    },
};
