import { Component, OnInit, ViewChild } from '@angular/core';
import {MatSort} from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Employee, LeaveDetails} from 'src/app/Models/leave-types';
import { LeaveService } from 'src/app/Services/leave.service';
import {MatPaginator} from '@angular/material/paginator';
import { NotificationService } from 'src/app/Services/notification.service';
import { DialogService } from 'src/app/Services/dialog.service';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { LeaveFormComponent } from '../leave-form/leave-form.component';

@Component({
  selector: 'app-leave-history',
  templateUrl: './leave-history.component.html',
  styleUrls: ['./leave-history.component.css']
})

export class LeaveHistoryComponent implements OnInit {
  mainDataSource = new MatTableDataSource<LeaveDetails>();
  displayedColumns = ['name','fromDate', 'toDate', 'leaveCount','dayType' ,'totalAbsenceHour', 'approvalStatus','reason', 'actions'];
  dataSource:any;
  duplicatedataSource:any;
  isChangeable:boolean=false;
  searchKey: string;

  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private service:LeaveService,
              private notifyService:NotificationService,
              private dialogService: DialogService,
              private dialog: MatDialog,) {}

  ngOnInit(): void {
    this.GetCurrentUser();
  }  

  GetCurrentUser()
  {
    this.service.GetCurrentUser().subscribe ( (data) => {
      this.service.getCurrentUserDetails = data;
      if(this.service.getCurrentUserDetails)
      {
        this.loadHistory();
      }
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.mainDataSource.filter = filterValue.trim().toLowerCase();

    if (this.mainDataSource.paginator) {
      this.mainDataSource.paginator.firstPage();
    }
  }

  onSearchClear() {
    this.searchKey = "";
    this.mainDataSource.filter = "";

    if (this.mainDataSource.paginator) {
      this.mainDataSource.paginator.firstPage();
    }
  }

  ngAfterViewInit(): void {
    this.mainDataSource.sort = this.sort;
    this.mainDataSource.paginator = this.paginator;
  }

  loadHistory()
  {
   this.service.GetLeaveHistoryforCurrentUser(this.service.getCurrentUserDetails.id).subscribe ( (data) => {
    this.duplicatedataSource = data;

    this.dataSource = this.duplicatedataSource.map((object: any) => {
      return {...object, isChangeable: true};
    });

    this.dataSource.forEach((element: any) => {
      var date1 = new Date(element.fromDate);
      var date2 = new Date();
      if(date1 >= date2)
    {
      element.isChangeable = true;
    }
    else{
      element.isChangeable = false;
    }}); 
    this.mainDataSource.data =   this.dataSource;
  });
  }
  

  onCreate() {
    this.service.initializeFormGroup();
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    dialogConfig.width = "60%";
    const dialogRef = this.dialog.open(LeaveFormComponent,dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        this.loadHistory();
    });  
  }

 

  UpdateLeaveDetails(row: any)
  {
    this.service.populateForm(row);
    const dialogConfig = new MatDialogConfig();
  dialogConfig.disableClose = true;
  dialogConfig.autoFocus = true;
  dialogConfig.width = "60%";
  const dialogRef = this.dialog.open(LeaveFormComponent,dialogConfig);

  dialogRef.afterClosed().subscribe(
    data => {
      this.loadHistory();
  });  
  }

DeleteLeaveDetails(id:number)
  {
    this.dialogService.openConfirmDialog("Are you sure to delete the leave request")
    .afterClosed().subscribe(res =>{
      if(res){
        this.service.DisableLeave(id,this.service.getCurrentUserDetails.id).subscribe( 
          data => {
            this.service.DisableFileUpload(id,this.service.getCurrentUserDetails.id).subscribe ( (data) => {
              if(data)
              {
                this.notifyService.showWarning("Leave file has been deleted successfully","Leave Deleted");
              }
              
            });
          this.notifyService.showWarning("Leave Request has been deleted successfully","Leave Deleted");
          this.loadHistory(); 
          });
      }
    });
  }

}
