export class LeaveTypes {
    id: number;
    name: string;
    count: number;
    leaveFor: string;
    isActive: boolean;
}
export class LeaveDetails {
    id: number;
    empId: number;
    leaveTypeId: number;
    approverId: number;
    fromDate: Date;
    toDate: Date;
    leaveCount: number;
    reason: string;
    dayType: string;
    totalAbsenceHour: number;
    isActive: boolean;
    approvalStatus: string;
    createdBy: number;
    createdOn: Date;
    updatedBy: number;
    updatedOn: Date;
}
export class Employee {
    id: number;
    empId: string;
    email: string;
    userId: string;
    isActive: boolean;
    employeeName: string;
    departmentName: string;
    designation: string;
    australiaMailId: string;
    canadaMailId: string;
    ukMailId: string;
    lastUpdatedByTs: Date;
    lastUpdatedBy: number;
    subDepartmentName: string;
    departmentId: number;
    subDepartmentId: number;
    gender: string;
}

export class LeaveDetails_DayWise
{
    id:number;
}