export class Supplier {
    constructor(
        public id: number = 0,
        public name: string = '',
        public countryID :number = 0,
        public email :string = '',
        public website :string = '',
        public phone1 :string = '',
        public contactPerson :string = '',
        public phone3 :string = '',
        public phone2 :string = '',
        public address :string = '',
        public commercialRegister :number =0,
        public taxcard :string = '',
        public accountNumberId :number =0,
        public commercialRegisterId :string = '',
        public insertedByUserId :number =0,    
        public insertedAt :string ="",
    ) {}
}
