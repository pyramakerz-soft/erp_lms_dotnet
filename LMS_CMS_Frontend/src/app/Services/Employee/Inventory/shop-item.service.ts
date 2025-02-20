import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { ShopItem } from '../../../Models/Inventory/shop-item';

@Injectable({
  providedIn: 'root'
})
export class ShopItemService {
  baseUrl = ""
  header = ""

  constructor(public http: HttpClient, public ApiServ: ApiService) {
    this.baseUrl = ApiServ.BaseUrl
  }

  Get(DomainName: string) {
      if (DomainName != null) {
        this.header = DomainName
      }
      const token = localStorage.getItem("current_token");
      const headers = new HttpHeaders()
        .set('domain-name', this.header)
        .set('Authorization', `Bearer ${token}`)
        .set('Content-Type', 'application/json');
        return this.http.get<ShopItem[]>(`${this.baseUrl}/ShopItem`, { headers });
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
    return this.http.get<ShopItem>(`${this.baseUrl}/ShopItem/${id}`, { headers })
  }

  Add(ShopItem: ShopItem,DomainName:string) {
    if (DomainName != null) {
      this.header = DomainName;
    }
  
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`);
   
    const formData = new FormData();
    formData.append('enName', ShopItem.enName.toString());
    formData.append('arName', ShopItem.arName.toString())
    formData.append('enDescription', ShopItem.enDescription.toString() );
    formData.append('arDescription', ShopItem.arDescription.toString());
    if (ShopItem.purchasePrice !== null) {
      formData.append('purchasePrice', ShopItem.purchasePrice.toString());
    } 
    if (ShopItem.salesPrice !== null) {
      formData.append('salesPrice', ShopItem.salesPrice.toString());
    }
    formData.append('genderId', ShopItem.genderID.toString());
    if (ShopItem.vatForForeign !== null) {
      formData.append('vatForForeign', ShopItem.vatForForeign.toString());
    }
    if (ShopItem.limit !== null) {
      formData.append('limit', ShopItem.limit.toString());
    }
    formData.append('availableInShop', ShopItem.availableInShop ? 'true' : 'false');
    formData.append('inventorySubCategoriesID', ShopItem.inventorySubCategoriesID.toString()); 
    formData.append('schoolID', ShopItem.schoolID.toString()); 
    formData.append('gradeID', ShopItem.gradeID.toString()); 
    formData.append('shopItemColors', JSON.stringify(ShopItem.shopItemColors) ?? '');
    formData.append('shopItemSizes', JSON.stringify(ShopItem.shopItemSizes) ?? '');
    if (ShopItem.mainImageFile) {
      formData.append('mainImageFile', ShopItem.mainImageFile, ShopItem.mainImageFile.name);
    } else if (ShopItem.mainImage) {
      formData.append('mainImageFile', ShopItem.mainImage?.toString());
    } 
  
    if (ShopItem.otherImageFile) {
      formData.append('otherImageFile', ShopItem.otherImageFile, ShopItem.otherImageFile.name);
    } else if (ShopItem.otherImage) {
      formData.append('otherImageFile', ShopItem.otherImage?.toString());
    } 

    return this.http.post(`${this.baseUrl}/ShopItem`, formData, { headers });
  }
  
  Edit(ShopItem: ShopItem,DomainName:string) {
    if (DomainName != null) {
      this.header = DomainName;
    }
  
    const token = localStorage.getItem("current_token");
    const headers = new HttpHeaders()
      .set('domain-name', this.header)
      .set('Authorization', `Bearer ${token}`);
   
    const formData = new FormData();
    formData.append('id', ShopItem.id.toString() ?? '');
    formData.append('enName', ShopItem.enName.toString());
    formData.append('arName', ShopItem.arName.toString())
    formData.append('enDescription', ShopItem.enDescription.toString() );
    formData.append('arDescription', ShopItem.arDescription.toString());
    if (ShopItem.purchasePrice !== null) {
      formData.append('purchasePrice', ShopItem.purchasePrice.toString());
    } 
    if (ShopItem.salesPrice !== null) {
      formData.append('salesPrice', ShopItem.salesPrice.toString());
    }
    formData.append('genderId', ShopItem.genderID.toString());
    if (ShopItem.vatForForeign !== null) {
      formData.append('vatForForeign', ShopItem.vatForForeign.toString());
    }
    if (ShopItem.limit !== null) {
      formData.append('limit', ShopItem.limit.toString());
    }
    formData.append('availableInShop', ShopItem.availableInShop ? 'true' : 'false');
    formData.append('inventorySubCategoriesID', ShopItem.inventorySubCategoriesID.toString()); 
    formData.append('schoolID', ShopItem.schoolID.toString()); 
    formData.append('gradeID', ShopItem.gradeID.toString()); 

    const colors = ShopItem.shopItemColors ?? [];
    colors.forEach(color => {
        formData.append('shopItemColors', color);
    });

    const sizes = ShopItem.shopItemSizes ?? [];
    sizes.forEach(size => {
        formData.append('shopItemSizes', size);
    });

    if (ShopItem.mainImageFile) {
      formData.append('mainImageFile', ShopItem.mainImageFile, ShopItem.mainImageFile.name);
    } else if (ShopItem.mainImage) {
      formData.append('mainImageFile', ShopItem.mainImage?.toString());
    } 
  
    if (ShopItem.otherImageFile) {
      formData.append('otherImageFile', ShopItem.otherImageFile, ShopItem.otherImageFile.name);
    } else if (ShopItem.otherImage) {
      formData.append('otherImageFile', ShopItem.otherImage?.toString());
    } 

    return this.http.put(`${this.baseUrl}/ShopItem`, formData, { headers });
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
    return this.http.delete(`${this.baseUrl}/ShopItem/${id}`, { headers })
  }
}
