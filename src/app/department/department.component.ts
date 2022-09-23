import { Component,OnInit } from '@angular/core';
import { Sector } from '../Models/sector';
import { SectorService } from '../Services/sector.service';
import { MatTableDataSource } from '@angular/material/table';
import { DialogService } from '../Services/dialog.service';
import { NotificationService } from '../Services/notification.service';
import { DepartmentService } from '../Services/department.service';
declare var jQuery: any;

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {
  // dataSource = new MatTableDataSource<Sector>();
  // selection: any;
  // displayedColumns = ['SectorName','Edit','Disable'];
  // sectorName: any="";
  // sector:Sector ;
  // sectorDetails:Sector[];
  serviceName: DepartmentService;
  addValue: string;
  name:string;
  _self: this;
  data:any =[];
  constructor(private service:DepartmentService, 
              private dialogService: DialogService, private notifyService:NotificationService) {}

              
  ngOnInit(): void {
    this.serviceName = this.service;
    this.addValue ="Add New Department";
    this.name="Department Name:";
    this._self = this;
    this.getDepartmentDetails();
  }
  // GetCurrentUser() {
  //   this.service.GetCurrentUser().subscribe( (data) =>{
  //     this.service.getCurrentUserDetails = data;
  //   });
  // }

  getDepartmentDetails(){
    this.service.getTableDetails().subscribe((data)=>{
      this.data =data;
      });
  }

  // editDepartment(action:any,obj:any){
  //   if(action=="Update"){
  //     obj.isSelected = false;
  //     obj.lastUpdateBy = this.service.getCurrentUserDetails.id;
  //     obj.lastUpdateDate = new Date();

  //     this.service.updateDepartment(obj).subscribe(data=>{
  //      // console.log(data);
  //       this.notifyService.showSuccess("Updated successfully","UpdateDepartment");
  //     });
  //   }
  //   else if(action == "Cancel"){
  //     obj.isSelected = false;
  //   }
  //   else if(action == "Edit"){
  //     obj.isSelected = true;
  //   }
  // }

  // deleteDepartment(obj:any){
  //   obj.deleted = true;
  //   obj.lastUpdateBy = this.service.getCurrentUserDetails.id;
  //   obj.lastUpdateDate = new Date();
  //   this.dialogService.openConfirmDialog("Are you sure to delete this sector?")
  //   .afterClosed().subscribe(res =>{
  //     if(res){
  //         this.service.updateDepartment(obj).subscribe(data=>{
  //           this.dataSource.data.splice(this.dataSource.data.indexOf(obj.id),1);
  //           this.dataSource.data = [...this.dataSource.data];
  //           this.notifyService.showSuccess("Deleted successfully","DeleteDepartment");
  //         });  
  //     }
  //   });
  // }

  // addNewDepartment(sectorName:any){
  //   this.sector = {};
  //   var data1 :any;
  //   this.sector.name =sectorName;
  //   this.sector.isSelected =false;
  //   this.sector.deleted = false;
  //   this.service.AddDepartment(this.sector).subscribe(data=>{
  //     this.dataSource.data = [...this.dataSource.data,data];
  //     this.notifyService.showSuccess("Added successfully","AddDepartment");
  //   });
  //   this.sectorName ="";
  // }
}
