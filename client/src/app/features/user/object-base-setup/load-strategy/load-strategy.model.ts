
export interface  ILoadStrategy{
isSelected:boolean;
loadStrategy:string;
table_config_id: number,
load_stratgy_id: number,
}
export interface  IParameter{
value:any;
parameter:string;
table_config_id: number,
rtm_parmtrs_mstr_id: number,
action?: 'existingRecord',
isEditable?: false,
isNewRow?: false
}
export interface  IInstanceName{
isSelected:boolean;
instanceName:string;
order:string | null;
overlap:string | null;
tbl_confgrtn_id:number
action?: 'existingRecord',
isEditable?: false,
isNewRow?: false
}

export interface Create{
    loadStrategyId: number,
    tblConfigId: number
}