export class Receivable {
    constructor(
        public id: number = 0,
        public docNumber: number|null = null,
        public date: string = '',
        public notes: string|null = null,
        public receivableDocTypesID: number = 0,
        public receivableDocTypesName: string = '',
        public linkFileID: number = 0,
        public linkFileName: string = '',
        public bankOrSaveID: number = 0,
        public bankOrSaveName: string = '',
        public insertedByUserId: number = 0
    ) {}
}