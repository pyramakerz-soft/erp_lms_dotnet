export class SemesterWorkingWeek {
    constructor(
        public id: number = 0,
        public englishName : string = '',
        public arabicName  : string = '', 
        public semesterID: number = 0,
        public semesterName : string = '',
        public dateFrom  : string = '',
        public dateTo  : string = '', 
        public insertedByUserId: number = 0,  
    ){}
}
