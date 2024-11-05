import { Role } from "./role";

export class Employee {
    constructor(
        public id: number,
        public school_id: number,
        public user_Name: string,
        public password: string,
        public email: string,
        public roles: Role[]
    ){}
}
