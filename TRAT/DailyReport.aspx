<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DailyReport.aspx.cs" Inherits="TRAT.DailyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="formSection" runat="server">
    <script src="TRATScripts.js"></script>
    <form runat="server">
        <div class="container">
            <br />
            <div id="alert" runat="server" class="alert alert-success fade in" visible="false">

                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong>No data available for the filters done !</strong><br />
            </div>
            <h3>Daily Report</h3>
            <hr />
            <br />
            <div class="row">
                <div class="col-md-1">Start Date</div>
                <div class="col-md-2">
                    <input runat="server" class="form-control" id="dtStartDate" type="date" />
                </div>
                <div class="col-md-1">End Date</div>
                <div class="col-md-2">
                    <input runat="server" class="form-control" id="dtEndDate" type="date" />
                </div>
            </div>

            
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-lg-1">
                    <asp:Button runat="server" ID="btnDownloadReport" Text="Download Report" OnClick="btnDownloadReport_Click" />
                </div>
            </div>
        </div>
    </form>
</asp:Content>

