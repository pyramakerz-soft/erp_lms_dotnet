import { Component } from '@angular/core';
import { Section } from '../../../../Models/LMS/section';
import { TokenData } from '../../../../Models/token-data';
import { School } from '../../../../Models/school';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../../Component/search/search.component';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { SchoolService } from '../../../../Services/Employee/school.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { SectionService } from '../../../../Services/Employee/LMS/section.service';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-section',
  standalone: true,
  imports: [FormsModule,CommonModule,SearchComponent],
  templateUrl: './section.component.html',
  styleUrl: './section.component.css'
})
export class SectionComponent {
  keysArray: string[] = ['id', 'name','schoolName'];
  key: string= "id";
  value: any = "";

  sectionData:Section[] = []
  section:Section = new Section()
  editSection:boolean = false
  validationErrors: { [key in keyof Section]?: string } = {};

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
  path: string = ""

  DomainName: string = "";
  UserID: number = 0;
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")

  Schools: School[] = []

  constructor(public account: AccountService, public ApiServ: ApiService, public EditDeleteServ: DeleteEditPermissionService, 
      private menuService: MenuService, public activeRoute: ActivatedRoute, public schoolService: SchoolService, public router:Router,
      public sectionService:SectionService){}
      
  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
    });

    this.getSectionData()

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
      }
    });
  }

  openModal(Id?: number) {
    if (Id) {
      this.editSection = true;
      this.getSectionById(Id); 
    }
     
    this.getSchoolData()

    document.getElementById("Add_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Modal")?.classList.add("flex");
  }

  closeModal() {
    document.getElementById("Add_Modal")?.classList.remove("flex");
    document.getElementById("Add_Modal")?.classList.add("hidden");

    this.section= new Section()
    this.Schools = [] 

    if(this.editSection){
      this.editSection = false
    }
    this.validationErrors = {}; 
  }

  async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: Section[] = await firstValueFrom(this.sectionService.Get(this.DomainName));  
      this.sectionData = data || [];
  
      if (this.value !== "") {
        const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);
  
        this.sectionData = this.sectionData.filter(t => {
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
      this.sectionData = [];
    }
  }

  IsAllowDelete(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowDelete(InsertedByID, this.UserID, this.AllowDeleteForOthers);
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }

  getSectionData(){
    this.sectionService.Get(this.DomainName).subscribe(
      (data) => {
        this.sectionData = data;
      }
    )
  }

  getSectionById(id:number){
    this.sectionService.GetByID(id, this.DomainName).subscribe(
      (data) => {
        this.section = data;
      }
    )
  }

  getSchoolData(){
    this.schoolService.Get(this.DomainName).subscribe(
      (data) => {
        this.Schools = data;
      }
    )
  }

  capitalizeField(field: keyof Section): string {
      return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.section) {
      if (this.section.hasOwnProperty(key)) {
        const field = key as keyof Section;
        if (!this.section[field]) {
          if(field == "name" || field == "schoolID"){
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        } else {
          if(field == "name"){
            if(this.section.name.length > 100){
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

  onInputValueChange(event: { field: keyof Section, value: any }) {
    const { field, value } = event;
    
    (this.section as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }
  }

  Save(){
    if(this.isFormValid()){
      if(this.editSection == false){
        this.sectionService.Add(this.section, this.DomainName).subscribe(
          (result: any) => {
            this.closeModal()
            this.getSectionData()
          },
          error => {
          }
        );
      } else{
        this.sectionService.Edit(this.section, this.DomainName).subscribe(
          (result: any) => {
            this.closeModal()
            this.getSectionData()
          },
          error => {
          }
        );
      }  
    }
  } 

  deleteSection(id:number){
    Swal.fire({
      title: 'Are you sure you want to delete this Section?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.sectionService.Delete(id, this.DomainName).subscribe(
          (data: any) => {
            this.sectionData=[]
            this.getSectionData()
          }
        );
      }
    });
  }

  moveToSectionView(Id:number){
    this.router.navigateByUrl('Employee/Grade/' + this.DomainName + '/' + Id);
  }
}
