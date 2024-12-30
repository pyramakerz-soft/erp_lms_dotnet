export class RolePage {
    constructor(
        public pageId: number = 0,
        public allow_Edit: boolean = false,
        public allow_Delete :boolean = false,
        public allow_Edit_For_Others :boolean = false,
        public allow_Delete_For_Others :boolean = false
    ) {}
}
