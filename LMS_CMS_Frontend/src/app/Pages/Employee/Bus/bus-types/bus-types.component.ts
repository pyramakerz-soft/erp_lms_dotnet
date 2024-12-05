import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { TokenData } from '../../../../Models/token-data';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { BusType } from '../../../../Models/Bus/bus-type';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { Domain } from '../../../../Models/domain';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';

@Component({
  selector: 'app-bus-types',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './bus-types.component.html',
  styleUrls: ['./bus-types.component.css']
})
export class BusTypesComponent {

  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  EditType: BusType = new BusType(0, "", 0);

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  TableData: BusType[] = []
  DomainData: Domain[] = []

  DomainID: number = 0;
  UserID: number = 0;

  IsChoosenDomain: boolean = false;
  IsEmployee: boolean = true;
  
  newType: string = '';
  isModalVisible: boolean = false;
  mode: string = "";


  constructor(private router: Router, private menuService: MenuService, public account: AccountService, public BusTypeServ: BusTypeService, public DomainServ: DomainService, public EditDeleteServ: DeleteEditPermissionService) { }

  ngOnInit() {

    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    if (this.User_Data_After_Login.type === "employee") {
      this.IsChoosenDomain = true;
      this.GetTableData();
      this.menuService.menuItemsForEmployee$.subscribe((items) => {
        const settingsPage = this.menuService.findByPageName('Bus Type', items);
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
      });
    } else if (this.User_Data_After_Login.type === "octa") {
      this.GetAllDomains();
      this.IsEmployee = false;
      this.AllowEdit = true;
      this.AllowDelete = true;
    }
  }

  Create() {
    this.mode = "add";
    this.openModal();
  }

  AddNewType() {
    this.BusTypeServ.Add( this.newType).subscribe((data) => {
      this.GetTableData();
      this.closeModal();
    });
  }

  GetAllDomains() {
    this.DomainServ.Get().subscribe((data) => {
      this.DomainData = data;
    })
  }

  GetTableData() {
    if (this.DomainID !== null) {
      this.BusTypeServ.Get().subscribe((data) => {
        this.TableData = [];
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
    this.BusTypeServ.Delete(id).subscribe((data) => {
      this.GetTableData();
    })
  }

  Edit(id: number) {
    this.mode = "edit";
    const typeToEdit = this.TableData.find((t) => t.id === id);
    if (typeToEdit) {
      this.EditType = { ...typeToEdit };
      this.newType = this.EditType.name;
      this.openModal();
    } else {
      console.error("Type not found!");
    }
  }

  Save() {
    this.EditType.name = this.newType;
    this.BusTypeServ.Edit(this.EditType).subscribe(() => {
      this.GetTableData();
      this.closeModal();
      this.newType = "";
    })
  }

  CreateOREdit() {
    if (this.mode === "add") {
      this.AddNewType();
    }
    else if (this.mode === "edit") {
      this.Save();
    }
  }

  getBusDataByDomainId(event: Event) {
    this.IsChoosenDomain = true;
    const selectedValue: number = Number((event.target as HTMLSelectElement).value);
    this.DomainID = selectedValue;
    this.GetTableData();
  }

  IsAllowDelete(InsertedByID: number) {
    if (this.IsEmployee == false) { return true; }
    const IsAllow = this.EditDeleteServ.IsAllowDelete(InsertedByID, this.UserID, this.AllowDeleteForOthers);
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    if (this.IsEmployee == false) { return true; }
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }
}
