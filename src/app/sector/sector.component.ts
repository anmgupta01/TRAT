import { Component,OnInit } from '@angular/core';
import { Sector } from '../Models/sector';
import { SectorService } from '../Services/sector.service';
import { MatTableDataSource } from '@angular/material/table';
import { DialogService } from '../Services/dialog.service';
import { NotificationService } from '../Services/notification.service';
declare var jQuery: any;

@Component({
  selector: 'app-sector',
  templateUrl: './sector.component.html',
  styleUrls: ['./sector.component.css']
})
export class SectorComponent implements OnInit {
 // dataSource = new MatTableDataSource<Sector>();
  //displayedColumns = ['SectorName','Edit','Disable'];
  // sectorName: any="";
  // sector:Sector ;
  serviceName: SectorService;
  addValue: string;
  name:string;
  _self: this;
  data:any =[];
  constructor(private service:SectorService){}
              // private dialogService: DialogService, private notifyService:NotificationService) {}

              
  ngOnInit(): void {
   
    this.serviceName = this.service;
    this.addValue ="Add New Sector";
    this.name="Sector Name:";
    this._self = this;
  //  this.GetCurrentUser();
    this.getSectorDetails();
  }
  // GetCurrentUser() {
  //   this.service.GetCurrentUser().subscribe( (data) =>{
  //     this.service.getCurrentUserDetails = data;
  //   });
  // }

  getSectorDetails(){
    this.service.getTableDetails().subscribe((data)=>{
    this.data =data;
    });
  }

  // editSector(action:any,obj:any){
  //   if(action=="Update"){
  //     obj.isSelected = false;
  //     obj.lastUpdateBy = this.service.getCurrentUserDetails.id;
  //     obj.lastUpdateDate = new Date();

  //     this.service.updateSector(obj).subscribe(data=>{
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
  //         this.service.updateSector(obj).subscribe(data=>{
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
  //   this.service.AddData(this.sector).subscribe(data=>{
  //     this.dataSource.data = [...this.dataSource.data,data];
  //     this.notifyService.showSuccess("Added successfully","AddSector");
  //   });
  //   this.sectorName ="";
  // }

}
