export class FieldAddEdit {
     constructor(
            public id: number = 0,
            public arName: string = '',
            public enName: string = '',
            public orderInForm: number = 0,
            public isMandatory: boolean = true,
            public fieldTypeID: number = 0,
            public registrationCategoryID: number = 0,
            public options :string[] =[],
        ) {}
}
