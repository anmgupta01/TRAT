import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from '../Models/leave-types';
import { Sector } from '../Models/sector';

@Injectable({
  providedIn: 'root'
})
export class MemberFirmService {
  constructor(public http: HttpClient) { }
  private readonly url = "https://indijvtrat.deloitte.com/TRAT_TESTAPI/api";

  getTableDetails(): Observable<any>{
    return this.http.get<Element[]>(this.url+"/MemberFirmCountryController/GetAllCountries");
  }
  
  updateData(obj:any): Observable<any>{
    obj.office=obj.name;
    obj.isDeleted = obj.deleted;
    return this.http.put<Element>(this.url +"/MemberFirmCountryController/UpdateCountry/",obj);
  }

  addData(obj:any): Observable<any>{
    obj.office = obj.name;
    obj.isDeleted =false;
    return this.http.post<Element>(this.url +"/MemberFirmCountryController/AddCountry/",obj);
  }
}