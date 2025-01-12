import { RegistrationCategory } from "./registration-category";

export class RegistrationForm {
    constructor(
        public id: number = 0,
        public name: string = '',
        public categories: RegistrationCategory[] = []
    ) {}
}
