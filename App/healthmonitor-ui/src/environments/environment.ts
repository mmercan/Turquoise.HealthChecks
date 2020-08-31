// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
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
  grpc: {
    url: window.location.hostname === 'localhost' ? 'http://localhost:80' : 'https://health-api.dev.turk.mercan.io',
  },
  signalrBaseUrl: 'http://localhost',
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
