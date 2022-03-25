<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SuperAdmin.aspx.cs" Inherits="TRAT.SuperAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="formSection" ContentPlaceHolderID="formSection" runat="server">
    <br />
    <br />
    <div runat="server" id="divOnlyForAdmin" visible="false">
        <ul> 
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
            <li><a href="MasterNatureOfWork.aspx">Add Nature of Work</a><br />
                <br />
            </li>
            <li><a href="MasterAdmin.aspx">Add Admin</a><br />
                <br />
            </li>
            <li><a href="MasterReportsAdmin.aspx">Add Reports Admin</a><br />
                <br />
            </li>
            <li><a href="MasterReportsAdmin.aspx">Add SAP Code</a><br />
                <br />
            </li>

        </ul>
    </div>
    <div runat="server" id="divForNonAdmins" visible="false">
        <span>You are not authorized to view this page</span>
    </div>
</asp:Content>