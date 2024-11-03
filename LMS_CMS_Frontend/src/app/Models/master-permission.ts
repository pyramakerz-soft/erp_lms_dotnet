import { DetailedPermission } from "./detailed-permission";

export class MasterPermission {
    constructor(
        public masterPermissionID: number,
        public masterPermissionName: string,
        public detailedPermissions: DetailedPermission[]
    ) {}
}
