export class Job {
    constructor(
        public id: number = 0,
        public name: string = '',
        public JobCategoryId :number =0,
        public insertedByUserId :number =0,
        public insertedAt :number =0,
    ) {}
}
