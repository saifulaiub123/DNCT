export class TreeViewResponse {
  constructor(
    public id: number | 0 = 0,
    public name: string ,
    public hasChildren: boolean,
    public nodeType: string,

    // public parentId: number | 0 = 0,
    // public message: string | null = null,
    // public messageArabic: string | null = null,
    // public input: string | null = null,
    // public action: number | 0 = 0,
    // public isDefault: boolean = false,
    public hasChild: boolean = true
  ){}
}
