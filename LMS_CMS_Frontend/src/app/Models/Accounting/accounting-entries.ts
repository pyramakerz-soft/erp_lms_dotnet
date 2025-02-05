export class AccountingEntries {
    constructor(
        public id: number = 0,
        public docNumber: number|null = null,
        public date: string = '',
        public notes: string|null = null,
        public accountingEntriesDocTypeID: number = 0,
        public accountingEntriesDocTypeName: string = '', 
        public insertedByUserId: number = 0
    ) {}
} 