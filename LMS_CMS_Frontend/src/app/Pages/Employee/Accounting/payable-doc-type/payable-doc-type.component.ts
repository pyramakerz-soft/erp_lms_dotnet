import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';

@Component({
  selector: 'app-payable-doc-type',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './payable-doc-type.component.html',
  styleUrl: './payable-doc-type.component.css'
})
export class PayableDocTypeComponent {

  // User_Data_After_Login: TokenData = new TokenData(
  //   '',
  //   0,
  //   0,
  //   0,
  //   0,
  //   '',
  //   '',
  //   '',
  //   '',
  //   ''
  // );

  // AllowEdit: boolean = false;
  // AllowDelete: boolean = false;
  // AllowEditForOthers: boolean = false;
  // AllowDeleteForOthers: boolean = false;

  // TableData: payabledoc[] = [];

  // DomainName: string = '';
  // UserID: number = 0;

  // isModalVisible: boolean = false;
  // mode: string = '';

  // path: string = '';
  // key: string = 'id';
  // value: any = '';
  // keysArray: string[] = ['id', 'name'];

  // accountingEntriesDocType: AccountingEntriesDocType = new AccountingEntriesDocType();

  // validationErrors: { [key in keyof AccountingEntriesDocType]?: string } = {};

  // constructor(
  //   private router: Router,
  //   private menuService: MenuService,
  //   public activeRoute: ActivatedRoute,
  //   public account: AccountService,
  //   public BusTypeServ: BusTypeService,
  //   public DomainServ: DomainService,
  //   public EditDeleteServ: DeleteEditPermissionService,
  //   public ApiServ: ApiService,
  //   public AccountingEntriesDocTypeServ: AccountingEntriesDocTypeService,
  // ) { }
  // ngOnInit() {
  //   this.User_Data_After_Login = this.account.Get_Data_Form_Token();
  //   this.UserID = this.User_Data_After_Login.id;
  //   this.DomainName = this.ApiServ.GetHeader();
  //   this.activeRoute.url.subscribe((url) => {
  //     this.path = url[0].path;
  //   });

  //   this.menuService.menuItemsForEmployee$.subscribe((items) => {
  //     const settingsPage = this.menuService.findByPageName(this.path, items);
  //     if (settingsPage) {
  //       this.AllowEdit = settingsPage.allow_Edit;
  //       this.AllowDelete = settingsPage.allow_Delete;
  //       this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others;
  //       this.AllowEditForOthers = settingsPage.allow_Edit_For_Others;
  //     }
  //   });

  //   this.GetAllData();
  // }

  // GetAllData() {
  //   this.AccountingEntriesDocTypeServ.Get(this.DomainName).subscribe((d) => {
  //     this.TableData = d
  //   })
  // }

  // Create() {
  //   this.mode = 'Create';
  //   this.openModal();
  // }

  // Delete(id: number) {
  //   Swal.fire({
  //     title: 'Are you sure you want to delete this Supplier?',
  //     icon: 'warning',
  //     showCancelButton: true,
  //     confirmButtonColor: '#FF7519',
  //     cancelButtonColor: '#17253E',
  //     confirmButtonText: 'Delete',
  //     cancelButtonText: 'Cancel',
  //   }).then((result) => {
  //     if (result.isConfirmed) {
  //       this.AccountingEntriesDocTypeServ.Delete(id, this.DomainName).subscribe((d) => {
  //         this.GetAllData()
  //       })
  //     }
  //   });
  // }

  // Edit(row: AccountingEntriesDocType) {
  //   this.mode = 'Edit';
  //   this.accountingEntriesDocType = row;
  //   this.openModal();
  // }

  // IsAllowDelete(InsertedByID: number) {
  //   const IsAllow = this.EditDeleteServ.IsAllowDelete(
  //     InsertedByID,
  //     this.UserID,
  //     this.AllowDeleteForOthers
  //   );
  //   return IsAllow;
  // }

  // IsAllowEdit(InsertedByID: number) {
  //   const IsAllow = this.EditDeleteServ.IsAllowEdit(
  //     InsertedByID,
  //     this.UserID,
  //     this.AllowEditForOthers
  //   );
  //   return IsAllow;
  // }

  // CreateOREdit() {
  //   if (this.isFormValid()) {
  //     if (this.mode == 'Create') {
  //       this.AccountingEntriesDocTypeServ.Add(this.accountingEntriesDocType, this.DomainName).subscribe((d) => {
  //         this.GetAllData();
  //         this.closeModal()
  //       })
  //     }
  //     if (this.mode == 'Edit') {
  //       this.AccountingEntriesDocTypeServ.Edit(this.accountingEntriesDocType, this.DomainName).subscribe((d) => {
  //         this.GetAllData();
  //         this.closeModal()
  //       })
  //     }
  //   }
  // }

  // closeModal() {
  //   this.isModalVisible = false;
  // }

  // openModal() {
  //   this.isModalVisible = true;
  // }

  // isFormValid(): boolean {
  //   let isValid = true;
  //   for (const key in this.accountingEntriesDocType) {
  //     if (this.accountingEntriesDocType.hasOwnProperty(key)) {
  //       const field = key as keyof AccountingEntriesDocType;
  //       if (!this.accountingEntriesDocType[field]) {
  //         if (
  //           field == 'name'
  //         ) {
  //           this.validationErrors[field] = `*${this.capitalizeField(
  //             field
  //           )} is required`;
  //           isValid = false;
  //         }
  //       }
  //     }
  //   }
  //   return isValid;
  // }
  // capitalizeField(field: keyof AccountingEntriesDocType): string {
  //   return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  // }
  // onInputValueChange(event: { field: keyof AccountingEntriesDocType; value: any }) {
  //   const { field, value } = event;
  //   (this.accountingEntriesDocType as any)[field] = value;
  //   if (value) {
  //     this.validationErrors[field] = '';
  //   }
  // }

  // async onSearchEvent(event: { key: string; value: any }) {
  //   this.key = event.key;
  //   this.value = event.value;
  //   try {
  //     const data: AccountingEntriesDocType[] = await firstValueFrom(
  //       this.AccountingEntriesDocTypeServ.Get(this.DomainName)
  //     );
  //     this.TableData = data || [];

  //     if (this.value !== '') {
  //       const numericValue = isNaN(Number(this.value))
  //         ? this.value
  //         : parseInt(this.value, 10);

  //       this.TableData = this.TableData.filter((t) => {
  //         const fieldValue = t[this.key as keyof typeof t];
  //         if (typeof fieldValue === 'string') {
  //           return fieldValue.toLowerCase().includes(this.value.toLowerCase());
  //         }
  //         if (typeof fieldValue === 'number') {
  //           return fieldValue === numericValue;
  //         }
  //         return fieldValue == this.value;
  //       });
  //     }
  //   } catch (error) {
  //     this.TableData = [];
  //   }
  // }
}
