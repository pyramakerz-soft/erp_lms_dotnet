import { CommonModule } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TokenData } from '../../../../Models/token-data';
import { AccountingEntries } from '../../../../Models/Accounting/accounting-entries';
import { AccountingEntriesDetails } from '../../../../Models/Accounting/accounting-entries-details';
import { AccountingEntriesDocType } from '../../../../Models/Accounting/accounting-entries-doc-type';
import { LinkFile } from '../../../../Models/Accounting/link-file';
import { AccountingEntriesDocTypeService } from '../../../../Services/Employee/Accounting/accounting-entries-doc-type.service';
import { AccountingEntriesService } from '../../../../Services/Employee/Accounting/accounting-entries.service';
import { AccountingEntriesDetailsService } from '../../../../Services/Employee/Accounting/accounting-entries-details.service';
import { BankService } from '../../../../Services/Employee/Accounting/bank.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { DataAccordingToLinkFileService } from '../../../../Services/Employee/Accounting/data-according-to-link-file.service';
import { LinkFileService } from '../../../../Services/Employee/Accounting/link-file.service';
import { SaveService } from '../../../../Services/Employee/Accounting/save.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import Swal from 'sweetalert2';
import { AccountingTreeChart } from '../../../../Models/Accounting/accounting-tree-chart';
import { AccountingTreeChartService } from '../../../../Services/Employee/Accounting/accounting-tree-chart.service';
import html2pdf from 'html2pdf.js';
import { PdfPrintComponent } from '../../../../Component/pdf-print/pdf-print.component';
import { ReportsService } from '../../../../Services/shared/reports.service';

@Component({
  selector: 'app-accounting-entries-details',
  standalone: true,
  imports: [CommonModule, FormsModule, PdfPrintComponent],
  templateUrl: './accounting-entries-details.component.html',
  styleUrl: './accounting-entries-details.component.css'
})
export class AccountingEntriesDetailsComponent {
  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false; 

  DomainName: string = '';
  UserID: number = 0;

  path: string = '';
  AccountingEntriesID: number = 0;

  isCreate:boolean = false
  isEdit:boolean = false
  isView:boolean = false

  accountingEntries:AccountingEntries = new AccountingEntries()
  validationErrors: { [key in keyof AccountingEntries]?: string } = {};
  validationErrorsForDetails: { [key in keyof AccountingEntriesDetails]?: string } = {};
   
  dataTypesData: AccountingEntriesDocType[] = []
  bankOrSaveData: any[] = []
  accountingEntriesDetailsData: AccountingEntriesDetails[] = []
  AccountingTreeChartData: AccountingTreeChart[] = []
  subAccountData: any[] = []
  newDetails:AccountingEntriesDetails = new AccountingEntriesDetails()
  totalCredit: number = 0;
  totalDebit: number = 0;
  theDifference: number = 0;

  isNewDetails:boolean = false
  isDetailsValid:boolean = false

  editingRowId: number | null = null;
  editedRowData:AccountingEntriesDetails = new AccountingEntriesDetails() 

  isLoading = false;


  @ViewChild(PdfPrintComponent) pdfComponentRef!: PdfPrintComponent;
  showPDF = false;
  
  constructor(
    private router: Router, private menuService: MenuService, public activeRoute: ActivatedRoute, public account: AccountService, public accountingEntriesDocTypeService:AccountingEntriesDocTypeService,
    public DomainServ: DomainService, public EditDeleteServ: DeleteEditPermissionService, public ApiServ: ApiService, public accountingEntriesService:AccountingEntriesService,
    public bankService:BankService, public saveService:SaveService, public accountingEntriesDetailsService:AccountingEntriesDetailsService, public linkFileService:LinkFileService,
    public dataAccordingToLinkFileService: DataAccordingToLinkFileService, public accountingTreeChartService:AccountingTreeChartService, public reportsService: ReportsService){}
    
  ngOnInit(){
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.AccountingEntriesID = Number(this.activeRoute.snapshot.paramMap.get('id'))

    if(!this.AccountingEntriesID){
      this.isCreate = true
    }else{
      this.GetAccountingEntriesByID()
      this.GetAccountingEntriesDetails()
    }

    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
      if(url[1].path == "View"){ 
        this.isView = true
      } else{
        if(this.AccountingEntriesID){
          this.isEdit = true
        }
      } 
    });
  
    this.menuService.menuItemsForEmployee$.subscribe((items) => {
      const settingsPage = this.menuService.findByPageName(this.path, items);
      if (settingsPage) {
        this.AllowEdit = settingsPage.allow_Edit;
        this.AllowDelete = settingsPage.allow_Delete;
        this.AllowDeleteForOthers = settingsPage.allow_Delete_For_Others
        this.AllowEditForOthers = settingsPage.allow_Edit_For_Others
      }
    });

    this.GetDocType()
  }

  moveToAccountingEntries() {
    this.router.navigateByUrl("Employee/Accounting Entries")
  }

  GetDocType(){
    this.accountingEntriesDocTypeService.Get(this.DomainName).subscribe(
      (data) => { 
        this.dataTypesData = data
      }
    )
  }

  GetAccountingTreeChartData(){
    this.accountingTreeChartService.GetBySubID(this.DomainName).subscribe(
      (data) => { 
        this.AccountingTreeChartData = data
      }
    )
  }

  GetSubAccountData(event: Event) {
    this.subAccountData = []
    this.newDetails.subAccountingID = null
    this.editedRowData.subAccountingID = null
    const target = event.target as HTMLSelectElement;
    const selectedValue = target ? target.value : null;

    if (selectedValue) { 
      this.accountingTreeChartService.GetByID(+selectedValue, this.DomainName).subscribe(
        (data) => {  
          if(data.linkFileID && data.id){
            this.dataAccordingToLinkFileService.GetTableDataAccordingToLinkFileAndSubAccount(this.DomainName, data.linkFileID, data.id).subscribe(
              (data) => {  
                this.subAccountData = data
              }
            )
          } else{
            this.newDetails.subAccountingID = null
            this.editedRowData.subAccountingID = null
          }
        }
      )
    }
  }
  
  GetAccountingEntriesByID(){
    this.accountingEntriesService.GetByID(this.AccountingEntriesID, this.DomainName).subscribe(
      (data) => {
        this.accountingEntries = data 
      }
    )
  } 
  
  IsAllowDelete(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowDelete(InsertedByID, this.UserID, this.AllowDeleteForOthers);
    return IsAllow;
  }

  IsAllowEdit(InsertedByID: number) {
    const IsAllow = this.EditDeleteServ.IsAllowEdit(InsertedByID, this.UserID, this.AllowEditForOthers);
    return IsAllow;
  }

  capitalizeField(field: keyof AccountingEntries): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.accountingEntries) {
      if (this.accountingEntries.hasOwnProperty(key)) {
        const field = key as keyof AccountingEntries;
        if (!this.accountingEntries[field]) {
          if(field == "accountingEntriesDocTypeID" || field == "date"){
            this.validationErrors[field] = `*${this.capitalizeField(field)} is required`
            isValid = false;
          }
        } else { 
          this.validationErrors[field] = '';
        }
      }
    }
    return isValid;
  }

  onInputValueChange(event: { field: keyof AccountingEntries, value: any }) {
    const { field, value } = event;
    (this.accountingEntries as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    } 
  }

  onInputValueChangeForDetails(event: { field: keyof AccountingEntriesDetails, value: any }) {
    const { field, value } = event;
    (this.newDetails as any)[field] = value;
    if (value) {
      this.validationErrorsForDetails[field] = '';
    }
     
    if((this.newDetails.creditAmount || this.editedRowData.creditAmount) && 
    (this.newDetails.debitAmount || this.editedRowData.debitAmount) && 
    (!isNaN(this.newDetails.creditAmount?this.newDetails.creditAmount:0) && !isNaN(this.newDetails.debitAmount?this.newDetails.debitAmount:0)) && 
    (!isNaN(this.editedRowData.creditAmount? this.editedRowData.creditAmount:0) && !isNaN(this.editedRowData.debitAmount?this.editedRowData.debitAmount:0)) &&  
    (this.newDetails.accountingTreeChartID || this.editedRowData.accountingTreeChartID)){
      this.isDetailsValid = true
    } else{
      this.isDetailsValid = false
    } 
  }

  validateNumber(event: any, field: keyof AccountingEntries): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = ''; 
      if (typeof this.accountingEntries[field] === 'string') {
        this.accountingEntries[field] = '' as never;  
      }
    }
  }

  validateNumberNewDetails(event: any, field: keyof AccountingEntriesDetails): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = ''; 
      if (typeof this.newDetails[field] === 'string') {
        this.newDetails[field] = '' as never;  
      }
    }
  }

  validateNumberEditedRowData(event: any, field: keyof AccountingEntriesDetails): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = ''; 
      if (typeof this.editedRowData[field] === 'string') {
        this.editedRowData[field] = '' as never;  
      }
    }
  }

  Save(){ 
    if(this.isFormValid()){
      if(this.isCreate){
        this.accountingEntriesService.Add(this.accountingEntries, this.DomainName).subscribe(
          (data) => {
            let id = JSON.parse(data).id 
            this.router.navigateByUrl(`Employee/Accounting Entries Details/${id}`)
            Swal.fire({
              title: 'Saved Successfully',
              icon: 'success', 
              confirmButtonColor: '#FF7519',  
            })
          }
        )
      } else if(this.isEdit){
        this.accountingEntriesService.Edit(this.accountingEntries, this.DomainName).subscribe(
          (data) => {
            this.GetAccountingEntriesByID() 
            this.router.navigateByUrl(`Employee/Accounting Entries Details/${this.AccountingEntriesID}`)
            Swal.fire({
              title: 'Updated Successfully',
              icon: 'success', 
              confirmButtonColor: '#FF7519',  
            }
          )
        } 
        )
      }
    }
  }

  GetAccountingEntriesDetails(){
    this.accountingEntriesDetailsData = []
    this.accountingEntriesDetailsService.Get(this.DomainName, this.AccountingEntriesID).subscribe(
      (data) => {
        this.accountingEntriesDetailsData = data 
        let totalCredit = 0
        let totalDebit = 0
        this.accountingEntriesDetailsData.forEach(element => {
          totalCredit = totalCredit + (element.creditAmount?element.creditAmount:0)
          totalDebit = totalDebit + (element.debitAmount?element.debitAmount:0)
        });
        this.totalCredit = totalCredit
        this.totalDebit = totalDebit
        this.theDifference = this.totalCredit - this.totalDebit
      }
    )
  }

  AddAccountingEntriesDetails(){
    this.isDetailsValid = false
    this.editingRowId = null; 
    this.editedRowData = new AccountingEntriesDetails(); 
    this.isNewDetails = true 
    this.GetAccountingTreeChartData()
  }

  SaveNewDetails(){
    this.isLoading = true;
    this.newDetails.accountingEntriesMasterID = this.AccountingEntriesID
    this.accountingEntriesDetailsService.Add(this.newDetails, this.DomainName).subscribe(
      (data) => {
        this.isLoading = false;
        this.isNewDetails = false
        this.newDetails = new AccountingEntriesDetails()
        this.GetAccountingEntriesDetails()
        this.editingRowId = null; 
        this.editedRowData = new AccountingEntriesDetails(); 
        this.isDetailsValid = false
      }
    )
  }

  EditDetail(row: AccountingEntriesDetails) {
    this.isNewDetails = false
    this.isDetailsValid = true
    this.newDetails = new AccountingEntriesDetails() 
    this.editingRowId = row.id
    this.editedRowData = { ...row }
    this.GetAccountingTreeChartData() 
    if (this.editedRowData.accountingTreeChartID) {
      this.accountingTreeChartService.GetByID(+this.editedRowData.accountingTreeChartID, this.DomainName).subscribe(
        (data) => { 
          if(data.linkFileID && data.id){
            this.dataAccordingToLinkFileService.GetTableDataAccordingToLinkFileAndSubAccount(this.DomainName, data.linkFileID, data.id).subscribe(
              (data) => {
                this.subAccountData = data
              }
            )
          } else{
            this.newDetails.subAccountingID = null
            this.editedRowData.subAccountingID = null
          }
        }
      )
    }
  }

  SaveEditedDetail() {
    this.isLoading = true;
    this.accountingEntriesDetailsService.Edit(this.editedRowData, this.DomainName).subscribe(
      (data) =>{
        this.isLoading = false;
        this.editingRowId = null; 
        this.editedRowData = new AccountingEntriesDetails(); 
        this.isDetailsValid = false
        this.isNewDetails = false
        this.newDetails = new AccountingEntriesDetails()
        this.GetAccountingEntriesDetails()
      }
    )
  } 

  DeleteDetail(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Accounting Entries Detail?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.accountingEntriesDetailsService.Delete(id, this.DomainName).subscribe(
          (data) => {
            this.GetAccountingEntriesDetails()
          }
        )
      }
    });
  }


  // DownloadData() {
  //   let orderElement = document.getElementById('DataToDownload');

  //   if (!orderElement) {
  //     console.error("Page body not found!");
  //     return;
  //   }

  //   document.querySelectorAll('.no-print').forEach(el => {
  //     (el as HTMLElement).style.display = 'none';
  //   });

  //   setTimeout(() => {
  //     html2pdf().from(orderElement).set({
  //       margin: 10,
  //       filename: `AccountingEntries_${this.AccountingEntriesID}.pdf`,
  //       image: { type: 'jpeg', quality: 0.98 },
  //       html2canvas: { scale: 3, useCORS: true, allowTaint: true, logging: true },
  //       jsPDF: { orientation: 'portrait', unit: 'mm', format: 'a4' }
  //     }).save().then(() => {
  //       document.querySelectorAll('.no-print').forEach(el => {
  //         (el as HTMLElement).style.display = '';
  //       });
  //     });
  //   }, 500);
  // }

  DownloadAsPDF() {
    this.showPDF = true;
    setTimeout(() => {
      this.pdfComponentRef.downloadPDF();
      setTimeout(() => this.showPDF = false, 2000);
    }, 500);
  }

  Print() {
    this.showPDF = true;
    setTimeout(() => {
      const printContents = document.getElementById("Data")?.innerHTML;
      if (!printContents) {
        console.error("Element not found!");
        return;
      }

      const printStyle = `
        <style>
          @page { size: auto; margin: 0mm; }
          body { margin: 0; }
          @media print {
            body > *:not(#print-container) {
              display: none !important;
            }
            #print-container {
              display: block !important;
              position: static !important;
              top: auto !important;
              left: auto !important;
              width: 100% !important;
              height: auto !important;
              background: white !important;
              box-shadow: none !important;
              margin: 0 !important;
            }
          }
        </style>
      `;

      const printContainer = document.createElement('div');
      printContainer.id = 'print-container';
      printContainer.innerHTML = printStyle + printContents;

      document.body.appendChild(printContainer);
      window.print();
      
      setTimeout(() => {
        document.body.removeChild(printContainer);
        this.showPDF = false;
      }, 100);
    }, 500);
  }


  async DownloadAsExcel() {
    await this.reportsService.generateExcelReport({
      mainHeader: {
        en: "Accounting Entries Report",
        ar: "تقرير القيود المحاسبية"
      },
      subHeaders: [
        { en: "Detailed accounting entries information", ar: "معلومات تفصيلية عن القيود المحاسبية" },
      ],
      infoRows: [
        { key: 'Document Type', value: this.accountingEntries.accountingEntriesDocTypeName || '' },
        { key: 'Document Number', value: this.accountingEntries.docNumber || '' },
        { key: 'Date', value: this.accountingEntries.date || '' },
        { key: 'Total Credit', value: this.totalCredit || 0 },
        { key: 'Total Debit', value: this.totalDebit || 0 },
        { key: 'Difference', value: this.theDifference || 0 }
      ],
      reportImage: '', // Add image URL if available
      filename: "Accounting_Entries_Report.xlsx",
      tables: [
        {
          title: "Accounting Entries Details",
          headers: ['id', 'debitAmount', 'creditAmount', 'accountingTreeChartName', 'subAccountingName', 'note'],
          data: this.accountingEntriesDetailsData.map((row) => [
            row.id || 0, 
            row.debitAmount || 0, 
            row.creditAmount || 0, 
            row.accountingTreeChartName || '', 
            row.subAccountingName || '', 
            row.note || ''
          ])
        }
      ]
    });
  }
}
