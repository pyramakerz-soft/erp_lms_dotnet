export class Subject {
    constructor(
        public id: number = 0,
        public insertedByUserId: number = 0,
        public en_name : string = '',
        public ar_name  : string = '',
        public orderInCertificate : number = 0,
        public creditHours : number = 0,
        public subjectCode : number = 0,
        public passByDegree : number = 0,
        public totalMark : number = 0,
        public hideFromGradeReport : boolean = false,
        public numberOfSessionPerWeek : number = 0,
        public gradeID : number = 0,
        public gradeName : string = '',
        public subjectCategoryID : number = 0,
        public subjectCategoryName : string = '',
        public iconFile : File|null = null,
        public iconLink : string = "",
        public insertedAt: string = '',
    ) {}
}
