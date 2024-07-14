export const environment = {
  production: false,
  webUrl: 'https://localhost:5200/',
  apiUrl: 'https://localhost:5201/api',
  apiVersion: 'v1',
  mslConfig: {
    clientId: "",
    authority: "https://login.microsoftonline.com/<Tenant_Id>",
    redirectUri: 'https://localhost:5200/' // change redirect url based on where u want to redirect after the authentication
  }
};

