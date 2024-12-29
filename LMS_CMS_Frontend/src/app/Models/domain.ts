export class Domain {
    constructor(
        public id: number = 0,
        public name: string = '',
        public connectionString: string = '',
        public insertedAt: string = '',
        public pages: number[] = []
    ) {}
}