export class Bank {
    constructor(
        public id: number = 0,
        public name: string = '',
        public bankAccountName: string = '',
        public bankName: string = '',
        public iban: string = '',
        public bankAccountNumber: number = 0,
        public accountOpeningDate: string = '',
        public accountClosingDate: string = '',
        public accountNumberName: string = '',
        public accountNumberId :number =0,
        public insertedByUserId :number =0,
        public insertedAt :number =0,
    ) {}
}

