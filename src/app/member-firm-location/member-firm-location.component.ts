import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Sector } from '../Models/sector';
import { DialogService } from '../Services/dialog.service';
import { MemberFirmLocationService } from '../Services/member-firm-location.service';
import { MemberFirmService } from '../Services/member-firm.service';
import { NotificationService } from '../Services/notification.service';

@Component({
  selector: 'app-member-firm-location',
  templateUrl: './member-firm-location.component.html',
  styleUrls: ['./member-firm-location.component.css']
})
export class MemberFirmLocationComponent implements OnInit {

  // searchKey: string;
  // dataSource = new MatTableDataSource<Sector>();
  // isAllSelected: any;
  // selection: any;
  // displayedColumns = ['SectorName','Edit','Disable'];
  sectorDetails: any=[];
  sector: Sector;
  sectorName: string;
  sectorId:number;
  data: any;
  // sectorName: string;
  // sectorId:number;
  addValue: string;
  serviceName: MemberFirmLocationService;
  name: string;
 _self: this;
  parentId: any;
  //data: any;
  constructor(private service:MemberFirmLocationService, 
    private dialogService: DialogService, private notifyService:NotificationService,private memberFirmService:MemberFirmService) {}
  ngOnInit(): void {
    this.serviceName = this.service;
    this.addValue ="Add New Member Firm Location";
    this.name="Member Firm Location Name:";
    this._self = this;
  //  this.GetCurrentUser();
    this.getCountryDetails();
  }
  getCountryDetails(){
    this.memberFirmService.getTableDetails().subscribe((data)=>{
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
  onCountrySelect(selectedid:any){
    this.parentId = selectedid;
    this.service.getTableDetails(selectedid).subscribe((data)=>{
      this.data=data;
      this.data.forEach((row:any)=>row.name =row.office);
      //  this.dataSource.data = data;
      //  this.dataSource.data.forEach(row=>row.isSelected =false);
      
       //var i=data;
      });
  }

  // editSubSector(action:any,obj:any){
  //   if(action=="Update"){
  //     obj.isSelected = false;
  //     obj.lastUpdateBy = this.service.getCurrentUserDetails.id;
  //     obj.lastUpdateDate = new Date();

  //     this.service.updateMemberFirmLocation(obj).subscribe(data=>{
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
  //   obj.isDeleted = true;
  //   obj.lastUpdateBy = this.service.getCurrentUserDetails.id;
  //   obj.lastUpdateDate = new Date();
  //   this.dialogService.openConfirmDialog("Are you sure to delete this member firm location?")
  //   .afterClosed().subscribe(res =>{
  //     if(res){
  //         this.service.updateMemberFirmLocation(obj).subscribe(data=>{
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
  //   this.sector.office =sectorName;
  //   this.sector.isSelected =false;
  //   this.sector.deleted = false;
  //   this.sector.countryId = this.sectorId;
  //   this.service.AddMemberFirmLocation(this.sector).subscribe(data=>{
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

}

