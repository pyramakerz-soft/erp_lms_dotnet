import { Classroom } from "./classroom";

export class LessonResource {
    constructor(
        public id: number = 0,
        public englishTitle : string = '',
        public arabicTitle  : string = '',  
        public lessonID: number = 0,
        public lessonEnglishTitle : string = '',
        public lessonArabicTitle  : string = '',
        public lessonResourceTypeID: number = 0,
        public lessonResourceTypeEnglishName : string = '',
        public lessonResourceTypeArabicName  : string = '',
        public insertedByUserId: number = 0,
        public attachmentFile : File|null = null,
        public attachmentLink : string = "",
        public classes : number[] = [],
        public newClassRooms : number[] = [], 
        public classrooms : Classroom[] = []
    ){}
}
