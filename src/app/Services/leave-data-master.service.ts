import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Element } from '../Models/element';

@Injectable({
  providedIn: 'root'
})
export class LeaveDataMasterService {
  constructor(public http: HttpClient) { }
  private readonly url = "https://indijvtrat.deloitte.com/TRAT_TESTAPI/api";
  
  getTableDetails(): Observable<any>{
    return this.http.get<Element[]>(this.url +"/LeaveTypesController/GetAllLeaveTypes");
  }
  
  updateData(obj:any): Observable<any>{
    return this.http.put<boolean>(this.url +"/LeaveTypesController/AddLeaveType/",obj);
  }

  addData(obj:any): Observable<any>{
    return this.http.post<boolean>(this.url +"/LeaveTypesController/UpdateLeaveType/",obj);
  }
}