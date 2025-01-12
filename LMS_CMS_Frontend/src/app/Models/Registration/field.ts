import { FieldOption } from "./field-option";

export class Field {
    constructor(
        public id: number = 0,
        public arName: string = '',
        public enName: string = '',
        public orderInForm: number = 0,
        public isMandatory: boolean = true,
        public fieldTypeID: number = 0,
        public fieldTypeName: string = '',
        public registrationCategoryName: string = '',
        public registrationCategoryID: number = 0,
        public insertedByUserId: number = 0,
        public options: FieldOption[] = []
    ) {}
}
