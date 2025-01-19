export class QuestionAddEdit {
    constructor(
        public id: number = 0,
        public description: string = '',
        public image: File |any = '',
        public video: File |any = '',
        public questionTypeID: number = 0,
        public correctAnswerName: string = '',
        public testID: number = 0,
        public options :string[] =[],
        
    ) {}
}
