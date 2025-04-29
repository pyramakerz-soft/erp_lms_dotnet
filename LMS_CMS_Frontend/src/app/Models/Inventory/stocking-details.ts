export class StockingDetails {
    constructor(
        public id: number = 0,
        public currentStock:number =0,
        public actualStock :number|any =0,
        public theDifference :number|any =0,
        public shopItemID :number =0,
        public stockingId :number =0,
        public shopItemName :string = '',
        public barCode: string = '',
        public insertedAt :string ="",
        public ItemPrice : number =0 ,
        public insertedByUserId :number =0,
    ) {}
}
