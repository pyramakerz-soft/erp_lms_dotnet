import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RegisterationFormTest } from '../../../../Models/Registration/registeration-form-test';
import { RegisterationFormTestAnswer } from '../../../../Models/Registration/registeration-form-test-answer';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { RegisterationFormTestAnswerService } from '../../../../Services/Employee/Registration/registeration-form-test-answer.service';
import { RegisterationFormTestService } from '../../../../Services/Employee/Registration/registeration-form-test.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuestionService } from '../../../../Services/Employee/Registration/question.service';
import { Question } from '../../../../Models/Registration/question';
import { TestWithQuestion } from '../../../../Models/Registration/test-with-question';
import { Answer } from '../../../../Models/Registration/answer';
import { QuestionOption } from '../../../../Models/Registration/question-option';

@Component({
  selector: 'app-registraion-test',
  standalone: true,
  imports: [CommonModule ,FormsModule],
  templateUrl: './registraion-test.component.html',
  styleUrl: './registraion-test.component.css',
})
export class RegistraionTestComponent {
  User_Data_After_Login: TokenData = new TokenData(
    '',
    0,
    0,
    0,
    0,
    '',
    '',
    '',
    '',
    ''
  );
  
  DomainName: string = '';
  UserID: number = 0;
  path: string = '';

  Data: RegisterationFormTestAnswer[] = [];
  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;


  isModalVisible: boolean = false;
  TestId: number = 0;
  registerationFormParentID: number = 0;
  registerationFormID: number = 0;
  
  TestName: string = '';
  TotalMark:number = 0;
  mark:number =0
  mode : string = 'degree' ;
  MarkIsEmpty: boolean = false;
  
  questions:TestWithQuestion[]=[]
  
  RegesterForm: RegisterationFormTest = new RegisterationFormTest();
  Answers:Answer[]=[];
  
  QuestionsByTest:Question[]=[]
  constructor(
    public activeRoute: ActivatedRoute,
    public account: AccountService,
    public ApiServ: ApiService,
    private menuService: MenuService,
    public EditDeleteServ: DeleteEditPermissionService,
    private router: Router,
    public registerServ: RegisterationFormTestAnswerService,
    public registrationserv: RegisterationFormTestService,
    public questionServ :QuestionService
  ) {}
  
  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;
    this.DomainName = this.ApiServ.GetHeader();
    this.activeRoute.url.subscribe((url) => {
      this.path = url[0].path;
      this.activeRoute.paramMap.subscribe((params) => {
        this.registerationFormParentID = Number(params.get('registerationFormParentID')); // Retrieve and convert Pid to a number
        this.TestId = Number(params.get('TestId')); // Retrieve and convert Tid to a number
        this.registerationFormID = Number(params.get('registerationFormID')); // Retrieve and convert Tid to a number
      });
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
    
    this.GetAllData();
  }

  GetAllData() {
    this.registerServ
      .GetByRegistrationParentId(this.registerationFormParentID, this.TestId, this.DomainName)
      .subscribe((d: any) => {
        console.log(d)
        this.Data = d.questionWithAnswer;
        this.TestName = d.testName;
        this.mark =d.mark;
        this.TotalMark=d.totalmark;
        console.log("ff",this.Data);
      },(error)=>{
          this.mode= 'test';
          console.log(this.mode)
          this.questionServ.GetByTestIDGroupBy(this.TestId,this.DomainName).subscribe((d: any) => {
           this.questions=d;
           this.questionServ.GetByTestID(this.TestId,this.DomainName).subscribe((q: any) => {
            this.QuestionsByTest=q;
            if (this.QuestionsByTest && Array.isArray(this.QuestionsByTest)) {
              this.Answers = this.QuestionsByTest.map((question: any) => {
                return new Answer(
                  question.registerationFormParentID || this.registerationFormParentID, 
                  '',                                     
                  0,                                    
                  question.id || 0                       
                );
              });
            } else {
              this.Answers = []; 
            }
          });
        });
      });
    }
  moveToEmployee() {
    this.router.navigateByUrl(`Parent/Admission Test`)
  }
  selectOption(questionId: number,OptionId: number) {
       const answer = this.Answers.find(a => a.questionID === questionId);
       if (answer) {
         answer.answerID = OptionId;
       } 
       console.log('Updated Answers:', this.Answers);
  }

  EssayAnswer(questionId: number, event: Event): void {
    const textarea = event.target as HTMLTextAreaElement;
    const answer = this.Answers.find(a => a.questionID === questionId);
    if (answer) {
      answer.essayAnswer = textarea.value;
      answer.answerID=null
    } 
       
  }

  Save(){
    console.log(this.Answers)
    this.registerServ.Add(this.Answers,this.DomainName).subscribe((a)=>{
      this.GetAllData();
     this.router.navigateByUrl(`Parent/Admission Test`)
    })
  }

}
