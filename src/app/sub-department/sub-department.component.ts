import { Component,OnInit } from '@angular/core';
import { Sector } from '../Models/sector';
import { SectorService } from '../Services/sector.service';
import { MatTableDataSource } from '@angular/material/table';
import { DialogService } from '../Services/dialog.service';
import { NotificationService } from '../Services/notification.service';
import { SubSectorService } from '../Services/sub-sector.service';
import { SubDepartmentService } from '../Services/sub-department.service';
import { DepartmentService } from '../Services/department.service';
declare var jQuery: any;

@Component({
  selector: 'app-sub-department',
  templateUrl: './sub-department.component.html',
  styleUrls: ['./sub-department.component.css']
})
export class SubDepartmentComponent implements OnInit {
  // searchKey: string;
  // dataSource = new MatTableDataSource<Sector>();
  // isAllSelected: any;
  // selection: any;
  // displayedColumns = ['SectorName','Edit','Disable'];
   sectorDetails: any=[];
  // sector: Sector;
  sectorName: string;
  sectorId:number;
  addValue: string;
  serviceName: SubDepartmentService;
  name: string;
 _self: this;
  data: any;
  parentId: any;



  constructor(private service:SubDepartmentService, 
    private dialogService: DialogService, private notifyService:NotificationService,private departmentService:DepartmentService) {}
  ngOnInit(): void {
   // this.GetCurrentUser();
   this.serviceName = this.service;
   this.addValue ="Add New Sub-Department";
   this.name="Sub-Department Name:";
   this._self = this;
    this.getDepartmentDetails();
  }
  getDepartmentDetails(){
    this.departmentService.getTableDetails().subscribe((data)=>{
      data.forEach((sector: { id:any; name: any; })=>{
        this.sectorDetails.push({id:sector.id, name:sector.name});
      });
    });
  }
  // GetCurrentUser() {
  //   this.service.GetCurrentUser().subscribe( (data) =>{
  //     this.service.getCurrentUserDetails = data;
  //   });
  // }
  onSectorSelect(selectedid:any){
    this.parentId =selectedid;
    this.service.getTableDetails(selectedid).subscribe((data)=>{
       this.data = data;
      });
  }

  // editSubSector(action:any,obj:any){
  //   if(action=="Update"){
  //     obj.isSelected = false;
  //     obj.lastUpdateBy = this.service.getCurrentUserDetails.id;
  //     obj.lastUpdateDate = new Date();

  //     this.service.updateSubDepartment(obj).subscribe(data=>{
  //      // console.log(data);
  //       this.notifyService.showSuccess("Updated successfully","UpdateSector");
  //     });
  //   }
  //   else if(action == "Cancel"){
  //     obj.isSelected = false;
  //   }
  //   else if(action == "Edit"){
  //     obj.isSelected = true;
  //   }
  // }

  // deleteSubSector(obj:any){
  //   obj.deleted = true;
  //   obj.lastUpdateBy = this.service.getCurrentUserDetails.id;
  //   obj.lastUpdateDate = new Date();
  //   this.dialogService.openConfirmDialog("Are you sure to delete this sector?")
  //   .afterClosed().subscribe(res =>{
  //     if(res){
  //         this.service.updateSubDepartment(obj).subscribe(data=>{
  //           this.dataSource.data.splice(this.dataSource.data.indexOf(obj.id),1);
  //           this.dataSource.data = [...this.dataSource.data];
  //           this.notifyService.showSuccess("Deleted successfully","DeleteSector");
  //         });  
  //     }
  //   });
  // }

  // addNewSubSector(sectorName:any){
  //   this.sector = {};
  //   var data1 :any;
  //   this.sector.name =sectorName;
  //   this.sector.isSelected =false;
  //   this.sector.deleted = false;
  //   this.sector.departmentId = this.sectorId;
  //   this.service.AddSubDepartment(this.sector).subscribe(data=>{
  //     this.dataSource.data = [...this.dataSource.data,data];
  //     this.notifyService.showSuccess("Added successfully","AddSector");
  //   });
  //   this.sectorName ="";
  // }

  // onSearchClear() {
  //   this.searchKey = "";
  //   this.dataSource.filter = "";

  //   if (this.dataSource.paginator) {
  //     this.dataSource.paginator.firstPage();
  //   }
  // }
  // applyFilter(event: Event) {
  //   const filterValue = (event.target as HTMLInputElement).value;
  //   this.dataSource.filter = filterValue.trim().toLowerCase();

  //   if (this.dataSource.paginator) {
  //     this.dataSource.paginator.firstPage();
  //   }
  // }
  // toggleAllRows() {
  //   if (this.isAllSelected()) {
  //     this.selection.clear();
  //     return;
  //   }

  //   this.selection.select(...this.dataSource.data);
  // }

  
  // performAction(action:any,obj:any,event:any){
  //   var actionId = event.currentTarget.id;
  //   if(action=="Update"){
  //     if( actionId.split("_")[1]==obj.id){
  //         jQuery("#update_"+obj.id).hide();
  //         jQuery("#cancel_"+obj.id).hide();
  //         jQuery("#inputtext_"+obj.id).hide();
  //         jQuery("#label_"+obj.id).show();
  //         jQuery("#edit_"+obj.id).show();
  //         obj.name = jQuery("#inputtext_"+obj.id)[0].value;
  //     }
  //   }
  //   else if(action == "Cancel"){
  //     if( actionId.split("_")[1]==obj.id){
  //         jQuery("#update_"+obj.id).hide();
  //         jQuery("#cancel_"+obj.id).hide();
  //         jQuery("#inputtext_"+obj.id).hide();
  //         jQuery("#label_"+obj.id).show();
  //         jQuery("#edit_"+obj.id).show();
  //     }

  //   }
  //   else if(action == "Edit"){
      
  //     if( actionId.split("_")[1]==obj.id){
  //         jQuery("#update_"+obj.id).show();
  //         jQuery("#cancel_"+obj.id).show();
  //         jQuery("#inputtext_"+obj.id).show();
  //         jQuery("#label_"+obj.id).hide();
  //         jQuery("#edit_"+obj.id).hide();
  //     }
  //   }
  //   else if(action == "Disable"){
  //     // this.dialogService.openConfirmDialog("Are you sure to delete this sector?")
  //     // .afterClosed().subscribe(res =>{
  //     //   if(res){
  //     //       this.service.disableSector(obj);
  //     //       this.notifyService.showSuccess("Deleted successfully","DeleteSector");
  //     //   }
  //     // });
  //   }

  // }
}
