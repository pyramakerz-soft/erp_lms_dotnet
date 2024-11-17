import { Component } from '@angular/core';
import { SchoolAdd } from '../../../Models/school-add';
import { TokenData } from '../../../Models/token-data';
import { AccountService } from '../../../Services/account.service';
import { SchoolService } from '../../../Services/Domain/school.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RoleAdd } from '../../../Models/role-add';
import { SchoolRoleService } from '../../../Services/Domain/school-role.service';
import { ModuleDomainService } from '../../../Services/Domain/module-domain.service';
import { DomainModule } from '../../../Models/domain-module';
import { DetailedPermission } from '../../../Models/detailed-permission';
import { MasterPermission } from '../../../Models/master-permission';
import { Module } from '../../../Models/module';

@Component({
  selector: 'app-add-school',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-school.component.html',
  styleUrls: ['./add-school.component.css'] // Note the correct property name is 'styleUrls'
})
export class AddSchoolComponent {
  newSchool: SchoolAdd = new SchoolAdd("", 0);
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "");
  roles: string[] = [];
  School_id: number = 0;
  newRole: RoleAdd = new RoleAdd(0, "");
  DomainModule: DomainModule[] = []

  selectedModules: { [moduleId: number]: boolean } = {};
  selectedMasterPermissions: { [masterPermissionId: number]: boolean } = {};
  selectedDetailedPermissions: { [detailedPermissionId: number]: boolean } = {};

  constructor(
    private router: Router,
    public schoolServ: SchoolService,
    public account: AccountService,
    public schoolRoleServ: SchoolRoleService,
    public permissionServ: ModuleDomainService
  ) {}

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.newSchool.domainId = this.User_Data_After_Login.id;

    this.permissionServ.Get_DomainModule_By_Id(this.User_Data_After_Login.id).subscribe(
      (d: any) => {
        this.DomainModule = d[0].modules; // Ensure it's an array
        this.initializeSelections();
      }
    );
  }

  private initializeSelections() {
    this.DomainModule.forEach(module => {
      this.selectedModules[module.moduleID] = false;
      module.permissions.forEach(permission => {
        this.selectedMasterPermissions[permission.masterPermissionID] = false;
        permission.detailedPermissions.forEach(detail => {
          this.selectedDetailedPermissions[detail.detailedPermissionID] = false;
        });
      });
    });
  }

  // Toggle selection for detailed permissions
  toggleDetailedPermission(detailId: number) {
    this.selectedDetailedPermissions[detailId] = !this.selectedDetailedPermissions[detailId];
  }


  
  addRole() {
    this.roles.push(''); // Push an empty string to create a new input
  }

  async addSchool() {
    this.schoolServ.AddSchool(this.newSchool).subscribe(
      (d: any) => {
        this.School_id = d;

        this.roles.forEach(element => {
          if (element.trim()) { // Check for non-empty strings
            this.newRole.role_Name = element;
            this.newRole.school_id = this.School_id;
            this.schoolRoleServ.AddSchoolRole(this.newRole).subscribe(
              (response: any) => {
                // Redirect to another page only after all roles are added
              }
            );
          }
        });
        this.router.navigateByUrl("Domain/Home"); // Navigate only after all roles are added
      }
    );
  }

  deleteRole(index: number) {
    this.roles.splice(index, 1); // Use splice for better performance
  }
}
