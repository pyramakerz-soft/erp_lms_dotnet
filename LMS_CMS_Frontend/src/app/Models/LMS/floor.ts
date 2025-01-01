export class Floor {
    constructor(
        public id: number = 0,
        public insertedByUserId: number = 0,
        public name: string = '',
        public buildingName: string = '',
        public buildingID: number = 0,
        public floorMonitorName: string = '',
        public floorMonitorID: number = 0,
    ) {}
}
