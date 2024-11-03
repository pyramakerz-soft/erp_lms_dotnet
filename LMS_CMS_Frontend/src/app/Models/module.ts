import { MasterPermission } from "./master-permission";

export class Module {
    constructor(
        public moduleID: number,
        public moduleName: string,
        public masterPermissions: MasterPermission[]
    ) {}
}
