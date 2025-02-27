import { Injectable } from '@angular/core';
import { Sales } from '../../../Models/Inventory/sales';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiService } from '../../api.service';
import { InventoryFlag } from '../../../Models/Inventory/inventory-flag';

@Injectable({
  providedIn: 'root'
})
export class SalesService {

  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  Get(DomainName: string, FlagId: number, pageNumber: number, pageSize: number) {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`)
      .set('Content-Type', 'application/json');
    return this.http.get<{ data: Sales[], pagination: any ,inventoryFlag :InventoryFlag }>(`${this.baseUrl}/InventoryMaster/ByFlagId/${FlagId}?pageNumber=${pageNumber}&pageSize=${pageSize}`, { headers });
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
    return this.http.get<Sales>(`${this.baseUrl}/InventoryMaster/${id}`, { headers })
  }

  Add(sales: Sales, DomainName: string): Observable<any> {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`);

    const formData = new FormData();

    formData.append('invoiceNumber', sales.invoiceNumber.toString());
    formData.append('date', sales.date);
    formData.append('isCash', sales.isCash.toString());
    formData.append('isVisa', sales.isVisa.toString());
    formData.append('cashAmount', sales.cashAmount.toString());
    formData.append('visaAmount', sales.visaAmount.toString());
    formData.append('remaining', sales.remaining.toString());
    formData.append('total', sales.total.toString());
    formData.append('notes', sales.notes);
    formData.append('storeID', sales.storeID.toString());
    formData.append('flagId', sales.flagId.toString());
    formData.append('studentID', sales.studentID.toString());
    formData.append('saveID', sales.saveID.toString());
    formData.append('bankID', sales.bankID.toString());

    if (sales.attachment && sales.attachment.length > 0) {
      sales.attachment.forEach((file, index) => {
        formData.append('attachment', file);
      });
    }

    if (sales.inventoryDetails && sales.inventoryDetails.length > 0) {
      sales.inventoryDetails.forEach((item, index) => {
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

  Edit(sales: Sales, DomainName: string): Observable<Sales> {
    if (DomainName != null) {
      this.header = DomainName
    }
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`);
    const formData = new FormData();
    formData.append('id', sales.id?.toString() || '');
    formData.append('invoiceNumber', sales.invoiceNumber?.toString() || '');
    formData.append('date', sales.date || '');
    formData.append('isCash', sales.isCash?.toString() || 'false');
    formData.append('isVisa', sales.isVisa?.toString() || 'false');
    formData.append('cashAmount', sales.cashAmount?.toString() || '0');
    formData.append('visaAmount', sales.visaAmount?.toString() || '0');
    formData.append('remaining', sales.remaining?.toString() || '0');
    formData.append('total', sales.total.toString());
    formData.append('notes', sales.notes || '');
    formData.append('storeID', sales.storeID?.toString() || '0');
    formData.append('flagId', sales.flagId?.toString() || '0');
    formData.append('studentID', sales.studentID?.toString() || '0');
    formData.append('saveID', sales.saveID?.toString() || '0');
    formData.append('bankID', sales.bankID?.toString() || '0');


    if (sales.NewAttachments && sales.NewAttachments.length > 0) {
      sales.NewAttachments.forEach((file, index) => {
        formData.append('NewAttachments', file);
      });
    }

    if (sales.DeletedAttachments && sales.DeletedAttachments.length > 0) {
      sales.DeletedAttachments.forEach((file, index) => {
        formData.append('DeletedAttachments', file);
      });
    }

    return this.http.put<Sales>(`${this.baseUrl}/InventoryMaster`, formData, { headers });
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
