export class FeesActivation {
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
        public feeTypeName: string ="",
        public feeDiscountTypeName: string ="",
        public studentName: string ="",
        public academicYearName: string ="",
        public insertedByUserId :number =0,
        public insertedAt :number =0,
        public studentAcademicYearID: number = 0,
        public schoolName: string = '',
        public schoolID: number = 0,
        public className: string = '',
        public classID: number = 0,
        public gradeName: string = '',
        public gradeID: number = 0,
        public sectionName: string = '',
        public sectionId: number = 0,
    ) {}
}

