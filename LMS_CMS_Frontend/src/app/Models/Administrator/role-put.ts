import { RolePage } from "./role-page";

export class RolePut {
     constructor(
           public id: number = 0,
            public name: string = '',
            public pages:RolePage[]=[]
        ) {}
}
