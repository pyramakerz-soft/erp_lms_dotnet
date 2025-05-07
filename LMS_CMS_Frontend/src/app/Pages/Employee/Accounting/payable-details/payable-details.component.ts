import { CommonModule } from '@angular/common';
import { Component, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Payable } from '../../../../Models/Accounting/payable';
import { PayableDocType } from '../../../../Models/Accounting/payable-doc-type';
import { PayableDetails } from '../../../../Models/Accounting/payable-details';
import { LinkFile } from '../../../../Models/Accounting/link-file';
import { TokenData } from '../../../../Models/token-data';
import { PayableService } from '../../../../Services/Employee/Accounting/payable.service';
import { PayableDocTypeService } from '../../../../Services/Employee/Accounting/payable-doc-type.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { BankService } from '../../../../Services/Employee/Accounting/bank.service';
import { DataAccordingToLinkFileService } from '../../../../Services/Employee/Accounting/data-according-to-link-file.service';
import { LinkFileService } from '../../../../Services/Employee/Accounting/link-file.service';
import { ReceivableDetailsService } from '../../../../Services/Employee/Accounting/receivable-details.service';
import { SaveService } from '../../../../Services/Employee/Accounting/save.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { PayableDetailsService } from '../../../../Services/Employee/Accounting/payable-details.service';
import Swal from 'sweetalert2';
import html2pdf from 'html2pdf.js';
import { PdfPrintComponent } from '../../../../Component/pdf-print/pdf-print.component';
import { ReportsService } from '../../../../Services/shared/reports.service';

@Component({
  selector: 'app-payable-details',
  standalone: true,
  imports: [CommonModule, FormsModule, PdfPrintComponent],
  templateUrl: './payable-details.component.html',
  styleUrl: './payable-details.component.css'
})
export class PayableDetailsComponent {
  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  DomainName: string = '';
  UserID: number = 0;

  path: string = '';
  PayableID: number = 0;

  isCreate: boolean = false
  isEdit: boolean = false
  isView: boolean = false

  payable: Payable = new Payable()
  validationErrors: { [key in keyof Payable]?: string } = {};
  validationErrorsForDetails: { [key in keyof PayableDetails]?: string } = {};

  dataTypesData: PayableDocType[] = []
  bankOrSaveData: any[] = []
  payableDetailsData: PayableDetails[] = []
  newDetails: PayableDetails = new PayableDetails()
  linkFilesData: LinkFile[] = []
  linkFileTypesData: any[] = []
  totalAmount: number = 0;

  isNewDetails: boolean = false
  isDetailsValid: boolean = false

  editingRowId: number | null = null;
  editedRowData: PayableDetails = new PayableDetails()

  isLoading = false;


  @ViewChild(PdfPrintComponent) pdfComponentRef!: PdfPrintComponent;
  showPDF = false;
  
  constructor(
    private router: Router, private menuService: MenuService, public activeRoute: ActivatedRoute, public account: AccountService, public payableDocTypeService: PayableDocTypeService,
    public DomainServ: DomainService, public EditDeleteServ: DeleteEditPermissionService, public ApiServ: ApiService, public payableService: PayableService,
    public bankService: BankService, public saveService: SaveService, public payableDetailsService: PayableDetailsService, public linkFileService: LinkFileService,
    public dataAccordingToLinkFileService: DataAccordingToLinkFileService, public reportsService: ReportsService) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.PayableID = Number(this.activeRoute.snapshot.paramMap.get('id'))

    if (!this.PayableID) {
      this.isCreate = true
    } else {
      this.GetPayableByID()
      this.GetPayableDetails()
    }

    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
      if (url[1].path == "View") {
        this.isView = true
      } else {
        if (this.PayableID) {
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

  moveToPayable() {
    this.router.navigateByUrl("Employee/Payable")
  }

  GetDocType() {
    this.payableDocTypeService.Get(this.DomainName).subscribe(
      (data) => {
        this.dataTypesData = data
      }
    )
  }

  GetPayableByID() {
    this.payableService.GetByID(this.PayableID, this.DomainName).subscribe(
      (data) => {
        this.payable = data
        if (this.payable.linkFileID == 5) {
          this.getSaveData()
        } else if (this.payable.linkFileID == 6) {
          this.getBankData()
        }
      }
    )
  }

  getBankData() {
    this.bankOrSaveData = []
    this.bankService.Get(this.DomainName).subscribe(
      (data) => {
        this.bankOrSaveData = data
      }
    )
  }

  getSaveData() {
    this.bankOrSaveData = []
    this.saveService.Get(this.DomainName).subscribe(
      (data) => {
        this.bankOrSaveData = data
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

  capitalizeField(field: keyof Payable): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  DetailsCapitalizeField(field: keyof PayableDetails): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.payable) {
      if (this.payable.hasOwnProperty(key)) {
        const field = key as keyof Payable;
        if (!this.payable[field]) {
          if (field == "payableDocTypeID" || field == "linkFileID" || field == "bankOrSaveID" || field == "date") {
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

  isDetailsFormValid(detail: PayableDetails): boolean {
    console.log(detail)
    let isValid = true;
    for (const key in detail) {
      if (this.hasOwnProperty(key)) {
        const field = key as keyof PayableDetails;
        if (!detail[field]) {
          if (field == "amount" || field == "linkFileName") {

            this.validationErrorsForDetails[field] = `*${this.DetailsCapitalizeField(field)} is required`
            isValid = false;
          }
        }
        else {
          this.validationErrorsForDetails[field] = '';
        }
      }
    }
    if (detail.linkFileID == 0) {
      this.validationErrorsForDetails["linkFileID"] = 'link File is required'
      isValid = false;
    }
    if (detail.linkFileTypeID == 0) {
      this.validationErrorsForDetails["linkFileTypeID"] = 'Link File Data is required'
      isValid = false;
    }
    if (detail.amount == null || detail.amount == "") {
      this.validationErrorsForDetails["amount"] = 'amount is required'
      isValid = false;
    }
    return isValid;
  }

  onInputValueChange(event: { field: keyof Payable, value: any }) {
    const { field, value } = event;
    (this.payable as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }

    if (field == "linkFileID") {
      this.payable.bankOrSaveID = 0
    }
  }

  onInputValueChangeForDetails(event: { field: keyof PayableDetails, value: any }) {
    const { field, value } = event;
    (this.newDetails as any)[field] = value;
    if (value) {
      this.validationErrorsForDetails[field] = '';
    }
    if (field == "linkFileID") {
      this.newDetails.linkFileTypeID = 0
    }

    if ((this.newDetails.amount || this.editedRowData.amount) &&
      (!isNaN(this.newDetails.amount ? this.newDetails.amount : 0) && !isNaN(this.editedRowData.amount ? this.editedRowData.amount : 0)) &&
      (this.newDetails.linkFileID || this.editedRowData.linkFileID) &&
      (this.newDetails.linkFileTypeID || this.editedRowData.linkFileTypeID)) {
      this.isDetailsValid = true
    } else {
      this.isDetailsValid = false
    }
  }

  onInputValueChangeForEditDetails(event: { field: keyof PayableDetails, value: any }) {
    const { field, value } = event;
    (this.editedRowData as any)[field] = value;
    if (value) {
      this.validationErrorsForDetails[field] = '';
    }
    if (field == "linkFileID") {
      this.editedRowData.linkFileTypeID = 0
    }

    if ((this.editedRowData.amount || this.editedRowData.amount) &&
      (!isNaN(this.editedRowData.amount ? this.editedRowData.amount : 0) && !isNaN(this.editedRowData.amount ? this.editedRowData.amount : 0)) &&
      (this.editedRowData.linkFileID || this.editedRowData.linkFileID) &&
      (this.editedRowData.linkFileTypeID || this.editedRowData.linkFileTypeID)) {
      this.isDetailsValid = true
    } else {
      this.isDetailsValid = false
    }
  }

  validateNumberPayable(event: any, field: keyof Payable): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = '';
      if (typeof this.payable[field] === 'string') {
        this.payable[field] = '' as never;
      }
    }
  }

  validateNumberDetails(event: any, field: keyof PayableDetails): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = '';
      if (typeof this.newDetails[field] === 'string') {
        this.newDetails[field] = '' as never;
      }
    }
  }

  validateNumberEditDetails(event: any, field: keyof PayableDetails): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = '';
      if (typeof this.editedRowData[field] === 'string') {
        this.editedRowData[field] = '' as never;
      }
    }
  }

  Save() {
    if (this.isFormValid()) {
      if (this.isCreate) {
        this.payableService.Add(this.payable, this.DomainName).subscribe(
          (data) => {
            let id = JSON.parse(data).id
            this.router.navigateByUrl(`Employee/Payable Details/${id}`)
            Swal.fire({
              title: 'Saved Successfully',
              icon: 'success',
              confirmButtonColor: '#FF7519',
            })
          }
        )
      } else if (this.isEdit) {
        this.payableService.Edit(this.payable, this.DomainName).subscribe(
          (data) => {
            this.GetPayableByID()
            Swal.fire({
              icon: 'success',
              title: 'Done!',
              text: 'The Payable has been edited successfully.',
              confirmButtonColor: '#FF7519',
            });
          }
        )
      }
    }
  }

  GetPayableDetails() {
    this.payableDetailsData = []
    this.payableDetailsService.Get(this.DomainName, this.PayableID).subscribe(
      (data) => {
        this.payableDetailsData = data
        let total = 0
        this.payableDetailsData.forEach(element => {
          total = total + (element.amount ? element.amount : 0)
        });
        this.totalAmount = total
      }
    )
  }

  GetLinkFiles() {
    this.linkFileService.Get(this.DomainName).subscribe(
      (data) => {
        this.linkFilesData = data
      }
    )
  }

  GetLinkFilesTypeData(id:number) {
    this.linkFileTypesData = []
    this.dataAccordingToLinkFileService.GetTableDataAccordingToLinkFile(this.DomainName, id).subscribe(
      (data) => {
        console.log(this.linkFileTypesData)
        this.linkFileTypesData = data
      }
    )
  }

  AddPayableDetails() {
    this.isDetailsValid = false
    this.editingRowId = null;
    this.editedRowData = new PayableDetails();
    this.isNewDetails = true
    this.GetLinkFiles()
  }

  SaveNewDetails() {
    this.newDetails.payableMasterID = this.PayableID
    console.log(1)
    if (this.isDetailsFormValid(this.newDetails)) {
      this.isLoading = true;
      this.payableDetailsService.Add(this.newDetails, this.DomainName).subscribe(
        (data) => {
          this.isLoading = false;
          this.isNewDetails = false
          this.newDetails = new PayableDetails()
          this.GetPayableDetails()
          this.editingRowId = null;
          this.editedRowData = new PayableDetails();
          this.isDetailsValid = false
        }
      )
    }
    this.isLoading = false;
  }

  EditDetail(row: PayableDetails) {
    this.isNewDetails = false
    this.isDetailsValid = true
    this.newDetails = new PayableDetails()
    this.GetLinkFiles()
    this.editingRowId = row.id
    this.editedRowData = { ...row }
    if (this.editedRowData.linkFileID) {
      this.dataAccordingToLinkFileService.GetTableDataAccordingToLinkFile(this.DomainName, +this.editedRowData.linkFileID).subscribe(
        (data) => {
          this.linkFileTypesData = data
        }
      )
    }
  }

  SaveEditedDetail() {
    if (this.isDetailsFormValid(this.editedRowData)) {
      this.payableDetailsService.Edit(this.editedRowData, this.DomainName).subscribe(
        (data) => {
          this.editingRowId = null;
          this.editedRowData = new PayableDetails();
          this.isDetailsValid = false
          this.isNewDetails = false
          this.newDetails = new PayableDetails()
          this.GetPayableDetails()
        }
      )
    }
  }

  DeleteDetail(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Payable Detail?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.payableDetailsService.Delete(id, this.DomainName).subscribe(
          (data) => {
            this.GetPayableDetails()
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
  //       filename: `Payable_${this.PayableID}.pdf`,
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
        en: "Payable Report",
        ar: "تقرير الدفع"
      },
      subHeaders: [
        { en: "Detailed payable information", ar: "معلومات تفصيلية عن الدفع" },
      ],
      infoRows: [
        { key: 'Document Type', value: this.payable.payableDocTypesName || '' },
        { key: 'Document Number', value: this.payable.docNumber || '' },
        { key: 'Date', value: this.payable.date || '' },
        { key: 'Total Amount', value: this.totalAmount || 0 }
      ],
      reportImage: '', // Add image URL if available
      filename: "Payable_Report.xlsx",
      tables: [
        {
          title: "Payable Details",
          headers: ['id', 'amount', 'linkFileName', 'linkFileTypeName', 'notes'],
          data: this.payableDetailsData.map((row) => [
            row.id || 0, 
            row.amount || 0, 
            row.linkFileName || '', 
            row.linkFileTypeName || '', 
            row.notes || ''
          ])
        }
      ]
    });
  }

}
