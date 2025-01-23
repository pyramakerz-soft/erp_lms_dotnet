export class Outcome {
    constructor(
        public id: number = 0,
        public name: string = '',
        public accountNumberId :number =0,
        public accountNumberName: string = '',
        public insertedByUserId :number =0,
        public insertedAt :number =0,
    ) {}
}
