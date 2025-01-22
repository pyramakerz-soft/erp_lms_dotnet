export class Department {
    constructor(
        public id: number = 0,
        public name: string = '',
        public insertedByUserId :number =0,
        public insertedAt :number =0,
    ) {}
}
