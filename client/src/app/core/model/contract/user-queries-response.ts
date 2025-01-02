export class UserQueriesResponse {
  constructor(
    public userQueryId?: number,
    public tableConfigId?: number,
    public userQuery: string | null = null,
    public baseQueryIndicator?: number,
    public queryOrderIndicator?: number,
    public rowInsertTimestamp?: number,
  ){}
}

