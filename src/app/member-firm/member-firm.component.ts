import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Sector } from '../Models/sector';
import { DialogService } from '../Services/dialog.service';
import { MemberFirmService } from '../Services/member-firm.service';
import { NotificationService } from '../Services/notification.service';

@Component({
  selector: 'app-member-firm',
  templateUrl: './member-firm.component.html',
  styleUrls: ['./member-firm.component.css']
})
export class MemberFirmComponent implements OnInit {

  // searchKey: string;
  // dataSource = new MatTableDataSource<Sector>();
  // AddSector:any;
  // isAllSelected: any;
  // isEditClicked:any;
  // isDisableClicked:any;
  // selection: any;
  // displayedColumns = ['SectorName','Edit','Disable'];
  // sectorName: any="";
  // sector:Sector ;
  // sectorDetails:Sector[];
  sectorName: string;
  sectorId:number;
  addValue: string;
  serviceName: MemberFirmService;
  name: string;
 _self: this;
  data: any;
  constructor(private service:MemberFirmService) {}

              
  ngOnInit(): void {
    this.serviceName = this.service;
    this.addValue ="Add New Country";
    this.name="Country Name:";
    this._self = this;
//    this.GetCurrentUser();
    this.getCountryDetails();
  //  this.isEditClicked =false;
  }
  // GetCurrentUser() {
  //   this.service.GetCurrentUser().subscribe( (data) =>{
  //     this.service.getCurrentUserDetails = data;
  //   });
  // }

  getCountryDetails(){
    this.service.getTableDetails().subscribe((data)=>{
    this.data =data;
    });
  }

  // editSector(action:any,obj:any){
  //   if(action=="Update"){
  //     obj.isSelected = false;
  //     obj.lastUpdateBy = this.service.getCurrentUserDetails.id;
  //     obj.lastUpdateDate = new Date();

  //     this.service.updateCountry(obj).subscribe(data=>{
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

  // deleteSector(obj:any){
  //   obj.deleted = true;
  //   obj.lastUpdateBy = this.service.getCurrentUserDetails.id;
  //   obj.lastUpdateDate = new Date();
  //   this.dialogService.openConfirmDialog("Are you sure to delete this sector?")
  //   .afterClosed().subscribe(res =>{
  //     if(res){
  //         this.service.updateCountry(obj).subscribe(data=>{
  //           this.dataSource.data.splice(this.dataSource.data.indexOf(obj.id),1);
  //           this.dataSource.data = [...this.dataSource.data];
  //           this.notifyService.showSuccess("Deleted successfully","DeleteSector");
  //         });  
  //     }
  //   });
  // }

  // addNewSector(sectorName:any){
  //   this.sector = {};
  //   var data1 :any;
  //   this.sector.name =sectorName;
  //   this.sector.isSelected =false;
  //   this.sector.deleted = false;
  //   this.service.AddCountry(this.sector).subscribe(data=>{
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
