import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShareDomainNameService {

  private DomainNameSource = new BehaviorSubject<string | null>(null);
  currentBusId = this.DomainNameSource.asObservable();

  setBusId(DomainName: string) {
    this.DomainNameSource.next(DomainName);
  }}
