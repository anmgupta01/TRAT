<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="TRAT.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="formSection" ContentPlaceHolderID="formSection" runat="server">
    <br />
    <br />
    <div runat="server" id="divOnlyForAdmin" visible="false">
        <ul>
            <%--            <li><a href="AddNewResource.aspx">Add New Resource</a><br />
                <br />
            </li>--%>
            <%--<li><a href="EditOrDeleteUser.aspx">Edit\De-activate Resource</a><br />
                <br />
            </li>--%>
            <%--<li><a href="EnableTimesheet.aspx">Unlock Timesheet</a><br />
                <br />
            </li>--%>
            <%--            <li><a href="MasterCountries.aspx">Add Member Firm</a><br />
                <br />
            </li>
            <li><a href="MasterCountryLocation.aspx">Add Member Firm Location</a><br />
                <br />
            </li>--%>
            <li><a href="MasterSector.aspx">Add Sector</a><br />
                <br />
            </li>
            <li><a href="MasterSubSector.aspx">Add Sub-Sector</a><br />
                <br />
            </li>
            <%--            <li><a href="MasterServiceLine.aspx">Add Service Line</a><br />
                <br />
            </li>--%>
            <li><a href="MasterSubServiceLine.aspx">Add Sub-Service Line</a><br />
                <br />
            </li>
            <li><a href="MasterNatureOfWork.aspx">Add Nature of Work</a><br />
                <br />
            </li>
            <%--            <li><a href="MasterDepartment.aspx">Add Department</a><br />
                <br />
            </li>--%>
            <li><a href="MasterSubDepartment.aspx">Add Sub-Department</a><br />
                <br />
            </li>
            <li><a href="MasterUpdateProjectDetails.aspx">Update Project Details for Submitted Work Orders only</a><br />
                <br />
            </li>
        </ul>

    </div>
    <div runat="server" id="divSuperAdmin" visible="false">
        <ul>
            <li><a href="AddNewResource.aspx">Add New Resource</a><br />
                <br />
            </li>
            <li><a href="EditOrDeleteUser.aspx">Edit\De-activate Resource</a><br />
                <br />
            </li>
            <li><a href="EnableTimesheet.aspx">Unlock Timesheet</a><br />
                <br />
            </li>
            <li><a href="MasterCountries.aspx">Add Member Firm</a><br />
                <br />
            </li>
            <li><a href="MasterCountryLocation.aspx">Add Member Firm Location</a><br />
                <br />
            </li>
            <li><a href="MasterSector.aspx">Add Sector</a><br />
                <br />
            </li>
            <li><a href="MasterSubSector.aspx">Add Sub-Sector</a><br />
                <br />
            </li>
            <li><a href="MasterServiceLine.aspx">Add Service Line</a><br />
                <br />
            </li>
            <li><a href="MasterSubServiceLine.aspx">Add Sub-Service Line</a><br />
                <br />
            </li>
            <li><a href="MasterNatureOfWork.aspx">Add Nature of Work</a><br />
                <br />
            </li>
            <li><a href="MasterDepartment.aspx">Add Department</a><br />
                <br />
            </li>
            <li><a href="MasterSubDepartment.aspx">Add Sub-Department</a><br />
                <br />
            </li>
            <li><a href="MasterLogTime.aspx">Fill Timesheet</a><br />
                <br />
            </li>
            <li><a href="MasterSAPCode.aspx">Add SAP Codes</a><br />
                <br />
            </li>
            <li>
                <a href="MasterGrantAdminRights.aspx">Give Admin Rights</a><br />
                <br />
            </li>
            <li>
                <a href="SetMFPartnerWOStatus.aspx">Member Firm WO Status Update</a><br />
                <br />
            </li>
            <%--            <li><a href="MasterUpdateProjectDetails.aspx">Update Project Details for Submitted Work Orders only</a><br />
                <br />
            </li>--%>
            <li><a href="WorkOrderUsers.aspx">Set IJV Approvals for Work Orders</a><br />
                <br />
            </li>

            <li>
                <a href="EPCC_IJV_Approval.aspx">Set IJV Approvals for EPCC 2</a><br />
                <br />
            </li>

            <li>
                <a href="EPCC_MF_Approval.aspx">Set MF Approvals for EPCC 2</a><br />
                <br />
            </li>

            <li>
                <a href="EPCC3_IJV_Approval.aspx">Set IJV Approvals for EPCC 3</a><br />
                <br />
            </li>

            <li>
                <a href="EPCC3_MF_Approval.aspx">Set MF Approvals for EPCC 3</a><br />
                <br />
            </li>
        </ul>
    </div>
    <%--added by Vijay Bhagat dated 5 Aug 2021 as Shruti Mantri instruction, It will be visible to only TS--%>
    <div runat="server" id="divTSReport" visible="false">
        <ul>
            <li>
                <a href="DailyReport.aspx">Workorder Summary Data</a><br />
                <br />
            </li>
        </ul>
    </div>
    <div runat="server" id="divForNonAdmins" visible="false">
        <span>You are not authorized to view this page</span>
    </div>
    <div runat="server" id="divForWorkOrderUsers" visible="false">
        <a href="WorkOrderUsers.aspx">Approve Work orders for IJV</a>
    </div>
    
    
</asp:Content>

