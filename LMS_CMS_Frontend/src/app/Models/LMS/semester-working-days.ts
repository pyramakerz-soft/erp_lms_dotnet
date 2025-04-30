export class SemesterWorkingDays {
    constructor(
        public id: number = 0,
        public englishName: string = '',
        public arabicName: string = '',
        public dateTo: string = '',
        public dateFrom: string = '',
        public semesterID: number = 0,
        public insertedByUserId: number = 0,
    ) {}
}
