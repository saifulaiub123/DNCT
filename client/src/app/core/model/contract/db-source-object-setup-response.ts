export class DbNameObjectSetupResponse {
  constructor(
    public databaseSourceId: number | 0 = 0,
    public connectionName: string ,
    public databaseName: string,
  ){}
}
