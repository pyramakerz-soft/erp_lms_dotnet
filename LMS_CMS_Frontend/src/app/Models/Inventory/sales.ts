import { SalesItem } from "./sales-item";

export class Sales {
     constructor(
            public id: number = 0,
            public name: string = '',
            public invoiceNumber :number =0,
            public date: string = '',
            public isCash: boolean = false,
            public isVisa: boolean = false,
            public cashAmount :number =0,
            public visaAmount :number =0,
            public remaining :number =0,
            public total :number =0,
            public notes :string = '',
            public flagId :number =0,
            public storeID :number =0,
            public studentID :number =0,
            public saveID :number =0,
            public bankID :number =0,
            public storeName :string = '',
            public studentName :string = '',
            public saveName :string = '',
            public bankName :string = '',
            public insertedAt :string ="",
            public insertedByUserId :number =0,
            public attachments : string [] =[],
            public attachment : File [] =[] ,
            public NewAttachments : File [] =[] ,
            public DeletedAttachments : string [] =[] ,
            public inventoryDetails : SalesItem [] =[] ,
        ) {}
}

