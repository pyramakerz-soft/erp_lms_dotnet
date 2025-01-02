export class AcademicYear {
    constructor(
        public id: number = 0,
        public name: string = '',
        public insertedByUserId: number = 0,
        public dateFrom: string = '',
        public dateTo: string = '',
        public isActive: boolean = false,
        public schoolName: string = '',
        public schoolID: number = 0,
    ) {}
}
