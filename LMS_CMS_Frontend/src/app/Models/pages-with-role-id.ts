export class PagesWithRoleId {
    constructor(
        public id: number = 0,
        public en_name: string = '',
        public ar_name: string = '',
        public page_ID: number | null = null,
        public allow_Edit: boolean = false,
        public allow_Delete: boolean = false,
        public children: PagesWithRoleId[] = []
    ) {}
}
