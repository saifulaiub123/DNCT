export const environment = {
  production: false,
  webUrl: 'https://localhost:5200/',
  apiUrl: 'https://localhost:5201/api',
  apiVersion: 'v1',
  msalConfig: {
    auth: {
      clientId: 'ENTER_CLIENT_ID',
      authority: 'https://login.microsoftonline.com/<Tenant_Id>',
    },
  },
  apiConfig: {
    scopes: ['user.read'],
    uri: 'https://localhost:5200/',
  },
};

