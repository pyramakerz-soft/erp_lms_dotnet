export class RegistrationFormSubmission {
    constructor(
        public textAnswer: string | null = null,
        public categoryFieldID: number = 0,
        public selectedFieldOptionID: number | null = null
    ) {}
}
