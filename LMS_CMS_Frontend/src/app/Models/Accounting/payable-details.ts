export class PayableDetails {
    constructor(
        public id: number = 0,
        public amount: number|any = null,
        public notes: string|null = null,
        public payableMasterID: number = 0,
        public linkFileID: number = 0,
        public linkFileName: string = '',
        public linkFileTypeID: number = 0,
        public linkFileTypeName: string = '',
        public insertedByUserId: number = 0
    ) {}
}
