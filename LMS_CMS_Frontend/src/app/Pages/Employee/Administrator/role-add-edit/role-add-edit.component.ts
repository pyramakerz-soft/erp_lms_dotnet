import { Component } from '@angular/core';
import { PagesWithRoleId } from '../../../../Models/pages-with-role-id';
import { PageService } from '../../../../Services/Employee/page.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { TokenData } from '../../../../Models/token-data';
import { firstValueFrom } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RoleAdd } from '../../../../Models/Administrator/role-add';
import { RoleService } from '../../../../Services/Employee/role.service';
import Swal from 'sweetalert2';
import { RoleDetailsService } from '../../../../Services/Employee/role-details.service';
import { RolePut } from '../../../../Models/Administrator/role-put';

@Component({
  selector: 'app-role-add-edit',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './role-add-edit.component.html',
  styleUrl: './role-add-edit.component.css'
})
export class RoleAddEditComponent {

  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  DomainName: string = "";
  UserID: number = 0;
  path: string = "";
  data: PagesWithRoleId[] = []
  PagecollapseStates: boolean[][] = [];
  collapseStates: boolean[] = [];
  checkboxStates: { [key: string]: boolean } = {};

  ResultArray: {
    Rowkey: number;
    pageId: number;
    allow_Edit: boolean;
    allow_Delete: boolean;
    allow_Edit_For_Others: boolean;
    allow_Delete_For_Others: boolean;
    IsSave: boolean;
  }[] = [];

  RoleName: string = ""
  RoleId:number=0;
  mode:string=""
  DataToSaveCreate: RoleAdd = new RoleAdd();
  DataToSaveEdit: RolePut = new RolePut();


  dataForEdit: PagesWithRoleId[] = []


  constructor(public pageServ: PageService,public RoleDetailsServ:RoleDetailsService, public activeRoute: ActivatedRoute, public account: AccountService, public ApiServ: ApiService, private menuService: MenuService, public EditDeleteServ: DeleteEditPermissionService, public RoleServ: RoleService, private router: Router) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    if (this.User_Data_After_Login.type === "employee") {
      this.DomainName = this.ApiServ.GetHeader();
      this.activeRoute.url.subscribe(url => {
        this.path = url[0].path
        this.getAllPges();
        if(this.path=="Role Edit"){
          this.RoleId = Number(this.activeRoute.snapshot.paramMap.get('id'))
          this.mode="Edit";
          this.GetRoleName();
          this.GetAllPgesForEdit();
        }
        else if(this.path=="Role Create"){
          this.mode="Create";
        }
      });
    }
  }

  GetRoleName(){
    this.RoleServ.Get_Role_id(this.RoleId,this.DomainName).subscribe((data)=>{
      this.RoleName=data.name;
    })
  }
  async getAllPges() {
    try {
      const data = await firstValueFrom(this.pageServ.Get_Pages(this.DomainName));
      this.data = data;
      this.data.forEach(item => {
        this.ResultArray.push({
          Rowkey: 0,
          pageId: item.id,
          allow_Edit: false,
          allow_Delete: false,
          allow_Edit_For_Others: false,
          allow_Delete_For_Others: false,
          IsSave: false,
        });
        item.children.forEach(element => {
          this.ResultArray.push({
            Rowkey: item.id,
            pageId: element.id,
            allow_Edit: false,
            allow_Delete: false,
            allow_Edit_For_Others: false,
            allow_Delete_For_Others: false,
            IsSave: false,
          });
        });
      });
      this.collapseStates = new Array(this.data.length).fill(false);
      this.PagecollapseStates = this.data.map(() => new Array(this.data[0].children.length).fill(false));
    } catch (error) {
      this.data = [];
      console.log('Error loading data:', error);
    }
  }

  async GetAllPgesForEdit(): Promise<void> {
    try {
      this.dataForEdit = await firstValueFrom(
        this.RoleDetailsServ.Get_Pages_With_RoleID(this.RoleId, this.DomainName)
      );
    } catch (error) {
      console.error('Error fetching pages for edit:', error);
    }

    this.dataForEdit.forEach(element => {
      const resultItems1 = this.ResultArray.find(item => item.Rowkey === 0 && item.pageId === element.id);
      if(resultItems1){
        resultItems1.IsSave=true;
        resultItems1.allow_Delete=true;
        resultItems1.allow_Edit=true;
        resultItems1.allow_Edit_For_Others=true;
        resultItems1.allow_Delete_For_Others=true;
      }
      element.children.forEach(item => {
        const resultItems2 = this.ResultArray.find(i => i.Rowkey ===element.id && i.pageId === item.id);
      if(resultItems2){
        resultItems2.IsSave=true;
        resultItems2.allow_Delete=item.allow_Delete;
        resultItems2.allow_Edit=item.allow_Edit;
        resultItems2.allow_Edit_For_Others=item.allow_Edit_For_Others;
        resultItems2.allow_Delete_For_Others=item.allow_Delete_For_Others;
      }
      });
    });
  }

  togglePageCollapse(rowIndex: number, pageIndex: number) {
    this.PagecollapseStates[rowIndex][pageIndex] = !this.PagecollapseStates[rowIndex][pageIndex];
  }

  toggleCollapse(rowIndex: number) {
    this.collapseStates[rowIndex] = !this.collapseStates[rowIndex];
  }

  toggleCheckboxState(id: string) {
    this.checkboxStates[id] = !this.checkboxStates[id];
  }

  isChecked(id: string): boolean {
    return this.checkboxStates[id] || false;
  }

  SaveCheck(row: number, page: number, per: 'allow_Edit' | 'allow_Delete' | 'allow_Edit_For_Others' | 'allow_Delete_For_Others') {
    const resultItem = this.ResultArray.find((item) => item.Rowkey === row && item.pageId === page);
    if (resultItem) {
      resultItem[per] = !resultItem[per];
    }
  }

  getPermissionState(rowId: number, pageId: number, permission: 'allow_Edit' | 'allow_Delete' | 'allow_Edit_For_Others' | 'allow_Delete_For_Others'): boolean {
    const resultItem = this.ResultArray.find(item => item.Rowkey === rowId && item.pageId === pageId);
    return resultItem ? resultItem[permission] : false;
  }

  setPermissionState(rowId: number, pageId: number, permission: 'allow_Edit' | 'allow_Delete' | 'allow_Edit_For_Others' | 'allow_Delete_For_Others'): void {
    const resultItem = this.ResultArray.find(item => item.Rowkey === rowId && item.pageId === pageId);
    if (resultItem) {
      resultItem[permission] = !resultItem[permission];
      if (resultItem[permission] == true && resultItem.IsSave == false) {
        resultItem.IsSave = true;
      }
    }
  }
  checkForPage(pageId: number, RowId: number): boolean {
    const resultItem = this.ResultArray.find(item => item.Rowkey === RowId && item.pageId === pageId);
    return resultItem ? resultItem.IsSave : false;
  }

  SetForPage(RowId: number, PageId: number): void {
    const resultItem = this.ResultArray.find(item => item.Rowkey === RowId && item.pageId === PageId);
    if (resultItem) {
      resultItem.IsSave = !resultItem.IsSave;
      const resultItems = this.ResultArray.filter(item => item.Rowkey === RowId);
      const allChecked = resultItems.every(item => item.IsSave);
      const anyChecked = resultItems.some(item => item.IsSave);
      if (allChecked || !anyChecked) {
        resultItems.forEach(item => (item.IsSave = allChecked));
      }
    }
  }

  checkForRow(RowId: number): boolean {
    const resultItems = this.ResultArray.filter(item => item.Rowkey === RowId);
    return resultItems.some(item => item.IsSave);
  }

  SetForRow(RowId: number): void {
    const resultItems = this.ResultArray.filter(item => item.Rowkey === RowId);
    const resultItems2 = this.ResultArray.find(item => item.Rowkey === 0 && item.pageId === RowId);
    if (resultItems.length > 0) {
      const newState = !this.checkForRow(RowId);
      resultItems.forEach(item => {
        item.IsSave = newState;
      });
      if (resultItems2) {
        resultItems2.IsSave = newState;
      }
    }
  }

  Save() {
    const resultItems = this.ResultArray.filter(item => item.IsSave === true);
    if(this.mode=="Create"){
      this.DataToSaveCreate.name = this.RoleName
      this.DataToSaveCreate.pages = resultItems;
      this.RoleServ.AddRole(this.DataToSaveCreate, this.DomainName).subscribe({
        next: (response) => {
          this.router.navigateByUrl("Employee/Role")
        },
        error: (error) => {
          const errorMessage = error?.error || 'An unexpected error occurred.';
  
          Swal.fire({
            icon: 'error',
            title: 'Error',
            confirmButtonColor: '#FF7519',
            text: errorMessage,
          });
        },
      });
    }
    else if(this.mode=="Edit"){
      this.DataToSaveEdit.id=this.RoleId;
      this.DataToSaveEdit.name = this.RoleName
      this.DataToSaveEdit.pages = resultItems;
      this.RoleServ.EditRole(this.DataToSaveEdit, this.DomainName).subscribe({
        next: (response) => {
          this.router.navigateByUrl("Employee/Role")
        },
        error: (error) => {
          const errorMessage = error?.error || 'An unexpected error occurred.';
  
          Swal.fire({
            icon: 'error',
            title: 'Error',
            confirmButtonColor: '#FF7519',
            text: errorMessage,
          });
        },
      });
    }

  }
}
