export class FeesActivationAddPut {
    push(fee: FeesActivationAddPut) {
      throw new Error('Method not implemented.');
    }
    constructor(
        public feeActivationID: number = 0,
        public amount: number = 0,
        public discount: number = 0,
        public net: number = 0,
        public date: string ="",
        public feeTypeID: number = 0,
        public feeDiscountTypeID: number = 0,
        public studentID: number = 0,
        public academicYearId: number = 0,
    ) {}
}

