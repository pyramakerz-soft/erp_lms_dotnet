import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { ReceivableService } from '../../../../Services/Employee/Accounting/receivable.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { TokenData } from '../../../../Models/token-data';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Receivable } from '../../../../Models/Accounting/receivable';
import { ReceivableDocType } from '../../../../Models/Accounting/receivable-doc-type';
import { ReceivableDocTypeService } from '../../../../Services/Employee/Accounting/receivable-doc-type.service';
import { BankService } from '../../../../Services/Employee/Accounting/bank.service';
import { SaveService } from '../../../../Services/Employee/Accounting/save.service';

@Component({
  selector: 'app-receivable-details',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './receivable-details.component.html',
  styleUrl: './receivable-details.component.css'
})
export class ReceivableDetailsComponent {
  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false; 

  DomainName: string = '';
  UserID: number = 0;

  path: string = '';
  ReceivableID: number = 0;

  isCreate:boolean = false
  isEdit:boolean = false
  isView:boolean = false

  receivable:Receivable = new Receivable()
  validationErrors: { [key in keyof Receivable]?: string } = {};

  dataTypesData: ReceivableDocType[] = []
  bankOrSaveData: any[] = []

  constructor(
    private router: Router, private menuService: MenuService, public activeRoute: ActivatedRoute, public account: AccountService, public receivableDocTypeService:ReceivableDocTypeService,
    public DomainServ: DomainService, public EditDeleteServ: DeleteEditPermissionService, public ApiServ: ApiService, public receivableService:ReceivableService,
    public bankService:BankService, public saveService:SaveService){}
  
  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.ReceivableID = Number(this.activeRoute.snapshot.paramMap.get('id'))
    if(!this.ReceivableID){
      this.isCreate = true
    }

    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
      if(url[1].path == "View"){ 
        this.isView = true
      } else{
        if(this.ReceivableID){
          this.isEdit = true
        }
      } 
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

    this.GetDocType()
  }

  moveToReceivable() {
    this.router.navigateByUrl("Employee/Receivable")
  }

  onInputValueChange(event: { field: keyof Receivable, value: any }) {
    const { field, value } = event;
    (this.receivable as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  GetDocType(){
    this.receivableDocTypeService.Get(this.DomainName).subscribe(
      (data) => {
        this.dataTypesData = data
      }
    )
  }

  validateNumber(event: any): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
        event.target.value = '';
    }
  }

  Save() {
    console.log(this.receivable)
  }

  getBankData() {
    this.bankService.Get(this.DomainName).subscribe(
      (data) => {
        this.bankOrSaveData = data
      }
    )
  }
  
  getSaveData() {
    this.saveService.Get(this.DomainName).subscribe(
      (data) => {
        this.bankOrSaveData = data
      }
    )
  }
}
