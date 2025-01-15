export class RegistrationFormParentIncludeRegistrationFormInterview {
    constructor(
        public id: number = 0,
        public studentName: string = '',
        public phone: string = '',
        public gradeID: number = 0,
        public gradeName: string = '',
        public academicYearID: number = 0,
        public academicYearName: string = '',
        public email: string = '',
        public parentID: number = 0,
        public parentName: string = '',
        public registrationFormID: number = 0,
        public registrationFormName: string = '',
        public registrationFormInterviewID: number = 0,
        public registrationFormInterviewStateID: number = 0,
        public registrationFormInterviewStateName: string = '',
        public insertedByUserId: number = 0,
        public interviewTimeID: number = 0,
        public interviewTimeDate: string = '',
        public interviewTimeFromTime: string = '',
        public interviewTimeToTime: string = '',
    ) {}
}