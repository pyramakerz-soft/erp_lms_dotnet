import { Component } from '@angular/core';
import { Bus } from '../../../../Models/Bus/bus';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { BusService } from '../../../../Services/Employee/Bus/bus.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BusStudentService } from '../../../../Services/Employee/Bus/bus-student.service';
import { BusStudent } from '../../../../Models/Bus/bus-student';
import Swal from 'sweetalert2';
import { MenuService } from '../../../../Services/shared/menu.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { ApiService } from '../../../../Services/api.service';
import { ShareDomainNameService } from '../../../../Services/Employee/share-domain-name.service';

@Component({
  selector: 'app-bus-student',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './bus-student.component.html',
  styleUrl: './bus-student.component.css'
})
export class BusStudentComponent {
  User_Data_After_Login :TokenData =new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  bus :Bus  = new Bus()
  busStudent :BusStudent  = new BusStudent()
  busId: number = 0
  busStudentData :BusStudent[] = []
  editBusStudent = false
  exception = false
  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
  UserID:number=0; 
  DomainName: string = "";


  constructor(public busService:BusService, public busStudentService:BusStudentService, public account:AccountService, public activeRoute:ActivatedRoute ,public EditDeleteServ:DeleteEditPermissionService,public menuService :MenuService,public ApiServ:ApiService,public DomainNameServ:ShareDomainNameService){}

  ngOnInit(){
    this.DomainNameServ.currentBusId.subscribe((DomainName) => {
      if (DomainName !== null) {
        this.DomainName=DomainName;
      }
    });
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID=this.User_Data_After_Login.id;
    this.busId = Number(this.activeRoute.snapshot.paramMap.get('busId'))
    this.GetBusById(this.busId);
    this.GetStudentsByBusId(this.busId);
    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName('Bus Student', items);
      this.AllowEdit = settingsPage.allow_Edit;
      this.AllowDelete = settingsPage.allow_Delete;
      this.AllowDeleteForOthers=settingsPage.allow_Delete_For_Others
      this.AllowEditForOthers=settingsPage.allow_Edit_For_Others
    });
  }

  GetBusById(busId:number){
    this.busService.GetbyBusId(busId,this.DomainName).subscribe((data) => {
      this.bus = data;
    });
  }
  
  GetStudentsByBusId(busId:number){
    this.busStudentService.GetbyBusId(busId,this.DomainName).subscribe((data) => {
      this.busStudentData = data;
    });
  }

  deleteBusStudent(busStudentId: number) {
    Swal.fire({
      title: 'Are you sure you want to delete student?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      this.busStudentService.DeleteBusStudent(busStudentId,this.DomainName).subscribe(
        (data: any) => {
          this.GetStudentsByBusId(this.busId);
        }
      );
    });
  }

  OpenModal(busStudentId?: number) {
    if (busStudentId) {
      this.editBusStudent = true;
      this.GetStudentsByBusId(this.busId);
    }
    
    document.getElementById("Add_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Modal")?.classList.add("flex");
  }

  closeModal() {
    document.getElementById("Add_Modal")?.classList.remove("flex");
    document.getElementById("Add_Modal")?.classList.add("hidden");
    this.busStudent = new BusStudent()

    if(this.editBusStudent){
      this.editBusStudent = false
    }
  }

  onIsExceptionChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.exception = isChecked
  }

  SaveBusStudent(){
    
  }
  IsAllowDelete(InsertedByID:number){
    const IsAllow=this.EditDeleteServ.IsAllowDelete(InsertedByID,this.UserID,this.AllowDeleteForOthers);
    return IsAllow;
  }
  IsAllowEdit(InsertedByID:number){
    const IsAllow=this.EditDeleteServ.IsAllowEdit(InsertedByID,this.UserID,this.AllowEditForOthers);
    return IsAllow;
  }
}
