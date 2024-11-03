import { Module } from "./module";

export class Role {
    constructor(
        public roleID: number,
        public roleName: string,
        public modules: Module[]
    ) {}
}
