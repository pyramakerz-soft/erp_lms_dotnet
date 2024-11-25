import { Component } from '@angular/core';
import { Bus } from '../../../../Models/Bus/bus';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { BusService } from '../../../../Services/Employee/Bus/bus.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-bus-student',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './bus-student.component.html',
  styleUrl: './bus-student.component.css'
})
export class BusStudentComponent {
  User_Data_After_Login :TokenData =new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  bus :Bus  = new Bus()
  busId: number = 0
  busStudentData :Bus[] = []

  constructor(public busService:BusService, public account:AccountService, public activeRoute:ActivatedRoute){}

  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.busId = Number(this.activeRoute.snapshot.paramMap.get('busId'))
    this.GetBusById(this.busId);
  }

  GetBusById(busId:number){
    this.busService.GetbyBusId(busId).subscribe((data) => {
      this.bus = data;
      console.log(this.bus)
    });
  }
}
