export class InterviewTimeTable {
    constructor(
        public id: number = 0,
        public date: string = '',
        public fromTime: string = '',
        public toTime: string = '',
        public capacity: number | null = null,
        public reserved: number = 0,
        public academicYearID: number = 0,
        public academicYearName: string = '',
        public insertedByUserId: number = 0,

        public fromDate:string = "",
        public toDate:string = "",

        public days:string[] = [],
    ) {}
}
