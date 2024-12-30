import { Class } from "./class";

export class Grade {
    constructor(
        public id: number = 0,
        public name: string = '',
        public sectionID: number = 0,
        public sectionName: string = '',
        public classes: Class[] = []
    ) {}
}