import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild, OnChanges} from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { LeaveDetails_DayWise } from '../Models/leave-types';
import {MatSort} from '@angular/material/sort';
import {MatPaginator} from '@angular/material/paginator';
import { ApproverService } from '../Services/approver.service';
import { LeaveService } from '../Services/leave.service';
import { NotificationService } from '../Services/notification.service';


@Component({
  selector: 'app-approver',
  templateUrl: './approver.component.html',
  styleUrls: ['./approver.component.css']
})


export class ApproverComponent implements OnInit {
  searchKey:string;
  displayedColumns = ['select','employeeName','dayType', 'leavetypeName', 'fromDate','toDate' ,'leaveDate', 'leaveDay','leaveCount', 'designation','gradeCutOff','overAllCutoff','sSLCutOff'];
  //,'sectorLeadConflict'
  dataSource = new MatTableDataSource<LeaveDetails_DayWise>();
  selection = new SelectionModel<LeaveDetails_DayWise>(true, []);

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;


  constructor(private service:ApproverService,
              private leaveService:LeaveService,
              private noftifyService:NotificationService) { }

  ngOnInit(): void {
    this.GetCurrentUser();
  }

  UpdateLeaveStatus(message:String)
  {
    var ids:Number[] =[];
    
    this.selection.selected.forEach(element => {
      ids.push(element.id)
    });
    this.service.UpdateLeaveStatus(message,ids).subscribe( ()=>{
      this.noftifyService.showInfo("Leaves has been "+message,"Approval Status");
      this.selection.clear();
      this.GetAllLeaves();
    })
    
  }
  


  GetCurrentUser()
  {
    this.leaveService.GetCurrentUser().subscribe( (data) =>{
      this.leaveService.getCurrentUserDetails = data;
      this.GetAllLeaves();
    });
  }

  GetAllLeaves()
  {
    
    this.service.GetAllLeaves(this.leaveService.getCurrentUserDetails.id).subscribe( (data) =>{
      this.dataSource.data = data;
    });
  }

  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.dataSource.data.length;
    return numSelected === numRows;
  }

  toggleAllRows() {
    if (this.isAllSelected()) {
      this.selection.clear();
      return;
    }

    this.selection.select(...this.dataSource.data);
  }

  checkboxLabel(row?: LeaveDetails_DayWise): string {
    if (!row) {
      return `${this.isAllSelected() ? 'deselect' : 'select'} all`;
    }
    return `${this.selection.isSelected(row) ? 'deselect' : 'select'} row ${row.id + 1}`;
  }

  onSearchClear() {
    this.searchKey = "";
    this.dataSource.filter = "";

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

}
