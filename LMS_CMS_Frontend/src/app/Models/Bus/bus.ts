export class Bus {
    constructor(
        public id: number = 0,
        public name: string = '',
        public capacity: number | null = null,
        public isCapacityRestricted: boolean = false,
        public busTypeID: number | null = null,
        public backPrice: number | null = null,
        public twoWaysPrice: number | null = null,
        public morningPrice: number | null = null,
        public busTypeName: string | null = null,
        public busDistrictID: number | null = null,
        public busDistrictName: string | null = null,
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
