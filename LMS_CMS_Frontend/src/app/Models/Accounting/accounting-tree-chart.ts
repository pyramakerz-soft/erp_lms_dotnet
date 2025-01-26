export class AccountingTreeChart {
    constructor(
        public id: number|null = null,
        public name: string = '',
        public level :number =1,
        public mainAccountNumberID :number = 0,
        public mainAccountNumberName: string = '',
        public subTypeID :number =0,
        public subTypeName: string = '',
        public endTypeID :number =0,
        public endTypeName: string = '',
        public linkFileID :number =0,
        public linkFileName: string = '',
        public motionTypeID :number =0,
        public motionTypeName: string = '',
        public insertedByUserId :number =0,
        public insertedAt :number =0,
        public children: AccountingTreeChart[] = []
    ) {}
}