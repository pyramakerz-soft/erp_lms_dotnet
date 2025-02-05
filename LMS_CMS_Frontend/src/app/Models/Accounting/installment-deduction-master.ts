export class InstallmentDeductionMaster {
    constructor(
        public id: number = 0,
        public docNumber: number = 0,
        public date: string = '',
        public notes: string = '',
        public employeeID: number = 0,
        public employeeName: string = '',
        public studentID: number = 0,
        public studentName: string = '',
        public insertedByUserId :number =0,
        public insertedAt :string ="",
    ) {}
}

