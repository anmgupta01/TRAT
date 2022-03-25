<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"AutoEventWireup="true" CodeBehind="TempReport.aspx.cs" Inherits="TRAT.TempReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="formSection" ContentPlaceHolderID="formSection" runat="server">
    <br />
    <br />
    <div runat="server" id="divOnly">
        <ul>            
            <li><a href="Reports.aspx">Reports</a><br />
                <br />
            </li>
            <li><a href="ComplianceReportsTab.aspx">Compliance Report</a><br />
                <br />
            </li>            
        </ul>
    </div>    
</asp:Content>