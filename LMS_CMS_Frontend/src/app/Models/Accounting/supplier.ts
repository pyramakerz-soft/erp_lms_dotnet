export class Supplier {
    constructor(
        public id: number = 0,
        public name: string = '',
        public country :string = '',
        public email :string = '',
        public Website :string = '',
        public Phone :string = '',
        public Mobile :string = '',
        public phoneMobile :string = '',
        public Address :string = '',
        public Email :string = '',
        public commercialRegister :number =0,
        public taxcard :string = '',
        public accountNumberId :number =0,
        public commercialRegisterId :number =0,  
        public insertedByUserId :number =0,    
        public insertedAt :string ="",
    ) {}
}
