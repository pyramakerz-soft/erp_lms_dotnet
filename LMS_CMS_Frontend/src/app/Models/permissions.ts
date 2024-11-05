import { DetailedPermission } from "./detailed-permission";
import { MasterPermission } from "./master-permission";

export class Permissions {
    constructor(
        public detailedPermissions:DetailedPermission[],
        public masterPermissionName: String,
        public masterPermissionID:number
    ) {}

}
