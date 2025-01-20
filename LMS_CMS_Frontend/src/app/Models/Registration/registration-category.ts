import { Field } from "./field";

export class RegistrationCategory {
    constructor(
        public id: number = 0,
        public arName: string = '',
        public enName: string = '',
        public orderInForm: number = 0,
        public registrationFormId: number = 0,
        public insertedByUserId: number = 0,
        public fields: Field[] = []
    ) {}
}
