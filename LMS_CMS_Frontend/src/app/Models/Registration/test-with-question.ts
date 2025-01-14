import { Question } from "./question";

export class TestWithQuestion {
    constructor(
        public questionTypeID: number = 0,
        public questionTypeName:string = '',
        public questions:Question[]=[]
    ) {}
}
