export class Answer {
    constructor(
            public registerationFormParentID: number = 0,
            public essayAnswer: string = '',
            public answerID: number | null = 0,
            public questionID: number = 0,
        ) {}
}
