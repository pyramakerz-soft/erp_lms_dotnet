export class Grade {
    constructor(
        public id: number = 0,
        public name : string = '',
        public dateFrom: string = '',
        public dateTo : string = '',
        public insertedByUserId: number = 0,
        public sectionID: number = 0,
        public sectionName: string = '',
    ) {}
}
