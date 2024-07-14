export class ServerResponse<T> {
  data: T |any;
  isSuccess: boolean;
  statusCode : number;
  message : string;
}
