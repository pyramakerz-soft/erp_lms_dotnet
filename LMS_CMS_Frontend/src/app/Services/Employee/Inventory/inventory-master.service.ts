import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { InventoryFlag } from '../../../Models/Inventory/inventory-flag';
import { InventoryMaster } from '../../../Models/Inventory/InventoryMaster';
import { ApiService } from '../../api.service';

@Injectable({
  providedIn: 'root'
})
export class InventoryMasterService {

  baseUrl = ""
   header = ""
 
   constructor(public http: HttpClient, public ApiServ: ApiService) {
     this.baseUrl = ApiServ.BaseUrl
   }
 
   Get(DomainName: string, FlagId: number, pageNumber: number, pageSize: number) {
    console.log("dd")
     if (DomainName != null) {
       this.header = DomainName
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
     return this.http.get<{ data: InventoryMaster[], pagination: any ,inventoryFlag :InventoryFlag }>(`${this.baseUrl}/InventoryMaster/ByFlagId/${FlagId}?pageNumber=${pageNumber}&pageSize=${pageSize}`, { headers });
   }
 
   GetById(id: number, DomainName: string) {
     if (DomainName != null) {
       this.header = DomainName
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
     return this.http.get<InventoryMaster>(`${this.baseUrl}/InventoryMaster/${id}`, { headers })
   }
 
   Add(master: InventoryMaster, DomainName: string): Observable<any> {
     if (DomainName != null) {
       this.header = DomainName
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`);
 
     const formData = new FormData();
 
     formData.append('invoiceNumber', master.invoiceNumber.toString());
     formData.append('date', master.date);
     formData.append('isCash', master.isCash.toString());
     formData.append('isVisa', master.isVisa.toString());
     formData.append('cashAmount', master.cashAmount.toString());
     formData.append('visaAmount', master.visaAmount.toString());
     formData.append('remaining', master.remaining.toString());
     formData.append('total', master.total.toString());
     formData.append('notes', master.notes);
     formData.append('storeID', master.storeID.toString());
     formData.append('flagId', master.flagId.toString());
     formData.append('studentID', master.studentID.toString());
     formData.append('saveID', master.saveID.toString());
     formData.append('bankID', master.bankID.toString());
     formData.append('supplierId', master.supplierId.toString());
     formData.append('storeToTransformId', master.storeToTransformId.toString());

 
     if (master.attachment && master.attachment.length > 0) {
       master.attachment.forEach((file, index) => {
         formData.append('attachment', file);
       });
     }
 
     if (master.inventoryDetails && master.inventoryDetails.length > 0) {
       master.inventoryDetails.forEach((item, index) => {
         formData.append(`inventoryDetails[${index}][price]`, item.price.toString());
         formData.append(`inventoryDetails[${index}][totalPrice]`, item.totalPrice.toString());
         formData.append(`inventoryDetails[${index}][quantity]`, item.quantity.toString());
         formData.append(`inventoryDetails[${index}][notes]`, item.notes.toString());
         formData.append(`inventoryDetails[${index}][shopItemID]`, item.shopItemID.toString());
         formData.append(`inventoryDetails[${index}][inventoryMasterId]`, item.inventoryMasterId.toString());
 
       });
     }
     return this.http.post<any>(`${this.baseUrl}/InventoryMaster`, formData, { headers });
   }
 
   Edit(master: InventoryMaster, DomainName: string ): Observable<InventoryMaster> {
     if (DomainName != null) {
       this.header = DomainName
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`);
     const formData = new FormData();
     formData.append('id', master.id?.toString() || '');
     formData.append('invoiceNumber', master.invoiceNumber?.toString() || '');
     formData.append('date', master.date || '');
     formData.append('isCash', master.isCash?.toString() || 'false');
     formData.append('isVisa', master.isVisa?.toString() || 'false');
     formData.append('isEditInvoiceNumber', master.isEditInvoiceNumber?.toString() || 'false');
     formData.append('cashAmount', master.cashAmount?.toString() || '0');
     formData.append('visaAmount', master.visaAmount?.toString() || '0');
     formData.append('remaining', master.remaining?.toString() || '0');
     formData.append('total', master.total.toString());
     formData.append('notes', master.notes || '');
     formData.append('storeID', master.storeID?.toString() || '0');
     formData.append('flagId', master.flagId?.toString() || '0');
     formData.append('studentID', master.studentID?.toString() || '0');
     formData.append('saveID', master.saveID?.toString() || '0');
     formData.append('bankID', master.bankID?.toString() || '0');
     formData.append('supplierId', master.supplierId?.toString()|| '0');
     formData.append('storeToTransformId', master.storeToTransformId?.toString()|| '0');
 
     if (master.NewAttachments && master.NewAttachments.length > 0) {
       master.NewAttachments.forEach((file, index) => {
         formData.append('NewAttachments', file);
       });
     }
 
     if (master.DeletedAttachments && master.DeletedAttachments.length > 0) {
       master.DeletedAttachments.forEach((file, index) => {
         formData.append('DeletedAttachments', file);
       });
     }
 
     return this.http.put<InventoryMaster>(`${this.baseUrl}/InventoryMaster`, formData, { headers });
   }
 
   Delete(id: number, DomainName: string) {
     if (DomainName != null) {
       this.header = DomainName
     }
     const token = localStorage.getItem("current_token");
     const headers = new HttpHeaders()
       .set('domain-name', this.header)
       .set('Authorization', `Bearer ${token}`)
       .set('Content-Type', 'application/json');
     return this.http.delete(`${this.baseUrl}/InventoryMaster/${id}`, { headers })
   }
 
 }
 