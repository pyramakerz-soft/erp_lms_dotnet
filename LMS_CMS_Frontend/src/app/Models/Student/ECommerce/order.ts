export class Order {
    constructor(
        public id: number = 0,
        public totalPrice: number = 0,
        public orderStateID: number = 0,
        public orderStateName: string = "",
        public cartID: number = 0,
        public studentID: number = 0, 
        public insertedAt: string = "", 
        public mainImage: string = "", 
    ){}
}