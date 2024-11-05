export class DetailedPermission {
    forEach(arg0: (detail: any) => void) {
      throw new Error('Method not implemented.');
    }
    constructor(
        public detailedPermissionID: number,
        public detailedPermissionName: string
    ) {}
}
