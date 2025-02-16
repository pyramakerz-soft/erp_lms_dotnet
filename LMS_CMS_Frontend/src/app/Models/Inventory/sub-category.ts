export class SubCategory {
    constructor(
        public id: number = 0,
        public name: string = '',
        public categoryId: number = 0,
        public categoryName: string = '',
        public insertedByUserId :number =0,
        public insertedAt :string ="",
    ) {}
}
