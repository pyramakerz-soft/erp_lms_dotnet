import { Component } from '@angular/core';
import { InstallmentDeductionMaster } from '../../../../Models/Accounting/installment-deduction-master';
import { InstallmentDeductionMasterService } from '../../../../Services/Employee/Accounting/installment-deduction-master.service';
import { firstValueFrom } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';
import { SearchComponent } from '../../../../Component/search/search.component';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';

@Component({
  selector: 'app-installment-deduction-master',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './installment-deduction-master.component.html',
  styleUrl: './installment-deduction-master.component.css'
})
export class InstallmentDeductionMasterComponent {
 User_Data_After_Login: TokenData = new TokenData('',0,0,0,0,'','','','','');

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  TableData: InstallmentDeductionMaster[] = [];

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  mode: string = '';

  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'name' ,'accountNumberName'];

  CurrentPage:number = 1
  PageSize:number = 10
  TotalPages:number = 1
  TotalRecords:number = 0
  isDeleting:boolean = false;

  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public BusTypeServ: BusTypeService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService,
    public InstallmentDeductionServ:InstallmentDeductionMasterService 
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

    this.GetAllData(this.CurrentPage, this.PageSize)
  }
  Create() {
    this.mode = 'Create';
    this.router.navigateByUrl(`Employee/Installment Deduction Details`)
  }

  Delete(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Installment Deduction?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.InstallmentDeductionServ.Delete(id,this.DomainName).subscribe((D)=>{
          this.GetAllData(this.CurrentPage, this.PageSize)
        })
      }
    });
  }

  Edit(row: InstallmentDeductionMaster) {
    this.router.navigateByUrl(`Employee/Installment Deduction Details/Edit/${row.id}`)
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

  async onSearchEvent(event: { key: string; value: any }) {
    // this.key = event.key;
    // this.value = event.value;
    // try {
    //   const data: InstallmentDeductionMaster[] = await firstValueFrom(
    //     this.InstallmentDeductionServ.Get(this.DomainName, this.CurrentPage, this.PageSize)
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

  GetAllData(pageNumber:number, pageSize:number){
    this.InstallmentDeductionServ.Get(this.DomainName, pageNumber, pageSize).subscribe(
      (data) => {
        this.CurrentPage = data.pagination.currentPage
        this.PageSize = data.pagination.pageSize
        this.TotalPages = data.pagination.totalPages
        this.TotalRecords = data.pagination.totalRecords 
        this.TableData = data.data
      }, 
      (error) => { 
        if(error.status == 404){
          if(this.TotalRecords != 0){
            let lastPage = this.TotalRecords / this.PageSize 
            console.log(lastPage)
            if(lastPage >= 1){
              if(this.isDeleting){
                this.CurrentPage = Math.floor(lastPage) 
                this.isDeleting = false
              } else{
                this.CurrentPage = Math.ceil(lastPage) 
              }
              this.GetAllData(this.CurrentPage, this.PageSize)
            }
          } 
        }
      }
    )
  }

  changeCurrentPage(currentPage:number){
    this.CurrentPage = currentPage
    this.GetAllData(this.CurrentPage, this.PageSize)
  }

  validatePageSize(event: any) { 
    const value = event.target.value;
    if (isNaN(value) || value === '') {
        event.target.value = '';
    }
  }

  View(id:number){
    this.router.navigateByUrl(`Employee/Installment Deduction Details/View/${id}`)
  }

  validateNumber(event: any): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
        event.target.value = '';
    }
  }
}
