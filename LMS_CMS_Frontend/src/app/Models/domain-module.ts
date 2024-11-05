import { Module } from "./module";
import { Permissions } from "./permissions";

export class DomainModule {
    constructor(
        public moduleID: number,
        public moduleName: string,
        public permissions:Permissions[]
    ) {}
}
