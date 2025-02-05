export class ReceivableDetails {
    constructor(
        public id: number = 0,
        public amount: number = 0,
        public notes: string|null = null,
        public receivableMasterID: number = 0,
        public linkFileID: number = 0,
        public linkFileName: string = '',
        public linkFileTypeID: number = 0,
        public linkFileTypeName: string = '',
        public insertedByUserId: number = 0
    ) {}
}
