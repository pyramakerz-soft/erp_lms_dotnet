export class BusStudent {
    constructor(
        public id: number = 0,
        public busName: string = '',
        public busID: number = 0,
        public studentID: number = 0,
        public studentName: string = "",
        public busCategoryID: number | null = null,
        public busCategoryName: string | null = null,
        public semseterID: number | null = null,
        public semseterName: string | null = null,
        public isException: boolean = false,
        public exceptionFromDate: string | null = null,
        public exceptionToDate: string | null = null,
        public deletedByUserId :number =0,
        public updatedByUserId :number =0,
        public insertedByUserId :number =0,

    ) {}
}