import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from '../Models/leave-types';
import { Sector } from '../Models/sector';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  constructor(public http: HttpClient) { }
  private readonly url = "https://indijvtrat.deloitte.com/TRAT_TESTAPI/api";

  getTableDetails(): Observable<any>{
    return this.http.get<Element[]>(this.url+"/DepartmentController/GetAllDepartments");
  }
  
  updateData(obj:any): Observable<any>{
    return this.http.put<boolean>(this.url +"/DepartmentController/UpdateDepartment/",obj);
  }

  addData(obj:any): Observable<any>{
    return this.http.post<boolean>(this.url +"/DepartmentController/AddDepartment/",obj);
  }
}