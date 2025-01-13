
export interface LoadStrategy {
    loadStrategyId: number;
    loadStrategyName: string;
    loadStrategyDescription?: null;
    isSelected: boolean;
    tblConfigId: number,
}
export interface RunTimeParameter {
    value: any;
    parameter: string;
    table_config_id: number,
    rtm_parmtrs_mstr_id: number,
    action?: 'existingRecord',
    isEditable?: false,
    isNewRow?: false
}
export interface RunTimeInstance {
    isSelected: boolean;
    instanceName: string;
    order: string | null;
    overlap: string | null;
    tbl_confgrtn_id: number
    action?: 'existingRecord',
    isEditable?: false,
    isNewRow?: false
}

export interface Create {
    loadStrategyId: number,
    tblConfigId: number
}