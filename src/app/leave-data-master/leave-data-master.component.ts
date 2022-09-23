import { Component, OnInit } from '@angular/core';
import { LeaveDataMasterService } from '../Services/leave-data-master.service';

@Component({
  selector: 'app-leave-data-master',
  templateUrl: './leave-data-master.component.html',
  styleUrls: ['./leave-data-master.component.css']
})
export class LeaveDataMasterComponent implements OnInit {
  serviceName: any;
  addValue: string;
  name: string;
   _self: this;
  data: any;

  constructor(private service:LeaveDataMasterService  ) {}

  ngOnInit(): void {
    this.serviceName = this.service;
    this.addValue ="Add New Leave";
    this.name="Leave Name:";
    this._self = this;
    this.getLeaveDetails();
  }
  getLeaveDetails(){
    this.service.getTableDetails().subscribe((data)=>{
      this.data =data;
      });
  }

}
