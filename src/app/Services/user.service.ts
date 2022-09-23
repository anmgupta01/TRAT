import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from '../Models/leave-types';
import { Sector } from '../Models/sector';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  getCurrentUserDetails: Employee;
  constructor(public http: HttpClient) { }
  sectorDetails: Sector[];
  private readonly url = "https://indijvtrat.deloitte.com/TRAT_TESTAPI/api";

  GetCurrentUser(): Observable<Employee> {
    return this.http.get<Employee>(this.url + "/EmployeeController/GetCurrentUser");
  }
}