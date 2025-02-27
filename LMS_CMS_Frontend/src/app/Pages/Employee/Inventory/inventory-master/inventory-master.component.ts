import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import Swal from 'sweetalert2';
import { SearchComponent } from '../../../../Component/search/search.component';
import { Sales } from '../../../../Models/Inventory/sales';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { SalesService } from '../../../../Services/Employee/Inventory/sales.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { InventoryFlag } from '../../../../Models/Inventory/inventory-flag';

@Component({
  selector: 'app-inventory-master',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './inventory-master.component.html',
  styleUrl: './inventory-master.component.css'
})
export class InventoryMasterComponent {
 User_Data_After_Login: TokenData = new TokenData('',0,0,0,0,'','','','','');
 
   AllowEdit: boolean = false;
   AllowDelete: boolean = false;
   AllowEditForOthers: boolean = false;
   AllowDeleteForOthers: boolean = false;
 
   TableData: Sales[] = [];
 
   DomainName: string = '';
   UserID: number = 0;
 
   isModalVisible: boolean = false;
   mode: string = '';
 
   path: string = '';
   key: string = 'id';
   value: any = '';
   keysArray: string[] = ['id', 'storeName' ,'invoiceNumber' ,'date' ,'amount'];
 
   CurrentPage:number = 1
   PageSize:number = 10
   TotalPages:number = 1
   TotalRecords:number = 0
   isDeleting:boolean = false;

   FlagId:number =0;

   inventoryFlag :InventoryFlag =new InventoryFlag()
 
   constructor(
     private router: Router,
     private menuService: MenuService,
     public activeRoute: ActivatedRoute,
     public account: AccountService,
     public BusTypeServ: BusTypeService,
     public DomainServ: DomainService,
     public EditDeleteServ: DeleteEditPermissionService,
     public ApiServ: ApiService,
     public salesServ:SalesService 
   ) {}
   ngOnInit() {
     this.User_Data_After_Login = this.account.Get_Data_Form_Token();
     this.UserID = this.User_Data_After_Login.id;
     this.DomainName = this.ApiServ.GetHeader();
     this.activeRoute.url.subscribe((url) => {
       this.path = url[0].path;
     });
     this.FlagId = this.activeRoute.snapshot.data['id'];
     console.log(this.FlagId);
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
     this.router.navigateByUrl(`Employee/Sales Item`)
   }
 
   Delete(id: number) {
     Swal.fire({
       title: 'Are you sure you want to delete this Sales?',
       icon: 'warning',
       showCancelButton: true,
       confirmButtonColor: '#FF7519',
       cancelButtonColor: '#17253E',
       confirmButtonText: 'Delete',
       cancelButtonText: 'Cancel',
     }).then((result) => {
       if (result.isConfirmed) {
         this.salesServ.Delete(id,this.DomainName).subscribe((D)=>{
           this.GetAllData(this.CurrentPage, this.PageSize)
         })
       }
     });
   }
 
   Edit(row: Sales) {
     this.router.navigateByUrl(`Employee/Sales Item/Edit/${row.id}`)
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
     this.key = event.key;
     this.value = event.value;
     try {
       const data: any = await firstValueFrom(
         this.salesServ.Get(this.DomainName, 1, this.CurrentPage, this.PageSize)
       );
       this.TableData = data.data || [];
 
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
 
   GetAllData(pageNumber:number, pageSize:number){
     this.salesServ.Get(this.DomainName,this.FlagId, pageNumber, pageSize).subscribe(
       (data) => {
        console.log(data)
         this.CurrentPage = data.pagination.currentPage
         this.PageSize = data.pagination.pageSize
         this.TotalPages = data.pagination.totalPages
         this.TotalRecords = data.pagination.totalRecords 
         this.TableData = data.data
         this.inventoryFlag = data.inventoryFlag
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
         this.TableData=[]
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
     this.router.navigateByUrl(`Employee/Sales Item/View/${id}`)
   }
 
   validateNumber(event: any): void {
     const value = event.target.value;
     if (isNaN(value) || value === '') {
         event.target.value = '';
     }
   }
 }
 