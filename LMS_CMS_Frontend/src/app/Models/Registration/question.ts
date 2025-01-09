export class Question {
    constructor(
        public id: number = 0,
        public description: string = '',
        public image: string = '',
        public video: string = '',
        public questionTypeID: number = 0,
        public correctAnswerID: number = 0,
        public testID: number = 0,
        public questionTypeName: string = '',
        public correctAnswerName: string = '',
        public testName: string = '',
        public insertedByUserId: number = 0
    ) {}
}
