import { Component } from '@angular/core';
import { EvaluationEmployee } from '../../../../Models/LMS/evaluation-employee';
import { Router, ActivatedRoute } from '@angular/router';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { EvaluationEmployeeService } from '../../../../Services/Employee/LMS/evaluation-employee.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-evaluation-employee-answer',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './evaluation-employee-answer.component.html',
  styleUrl: './evaluation-employee-answer.component.css'
})
export class EvaluationEmployeeAnswerComponent {
  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;
  Feedback : string = ""
  data: EvaluationEmployee = new EvaluationEmployee();

  DomainName: string = '';
  UserID: number = 0;

  isModalVisible: boolean = false;
  mode: string = '';

  path: string = '';
  key: string = 'id';
  value: any = '';
  keysArray: string[] = ['id', 'englishName', 'arabicName'];
  EvaluationId: number = 0;
  isLoading = false

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
    if (this.path == "EvaluationBookCorrectionForEvaluated") {
      this.mode = "Evaluated"
      console.log(this.path)
    } else if (this.path == "EvaluationBookCorrectionForEvaluator") {
      this.mode = "Evaluator"
    }
    this.EvaluationId = Number(this.activeRoute.snapshot.paramMap.get('id'));
    this.GetData()
  }

  GetData() {
    this.EvaluationEmployeeServ.GetEvaluations(this.EvaluationId, this.DomainName).subscribe((d) => {
      this.data = d;
      console.log(this.data)
    });
  }

  moveEvaluation() {
    if (this.path == "EvaluationBookCorrectionForEvaluated") {
      this.router.navigateByUrl(`Employee/EvaluationBookCorrectionForEvaluated`);
    } else if (this.path == "EvaluationBookCorrectionForEvaluator") {
      this.router.navigateByUrl(`Employee/EvaluationBookCorrectionForEvaluator`);
    }
  }
  
  SaveFeedback() {
    this.isLoading=true
    const obj = { evaluationEmployeeID: this.data.id, feedback: this.Feedback };
    this.EvaluationEmployeeServ.EditFeedback(obj, this.DomainName).subscribe((d) => {
      this.GetData();
      this.isLoading=false
    },(error)=>{
      this.isLoading=false
    });
  }
}
