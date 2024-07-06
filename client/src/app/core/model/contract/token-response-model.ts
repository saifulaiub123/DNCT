export class TokenResponseModel {
  constructor(
    public userId: number | null = null,
    public email: string | null = null,
    public name: string | null = null,
    public token: Token = new Token(),
  ){}

}
export class Token {
  constructor(
    public accessToken : string = '',
    public refreshToken : string = '',
    public tokenType : string = '',
    public expiresIn : number = 0,
  )
  {}

}
