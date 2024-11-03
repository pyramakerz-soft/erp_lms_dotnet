import { Component } from '@angular/core';
import { GetDataFromLayoutService } from '../../../Services/Employee/get-data-from-layout.service';
import { EmployeePermission } from '../../../Models/employee-permission';
import { DetailedPermission } from '../../../Models/detailed-permission';
import { ActivatedRoute, Route } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-management',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-management.component.html',
  styleUrl: './user-management.component.css'
})
export class UserManagementComponent {
  public Employee_With_Permission = new EmployeePermission(0, "", "", []);
  public details_permission:DetailedPermission[] =[];
  public path :string ="";

  constructor(public getDataService: GetDataFromLayoutService , private activatedRoute: ActivatedRoute){
  }

  ngOnInit() {
    this.activatedRoute.url.subscribe(urlSegments => {
      this.path = urlSegments.map(segment => segment.path).join('/');
    });

    this.Get_Detailed_Permissions()
  }

  Get_Detailed_Permissions(){
    this.getDataService.sharedData$.subscribe(data => {
      if (data) {
        this.Employee_With_Permission = data;
        this.Employee_With_Permission.roles.forEach(role => {
          role.modules.forEach(mod => {
            mod.masterPermissions.forEach(master => {
              master.detailedPermissions.forEach(detail => {
                if(`${mod.moduleName.replace(" ", "-")}/${master.masterPermissionName.replace(" ", "-")}` == this.path){
                  this.details_permission.push(detail)
                }
              });
            });
          });
        });
      }
    });
  }
}
