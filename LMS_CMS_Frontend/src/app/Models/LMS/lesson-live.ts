export class LessonLive {
    constructor(
        public id: number = 0,
        public period: number = 0,
        public liveLink: string = '',
        public recordLink: string = '',
        public weekDayName: string = '',
        public weekDayID: number = 0,
        public classroomName: string = '',
        public classroomID: number = 0,
        public subjectEnglishName: string = '',
        public subjectID: number = 0,
        public subjectArabicName: string = '',
    ) {}
}
