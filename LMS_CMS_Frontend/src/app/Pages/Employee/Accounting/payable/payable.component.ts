import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';
import { TokenData } from '../../../../Models/token-data';
import { Payable } from '../../../../Models/Accounting/payable';
import { ApiService } from '../../../../Services/api.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { PayableService } from '../../../../Services/Employee/Accounting/payable.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-payable',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './payable.component.html',
  styleUrl: './payable.component.css'
})
export class PayableComponent {
User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false; 

  DomainName: string = '';
  UserID: number = 0;

  keysArray: string[] = [];

  path: string = '';
  key: string = 'id';
  value: any = '';

  payableData: Payable[] = []

  CurrentPage:number = 1
  PageSize:number = 10
  TotalPages:number = 1
  TotalRecords:number = 0

  isDeleting:boolean = false;

  constructor(
    private router: Router, private menuService: MenuService, public activeRoute: ActivatedRoute, public account: AccountService, 
    public DomainServ: DomainService, public EditDeleteServ: DeleteEditPermissionService, public ApiServ: ApiService, public payableService:PayableService){}

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

    this.GetPayableDate(this.DomainName, this.CurrentPage, this.PageSize)
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    // try {
    //   const data: Supplier[] = await firstValueFrom(
    //     this.SupplierServ.Get(this.DomainName)
    //   );
    //   this.TableData = data || [];

    //   if (this.value !== '') {
    //     const numericValue = isNaN(Number(this.value))
    //       ? this.value
    //       : parseInt(this.value, 10);

    //     this.TableData = this.TableData.filter((t) => {
    //       const fieldValue = t[this.key as keyof typeof t];
    //       if (typeof fieldValue === 'string') {
    //         return fieldValue.toLowerCase().includes(this.value.toLowerCase());
    //       }
    //       if (typeof fieldValue === 'number') {
    //         return fieldValue === numericValue;
    //       }
    //       return fieldValue == this.value;
    //     });
    //   }
    // } catch (error) {
    //   this.TableData = [];
    // }
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

  validateNumber(event: any): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
        event.target.value = '';
    }
  }
  
  GetPayableDate(DomainName: string, pageNumber:number, pageSize:number){
    this.payableService.Get(DomainName, pageNumber, pageSize).subscribe(
      (data) => {
        this.CurrentPage = data.pagination.currentPage
        this.PageSize = data.pagination.pageSize
        this.TotalPages = data.pagination.totalPages
        this.TotalRecords = data.pagination.totalRecords 
        this.payableData = data.data
      }, 
      (error) => { 
        if(error.status == 404){
          if(this.TotalRecords != 0){ 
            let lastPage 
            if(this.isDeleting){
              lastPage = (this.TotalRecords - 1) / this.PageSize 
            }else{
              lastPage = this.TotalRecords / this.PageSize 
            } 
            if(lastPage >= 1){
              if(this.isDeleting){
                this.CurrentPage = Math.floor(lastPage) 
                this.isDeleting = false
              } else{
                this.CurrentPage = Math.ceil(lastPage) 
              }
              this.GetPayableDate(this.DomainName, this.CurrentPage, this.PageSize)
            }
          } 
        }
      }
    )
  }

  changeCurrentPage(currentPage:number){
    this.CurrentPage = currentPage
    this.GetPayableDate(this.DomainName, this.CurrentPage, this.PageSize)
  }

  validatePageSize(event: any) { 
    const value = event.target.value;
    if (isNaN(value) || value === '') {
        event.target.value = '';
    }
  }

  Create(id?:number, isEdit?:boolean){
    if(id){
      if(isEdit){
        this.router.navigateByUrl(`Employee/Payable Details/${id}`)
      }else{
        this.router.navigateByUrl(`Employee/Payable Details/View/${id}`)
      }
    } else{
      this.router.navigateByUrl("Employee/Payable Details")
    }
  }

  Delete(id:number){
    Swal.fire({
      title: 'Are you sure you want to delete this Payable?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.payableService.Delete(id,this.DomainName).subscribe((D)=>{
          this.isDeleting = true
          this.GetPayableDate(this.DomainName, this.CurrentPage, this.PageSize);
          if(this.TotalRecords == 1){
            this.TotalRecords = 0
            this.payableData = []
          }
        })
      }
    });
  }
}
