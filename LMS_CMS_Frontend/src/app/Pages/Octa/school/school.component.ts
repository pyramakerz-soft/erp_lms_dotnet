import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../Component/search/search.component';
import { DomainService } from '../../../Services/Employee/domain.service';
import { Domain } from '../../../Models/domain';
import { School } from '../../../Models/school';
import Swal from 'sweetalert2';
import { SchoolType } from '../../../Models/Octa/school-type';
import { SchoolTypeService } from '../../../Services/Octa/school-type.service';
import { SchoolService } from '../../../Services/Employee/school.service';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-school',
  standalone: true,
  imports: [FormsModule,CommonModule,SearchComponent],
  templateUrl: './school.component.html',
  styleUrl: './school.component.css'
})
export class SchoolComponent {
  key: string= "id";
  value: any = "";
  keysArray: string[] = ['id', 'address','name', 'schoolTypeName'];
  DomainData: Domain[] = []
  schoolType: SchoolType[] = []
  DomainName: string = "";
  IsChoosenDomain: boolean = false;
  schoolData :School[] = []
  school :School  = new School()
  editSchool = false

  validationErrors: { [key in keyof School]?: string } = {};

  constructor(public DomainServ: DomainService, public schoolTypeService: SchoolTypeService, public schoolService: SchoolService){}

  ngOnInit(){
    this.getAllDomains();
  }

  getAllDomains() {
    this.DomainServ.Get().subscribe((data) => {
      this.DomainData = data;
    })
  }

  getSchoolDataByDomainId(event:Event){
    this.IsChoosenDomain=true;
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.DomainName=selectedValue;
    this.schoolData = []

    this.schoolService.Get(this.DomainName).subscribe(
      (data: any) => {
        this.schoolData=[]
        this.schoolData = data;
      } ,(error)=>{
        this.schoolData=[];
      });
  }

  GetSchoolById(schoolId: number) {
    this.schoolService.GetBySchoolId(schoolId,this.DomainName).subscribe((data) => {
      this.school = data;
    });
  }

  GetSchoolType() {
    this.schoolTypeService.Get().subscribe((data) => {
      this.schoolType = data;
    });
  }

  openModal(schoolId?: number) {
    if (schoolId) {
      this.editSchool = true;
      this.GetSchoolById(schoolId); 
    }
    
    this.GetSchoolType();
    
    document.getElementById("Add_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Modal")?.classList.add("flex");
  }

  closeModal() {
    document.getElementById("Add_Modal")?.classList.remove("flex");
    document.getElementById("Add_Modal")?.classList.add("hidden");
    this.schoolType= []
    this.school = new School()

    if(this.editSchool){
      this.editSchool = false
    }

    this.validationErrors = {}; 
  }

  async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: School[] = await firstValueFrom(this.schoolService.Get(this.DomainName));  
      this.schoolData = data || [];
  
      if (this.value !== "") {
        const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);
  
        this.schoolData = this.schoolData.filter(t => {
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
      this.schoolData = [];
    }
  }

  deleteSchool(busId: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this school?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.schoolService.Delete(busId,this.DomainName).subscribe(
          (data: any) => {
            this.schoolData=[]
            this.schoolService.Get(this.DomainName).subscribe(
              (data: any) => {
                this.schoolData=[]
                this.schoolData = data;
              } ,(error)=>{
                this.schoolData=[];
              }
            );
          }
        );
      }
    });
  }

  capitalizeField(field: keyof School): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.school) {
      if (this.school.hasOwnProperty(key)) {
        const field = key as keyof School;
        if (!this.school[field]) {
          if(field == "name" || field == 'schoolTypeID'){
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        } else {
          if(field == "name"){
            if(this.school.name.length > 100){
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

  onInputValueChange(event: { field: keyof School, value: any }) {
    const { field, value } = event;
    if (field == "name" || field == "schoolTypeID") {
      (this.school as any)[field] = value;
      if (value) {
        this.validationErrors[field] = '';
      }
    }
  }

  SaveSchool(){
    if (this.isFormValid()) {
      if(this.editSchool == false){
        this.schoolService.Add(this.school,this.DomainName).subscribe(
          (result: any) => {
            this.closeModal()
            this.schoolService.Get(this.DomainName).subscribe(
              (data: any) => {
                this.schoolData = data;
              }
            );
          },
          error => {
          }
        );
      } else{
        this.schoolService.Edit(this.school,this.DomainName).subscribe(
          (result: any) => {
            this.closeModal()
            this.schoolService.Get(this.DomainName).subscribe(
              (data: any) => {
                this.schoolData = data;
              }
            );
          },
          error => {
          }
        );
      }  
    }
  }
}
