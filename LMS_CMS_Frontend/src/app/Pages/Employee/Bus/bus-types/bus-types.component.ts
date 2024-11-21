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

@Component({
  selector: 'app-bus-types',
  standalone: true,
  imports: [FormsModule, CommonModule],  // Make sure FormsModule is included here
  templateUrl: './bus-types.component.html',
  styleUrls: ['./bus-types.component.css']
})
export class BusTypesComponent {

  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  TableData: BusType[] = []
  DomainData: Domain[] = []
  ChoosenDomain: number = 1;
  newType: string = '';
  isModalVisible: boolean = false; // Modal is visible by default
  EditType:BusType=new BusType(0,"",0);



  constructor(private router: Router, private menuService: MenuService, public account: AccountService, public BusTypeServ: BusTypeService, public DomainServ: DomainService) { }

  ngOnInit() {
    this.GetAllDomains();
    this.GetTableData(1);
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName('BusType', items);
      this.AllowEdit = settingsPage.allow_Edit;
      this.AllowDelete = settingsPage.allow_Delete;

    });
  }

  AddNewType() {
    this.BusTypeServ.Add(this.ChoosenDomain, this.newType).subscribe((data) => {
      console.log(data);
      this.GetTableData(1);
      this.isModalVisible = false;

    });
  }
  GetAllDomains() {
    this.DomainServ.Get().subscribe((data) => {
      console.log(data)
      this.DomainData = data;
    })
  }
  GetTableData(id: number) {
    console.log("Domain selected:", id);
    if (this.ChoosenDomain !== null) {
      this.BusTypeServ.GetByDomainId(this.ChoosenDomain).subscribe((data) => {
        console.log("Fetched data:", data);
        this.TableData = data;
      });
    } else {
      console.log("No domain selected");
    }
  }

  openModal() {
    this.isModalVisible = true;
  }

  // Optionally, you can have a method to close the modal
  closeModal() {
    this.isModalVisible = false;
  }

  Delete(id: number) {
    this.BusTypeServ.Delete(id).subscribe((data) => {
      this.GetTableData(1);
    })
  }
  Edit(id:number){
  }
}
