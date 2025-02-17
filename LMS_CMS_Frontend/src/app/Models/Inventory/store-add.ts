import { Category } from "./category";

export class StoreAdd {
        constructor(
            public id: number = 0,
            public name: string = '',
            public categoriesIds :number[] = [],
        ) {}
}
