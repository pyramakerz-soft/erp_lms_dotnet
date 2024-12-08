import { Class } from "./class";

export class Grade {
    constructor(
        public id: number = 0,
        public name: string = '',
        public classes: Class[] = []
    ) {}
}