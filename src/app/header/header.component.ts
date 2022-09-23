import { Component, OnInit } from '@angular/core';
import { LeaveService } from '../Services/leave.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
name:string;
  constructor(private service:LeaveService) { }

  ngOnInit(): void {
    this.GetCurrentUser();
  }


  GetCurrentUser()
  {
    //calling service method to get currentUser 
    this.service.GetCurrentUser().subscribe(data => {
      this.name = data.employeeName;
    }); 
  }
}
