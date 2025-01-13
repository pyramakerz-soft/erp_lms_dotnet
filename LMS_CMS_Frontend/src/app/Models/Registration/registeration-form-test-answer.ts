import { QuestionWithAnswer } from "./question-with-answer";

export class RegisterationFormTestAnswer {
    constructor(
        public questionTypeID: number = 0,
        public questionTypeName:string = '',
        public questions:QuestionWithAnswer[]=[]
    ) {}
}
