import { Component } from '@angular/core';
import { EvaluationEmployeeService } from '../../../../Services/Employee/LMS/evaluation-employee.service';
import { EvaluationEmployeeAdd } from '../../../../Models/LMS/evaluation-employee-add';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { SearchComponent } from '../../../../Component/search/search.component';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';

@Component({
  selector: 'app-evaluation-feedback',
  standalone: true,
  imports: [FormsModule, CommonModule, SearchComponent],
  templateUrl: './evaluation-feedback.component.html',
  styleUrl: './evaluation-feedback.component.css'
})
export class EvaluationFeedbackComponent {

  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  TableData: EvaluationEmployeeAdd[] = [];

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  mode: string = '';

  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'englishName' ,'arabicName'];


  constructor(
    private router: Router,
    private menuService: MenuService,
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public DomainServ: DomainService,
    public EditDeleteServ: DeleteEditPermissionService,
    public ApiServ: ApiService,
    public EvaluationEmployeeServ: EvaluationEmployeeService
  ) { }
  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
    });

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others;
      }
    });
    if(this.path=="Received Evaluations"){
      this.mode="Evaluated"
      console.log(this.path)
      this.GetAllDataForEvaluated();
    }else if(this.path=="Created Evaluations"){
      this.mode="Evaluator"
      this.GetAllDataForEvaluator();
    }
  }

  GetAllDataForEvaluated() {
    this.TableData = [];
    this.EvaluationEmployeeServ.GetEvaluatedEvaluations(this.UserID,this.DomainName).subscribe((d) => {
      this.TableData = d;
      console.log(this.TableData)
    });
  }

  GetAllDataForEvaluator() {
    this.TableData = [];
    this.EvaluationEmployeeServ.GetEvaluatorEvaluations(this.UserID,this.DomainName).subscribe((d) => {
      this.TableData = d;
      console.log(this.TableData)
    });
  }

  async onSearchEvent(event: { key: string; value: any }) {
    this.key = event.key;
    this.value = event.value;
    try {
      const data: EvaluationEmployeeAdd[] = await firstValueFrom(
        this.EvaluationEmployeeServ.GetEvaluatorEvaluations(this.UserID,this.DomainName)
      );
      this.TableData = data || [];

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
            return fieldValue.toString().includes(numericValue.toString())
          }
          return fieldValue == this.value;
        });
      }
    } catch (error) {
      this.TableData = [];
    }
  }

  moveEvaluation(id:number){
    if (this.path == "Received Evaluations") {
      this.router.navigateByUrl(`Employee/Received Evaluations/${id}`);
    } else if (this.path == "Created Evaluations") {
      this.router.navigateByUrl(`Employee/Created Evaluations/${id}`);
    }
  }
}
