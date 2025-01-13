export class QuestionWithAnswer {
    constructor(
        public id: number = 0,
        public essayAnswer : string = '',
        public questionID: number = 0,
        public studentName: string = '',
        public description: string = '',
        public image: string = '',
        public video: string = '',
        public correctAnswerID: number = 0,
        public correctAnswerName:string = '',
        public questionTypeID: number = 0,
        public questionTypeName: string = '',
        public answerID: number = 0,
        public answerName: string = '',
        public insertedByUserId: number = 0,
    ) {}
}
