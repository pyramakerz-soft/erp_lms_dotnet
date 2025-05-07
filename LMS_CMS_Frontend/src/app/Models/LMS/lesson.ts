import { Tag } from "./tag";

export class Lesson {
    constructor(
        public id: number = 0,
        public englishTitle : string = '',
        public arabicTitle  : string = '',
        public details: string = '',
        public order: number|null = null,
        public subjectID: number = 0,
        public subjectEnglishName : string = '',
        public subjectArabicName  : string = '',
        public semesterWorkingWeekID: number = 0,
        public semesterWorkingWeekEnglishName : string = '',
        public semesterWorkingWeekArabicName  : string = '',
        public semesterID: number = 0,
        public academicYearID: number = 0,
        public gradeID: number = 0,
        public schoolID: number = 0,
        public insertedByUserId: number = 0,
        public tags : Tag [] =[],
        public tagNames : string [] =[],
        public tagIDs : number [] =[],
        public fromLessonID: number = 0,
        public toSemesterWorkingWeekID: number = 0
    ){}
} 