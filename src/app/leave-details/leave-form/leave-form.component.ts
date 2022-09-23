import { Component, Input, OnInit } from '@angular/core';
import { LeaveService } from 'src/app/Services/leave.service';
import {FormGroup,FormBuilder,Validators } from '@angular/forms';
import * as moment from 'moment';
import { LeaveDetails } from 'src/app/Models/leave-types';
import { Observable } from 'rxjs';
import { HttpEventType, HttpResponse } from '@angular/common/http';
import { NotificationService } from 'src/app/Services/notification.service';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'apgp-leave-form',
  templateUrl: './leave-form.component.html',
  styleUrls: ['./leave-form.component.css']
})
export class LeaveFormComponent implements OnInit {
  ManagerName:string;
  TotalAbsenceHour = 0;
  fromDate: any;
  toDate: Date;
  dayType: any;
  myHolidayDates: Date[] = [];
  minDate = new Date();
  //File Upload
  selectedFiles?: FileList;
  currentFile?: File;
  progress = 0;
  message = '';
  leaveId:number=0;
  formSubmitted : boolean = false;

  constructor(private formBuilder: FormBuilder, 
    public service: LeaveService, 
    private notifyService: NotificationService,
    public dialogRef: MatDialogRef<LeaveFormComponent>) { }

  ngOnInit(): void {
    this.GetHolidaysList();
    this.LoadManagerDetails();
  }

  GetHolidaysList()
  {
    this.service.GetHolidayList().subscribe(holidays => {
      for (let item of holidays) {
        this.myHolidayDates.push(new Date(item));
      }
    });
  }
  //Function to filter out the public holidays and weekends
  myHolidayFilter = (d: Date | null): boolean => {
    const time = d?.getTime();
    const day = d?.getDay();
    
    return !this.myHolidayDates.find(x => x.getTime() == time) && day !== 0 && day !== 6;;
  }

  LoadManagerDetails()
  {
    this.service.GetManagerDetails(this.service.getCurrentUserDetails.id).subscribe ( (data) =>{
      this.service.getManagerDetails = data;
      this.ManagerName = data.name;
    })
  }

  get registerFormControl() {
    return this.service.LeaveDetailForm.controls;
  }

  public myError = (controlName: string, errorName: string) =>{
    return this.registerFormControl[controlName].hasError(errorName);
    }

  

  calcBusinessDays = (startDate: Date, endDate: Date) => {
    const day = moment(startDate);
    let businessDays = 0;
    while (day.isSameOrBefore(endDate, 'day')) {
      if (day.day() !== 0 && day.day() !== 6) {
        businessDays++;
      }
      day.add(1, 'd');
    }
    return businessDays;
  }
  
  GetAbsenceHour() {
    this.fromDate = this.service.LeaveDetailForm.get('fromDate')?.value;
    this.toDate = this.service.LeaveDetailForm.get('toDate')?.value;
    this.dayType = this.service.LeaveDetailForm.get('dayType')?.value;
    let countHoliday = 0;
    for (let item of this.myHolidayDates) {
      const date = new Date(item);
      const start = new Date(this.fromDate);
      const end = new Date(this.toDate);
      if (date > start && date < end) {
        countHoliday++;
      }
    }
    const numberofWeekends = this.calcBusinessDays(this.fromDate, this.toDate);
    const diffDays = numberofWeekends - countHoliday;
    this.service.leaveDiff = diffDays;
    if (diffDays == 0) {
      if (this.dayType == 1 || this.dayType == 2) {
        this.TotalAbsenceHour = 4;
      }
      else if (this.dayType == 3) {
        this.TotalAbsenceHour = 8;
      }
      else {
        this.TotalAbsenceHour = 0;
      }
    }
    else {
      if (this.dayType == 1 || this.dayType == 2) {
        this.TotalAbsenceHour = (diffDays * 4);
      }
      else if (this.dayType == 3) {
        this.TotalAbsenceHour = (diffDays * 8);
      }
      else {
        this.TotalAbsenceHour = 0;
      }
    }
  }

  onSubmit() {
    if((this.service.leaveCredit-this.service.leaveUsed) >= this.service.leaveDiff)
    {
      this.PostLeave(this.service.LeaveDetailForm.value);
    }
    else{
      this.notifyService.showError("OOPS! You dont have enough leaves","Leave Request");
    }
    
    }

  onClear()
    {
      this.notifyService.showInfo("Form Reset","Form Reset")
      this.service.LeaveDetailForm.reset({
        id:0,
        leaveTypeId: null,
        fromDate: null,
        toDate: null,
        dayType: "3",
        totalAbsenceHour:0,
        approverId: this.service.getManagerDetails.name,
        reason: '',
        fileUpload:null
       });
      this.selectedFiles = undefined;
    }

  ConvertDate(date: Date) {
    let query = date.toLocaleDateString();
    let momentVariable = moment(query, 'MM-DD-YYYY');
    let stringvalue = momentVariable.format('YYYY-MM-DD');
    return date;
  }

  PostLeave(objLeaveDetails: LeaveDetails) {
    objLeaveDetails.leaveTypeId = this.service.LeaveDetailForm.controls['leaveTypeId'].value;
    objLeaveDetails.fromDate = this.ConvertDate(this.service.LeaveDetailForm.controls['fromDate'].value);
    objLeaveDetails.toDate = this.ConvertDate(this.service.LeaveDetailForm.controls['toDate'].value);

    if (this.service.LeaveDetailForm.controls['dayType'].value == 1) {
      objLeaveDetails.dayType = "FH";
      objLeaveDetails.leaveCount = this.service.leaveDiff/2;
    }
    else if (this.service.LeaveDetailForm.controls['dayType'].value == 2) {
      objLeaveDetails.dayType = "SH";
      objLeaveDetails.leaveCount = this.service.leaveDiff/2;
    }
    else {
      objLeaveDetails.dayType = "FD";
      objLeaveDetails.leaveCount = this.service.leaveDiff;
    }
    objLeaveDetails.totalAbsenceHour = this.TotalAbsenceHour;
    objLeaveDetails.approverId = this.service.getManagerDetails.id;
    objLeaveDetails.reason = this.service.LeaveDetailForm.controls['reason'].value;
    objLeaveDetails.approvalStatus = "Pending";
    objLeaveDetails.empId = this.service.getCurrentUserDetails.id;
    objLeaveDetails.id = this.service.LeaveDetailForm.controls['id'].value;
    objLeaveDetails.isActive = true;


    //Create New Leave Request
    if(objLeaveDetails.id == 0)
    {
      objLeaveDetails.createdBy = this.service.getCurrentUserDetails.id;
      objLeaveDetails.createdOn = new Date();
      this.service.PostLeaveDetail(objLeaveDetails).subscribe((response) => {
        if(this.selectedFiles != null)
      {
        this.leaveId = response.id;
         this.upload();
      }
        this.notifyService.showSuccess("Leave request raised Successfully", "Leave Request"); 
        this.onClear();
        this.onClose();
      });
    }

    //Update exsisting leave request 
    else{
      objLeaveDetails.updatedBy = this.service.getCurrentUserDetails.id;
      objLeaveDetails.updatedOn = new Date();
      this.service.PutLeaveDetail(objLeaveDetails).subscribe((response) => {
        if(this.selectedFiles != null)
      {
        this.leaveId = response.id;
         this.upload();
      }
        this.notifyService.showSuccess("Leave request details updated Successfully", "Leave Request"); 
        this.onClear();
        this.onClose();
      });
    }
    
  }

  upload(): void {
    this.progress = 0;
    if (this.selectedFiles) {
      const file: File | null = this.selectedFiles.item(0);
      if (file) {
        this.currentFile = file;
        this.service.upload(this.currentFile,this.leaveId).subscribe(
          (event: any) => {
            if (event.type === HttpEventType.UploadProgress) {
              this.progress = Math.round(100 * event.loaded / event.total);
            } else if (event instanceof HttpResponse) {
              this.message = event.body.message;
            }
          });
      this.selectedFiles = undefined;
    }
  }

}

  onClose() {
    this.service.LeaveDetailForm.reset();
    this.dialogRef.close();
  }

  selectFile(event: any): void {
    this.selectedFiles = event.target.files;
  }

}
