import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BusService } from '../../../../Services/Employee/Bus/bus.service';
import { AccountService } from '../../../../Services/account.service';
import { TokenData } from '../../../../Models/token-data';
import { Bus } from '../../../../Models/Bus/bus';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { Domain } from '../../../../Models/domain';
import { BusType } from '../../../../Models/Bus/bus-type';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { BusDistrictService } from '../../../../Services/Employee/Bus/bus-district.service';
import { BusStatusService } from '../../../../Services/Employee/Bus/bus-status.service';
import { BusCompanyService } from '../../../../Services/Employee/Bus/bus-company.service';
import { Employee } from '../../../../Models/Employee/employee';
import { EmployeeService } from '../../../../Services/Employee/employee.service'; 
import Swal from 'sweetalert2';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuService } from '../../../../Services/shared/menu.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { ApiService } from '../../../../Services/api.service';
import { SearchComponent } from '../../../../Component/search/search.component';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-bus-details',
  standalone: true,
  imports: [FormsModule,CommonModule,SearchComponent],
  templateUrl: './bus-details.component.html',
  styleUrl: './bus-details.component.css'
})
export class BusDetailsComponent {
  User_Data_After_Login :TokenData =new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  DomainData: Domain[] = []
  busData :Bus[] = []
  bus :Bus  = new Bus()
  editBus = false
  DomainName: string = "";

  BusType: BusType[] = []
  BusDistrict: BusType[] = []
  BusStatus: BusType[] = []
  BusCompany: BusType[] = []
  BusDriver: Employee[] = []
  BusDriverAssistant: Employee[] = []
  UserID:number=0;
  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
 
  IsChoosenDomain: boolean = false;
  IsEmployee: boolean = true;
  
  validationErrors: { [key in keyof Bus]?: string } = {};

  key: string= "id";
  value: any = "";
  keysArray: string[] = ['id', 'name','capacity','backPrice','twoWaysPrice','morningPrice','busTypeName','busDistrictsName','busStatusName','driverName','driverAssistantName','busCompanyName'];

  path:string = ""

  constructor(public busService:BusService, public account:AccountService, public activeRoute:ActivatedRoute, public DomainServ: DomainService, public BusTypeServ: BusTypeService, 
    public busDistrictServ: BusDistrictService, public busStatusServ: BusStatusService, public BusCompanyServ: BusCompanyService, public EmployeeServ: EmployeeService, 
    private menuService: MenuService,public EditDeleteServ:DeleteEditPermissionService, public router:Router,public ApiServ:ApiService){}

  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID=this.User_Data_After_Login.id;
    if (this.User_Data_After_Login.type === "employee") {
      this.IsChoosenDomain = true;
      this.DomainName=this.ApiServ.GetHeader();
      this.busService.Get(this.DomainName).subscribe(
        (data: any) => {
          this.busData = data;
        }
      );

      this.activeRoute.url.subscribe(url => {
        this.path = url[0].path
      });

      this.menuService.menuItemsForEmployee$.subscribe((items) => {
        const settingsPage = this.menuService.findByPageName(this.path, items);
        if (settingsPage) {
          this.AllowEdit = settingsPage.allow_Edit;
          this.AllowDelete = settingsPage.allow_Delete;
          this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others
          this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
        }
      });
    } else if (this.User_Data_After_Login.type === "octa") {
      this.getAllDomains();
      this.IsEmployee = false;
      this.AllowEdit = true;
      this.AllowDelete = true;
    }
  }

  getAllDomains() {
    this.DomainServ.Get().subscribe((data) => {
      this.DomainData = data;
    })
  }

  getBusDataByDomainId(event:Event){
    this.IsChoosenDomain=true;
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.DomainName=selectedValue;
    this.busData = []

    this.busService.Get(this.DomainName).subscribe(
      (data: any) => {
        this.busData=[]
        this.busData = data;
      } ,(error)=>{
        this.busData=[];
      });
  }

  deleteBus(busId: number) {
    Swal.fire({
      title: 'Are you sure you want to delete bus?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.busService.DeleteBus(busId,this.DomainName).subscribe(
          (data: any) => {
            this.busData=[]
            this.busService.Get(this.DomainName).subscribe(
              (data: any) => {
                this.busData = data;
              }
            );
          }
        );
      }
    });
  }

  openModal(busId?: number) {
    this.bus.busCompanyID=null;
    if (busId) {
      this.editBus = true;
      this.GetBusById(busId); 
    }
    
    this.GetBusType();
    this.GetBusDistricts();
    this.GetBusStatus();
    this.GetBusCompany();
    this.GetBusDriver();
    this.GetBusDriverAssistant();
    
    document.getElementById("Add_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Modal")?.classList.add("flex");
  }

  closeModal() {
    document.getElementById("Add_Modal")?.classList.remove("flex");
    document.getElementById("Add_Modal")?.classList.add("hidden");
    this.BusType= []
    this.BusDistrict= []
    this.BusStatus= []
    this.BusCompany= []
    this.BusDriver= []
    this.BusDriverAssistant= []
    this.bus = new Bus()

    if(this.editBus){
      this.editBus = false
    }

    this.validationErrors = {}; 
  }

  GetBusById(busId: number) {
    this.busService.GetbyBusId(busId,this.DomainName).subscribe((data) => {
      this.bus = data;
    });
  }

  GetBusType() {
    this.BusTypeServ.Get(this.DomainName).subscribe((data) => {
      this.BusType = data;
    });
  }
 
  GetBusDistricts() {
    this.busDistrictServ.Get(this.DomainName).subscribe((data) => {
      this.BusDistrict = data;
    });
  }

  GetBusStatus() {
    this.busStatusServ.Get(this.DomainName).subscribe((data) => {
      this.BusStatus = data;
    });
  }
  
  GetBusCompany() {
    this.BusCompanyServ.Get(this.DomainName).subscribe((data) => {
      this.BusCompany = data;
    });
  }
  
  GetBusDriver() {
    this.EmployeeServ.GetWithTypeId(2).subscribe((data) => {
      this.BusDriver = data;
    });
  }
  
  GetBusDriverAssistant() {
    this.EmployeeServ.GetWithTypeId(3).subscribe((data) => {
      this.BusDriverAssistant = data;
    });
  }

  onIsDistrictedChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.bus.isCapacityRestricted = isChecked
  }

  capitalizeField(field: keyof Bus): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.bus) {
      if (this.bus.hasOwnProperty(key)) {
        const field = key as keyof Bus;
        if (!this.bus[field]) {
          if(field == "name" || field == 'capacity'|| field == 'backPrice'|| field == 'twoWaysPrice'|| field == 'morningPrice'){
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        } else {
          if(field == "name"){
            if(this.bus.name.length > 100){
              this.validationErrors[field] = `*${this.capitalizeField(field)} cannot be longer than 100 characters`
              isValid = false;
            }
          } else{
            this.validationErrors[field] = '';
          }
        }
      }
    }
    return isValid;
  }

  onInputValueChange(event: { field: keyof Bus, value: any }) {
    const { field, value } = event;
    if (field == "name" || field == "capacity"|| field == "twoWaysPrice"|| field == "backPrice"|| field == "morningPrice") {
      (this.bus as any)[field] = value;
      if (value) {
        this.validationErrors[field] = '';
      }
    }
  }
  
  validateNumber(event: any, field: keyof Bus): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = ''; 
      if (typeof this.bus[field] === 'string') {
        this.bus[field] = '' as never;  
      }
    }
  }

  SaveBus(){
    if (this.isFormValid()) {
      if(this.editBus == false){
        this.busService.Add(this.bus,this.DomainName).subscribe(
          (result: any) => {
            this.closeModal()
            this.busService.Get(this.DomainName).subscribe(
              (data: any) => {
                this.busData = data;
              }
            );
          },
          error => {
          }
        );
      } else{
        this.busService.Edit(this.bus,this.DomainName).subscribe(
          (result: any) => {
            this.closeModal()
            this.busService.Get(this.DomainName).subscribe(
              (data: any) => {
                this.busData = data;
              }
            );
          },
          error => {
          }
        );
      }  
    }
  }

  MoveToBusStudent(busId:number){
    this.router.navigateByUrl('Employee/Bus Students/' + this.DomainName + '/' + busId);
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

  async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: Bus[] = await firstValueFrom(this.busService.Get(this.DomainName));  
      this.busData = data || [];
  
      if (this.value !== "") {
        const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);
  
        this.busData = this.busData.filter(t => {
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
    } catch (error) {
      this.busData = [];
    }
  }
  
}
