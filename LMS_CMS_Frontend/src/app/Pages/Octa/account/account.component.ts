import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SearchComponent } from '../../../Component/search/search.component';
import Swal from 'sweetalert2';
import { Account } from '../../../Models/Octa/account';
import { OctaService } from '../../../Services/Octa/octa.service';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [FormsModule,CommonModule,SearchComponent],
  templateUrl: './account.component.html',
  styleUrl: './account.component.css'
})
export class AccountComponent {
  keysArray: string[] = ['id', 'name','date'];
  key: string= "id";
  value: any = "";

  accountData:Account[] = []
  account:Account = new Account()
  editAccount:boolean = false
  validationErrors: { [key in keyof Account]?: string } = {};

  constructor(public octaService: OctaService){}
  
  ngOnInit(){
    this.getAccountData()
  }

  getAccountData(){
    this.octaService.Get().subscribe(
      (data) => {
        this.accountData = data;
      }
    )
  }

  GetAccountById(accountId: number) {
    this.octaService.GetByID(accountId).subscribe((data) => {
      this.account = data;
    });
  }

  openModal(accountId?: number) {
    if (accountId) {
      this.editAccount = true;
      this.GetAccountById(accountId); 
    }
    
    document.getElementById("Add_Modal")?.classList.remove("hidden");
    document.getElementById("Add_Modal")?.classList.add("flex");
  }

  closeModal() {
    document.getElementById("Add_Modal")?.classList.remove("flex");
    document.getElementById("Add_Modal")?.classList.add("hidden");

    this.account= new Account()

    if(this.editAccount){
      this.editAccount = false
    }
    this.validationErrors = {}; 
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

  capitalizeField(field: keyof Account): string {
      return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.account) {
      if (this.account.hasOwnProperty(key)) {
        const field = key as keyof Account;
        if (!this.account[field]) {
          if(field == "user_Name" || field == "arabic_Name" || field == "password"){
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        } else {
          if(field == "user_Name" || field == "arabic_Name"){
            if(this.account.user_Name.length > 100 || this.account.arabic_Name.length > 100){
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

  onInputValueChange(event: { field: keyof Account, value: any }) {
    const { field, value } = event;
    if (field == "user_Name" || field == "arabic_Name" || field == "password") {
      (this.account as any)[field] = value;
      if (value) {
        this.validationErrors[field] = '';
      }
    }
  }

  SaveAccount(){
    if(this.isFormValid()){
      if(this.editAccount == false){
        this.octaService.Add(this.account).subscribe(
          (result: any) => {
            this.closeModal()
            this.getAccountData()
          },
          error => {
            console.log(error)
          }
        );
      } else{
        this.octaService.Edit(this.account).subscribe(
          (result: any) => {
            this.closeModal()
            this.getAccountData()
          },
          error => {
            console.log(error)
          }
        );
      }  
    }
  } 

  deleteAccount(id:number){
    Swal.fire({
      title: 'Are you sure you want to delete this account?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.octaService.Delete(id).subscribe(
          (data: any) => {
            this.accountData=[]
            this.getAccountData()
          }
        );
      }
    });
  }
}
