import { Grade } from "./grade";

export class School {
    constructor(
        public id: number = 0,
        public name: string = '',
        public grades: Grade[] = []
    ) {}
}
