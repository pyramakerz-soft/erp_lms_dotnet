import { Category } from "./category";

export class Store {
    constructor(
        public id: number = 0,
        public name: string = '',
        public storeCategories :Category[] = [],
        public insertedByUserId :number =0,
        public insertedAt :string ="",
    ) {}
}
