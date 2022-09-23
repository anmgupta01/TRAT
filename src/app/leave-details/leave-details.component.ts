import { Component,OnInit } from '@angular/core';
import { LeaveService } from '../Services/leave.service';

@Component({
  selector: 'app-leave-details',
  templateUrl: './leave-details.component.html',
  styleUrls: ['./leave-details.component.css']
})
export class LeaveDetailsComponent implements OnInit {

  constructor(private service:LeaveService) {}

  ngOnInit(): void {
    this.service.GetAllLeaveTypes().subscribe((data) => {
      this.service.allLeaveTypes = data;
    }); 
  }
}
