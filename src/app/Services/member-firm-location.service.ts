import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Element } from '../Models/element';
import { Employee } from '../Models/leave-types';
import { Sector } from '../Models/sector';

@Injectable({
  providedIn: 'root'
})
export class MemberFirmLocationService {
  constructor(public http: HttpClient) { }
  private readonly url = "https://indijvtrat.deloitte.com/TRAT_TESTAPI/api";
  
  getTableDetails(id:any): Observable<any>{
    return this.http.get<Element[]>(this.url +"/MemberFirmLocationController/GetMemberFirmLocationbyCountryId/"+id);
  }
  
  updateData(obj:any): Observable<any>{
    obj.office=obj.name;
    obj.isDeleted = obj.deleted;
    return this.http.put<boolean>(this.url +"/MemberFirmLocationController/UpdateMemberFirmLocation/",obj);
  }

  addData(obj:any,id?:any): Observable<any>{
    if(id){
      obj.countryId=id;
    }
    obj.office=obj.name;
    obj.name ="";
    return this.http.post<boolean>(this.url +"/MemberFirmLocationController/AddMemberFirmLocation/",obj);
  }
}