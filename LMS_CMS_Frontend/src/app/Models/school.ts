export class School {
    constructor(
        public id: number = 0,
        public name: string = '',
        public schoolTypeName: string = '',
        public schoolTypeID: number = 0,
        public insertedByUserId: number = 0,
        public address: string = '',
        public reportHeaderOneEn: string = '',
        public reportHeaderOneAr: string = '',
        public reportHeaderTwoEn: string = '',
        public reportHeaderTwoAr: string = '',
        public reportImageFile : File|null = null,
        public vatNumber: string|null = null,
        public reportImage : string = "",
    ) {}
}
