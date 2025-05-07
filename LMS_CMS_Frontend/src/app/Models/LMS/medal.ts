export class Medal {
    constructor(
        public id: number = 0,
        public englishName : string = '',
        public arabicName: string = '',
        public imageLink : string = '',
        public imageForm: File|null = null,
        public insertedByUserId: number = 0,
    ) {}
}
