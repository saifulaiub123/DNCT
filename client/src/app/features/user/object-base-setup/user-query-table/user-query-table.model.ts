export interface IUserQueryTable {
    isSelected:       boolean;
    tableConfigId: number;
    queryId:          number;
    fullQuery:        string;
    seedQuery:        number;
    qtyOrder:         number;
    validationResult?: string;
}