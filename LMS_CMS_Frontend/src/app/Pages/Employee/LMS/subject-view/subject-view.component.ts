import { Component } from '@angular/core';
import { SubjectService } from '../../../../Services/Employee/LMS/subject.service';
import { Subject } from '../../../../Models/LMS/subject';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { TokenData } from '../../../../Models/token-data';
import { AccountService } from '../../../../Services/account.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { AddEditSubjectComponent } from '../../../../Component/Employee/LMS/add-edit-subject/add-edit-subject.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-subject-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './subject-view.component.html',
  styleUrl: './subject-view.component.css'
})
export class SubjectViewComponent {
  subject:Subject = new Subject()
  subjectId = 0;
  DomainName = "";
  AllowEditForOthers: boolean = false;
  AllowEdit: boolean = false;

  UserID: number = 0;
  User_Data_After_Login: TokenData = new TokenData("", 0, 0, 0, 0, "", "", "", "", "")
  path: string = ""

  constructor(public subjectService: SubjectService, public activeRoute:ActivatedRoute, public router:Router, public EditDeleteServ: DeleteEditPermissionService, 
    public account: AccountService, private menuService: MenuService, public dialog: MatDialog){}

  async ngOnInit(){
    this.subjectId = await Number(this.activeRoute.snapshot.paramMap.get('SubId'))
    this.DomainName = await String(this.activeRoute.snapshot.paramMap.get('domainName'))

    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.GetSubjectById()

    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
    });

    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
      }
    });
  }

  GetSubjectById() {
    this.subjectService.GetByID(this.subjectId, this.DomainName).subscribe((data) => {
      this.subject = data;
    })
  }

  moveToSubjects(){
    this.router.navigateByUrl('Employee/Subject');
  }

  editModal(){
    this.openDialog(this.subject.id, true); 
  }

  openDialog(subjectId?: number, editSubject?: boolean): void {
    const dialogRef = this.dialog.open(AddEditSubjectComponent, {
      data: editSubject
        ? {
          subjectId: subjectId,
          editSubject: editSubject
        }
        : {
          editSubject: false
        },
    });

    dialogRef.afterClosed().subscribe(result => {
      this.GetSubjectById()
    });
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }
}
