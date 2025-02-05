export class InstallmentDeductionDetail {
    constructor(
        public id: number = 0,
        public amount: number = 0,
        public date: string = '',
        public installmentDeductionMasterID: number = 0,
        public installmentDeductionMasterName: string = '',
        public feeTypeID: number = 0,
        public feeTypeName: string = '',
        public insertedByUserId :number =0,
        public insertedAt :string ="",
    ) {}
}
