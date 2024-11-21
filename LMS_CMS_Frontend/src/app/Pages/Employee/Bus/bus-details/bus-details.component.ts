import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BusService } from '../../../../Services/Employee/Bus/bus.service';
import { AccountService } from '../../../../Services/account.service';
import { TokenData } from '../../../../Models/token-data';
import { Bus } from '../../../../Models/Bus/bus';

@Component({
  selector: 'app-bus-details',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './bus-details.component.html',
  styleUrl: './bus-details.component.css'
})
export class BusDetailsComponent {
  User_Data_After_Login :TokenData =new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  busData :Bus[] = []

  constructor(public busService:BusService, public account:AccountService ){}

  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();

    this.getBusDataByDomainId()
  }

  getBusDataByDomainId(){
    this.busService.GetbyDomainId(2).subscribe(
      (data: any)=>{
        this.busData = data
        console.log(data)
      }
    );
  }
}
