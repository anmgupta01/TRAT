import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Element } from '../Models/element';

@Injectable({
  providedIn: 'root'
})
export class SubDepartmentService {

  constructor(public http: HttpClient) { }
  private readonly url = "https://indijvtrat.deloitte.com/TRAT_TESTAPI/api";
  
  getTableDetails(id:any): Observable<any>{
    return this.http.get<Element[]>(this.url +"/SubDepartmentController/GetSubDepartmentbyDepartmentId/"+id);
  }
  
  updateData(obj:any): Observable<any>{
    return this.http.put<boolean>(this.url +"/SubDepartmentController/UpdateSubDepartment/",obj);
  }

  addData(obj:any,id?:any): Observable<any>{
    if(id){
      obj.departmentId = id;
    }
    return this.http.post<boolean>(this.url +"/SubDepartmentController/AddSubDepartment/",obj);
  }
}