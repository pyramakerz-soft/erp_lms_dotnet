export class CartShopItem {
    constructor(
        public quantity: number = 0,
        public cartID: number|null = null,
        public shopItemID: number = 0,
        public shopItemSizeID: number|null = null,
        public shopItemColorID: number|null = null,
        public studentID: number = 0, 
        public id: number = 0,
        public shopItemEnName: string = "",
        public shopItemArName: string = "",
        public salesPrice: number = 0,
        public vatForForeign: number = 0,
        public mainImage: string = "",
        public shopItemSizeName: string = "",
        public shopItemLimit: number = 0,
        public shopItemColorName: string = ""
    ) {}
}  