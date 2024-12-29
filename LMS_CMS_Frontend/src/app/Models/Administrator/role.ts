export class Role {
    constructor(
        public id: number = 0,
        public name: string = '',
        public deletedByUserId :number =0,
        public updatedByUserId :number =0,
        public insertedByUserId :number =0,
        public insertedAt :string ="",
    ) {}
}
