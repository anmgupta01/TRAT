import { HttpClient, HttpEvent, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { Employee,LeaveDetails, LeaveTypes } from '../Models/leave-types';
import { Manager } from '../Models/manager';
import { dateLessThan } from '../Models/validations';

@Injectable({
  providedIn: 'root'
})
export class LeaveService {
  getCurrentUserDetails: Employee;
  getManagerDetails: Manager;
  allLeaveTypes:LeaveTypes[];
  leaveCredit:number;
  leaveUsed:number=0;
  leaveDiff:number=0;
  fileInfos?: Observable<any>;

 
  constructor(private http: HttpClient) { }

    LeaveDetailForm:FormGroup = new FormGroup({

        id:new FormControl(0),
        leaveTypeId: new FormControl(null,Validators.required),
        fromDate: new FormControl(null,Validators.required),
        toDate: new FormControl(null,[Validators.required]),
        dayType: new FormControl("3",Validators.required),
        totalAbsenceHour: new FormControl(0),
        approverId: new FormControl("Manager's Name"),
        reason:  new FormControl(null,[Validators.required, Validators.maxLength(250)]),
        fileUpload:new FormControl(null)
    }, {validators:[dateLessThan('fromDate','toDate')]});


  initializeFormGroup() {
    this.leaveCredit=0;
    this.leaveUsed =0;;
    this.LeaveDetailForm.setValue({
      id: 0,
      leaveTypeId: null,
      fromDate: null,
      toDate: null,
      dayType: "3",
      totalAbsenceHour: 0,
      approverId: "Managers name",
      reason: null,
      fileUpload:null
    });
  }

  populateForm(leaveDetails:any) {
    this.getFiles(leaveDetails.id).subscribe( (data) =>{
      this.fileInfos = data;
    });
    const dayTypeValue = leaveDetails.dayType;
    var result;
    if(dayTypeValue == "FD")
    {
      result = "3";
    }
    else if(dayTypeValue == "FH")
    {
      result = "1";
    }
    else{
      result = "2";
    }
    this.LeaveDetailForm.setValue({
      id: leaveDetails.id,
      leaveTypeId: leaveDetails.leaveTypeId,
      fromDate: new Date(leaveDetails.fromDate),
      toDate: new Date(leaveDetails.toDate),
      dayType: result,
      totalAbsenceHour:leaveDetails.totalAbsenceHour,
      approverId: "Manager's Name",
      reason: leaveDetails.reason,
      fileUpload:null
    });
    this.onChangeLeaveType(leaveDetails.leaveTypeId);
  }

  onChangeLeaveType(id: number)
  {
    this.GetLeaveCreditToEmployee(this.getCurrentUserDetails.id,id).subscribe(
      data => {
        this.leaveCredit = data;
      }
    );
    this.GetLeaveUsedByEmployee(this.getCurrentUserDetails.id,id).subscribe(
      response => {
        this.leaveUsed = response;
      }
    );
  }


  private readonly url = "https://indijvtrat.deloitte.com/TRAT_TESTAPI/api/";
  GetCurrentUser(): Observable<Employee> {
    return this.http.get<Employee>(this.url + "EmployeeController/GetCurrentUser");
  }
  GetAllLeaveTypes(): Observable<LeaveTypes[]> {
    return this.http.get<LeaveTypes[]>(this.url + "LeaveTypesController/GetAllLeaveTypes");
  }
  GetManagerDetails(id: number): Observable<Manager> {
    return this.http.get<Manager>(this.url + "ApproverController/GetManagerInfoforCurrentUser/" + id);
  }
  GetHolidayList(): Observable<Date[]> {
    return this.http.get<Date[]>(this.url + "HolidayMasterController/GetAllHolidaysfromToday");
  }

  GetLeaveHistoryforCurrentUser(id:number): Observable<LeaveDetails[]> {
    return this.http.get<LeaveDetails[]>(this.url + "LeaveDetailController/GetLeaveHistoryofEmployeebyID/"+id);
  }

  GetLeaveCreditToEmployee(EmpId:number,LeaveId:number): Observable<number> {
    return this.http.get<number>(this.url + "LeaveCreditController/GetLeavesAvailable/"+EmpId+"/"+LeaveId);
  }

  GetLeaveUsedByEmployee(EmpId:number,LeaveId:number): Observable<number> {
    return this.http.get<number>(this.url + "LeaveDetailController/GetTotalLeavesUsed/"+EmpId+"/"+LeaveId);
  }

  DisableLeave(id:number,EmpId:number):Observable<boolean>
  {
    const httpHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    const options = { headers: httpHeaders };
    return this.http.put<boolean>(this.url + "LeaveDetailController/DisableLeave/"+id+"/"+EmpId, options)
  }

  DisableFileUpload(id:number,EmpId:number):Observable<boolean>
  {
    const httpHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    const options = { headers: httpHeaders };
    return this.http.put<boolean>(this.url + "LeaveFileController/DeleteFile/"+id+"/"+EmpId,options);
  }
  
  PostLeaveDetail(objLeaveDetails: LeaveDetails): Observable<LeaveDetails> {
    const httpHeaders = new HttpHeaders().set('Content-Type', 'application/json');
    const options = { headers: httpHeaders };
    return this.http.post<LeaveDetails>(this.url + "LeaveDetailController/PostLeaveDetails", objLeaveDetails, options)
  }

  PutLeaveDetail(objLeaveDetails: LeaveDetails): Observable<LeaveDetails> {
    
    return this.http.put<LeaveDetails>(this.url + "LeaveDetailController/UpdateLeave", objLeaveDetails)
  }

  upload(file: File,id:number): Observable<HttpEvent<any>> {
    const formData: FormData = new FormData();
    formData.append('file', file);
    const req = new HttpRequest('POST', `${this.url}LeaveFileController/files/` +id, formData, {
      reportProgress: true,
      responseType: 'json'
    });
    return this.http.request(req);
  }
  
  getFiles(id:number): Observable<any> {
    return this.http.get(this.url+"LeaveFileController/files/"+id);
  }
 
}

