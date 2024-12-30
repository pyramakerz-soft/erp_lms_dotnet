import { RolePage } from "./role-page";

export class RoleAdd {
    constructor(
        public name: string = '',
        public pages:RolePage[]=[]
    ) {}
}
