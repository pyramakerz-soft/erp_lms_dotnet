import { Grade } from "./grade";

export class School {
    constructor(
        public id: number = 0,
        public name: string = '',
        public schoolTypeName: string = '',
        public schoolTypeID: number = 0,
        public address: string = '',
        public grades: Grade[] = []
    ) {}
}
