export class Employee {
    constructor(
        public id: number = 0,
        public user_Name: string = '',
        public en_name: string = '',
        public ar_name: string = '',
        public password: string = '',
        public role_ID: number = 0,
        public busCompanyID: number = 0,
        public employeeTypeID: number = 0,
        public canReceiveRequest: boolean = false,
        public canReceiveMessage: boolean = false,
    ) {}
}