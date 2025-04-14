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

  // BaseUrl="http://44.210.155.226:5000/api/with-domain"
  // BaseUrlOcta="http://44.210.155.226:5000/api

  constructor() { }


  GetHeader(){    
    var Header = "Domain_One"      
    // const hostname = window.location.hostname;  
    // var Header = hostname.split('.')[0] 
    return Header;
  }
}
