export class Template {
    constructor(
        public id: number = 0,
        public en_name : string = '',
        public ar_name  : string = '',
        public weight: number = 0,
        public afterCount: number = 0,
        public insertedAt: string = '',
        public insertedByUserId: number = 0,
    ) {}
}
