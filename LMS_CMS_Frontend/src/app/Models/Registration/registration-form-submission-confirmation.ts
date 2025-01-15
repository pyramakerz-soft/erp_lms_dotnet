export class RegistrationFormSubmissionConfirmation {
    constructor(
        public id: number = 0,
        public textAnswer: string = "",
        public categoryFieldID: number = 0,
        public selectedFieldOptionID: number | null = null,
        public selectedFieldOptionName: string | null = null,
        public registrationFormParentName: string = "",
        public registerationFormParentID: number = 0,
        public categoryFieldName: string = "",
        public categoryFieldOrderInForm: number = 0,
        public registrationCategoryID: number = 0,
        public registrationCategoryName: string = "",
        public registrationCategoryOrderInForm: string = "",
    ) {}
}