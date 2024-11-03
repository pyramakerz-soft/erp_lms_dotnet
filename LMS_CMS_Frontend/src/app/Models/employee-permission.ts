import { Role } from "./role";

export class EmployeePermission {
    constructor(
        public employeeID: number,
        public user_Name: string,
        public email: string,
        public roles: Role[]
    ) {}
}
