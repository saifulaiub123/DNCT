export interface IColumnDetails {
    columnId:       number;
    tbl_confrtn_id: number;
    columnName:     string;
    dataType:       string;
    transformSql:   string;
    generateSk:     number;
    type2StartInd:  number;
    type2EndInd:    number;
    curActiveInd:   number;
    pattern1:       string;
    pattern2:       string;
    pattern3:       string;
    loadInd:        number;
    joinDupInd:     number;
    status?:  'new' | 'changed' | 'unchanged';
    validationResult?: number;
}
