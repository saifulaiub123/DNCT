export interface TableConfiguration {
    tblColConfgrtnId:    number;
    tblConfgrtnId:       number;
    colmnName:           string;
    dataType:            string;
    colmnTrnsfrmtnStep1: string;
    genrtIdInd:          string;
    idGenrtnStratgyId:   number;
    type2StartInd:       number;
    type2EndInd:         number;
    currRowInd:          number;
    pattern1:            string;
    pattern2:            string;
    pattern3:            string;
    ladInd:              number;
    joinDupsInd:         number;
    confgrtnEffStartTs:  Date;
    confgrtnEffEndTs:    Date;
    status?:  string;
    validationResult?: number;
      action?: string,
      isEditable: boolean,
      isNewRow: boolean
}
