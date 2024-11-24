import { Component } from '@angular/core';
import { TokenData } from '../../../../Models/token-data';
import { BusType } from '../../../../Models/Bus/bus-type';
import { Domain } from '../../../../Models/domain';
import { Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { BusStatusService } from '../../../../Services/Employee/Bus/bus-status.service';
import { compileComponentClassMetadata } from '@angular/compiler';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';

@Component({
  selector: 'app-bus-status',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './bus-status.component.html',
  styleUrl: './bus-status.component.css'
})
export class BusStatusComponent {
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
  TableData: BusType[] = []
  DomainData: Domain[] = []
  IsChoosenDomain: boolean = false;
  newType: string = '';
  isModalVisible: boolean = false; 
  EditType:BusType=new BusType(0,"",0);
  mode:string="";
  DomainID:number=0;
  UserID:number=0;

  constructor(private router: Router, private menuService: MenuService, public account: AccountService, public busStatusServ: BusStatusService, public DomainServ: DomainService ,public EditDeleteServ:DeleteEditPermissionService) { }

  ngOnInit() {
    this.GetAllDomains();
    this.GetTableData(this.DomainID);
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID=this.User_Data_After_Login.id;

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName('Bus Status', items);
      console.log(settingsPage)
      this.AllowEdit = settingsPage.allow_Edit;
      this.AllowDelete = settingsPage.allow_Delete;
      this.AllowDeleteForOthers=settingsPage.allow_Delete_For_Others
      this.AllowEditForOthers=settingsPage.allow_Edit_For_Others
      console.log(this.AllowEditForOthers , this.AllowDeleteForOthers)
    });
  }
  Create(){
    this.mode="add";
    this.openModal();
  }

  AddNewType() {
    console.log(this.DomainID,this.newType)
    this.busStatusServ.Add(this.DomainID, this.newType).subscribe((data) => {
      this.closeModal();
      this.newType="";
      this.GetTableData(this.DomainID);

    });
  }
  GetAllDomains() {
    this.DomainServ.Get().subscribe((data) => {
      this.DomainData = data;
    })
  }
  GetTableData(id: number) {
    console.log("Domain selected:", id);
    if (this.DomainID !== null) {
      this.busStatusServ.GetByDomainId(id).subscribe((data) => {
        this.TableData=[];
        this.TableData = data;
      });
    } else {
      console.log("No domain selected");
    }
  }

  openModal() {
    this.isModalVisible = true;
  }

  closeModal() {
    this.isModalVisible = false;
  }

  Delete(id: number) {
    this.busStatusServ.Delete(id).subscribe((data) => {
      this.GetTableData(this.DomainID);
    })
  }
  Edit(id: number) {
    this.mode="edit";
    const typeToEdit = this.TableData.find((t) => t.id === id);
    if (typeToEdit) {
      this.EditType={ ...typeToEdit };
      this.newType = this.EditType.name;
      this.openModal();
    } else {
      console.error("Type not found!");
    }
  }

  Save(){
    this.EditType.name=this.newType;
    this.busStatusServ.Edit(this.EditType).subscribe(()=>{
      this.GetTableData(this.DomainID);
      this.closeModal();
      this.newType="";
    })
  }

  CreateOREdit(){
    console.log(this.mode)
    if(this.mode==="add"){
      this.AddNewType();
    }
    else if(this.mode==="edit"){
      this.Save();
    }
  }

  getBusDataByDomainId(event:Event){
    this.IsChoosenDomain=true;
    const selectedValue: number = Number((event.target as HTMLSelectElement).value);
    this.DomainID=selectedValue;
    this.GetTableData(selectedValue);
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
