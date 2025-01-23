export class Job {
    constructor(
        public id: number = 0,
        public name: string = '',
        public JobCategoryId :number =0,
        public jobCategoryName: string = '',
        public insertedByUserId :number =0,
        public insertedAt :number =0,
    ) {}
}
