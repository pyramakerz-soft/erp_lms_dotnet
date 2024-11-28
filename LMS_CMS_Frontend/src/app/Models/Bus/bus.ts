export class Bus {
    constructor(
        public id: number = 0,
        public name: string = '',
        public capacity: number | null = null,
        public isCapacityRestricted: boolean = false,
        public domainID: number = 0,
        public busTypeID: number | null = null,
        public busTypeName: string | null = null,
        public busRestrictID: number | null = null,
        public busRestrictName: string | null = null,
        public busStatusID: number | null = null,
        public busStatusName: string | null = null,
        public driverID: number | null = null,
        public driverName: string | null = null,
        public driverAssistantID: number | null = null,
        public driverAssistantName : string | null = null,
        public busCompanyID: number | null = null,
        public busCompanyName: string | null = null,
        public deletedByUserId :number =0,
        public updatedByUserId :number =0,
        public insertedByUserId :number =0,


    ) {}
}