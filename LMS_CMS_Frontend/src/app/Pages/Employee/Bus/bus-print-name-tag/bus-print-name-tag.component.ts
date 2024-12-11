import { Component } from '@angular/core';
import { TokenData } from '../../../../Models/token-data';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { BusStudentService } from '../../../../Services/Employee/Bus/bus-student.service';
import { AccountService } from '../../../../Services/account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuService } from '../../../../Services/shared/menu.service';
import { Bus } from '../../../../Models/Bus/bus';
import { Domain } from '../../../../Models/domain';
import { BusStudent } from '../../../../Models/Bus/bus-student';
import { BusService } from '../../../../Services/Employee/Bus/bus.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-bus-print-name-tag',
  standalone: true,
  imports: [CommonModule, FormsModule, SearchComponent],
  templateUrl: './bus-print-name-tag.component.html',
  styleUrl: './bus-print-name-tag.component.css'
})
export class BusPrintNameTagComponent {

  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  UserID: number = 0;
  path: string = ""

  key: string = "id";
  value: any = "";
  keysArray: string[] = ['id', 'schoolName', "studentName", "busCategoryName", "semseterName", "isException", "gradeName", "className", "isException"];

  AllowEdit: any;
  AllowDelete: any;
  AllowDeleteForOthers: any;

  IsChoosenDomain: boolean = false;
  IsEmployee: boolean = true;

  DomainData: Domain[] = []
  BusData: Bus[] = []

  busStudentData: BusStudent[] = []

  DomainName: string = "";
  busId: number = 0

  constructor(private router: Router, private menuService: MenuService, public activeRoute: ActivatedRoute, public account: AccountService, public busStudentServ: BusStudentService, public DomainServ: DomainService, public EditDeleteServ: DeleteEditPermissionService, public ApiServ: ApiService, public BusServ: BusService) { }

  ngOnInit() {

    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    if (this.User_Data_After_Login.type === "employee") {
      this.IsChoosenDomain = true;
      this.DomainName = this.ApiServ.GetHeader();
      this.activeRoute.url.subscribe(url => {
        this.path = url[0].path
        this.GetAllBus();
      });

      this.menuService.menuItemsForEmployee$.subscribe((items) => {
        const settingsPage = this.menuService.findByPageName(this.path, items);
        if (settingsPage) {
          this.AllowEdit = settingsPage.allow_Edit;
          this.AllowDelete = settingsPage.allow_Delete;
          this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others
          this.AllowDeleteForOthers = settingsPage.allow_Edit_For_Others
        }
      });
    } else if (this.User_Data_After_Login.type === "octa") {
      this.GetAllDomains();
      this.IsEmployee = false;
      this.AllowEdit = true;
      this.AllowDelete = true;
    }
  }

  GetAllDomains() {
    this.DomainServ.Get().subscribe((data) => {
      this.DomainData = data;
    })
  }

  GetAllBus() {
    this.BusServ.Get(this.DomainName).subscribe((data) => {
      this.BusData = data;
    })
  }

  async GetTableData(busId: number) {
    const data = await firstValueFrom(this.busStudentServ.GetbyBusId(busId, this.DomainName));
      this.busStudentData = data;
      console.log(this.busStudentData)
  }

  getDataByBusId(event: Event) {
    this.IsChoosenDomain = true;
    const selectedValue: number = Number((event.target as HTMLSelectElement).value);
    this.busId = selectedValue;
    console.log(this.busId,this.DomainName)
    this.GetTableData(this.busId);
  }

  DomainIsChanged(event: Event){
    this.IsChoosenDomain = true;
    const selectedValue: string = ((event.target as HTMLSelectElement).value);
    this.DomainName = selectedValue;
    this.busId=0;
    this.GetAllBus();
  }

  async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    await this.GetTableData(this.busId);
    if (this.value !== "") {
      const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);
      this.busStudentData = this.busStudentData.filter(t => {
        const fieldValue = t[this.key as keyof typeof t];
        if (typeof fieldValue === 'string') {
          return fieldValue.toLowerCase().includes(this.value.toLowerCase());
        }
        if (typeof fieldValue === 'number') {
          return fieldValue === numericValue;
        }
        return fieldValue == this.value;
      });
    }
  }
}