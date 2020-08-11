export enum logLevel {
    data = 6,
    debug = 5,
    info = 4,
    log = 3,
    warn = 2,
    error = 1,
    none = 0
}


export interface FuseConfig {
    colorTheme: string;
    customScrollbars: boolean;
    layout: {
        style: string,
        width: 'fullwidth' | 'boxed',
        navbar: {
            primaryBackground: string,
            secondaryBackground: string,
            hidden: boolean,
            folded: boolean,
            position: 'left' | 'right' | 'top',
            variant: string
        },
        toolbar: {
            customBackgroundColor: boolean,
            background: string,
            hidden: boolean,
            position: 'above' | 'above-static' | 'above-fixed' | 'below' | 'below-static' | 'below-fixed'
        }
        footer: {
            customBackgroundColor: boolean,
            background: string,
            hidden: boolean,
            position: 'above' | 'above-static' | 'above-fixed' | 'below' | 'below-static' | 'below-fixed'
        },
        sidepanel: {
            hidden: boolean,
            position: 'left' | 'right'
        }
    };
    authenticationType: 'Adal' | 'local';
    adalConfig: {
        tenant: string,
        clientId: string,
        redirectUri: string,
        postLogoutRedirectUri: string,
        cacheLocation: string
    };
    login: {
        loginUrl: string,
        bearerToken: string
    };
}
