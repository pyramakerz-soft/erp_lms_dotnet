export class Semester {
    constructor(
        public id: number = 0,
        public name: string = '',
        public dateFrom: string = '',
        public dateTo: string = '',
        public academicYearName: string = '',
        public academicYearID: number = 0,
        public insertedByUserId: number = 0,
    ) {}
}
