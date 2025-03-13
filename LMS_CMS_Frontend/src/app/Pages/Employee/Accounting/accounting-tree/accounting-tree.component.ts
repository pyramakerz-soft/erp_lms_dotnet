import { Component } from '@angular/core';
import { AccountingTreeChart } from '../../../../Models/Accounting/accounting-tree-chart';
import { Router, ActivatedRoute } from '@angular/router';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { SearchComponent } from '../../../../Component/search/search.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AccountingTreeChartService } from '../../../../Services/Employee/Accounting/accounting-tree-chart.service';
import { AccountingItemComponent } from '../../../../Component/Employee/Accounting/accounting-item/accounting-item.component';
import { LinkFileService } from '../../../../Services/Employee/Accounting/link-file.service';
import { MotionTypeService } from '../../../../Services/Employee/Accounting/motion-type.service';
import { SubTypeService } from '../../../../Services/Employee/Accounting/sub-type.service';
import { EndTypeService } from '../../../../Services/Employee/Accounting/end-type.service';
import { LinkFile } from '../../../../Models/Accounting/link-file';
import { MotionType } from '../../../../Models/Accounting/motion-type';
import { SubType } from '../../../../Models/Accounting/sub-type';
import { EndType } from '../../../../Models/Accounting/end-type';

@Component({
  selector: 'app-accounting-tree',
  standalone: true,
  imports: [SearchComponent, FormsModule, CommonModule, AccountingItemComponent],
  templateUrl: './accounting-tree.component.html',
  styleUrl: './accounting-tree.component.css'
})
export class AccountingTreeComponent {

  User_Data_After_Login: TokenData = new TokenData(
    '',
    0,
    0,
    0,
    0,
    '',
    '',
    '',
    '',
    ''
  );

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  TableData: AccountingTreeChart[] = [];
  MainData: AccountingTreeChart[] = [];
  LinkFileData: LinkFile[] = [];
  MotionTypeData: MotionType[] = [];
  SubTypeData: SubType[] = [];
  EndTypeData: EndType[] = [];

  DomainName: string = '';
  UserID: number = 0;
  
  isModalVisible: boolean = false;
  mode: string = '';
  
  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'name'];
  
  accountingTreeChart: AccountingTreeChart = new AccountingTreeChart();
  mainAccountingTreeChart: AccountingTreeChart = new AccountingTreeChart();
  isEdit: boolean = false;

  validationErrors: { [key in keyof AccountingTreeChart]?: string } = {};

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public BusTypeServ: BusTypeService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService,
    public accountingTreeChartService: AccountingTreeChartService,
    public linkFileService: LinkFileService,
    public motionTypeService: MotionTypeService,
    public subTypeService: SubTypeService,
    public endTypeService: EndTypeService
  ) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others;
      }
    });

    this.GetAllData();
    this.GetMainData();
    this.GetLinkFileData();
    this.GetMotionTypeData();
    this.GetSubTypeData();
    this.GetEndTypeData();
  }

  GetAllData() {
    this.accountingTreeChartService.Get(this.DomainName).subscribe(
      (data) => { 
        this.TableData = data
      }
    )
  }
  
  GetMainData() {
    this.accountingTreeChartService.GetByMainID(this.DomainName).subscribe(
      (data) => { 
        this.MainData = data
      }
    )
  }
  
  GetMainDataChildFiltered(id:number) {
    this.accountingTreeChartService.GetMainDataChildFiltered(id, this.DomainName).subscribe(
      (data) => { 
        this.MainData = data
      }
    )
  }
  
  GetDataByID() {
    let id = this.accountingTreeChart.id 
    this.validationErrors = {}
    if(this.accountingTreeChart.id && this.accountingTreeChart.id != null && this.accountingTreeChart.id != 0){
      this.accountingTreeChartService.GetByID(this.accountingTreeChart.id, this.DomainName).subscribe(
        (data) => { 
          this.isEdit = true
          this.accountingTreeChart = data
          if(this.accountingTreeChart.id)
          this.GetMainDataChildFiltered(this.accountingTreeChart.id)
        },
        (err) =>{
          this.isEdit = false
          this.accountingTreeChart = new AccountingTreeChart()
          this.accountingTreeChart.id = id
          this.GetMainData()
        }
      )
    } else{
      this.accountingTreeChart = new AccountingTreeChart()
    } 
  }
  
  GetMainByID() {
    if(this.accountingTreeChart.mainAccountNumberID == 0 || this.accountingTreeChart.mainAccountNumberID == null){
      this.mainAccountingTreeChart = new AccountingTreeChart()
      this.accountingTreeChart.motionTypeID = 0
      this.accountingTreeChart.endTypeID = 0
      this.accountingTreeChart.level = 1
    } else{
      this.accountingTreeChartService.GetByID(this.accountingTreeChart.mainAccountNumberID, this.DomainName).subscribe(
        (data) => { 
          this.mainAccountingTreeChart = data
          this.accountingTreeChart.motionTypeID = this.mainAccountingTreeChart.motionTypeID
          this.accountingTreeChart.endTypeID = this.mainAccountingTreeChart.endTypeID
          this.accountingTreeChart.level = this.mainAccountingTreeChart.level + 1
        }
      )
    }
  }
  
  GetLinkFileData() {
    this.linkFileService.Get(this.DomainName).subscribe(
      (data) => { 
        this.LinkFileData = data
      }
    )
  }
  
  GetMotionTypeData() {
    this.motionTypeService.Get(this.DomainName).subscribe(
      (data) => { 
        this.MotionTypeData = data
      }
    )
  }
  
  GetSubTypeData() {
    this.subTypeService.Get(this.DomainName).subscribe(
      (data) => { 
        this.SubTypeData = data
      }
    )
  }
  
  GetEndTypeData() {
    this.endTypeService.Get(this.DomainName).subscribe(
      (data) => { 
        this.EndTypeData = data
      }
    )
  }

  lockLinkFile(){
    this.accountingTreeChart.linkFileID = 0
  }

  handleIDSelected(accounting: number) {
    this.accountingTreeChart.id = accounting
    this.GetDataByID()
  }
   
  validateNumber(event: any, field: keyof AccountingTreeChart): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = ''; 
      if (typeof this.accountingTreeChart[field] === 'string') {
        this.accountingTreeChart[field] = '' as never;  
      } 
    }
  }

  capitalizeField(field: keyof AccountingTreeChart): string {
      return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.accountingTreeChart) {
      if (this.accountingTreeChart.hasOwnProperty(key)) {
        const field = key as keyof AccountingTreeChart;
        if (!this.accountingTreeChart[field]) {
          if(field == "name" || field == "subTypeID" || field == "id" 
            || (this.accountingTreeChart.subTypeID == 2 && field == "mainAccountNumberID")
            || ((this.accountingTreeChart.mainAccountNumberID == 0 ) && (field == "motionTypeID" || field == "endTypeID"))){
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        } else {
          if(field == "name"){
            if(this.accountingTreeChart.name.length > 100){
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

  onInputValueChange(event: { field: keyof AccountingTreeChart, value: any }) {
    const { field, value } = event;
    
    (this.accountingTreeChart as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
    if(field == "subTypeID"){
      this.validationErrors["mainAccountNumberID"] = ''
    }
    if(field == "mainAccountNumberID"){
      this.validationErrors["motionTypeID"] = ''
      this.validationErrors["endTypeID"] = ''
    }
  }

  Save(){
    if(this.isFormValid()){}
    if(this.isFormValid())
    {
      if(this.isEdit){
        this.accountingTreeChartService.Edit(this.accountingTreeChart, this.DomainName).subscribe(
          (data) => {
            this.GetAllData()
            this.validationErrors = {}; 
            this.accountingTreeChart = new AccountingTreeChart()
          }, (error) => {
          }
        )
      } else{
        this.accountingTreeChartService.Add(this.accountingTreeChart, this.DomainName).subscribe(
          (data) => {
            this.GetAllData()
            this.validationErrors = {}; 
            this.accountingTreeChart = new AccountingTreeChart()
          }, (error) => {
          }
        )
      }
    }
  }
}
