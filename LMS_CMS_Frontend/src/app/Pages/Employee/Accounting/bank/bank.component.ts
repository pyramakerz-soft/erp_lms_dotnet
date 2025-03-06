import { Component } from '@angular/core';
import { Bank } from '../../../../Models/Accounting/bank';
import { BankService } from '../../../../Services/Employee/Accounting/bank.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import Swal from 'sweetalert2';
import { SearchComponent } from '../../../../Component/search/search.component';
import { AccountingTreeChart } from '../../../../Models/Accounting/accounting-tree-chart';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { AccountingTreeChartService } from '../../../../Services/Employee/Accounting/accounting-tree-chart.service';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';

@Component({
  selector: 'app-bank',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './bank.component.html',
  styleUrl: './bank.component.css'
})
export class BankComponent {
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

  TableData: Bank[] = [];

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  mode: string = '';

  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'name' ,"iban", "bankName", "bankAccountName" ,"accountClosingDate","accountOpeningDate" ,"accountNumberName" ];

  bank: Bank = new Bank();

  validationErrors: { [key in keyof Bank]?: string } = {};
  AccountNumbers:AccountingTreeChart[]=[];

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public BusTypeServ: BusTypeService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService ,
    public BankServ:BankService,
    public accountServ:AccountingTreeChartService ,
  ) {}
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
    this.GetAllAccount();
  }

  GetAllData() {
    this.BankServ.Get(this.DomainName).subscribe((d)=>{
      this.TableData=d;
    })
  }

  GetAllAccount(){
    this.accountServ.GetBySubAndFileLinkID(6,this.DomainName).subscribe((d)=>{
      this.AccountNumbers=d;
    })
  }

  Create() {
    this.mode = 'Create';
    this.bank=new Bank()
    this.openModal();
    this.validationErrors={}
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Bank?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.BankServ.Delete(id,this.DomainName).subscribe((d)=>{
          this.GetAllData()
        })
      }
    });
  }

  Edit(row: Bank) {
    this.mode = 'Edit';
    this.BankServ.GetById(row.id,this.DomainName).subscribe((d)=>{
      this.bank=d
    })
    this.validationErrors={}
    this.openModal();
  }

  validateNumber(event: any): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
        event.target.value = '';
    }
  }

  IsAllowDelete(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowDelete(
      InsertedByID,
      this.UserID,
      this.AllowDeleteForOthers
    );
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(
      InsertedByID,
      this.UserID,
      this.AllowEditForOthers
    );
    return IsAllow;
  }

  CreateOREdit() {
    if (this.isFormValid()) {
      if (this.mode == 'Create') {
        this.BankServ.Add(this.bank,this.DomainName).subscribe((d)=>{
          this.GetAllData()
        })
      }
      if (this.mode == 'Edit') {
        this.BankServ.Edit(this.bank,this.DomainName).subscribe((d)=>{
          this.GetAllData()
        })
      }
      this.closeModal()
    }
    this.GetAllData()
  }

  closeModal() {
    this.isModalVisible = false;
  }

  openModal() {
    this.isModalVisible = true;
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.bank) {
      if (this.bank.hasOwnProperty(key)) {
        const field = key as keyof Bank;
        if (!this.bank[field]) {
          if (
            field == 'name' ||
            field == 'bankAccountName' ||
            field == 'bankName' ||
            field == 'iban' ||
            field == 'accountOpeningDate' ||
            field == 'accountClosingDate' ||
            field == 'bankAccountNumber'  ||
            field == 'accountNumberId'
          ) {
            this.validationErrors[field] = `*${this.capitalizeField(
              field
            )} is required`;
            isValid = false;
          }
        }
      }
    }
    return isValid;
  }
  capitalizeField(field: keyof Bank): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }
  onInputValueChange(event: { field: keyof Bank; value: any }) {
    const { field, value } = event;
    (this.bank as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: Bank[] = await firstValueFrom(
        this.BankServ.Get(this.DomainName)
      );
      this.TableData = data || [];

      if (this.value !== '') {
        const numericValue = isNaN(Number(this.value))
          ? this.value
          : parseInt(this.value, 10);

        this.TableData = this.TableData.filter((t) => {
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
      this.TableData = [];
    }
  }
}
