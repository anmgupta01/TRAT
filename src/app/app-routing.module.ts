import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ApproverComponent } from './approver/approver.component';
import { AdminComponent } from './admin/admin.component';
import { SectorComponent } from './sector/sector.component';
import { LeaveDetailsComponent } from './leave-details/leave-details.component';
import { SubSectorComponent } from './sub-sector/sub-sector.component';
import { DepartmentComponent } from './department/department.component';
import { MemberFirmComponent } from './member-firm/member-firm.component';
import { MemberFirmLocationComponent } from './member-firm-location/member-firm-location.component';
import {  SubDepartmentComponent } from './sub-department/sub-department.component';
import { LeaveDataMasterComponent } from './leave-data-master/leave-data-master.component';

const routes: Routes = [
  { path: 'Leave', component: LeaveDetailsComponent },
  { path: 'Approval', component: ApproverComponent },
  { path: 'Admin', component: AdminComponent },
  { path: 'Sector', component: SectorComponent },
  { path: 'SubSector', component: SubSectorComponent },
  { path: 'Department', component: DepartmentComponent },
  { path: 'SubDepartment', component: SubDepartmentComponent },
  { path: 'MemberFirm', component: MemberFirmComponent },
  { path: 'MemberFirmLocation', component: MemberFirmLocationComponent },
  { path: 'LeaveDataMaster', component: LeaveDataMasterComponent },
  { path: '',   redirectTo: '/Approval', pathMatch: 'full' },


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
