import { Component,Input,OnInit, Output, SimpleChange, SimpleChanges } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { DialogService } from '../Services/dialog.service';
import { NotificationService } from '../Services/notification.service';
import { UserService } from '../Services/user.service';
import { Element } from '../Models/element';
import { BehaviorSubject } from 'rxjs';
declare var jQuery: any;

@Component({
  selector: 'app-element-list',
  templateUrl: './element-list.component.html',
  styleUrls: ['./element-list.component.css']
})
export class ElementListComponent implements OnInit {

  @Input() model:any;
  dataSource = new MatTableDataSource<Element>();
  displayedColumns = ['Name','Edit','Disable'];
  element: Element;
  name: string;
  private _data = new BehaviorSubject<Element[]>([]);


	@Input()
	set data(value: Element[]) {
		this._data.next(value);
	};

	get data() {
		return this._data.getValue();
	}

  constructor(private userService:UserService, 
              private dialogService: DialogService, private notifyService:NotificationService) {}

              
  ngOnInit(): void {
    this.GetCurrentUser();
    this._data
			.subscribe(x => {
				this.dataSource.data =(this.data);
			});
  }

  GetCurrentUser() {
    this.userService.GetCurrentUser().subscribe( (data: any) =>{
      this.userService.getCurrentUserDetails = data;
    });
  }


  editData(action:any,obj:any){
    if(action=="Update"){
      obj.isSelected = false;
      obj.lastUpdateBy = this.userService.getCurrentUserDetails.id;
      obj.lastUpdateDate = new Date();
      this.model.serviceName.updateData(obj).subscribe((data:any)=>{
        this.notifyService.showSuccess("Updated successfully","Update Data");
      });
    }
    else if(action == "Cancel"){
      obj.isSelected = false;
    }
    else if(action == "Edit"){
      obj.isSelected = true;
    }
  }

  deleteData(obj:any){
    obj.deleted = true;
    obj.lastUpdateBy = this.userService.getCurrentUserDetails.id;
    obj.lastUpdateDate = new Date();
    this.dialogService.openConfirmDialog("Are you sure to delete this data?")
    .afterClosed().subscribe(res =>{
      if(res){
          this.model.serviceName.updateData(obj).subscribe((data:any)=>{
            this.dataSource.data.splice(this.dataSource.data.indexOf(obj.id),1);
            this.dataSource.data = [...this.dataSource.data];
            this.notifyService.showSuccess("Data deleted successfully","Delete Data");
          });  
      }
    });
  }

  addNewData(name:any){
    this.element = {};
    this.element.name =name;
    this.element.isSelected =false;
    this.element.deleted = false;
    this.model.serviceName.addData(this.element,this.model.parentId).subscribe((data:any)=>{
      this.dataSource.data = [...this.dataSource.data,data];
      this.notifyService.showSuccess("Data added successfully","Add Data");
    });
    this.name ="";
  }
 
}