export class TokenResponseModel {
  userId: number;
  email!: string;
  name!: string;
  token!: Token;
}
export class Token {
  accessToken !: string;
  refreshToken !: string;
  tokenType !: string;
  expiresIn !: number;
}
