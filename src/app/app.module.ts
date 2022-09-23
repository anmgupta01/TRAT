import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatListModule } from '@angular/material/list';
import { MatRadioModule } from '@angular/material/radio';
import { MatTableModule } from '@angular/material/table';
import { MatNativeDateModule } from '@angular/material/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LeaveDetailsComponent } from './leave-details/leave-details.component';
import { LeaveFormComponent } from './leave-details/leave-form/leave-form.component';
import { HeaderComponent } from './header/header.component';
import { LeaveHistoryComponent } from './leave-details/leave-history/leave-history.component';
import { LeaveService } from './Services/leave.service';
import { MatSortModule } from '@angular/material/sort';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatPaginatorModule } from '@angular/material/paginator';
import { ToastrModule } from 'ngx-toastr';
import { NotificationService } from './Services/notification.service';
import { MatConfirmDialogComponent } from './mat-confirm-dialog/mat-confirm-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { DialogService } from './Services/dialog.service';
import {MatGridListModule} from '@angular/material/grid-list';
import { ApproverComponent } from './approver/approver.component';
import { ApproverService } from './Services/approver.service';
import { AdminComponent } from './admin/admin.component';
import { SectorComponent } from './sector/sector.component';
import { SubSectorComponent } from './sub-sector/sub-sector.component';
import { DepartmentComponent } from './department/department.component';
import { WindowsAuthInterceptor } from './interceptor/windowsAuth';
import { ErrorInterceptor } from './interceptor/error';
import { DepartmentService } from './Services/department.service';
import { SubSectorService } from './Services/sub-sector.service';
import { MemberFirmComponent } from './member-firm/member-firm.component';
import { MemberFirmLocationComponent } from './member-firm-location/member-firm-location.component';
import { SubDepartmentService } from './Services/sub-department.service';
import { SubDepartmentComponent } from './sub-department/sub-department.component';
import { MemberFirmService } from './Services/member-firm.service';
import { MemberFirmLocationService } from './Services/member-firm-location.service';
import { ElementListComponent } from './element-list/element-list.component';
import { UserService } from './Services/user.service';
import { LeaveDataMasterComponent } from './leave-data-master/leave-data-master.component';

 


@NgModule({
  declarations: [
    AppComponent,
    LeaveDetailsComponent,
    LeaveFormComponent,
    HeaderComponent,
    LeaveHistoryComponent,
    MatConfirmDialogComponent,
    ApproverComponent,
    AdminComponent,
    SectorComponent,
    SubSectorComponent,
    DepartmentComponent,
    SubDepartmentComponent,
    MemberFirmComponent,
    MemberFirmLocationComponent,
    ElementListComponent,
    LeaveDataMasterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    MatGridListModule,
    MatToolbarModule,
    MatPaginatorModule,
    MatDialogModule,

    MatTooltipModule,
    MatDatepickerModule,
    MatIconModule,
    MatSortModule,
    MatButtonModule,
    MatTableModule,
    MatCheckboxModule,
    MatInputModule,
    MatCardModule,
    MatFormFieldModule,
    MatListModule,
    MatRadioModule,
    MatSelectModule,
    MatNativeDateModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  exports:[
    MatTableModule,
    MatSortModule,
    MatPaginatorModule
  ],
  providers: [LeaveService,NotificationService,DialogService,ApproverService,DepartmentService,SubSectorService,SubDepartmentService,MemberFirmService, 
    MemberFirmLocationService,UserService,
      {

    provide: HTTP_INTERCEPTORS,

    useClass: WindowsAuthInterceptor,

    multi: true

  },

  {

    provide: HTTP_INTERCEPTORS,

    useClass: ErrorInterceptor,

    multi: true

  }

],
  bootstrap: [AppComponent],
  entryComponents: [LeaveFormComponent,MatConfirmDialogComponent]
})
export class AppModule { 
  
}
