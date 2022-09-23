import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LeaveDetails_DayWise } from '../Models/leave-types';

@Injectable({
  providedIn: 'root'
})
export class ApproverService {
  allLeaves: any;

  constructor(private http: HttpClient) { }

  private readonly url = "http://localhost:33960/api/";

  GetAllLeaves(id: number): Observable<LeaveDetails_DayWise[]> {
    return this.http.get<LeaveDetails_DayWise[]>(this.url + "LeaveDetail_DayWiseController/GetAllLeavesForApproval/" + id);
  }

  UpdateLeaveStatus(message:String, ids : Number[]) : Observable<boolean>{
    const httpHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    const options = { headers: httpHeaders };
    return this.http.put<boolean>(this.url + "LeaveDetail_DayWiseController/UpdateApprovalStatus/" + message,ids, options);
  }
}
