export class QuestionAddEdit {
    constructor(
        public id: number = 0,
        public description: string = '',
        public image: string = '',
        public video: string = '',
        public questionTypeID: number = 0,
        public correctAnswerName: string = '',
        public testID: number = 0,
        public options :string[] =[],
    ) {}
}
