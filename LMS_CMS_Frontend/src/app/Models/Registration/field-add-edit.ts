export class FieldAddEdit {
     constructor(
            public id: number = 0,
            public arName: string = '',
            public enName: string = '',
            public orderInForm: number|null = null,
            public isMandatory: boolean = true,
            public fieldTypeID: number = 0,
            public registrationCategoryID: number = 0,
            public options :string[] =[],
        ) {}
}
