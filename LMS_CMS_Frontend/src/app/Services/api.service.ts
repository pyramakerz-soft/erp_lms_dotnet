import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  // BaseUrl="https://localhost:7205/api/with-domain" 
  // BaseUrlOcta="https://localhost:7205/api" 

  // BaseUrl="https://3.214.27.32/"
  BaseUrl="http://localhost:5094/api/with-domain" 
  BaseUrlOcta="http://localhost:5094/api" 


  constructor() { }


  GetHeader(){    
    var Header = "domain"      
    return Header;
  }
}
