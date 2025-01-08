export interface UserQuery {
    isSelected:       boolean;
    userQueryId:         number;
    tableConfigId:       number;
    userQuery:           string;
    baseQueryIndicator:  number;
    queryOrderIndicator: number;
    rowInsertTimestamp:  null;
}
export interface AutoPopulate {
    colunmId: number;
    sqlTxt:   string;
    att1:     string;
    att2:     string;
}
export interface ValidateSyntax extends AutoPopulate{}
export interface CreateUpdateQuery {
    userQueryId:         number;
    tableConfigId:       number;
    userQuery:           string;
    baseQueryIndicator:  number;
    queryOrderIndicator: number;
}

