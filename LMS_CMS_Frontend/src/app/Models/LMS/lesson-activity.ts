export class LessonActivity {
    constructor(
        public id: number = 0,
        public englishTitle : string = '',
        public arabicTitle  : string = '', 
        public details: string = '',
        public order: number|null = null,
        public lessonID: number = 0,
        public lessonEnglishTitle : string = '',
        public lessonArabicTitle  : string = '',
        public lessonActivityTypeID: number = 0,
        public lessonActivityTypeEnglishName : string = '',
        public lessonActivityTypeArabicName  : string = '',
        public insertedByUserId: number = 0,
        public attachmentFile : File|null = null,
        public attachmentLink : string = ""
    ){}
}  