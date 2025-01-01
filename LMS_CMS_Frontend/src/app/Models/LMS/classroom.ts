export class Classroom {
    constructor(
        public id: number = 0,
        public number: number|null = null,
        public insertedByUserId: number = 0,
        public name: string = '',
        public gradeName: string = '',
        public gradeID: number = 0,
        public floorName: string = '',
        public floorID: number = 0,
        public academicYearName: string = '',
        public academicYearID: number = 0,
    ) {}
}