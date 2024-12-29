import { Component, HostListener } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../Component/search/search.component';
import { CommonModule } from '@angular/common';
import { firstValueFrom } from 'rxjs';
import { Domain } from '../../../Models/domain';
import { DomainService } from '../../../Services/Octa/domain.service';
import Swal from 'sweetalert2';
import { PagesWithRoleId } from '../../../Models/pages-with-role-id';
import { RoleDetailsService } from '../../../Services/Employee/role-details.service';

@Component({
  selector: 'app-domains',
  standalone: true,
  imports: [FormsModule,CommonModule,SearchComponent],
  templateUrl: './domains.component.html',
  styleUrl: './domains.component.css'
})
export class DomainsComponent {
  keysArray: string[] = ['id', 'name','date'];
  key: string= "id";
  value: any = "";
  
  domainData:Domain[] = []
  ModulesData:PagesWithRoleId[] = []
  domain:Domain = new Domain()
  editDomain:boolean = false
  validationErrors: { [key in keyof Domain]?: string } = {};

  isDropdownOpen = false;
  isSaved = false

  constructor(public domainService:DomainService, public roleDetailsService:RoleDetailsService){}

  ngOnInit(){
    this.getDomainData()
  }

  getDomainData(){
    this.domainService.Get().subscribe(
      (data) => {
        this.domainData = data;
      }
    )
  }

  GetDomainById(domainId: number) {
    this.domainService.GetDomainBuID(domainId).subscribe((data) => {
      this.domain = data;
      console.log(this.domain)
    });
  }

  GetModules() {
    this.roleDetailsService.Get_All_Pages().subscribe((data) => {
      data.forEach(element => {
        if(element.page_ID == null){
          this.ModulesData.push(element)
        }
      });
    });
  }

  openModal(domainId?: number) {
    this.closeViewModal()
    
    if (domainId) {
      this.editDomain = true;
      this.GetDomainById(domainId); 
    }
    
    this.GetModules();
    
    document.getElementById("Add_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Modal")?.classList.add("flex");
  }

  closeModal() {
    document.getElementById("Add_Modal")?.classList.remove("flex");
    document.getElementById("Add_Modal")?.classList.add("hidden");

    this.ModulesData= []
    this.domain= new Domain()
    this.isDropdownOpen = false;

    if(this.editDomain){
      this.editDomain = false
    }
    this.validationErrors = {}; 
  }
  
  openViewModal(domainId: number) {
    this.GetDomainById(domainId); 
    this.GetModules();
    
    document.getElementById("View_Modal")?.classList.remove("hidden");
    document.getElementById("View_Modal")?.classList.add("flex");
  }

  closeViewModal() {
    document.getElementById("View_Modal")?.classList.remove("flex");
    document.getElementById("View_Modal")?.classList.add("hidden");

    this.ModulesData= []
    this.domain= new Domain()
  }

  toggleDropdown(event: MouseEvent) {
    event.stopPropagation(); // Prevent the click event from bubbling up
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  // Close dropdown if clicked outside
  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    const target = event.target as HTMLElement;
    const dropdown = document.querySelector('.dropdown-container') as HTMLElement;

    if (dropdown && !dropdown.contains(target)) {
      this.isDropdownOpen = false;
    }
  }

  // Cleanup event listener
  ngOnDestroy() {
    document.removeEventListener('click', this.onDocumentClick);
  }

  removeFromModules(moduleID:number, event: MouseEvent){
    event.stopPropagation();
    this.domain.pages = this.domain.pages.filter(_moduleID => _moduleID !== moduleID);
  }
  
  onModuleChange(module: number, event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;

    if (isChecked) {
      if (!this.domain.pages.includes(module)) {
        this.domain.pages.push(module);
      }
    } else {
      const index = this.domain.pages.indexOf(module);
      if (index > -1) {
        this.domain.pages.splice(index, 1);
      }
    }
    
    if (this.domain.pages.length > 0) {
      this.validationErrors['pages'] = '';
    } else {
      this.validationErrors['pages'] = '*Module is required.';
    }
  }

  async onSearchEvent(event: { key: string, value: any }) {
    this.key = event.key;
    this.value = event.value;
    // try {
    //   const data: Bus[] = await firstValueFrom(this.busService.Get(this.DomainName));  
    //   this.busData = data || [];
  
    //   if (this.value !== "") {
    //     const numericValue = isNaN(Number(this.value)) ? this.value : parseInt(this.value, 10);
  
    //     this.busData = this.busData.filter(t => {
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
    //   this.busData = [];
    //   console.log('Error fetching data:', error);
    // }
  }

  capitalizeField(field: keyof Domain): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.domain) {
      if (this.domain.hasOwnProperty(key)) {
        const field = key as keyof Domain;
        if (!this.domain[field]) {
          if(field == "name"){
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        } else {
          if(field == "name"){
            if(this.domain.name.length > 100){
              this.validationErrors[field] = `*${this.capitalizeField(field)} cannot be longer than 100 characters`
              isValid = false;
            }
          }
          if(field == "pages"){
            if(this.domain.pages.length == 0){
              this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
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

  onInputValueChange(event: { field: keyof Domain, value: any }) {
    const { field, value } = event;
    if (field == "name") {
      (this.domain as any)[field] = value;
      if (value) {
        this.validationErrors[field] = '';
      }
    }
  }

  SaveDomain(){
    if(this.isFormValid()){
      this.isSaved = true
      if(this.editDomain == false){
        this.domainService.Add(this.domain).subscribe(
          (result: any) => {
            Swal.fire({
              title: 'Domain Created Successfully',
              icon: 'success',
              html: `
                <div class='grid justify-items-start'>
                  <p><strong>Admin Name:</strong> ${result.userName}</p>
                  <p><strong>Password:</strong> ${result.password}p>
                </div>
              `,
              showCancelButton: false,
              confirmButtonColor: '#FF7519',
              confirmButtonText: 'Okay',
            }).then((r) => {
              this.closeModal()
              this.domainService.Get().subscribe(
                (data: any) => {
                  this.domainData = data;
                }
              );
              this.isSaved = false
            });
          },
          error => {
            this.isSaved = false
            console.log(error)
          }
        );
      } else{
        this.domainService.Edit(this.domain).subscribe(
          (result: any) => {
            this.closeModal()
            this.domainService.Get().subscribe(
              (data: any) => {
                this.domainData = data;
              }
            );
          },
          error => {
            this.isSaved = false
            console.log(error)
          }
        );
      }  
    }
  } 

  deleteDomain(id:number){
    Swal.fire({
      title: 'Are you sure you want to delete this domain?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.domainService.DeleteDomain(id).subscribe(
          (data: any) => {
            this.domainData=[]
            this.getDomainData()
          }
        );
      }
    });
  }
}
