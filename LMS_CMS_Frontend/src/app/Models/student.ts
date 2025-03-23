export class Student {
      [key: string]: any; // Add this line to allow dynamic properties

    constructor(
        public id: number = 0,
        public user_Name: string = '',
        public en_name: string = '',
        public ar_name: string = '',
        public password: string = '',
        public mobile: string = '',
        public phone: string = '',
        public address: string = '',
        public note: string = '',
        public nationalityName: string = '',
        public accountNumberName: string = '',
        public accountNumberID :number =0,
        public nationality :number =0,
        public email: string = '',
        public insertedByUserId :number =0,
        public genderName: string = '',
        public genderId :number =0,
    ) {}
}
