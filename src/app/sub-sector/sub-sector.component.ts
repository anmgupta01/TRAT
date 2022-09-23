import { Component,OnInit } from '@angular/core';
import { Sector } from '../Models/sector';
import { SectorService } from '../Services/sector.service';
import { MatTableDataSource } from '@angular/material/table';
import { DialogService } from '../Services/dialog.service';
import { NotificationService } from '../Services/notification.service';
import { SubSectorService } from '../Services/sub-sector.service';
declare var jQuery: any;

@Component({
  selector: 'app-sub-sector',
  templateUrl: './sub-sector.component.html',
  styleUrls: ['./sub-sector.component.css']
})
export class 
SubSectorComponent implements OnInit {
  searchKey: string;
  dataSource = new MatTableDataSource<Sector>();
  isAllSelected: any;
  selection: any;
  displayedColumns = ['SectorName','Edit','Disable'];
  sectorDetails: any=[];
  sector: Sector;
  sectorName: string;
  parentId:number;
  _self: this;
  serviceName: SubSectorService;
  addValue: string;
  name:string;
  data:any;

  constructor(private service:SubSectorService, 
    private dialogService: DialogService, private notifyService:NotificationService,private sectorService:SectorService) {}

    
  ngOnInit(): void {
    this.serviceName = this.service;
    this.addValue ="Add New Sub-Sector";
    this.name="Sub-Sector Name:";
    this._self = this;
   // this.GetCurrentUser();
    this.getSectorDetails();
  }
  getSectorDetails(){
    this.sectorService.getTableDetails().subscribe((data)=>{
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
      this.data=data;
      });
  }

  // editSubSector(action:any,obj:any){
  //   if(action=="Update"){
  //     obj.isSelected = false;
  //     obj.lastUpdateBy = this.service.getCurrentUserDetails.id;
  //     obj.lastUpdateDate = new Date();

  //     this.service.updateSubSector(obj).subscribe(data=>{
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
  //         this.service.updateSubSector(obj).subscribe(data=>{
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
  //   this.sector.sectorId = this.sectorId;
  //   this.service.AddData(this.sector).subscribe(data=>{
  //     this.dataSource.data = [...this.dataSource.data,data];
  //     this.notifyService.showSuccess("Added successfully","AddSector");
  //   });
  //   this.sectorName ="";
  // }

}
