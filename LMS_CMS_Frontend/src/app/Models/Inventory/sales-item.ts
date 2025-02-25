export class SalesItem {
    constructor(
        public id: number = 0,
        public barCode: string = '',
        public name : string = '',
        public quantity :number =0,
        public price :number =0,
        public totalPrice :number =0,
        public shopItemID :number =0,
        public inventoryMasterId :number =0,
        public shopItemName :string = '',
        public salesName :string = '',
        public notes :string = '',
        public insertedAt :string ="",
        public insertedByUserId :number =0,
    ) {}
}
