export class TokenResponseModel {
  AccessToken !: string;
  RefreshToken !: string;
  Email!: string;
  Roles !: string[];
  Business !: any
  Customer !: any
  Id!: string;
}
