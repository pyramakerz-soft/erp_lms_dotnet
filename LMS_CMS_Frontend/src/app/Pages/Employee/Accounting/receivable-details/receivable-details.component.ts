import { Component, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService } from '../../../../Services/account.service';
import { ApiService } from '../../../../Services/api.service';
import { ReceivableService } from '../../../../Services/Employee/Accounting/receivable.service';
import { DomainService } from '../../../../Services/Employee/domain.service';
import { DeleteEditPermissionService } from '../../../../Services/shared/delete-edit-permission.service';
import { MenuService } from '../../../../Services/shared/menu.service';
import { TokenData } from '../../../../Models/token-data';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Receivable } from '../../../../Models/Accounting/receivable';
import { ReceivableDocType } from '../../../../Models/Accounting/receivable-doc-type';
import { ReceivableDocTypeService } from '../../../../Services/Employee/Accounting/receivable-doc-type.service';
import { BankService } from '../../../../Services/Employee/Accounting/bank.service';
import { SaveService } from '../../../../Services/Employee/Accounting/save.service';
import { ReceivableDetailsService } from '../../../../Services/Employee/Accounting/receivable-details.service';
import { ReceivableDetails } from '../../../../Models/Accounting/receivable-details';
import { LinkFile } from '../../../../Models/Accounting/link-file';
import { LinkFileService } from '../../../../Services/Employee/Accounting/link-file.service';
import { DataAccordingToLinkFileService } from '../../../../Services/Employee/Accounting/data-according-to-link-file.service';
import Swal from 'sweetalert2';
import html2pdf from 'html2pdf.js';

import { PdfPrintComponent } from '../../../../Component/pdf-print/pdf-print.component';
import { ReportsService } from '../../../../Services/shared/reports.service';

@Component({
  selector: 'app-receivable-details',
  standalone: true,
  imports: [CommonModule, FormsModule, PdfPrintComponent],
  templateUrl: './receivable-details.component.html',
  styleUrl: './receivable-details.component.css'
})
export class ReceivableDetailsComponent {
  User_Data_After_Login: TokenData = new TokenData('', 0, 0, 0, 0, '', '', '', '', '');

  AllowEdit: boolean = false;
  AllowDelete: boolean = false;
  AllowEditForOthers: boolean = false;
  AllowDeleteForOthers: boolean = false;

  DomainName: string = '';
  UserID: number = 0;

  path: string = '';
  ReceivableID: number = 0;

  isCreate: boolean = false
  isEdit: boolean = false
  isView: boolean = false

  receivable: Receivable = new Receivable()
  validationErrors: { [key in keyof Receivable]?: string } = {};
  validationErrorsForDetails: { [key in keyof ReceivableDetails]?: string } = {};

  dataTypesData: ReceivableDocType[] = []
  bankOrSaveData: any[] = []
  receivableDetailsData: ReceivableDetails[] = []
  newDetails: ReceivableDetails = new ReceivableDetails()
  linkFilesData: LinkFile[] = []
  linkFileTypesData: any[] = []
  totalAmount: number = 0;

  isNewDetails: boolean = false
  isDetailsValid: boolean = false

  editingRowId: number | null = null;
  editedRowData: ReceivableDetails = new ReceivableDetails()
  isLoading = false

  @ViewChild(PdfPrintComponent) pdfComponentRef!: PdfPrintComponent;
  showPDF = false;
  
  constructor(
    private router: Router, private menuService: MenuService, public activeRoute: ActivatedRoute, public account: AccountService, public receivableDocTypeService: ReceivableDocTypeService,
    public DomainServ: DomainService, public EditDeleteServ: DeleteEditPermissionService, public ApiServ: ApiService, public receivableService: ReceivableService,
    public bankService: BankService, public saveService: SaveService, public receivableDetailsService: ReceivableDetailsService, public linkFileService: LinkFileService,

    public dataAccordingToLinkFileService: DataAccordingToLinkFileService, public reportsService: ReportsService) { }

  ngOnInit() {
    this.User_Data_After_Login = this.account.Get_Data_Form_Token();
    this.UserID = this.User_Data_After_Login.id;

    this.DomainName = this.ApiServ.GetHeader();

    this.ReceivableID = Number(this.activeRoute.snapshot.paramMap.get('id'))

    if (!this.ReceivableID) {
      this.isCreate = true
    } else {
      this.GetReceivableByID()
      this.GetReceivableDetails()
    }

    this.activeRoute.url.subscribe(url => {
      this.path = url[0].path
      if (url[1].path == "View") {
        this.isView = true
      } else {
        if (this.ReceivableID) {
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

  moveToReceivable() {
    this.router.navigateByUrl("Employee/Receivable")
  }

  GetDocType() {
    this.dataTypesData = []
    this.receivableDocTypeService.Get(this.DomainName).subscribe(
      (data) => {
        this.dataTypesData = data
      }
    )
  }

  GetReceivableByID() {
    this.receivableService.GetByID(this.ReceivableID, this.DomainName).subscribe(
      (data) => {
        this.receivable = data
        if (this.receivable.linkFileID == 5) {
          this.getSaveData()
        } else if (this.receivable.linkFileID == 6) {
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

  capitalizeField(field: keyof Receivable): string {
    return field.charAt(0).toUpperCase() + field.slice(1).replace(/_/g, ' ');
  }

  isFormValid(): boolean {
    let isValid = true;
    for (const key in this.receivable) {
      if (this.receivable.hasOwnProperty(key)) {
        const field = key as keyof Receivable;
        if (!this.receivable[field]) {
          if (field == "receivableDocTypesID" || field == "linkFileID" || field == "bankOrSaveID" || field == "date") {
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

  onInputValueChange(event: { field: keyof Receivable, value: any }) {
    const { field, value } = event;
    (this.receivable as any)[field] = value;
    if (value) {
      this.validationErrors[field] = '';
    }

    if (field == "linkFileID") {
      this.receivable.bankOrSaveID = 0
    }
  }

  onInputValueChangeForDetails(event: { field: keyof ReceivableDetails, value: any }) {
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

  validateNumberReceivable(event: any, field: keyof Receivable): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = '';
      if (typeof this.receivable[field] === 'string') {
        this.receivable[field] = '' as never;
      }
    }
  }

  validateNumberDetails(event: any, field: keyof ReceivableDetails): void {
    const value = event.target.value;
    if (isNaN(value) || value === '') {
      event.target.value = '';
      if (typeof this.newDetails[field] === 'string') {
        this.newDetails[field] = '' as never;
      }
    }
  }

  validateNumberEditDetails(event: any, field: keyof ReceivableDetails): void {
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
      this.isLoading = true
      if (this.isCreate) {
        this.receivableService.Add(this.receivable, this.DomainName).subscribe(
          (data) => {
            let id = JSON.parse(data).id
            this.router.navigateByUrl(`Employee/Receivable Details/${id}`)
            Swal.fire({
              title: 'Saved Successfully',
              icon: 'success', 
              confirmButtonColor: '#FF7519',  
            })
            this.isLoading = false
          },
          err => {
            this.isLoading = false
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Try Again Later!',
              confirmButtonText: 'Okay',
              customClass: { confirmButton: 'secondaryBg' },
            });
          }
        )
      } else if (this.isEdit) {
        this.isLoading = false
        this.receivableService.Edit(this.receivable, this.DomainName).subscribe(
          (data) => {
            this.GetReceivableByID()
            this.isLoading = false
            Swal.fire({
              icon: 'success',
              title: 'Done!',
              text: 'The Receivable has been edited successfully.',
              confirmButtonColor: '#FF7519',
            });
          },
          err => {
            this.isLoading = false
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: 'Try Again Later!',
              confirmButtonText: 'Okay',
              customClass: { confirmButton: 'secondaryBg' },
            });
          }
        )
      }
    }
  }


  GetReceivableDetails() {
    this.receivableDetailsData = []
    this.receivableDetailsService.Get(this.DomainName, this.ReceivableID).subscribe(
      (data) => {
        this.receivableDetailsData = data
        let total = 0
        this.receivableDetailsData.forEach(element => {
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

  GetLinkFilesTypeData() {
    this.linkFileTypesData = []
    this.dataAccordingToLinkFileService.GetTableDataAccordingToLinkFile(this.DomainName, +this.newDetails.linkFileID).subscribe(
      (data) => {
        this.linkFileTypesData = data
      }
    )
  }

  AddReceivableDetails() {
    this.isDetailsValid = false
    this.editingRowId = null;
    this.editedRowData = new ReceivableDetails();
    this.isNewDetails = true
    this.GetLinkFiles()
  }

  SaveNewDetails() {
    this.isLoading = true
    this.newDetails.receivableMasterID = this.ReceivableID
    this.receivableDetailsService.Add(this.newDetails, this.DomainName).subscribe(
      (data) => {
        this.isLoading = false
        this.isNewDetails = false
        this.newDetails = new ReceivableDetails()
        this.GetReceivableDetails()
        this.editingRowId = null;
        this.editedRowData = new ReceivableDetails();
        this.isDetailsValid = false
      }
    )
  }

  EditDetail(row: ReceivableDetails) {
    this.isNewDetails = false
    this.isDetailsValid = true
    this.newDetails = new ReceivableDetails()
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
    this.receivableDetailsService.Edit(this.editedRowData, this.DomainName).subscribe(
      (data) => {
        this.editingRowId = null;
        this.editedRowData = new ReceivableDetails();
        this.isDetailsValid = false
        this.isNewDetails = false
        this.newDetails = new ReceivableDetails()
        this.GetReceivableDetails()
      }
    )
  }

  DeleteDetail(id: number) {
    Swal.fire({
      title: 'Are you sure you want to delete this Receivable Detail?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#FF7519',
      cancelButtonColor: '#17253E',
      confirmButtonText: 'Delete',
      cancelButtonText: 'Cancel',
    }).then((result) => {
      if (result.isConfirmed) {
        this.receivableDetailsService.Delete(id, this.DomainName).subscribe(
          (data) => {
            this.GetReceivableDetails()
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
  //       filename: `Receivable_${this.ReceivableID}.pdf`,
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
        en: "Receivable Report",
        ar: "تقرير القبض"
      },
      subHeaders: [
        { en: "Detailed receivable information", ar: "معلومات تفصيلية عن القبض" },
      ],
      infoRows: [
        { key: 'Document Type', value: this.receivable.receivableDocTypesName || '' },
        { key: 'Document Number', value: this.receivable.docNumber || '' },
        { key: 'Date', value: this.receivable.date || '' },
        { key: 'Total Amount', value: this.totalAmount || 0 }
      ],
      reportImage: '', // Add image URL if available
      filename: "Receivable_Report.xlsx",
      tables: [
        {
          title: "Receivable Details",
          headers: ['id', 'amount', 'linkFileName', 'linkFileTypeName', 'notes'],
          data: this.receivableDetailsData.map((row) => [
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
