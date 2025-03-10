import { StockingDetails } from "./stocking-details";

export class Stocking {
    constructor(
        public id: number = 0,
        public date:string = '',
        public storeID :number =0,
        public storeName :string = '',
        public invoiceNumber :number =0,
        public stockingDetails : StockingDetails [] =[] ,
        public insertedAt :string ="",
        public insertedByUserId :number =0,
    ) {}
}
