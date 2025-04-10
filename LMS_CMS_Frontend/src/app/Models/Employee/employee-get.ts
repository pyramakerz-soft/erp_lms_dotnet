export class EmployeeGet {
    constructor(
        public id: number = 0,
        public user_Name: string = '',
        public en_name: string = '',
        public ar_name: string = '',
        public password: string = '',
        public email: string = '',
        public mobile: string = '',
        public phone: string = '',
        public licenseNumber: string = '',
        public expireDate: string = '',
        public address: string = '',
        public role_ID: number = 0,
        public role_Name: string = '',
        public busCompanyID: number = 0,
        public busCompanyName: string = '',
        public employeeTypeID: number = 0,
        public employeeTypeName: string = '',
        public files :File []=[],
        public insertedByUserId :number =0,
        public canReceiveRequest: boolean = false,
        public canReceiveMessage: boolean = false,
    ) {}
}

