export class AccountingEntriesDetails {
    constructor(
        public id: number = 0,
        public creditAmount: number = 0,
        public debitAmount: number = 0,
        public note: string|null = null,
        public subAccountingID: number = 0,
        public subAccountingName: string = '', 
        public accountingTreeChartID: number = 0,
        public accountingTreeChartName: string = '', 
        public accountingEntriesMasterID: number = 0,
        public insertedByUserId: number = 0
    ) {}
} 