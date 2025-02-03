export class FeesActivation {
    constructor(
        public id: number = 0,
        public amount: number = 0,
        public discount: number = 0,
        public net: number = 0,
        public feeTypeID: number = 0,
        public feeDiscountTypeID: number = 0,
        public studentID: number = 0,
        public academicYearId: number = 0,
        public feeTypeName: string ="",
        public feeDiscountTypeName: string ="",
        public studentName: string ="",
        public academicYearName: string ="",
        public insertedByUserId :number =0,
        public insertedAt :number =0,
    ) {}
}
