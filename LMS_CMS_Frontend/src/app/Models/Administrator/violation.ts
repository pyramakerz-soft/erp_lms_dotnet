import { EmployeeTypeGet } from "./employee-type-get";

export class Violation {
    constructor(
        public id: number = 0,
        public name: string = '',
        public employeeTypes :EmployeeTypeGet[]=[],
        public insertedByUserId :number =0,
    ) {}
}
