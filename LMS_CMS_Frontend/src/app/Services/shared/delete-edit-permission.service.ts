import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DeleteEditPermissionService {

  constructor() { }

  IsAllowDelete(InsertedByID:number , UserID:number , AllowDeleteForOthers :boolean){
    if(UserID==InsertedByID) return true ;
    else if(AllowDeleteForOthers) return true ;
    else return false;
  }
  IsAllowEdit(InsertedByID:number , UserID:number , AllowEditForOthers :boolean){
    if(UserID==InsertedByID) return true ;
    else if(AllowEditForOthers) return true ;
    else return false;
  }
}
