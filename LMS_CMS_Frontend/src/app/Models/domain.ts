import { School } from "./school";

export class Domain {
    constructor(
        public id: number,
        public name: string,
        public password: string,
        public user_Name :string,
        public schools:School[]
    ) {}
}
