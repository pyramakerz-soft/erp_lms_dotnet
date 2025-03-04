import { CartShopItem } from "./cart-shop-item";

export class Cart {
    constructor(
        public id: number = 0,
        public totalPrice: number = 0, 
        public percentage: number = 0,
        public studentID: number = 0,
        public cart_ShopItems :CartShopItem[] = []
    ){}
} 
