export class Subject {
    constructor(
        public id: number = 0,
        public insertedByUserId: number = 0,
        public en_name : string = '',
        public ar_name  : string = '',
        public orderInCertificate : number|null = null,
        public creditHours : number|null = null,
        public subjectCode : number|null = null,
        public passByDegree : number|null = null,
        public totalMark : number|null = null,
        public hideFromGradeReport : boolean = false,
        public numberOfSessionPerWeek : number|null = null,
        public gradeID : number = 0,
        public gradeName : string = '',
        public subjectCategoryID : number = 0,
        public subjectCategoryName : string = '',
        public iconFile : File|null = null,
        public iconLink : string = "",
        public insertedAt: string = '',
    ) {}
}
