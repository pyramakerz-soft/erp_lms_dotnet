import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { TokenData } from '../../../../Models/token-data';
import { BusTypeService } from '../../../../Services/Employee/Bus/bus-type.service';
import { BusType } from '../../../../Models/Bus/bus-type';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-bus-types',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './bus-types.component.html',
  styleUrl: './bus-types.component.css'
})
export class BusTypesComponent {

  User_Data_After_Login :TokenData =new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  AllowEdit:boolean=false;
  AllowDelete:boolean=false;
  TableData:BusType[]=[]

  constructor(private router: Router ,private menuService: MenuService ,public account:AccountService , public BusTypeServ:BusTypeService) {}

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
     this.BusTypeServ.Get().subscribe((data)=>{
        console.log(data)
        this.TableData=data;
     });
    
    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName('BusType', items);
      this.AllowEdit=settingsPage.allow_Edit;
      this.AllowDelete=settingsPage.allow_Delete;


    });
  }



}
