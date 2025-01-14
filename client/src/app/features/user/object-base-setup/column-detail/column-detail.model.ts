export interface TableConfiguration {
  tblColConfgrtnId: number | null;
  tblConfgrtnId: number | null;
  colmnName: string | null;
  dataType: string | null;
  colmnTrnsfrmtnStep1: string | null;
  genrtIdInd: string | null;
  idGenrtnStratgyId: number | null;
  type2StartInd: number | null;
  type2EndInd: number | null;
  currRowInd: number | null;
  pattern1: string | null;
  pattern2: string | null;
  pattern3: string | null;
  ladInd: number | null;
  joinDupsInd: number | null;
  confgrtnEffStartTs: Date;
  confgrtnEffEndTs: Date;
  status?: string | null;
  validationResult?: number | null;
  action?: string | null,
  isEditable: boolean,
}
