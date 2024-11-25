import { Component } from '@angular/core';
import { Bus } from '../../../../Models/Bus/bus';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { BusService } from '../../../../Services/Employee/Bus/bus.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BusStudentService } from '../../../../Services/Employee/Bus/bus-student.service';
import { BusStudent } from '../../../../Models/Bus/bus-student';
import Swal from 'sweetalert2';

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
  busStudentData :BusStudent[] = []

  constructor(public busService:BusService, public busStudentService:BusStudentService, public account:AccountService, public activeRoute:ActivatedRoute){}

  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.busId = Number(this.activeRoute.snapshot.paramMap.get('busId'))
    this.GetBusById(this.busId);
    this.GetStudentsByBusId(this.busId);
  }

  GetBusById(busId:number){
    this.busService.GetbyBusId(busId).subscribe((data) => {
      this.bus = data;
      console.log(this.bus)
    });
  }
  
  GetStudentsByBusId(busId:number){
    this.busStudentService.GetbyBusId(busId).subscribe((data) => {
      console.log(data)
      this.busStudentData = data;
    });
  }

  deleteBusStudent(busStudentId: number) {
    Swal.fire({
      title: 'Are you sure you want to delete student?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      this.busStudentService.DeleteBusStudent(busStudentId).subscribe(
        (data: any) => {
          this.GetStudentsByBusId(this.busId);
        }
      );
    });
  }
}
