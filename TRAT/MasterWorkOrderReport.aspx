<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterWorkOrderReport.aspx.cs" Inherits="TRAT.MasterWorkOrderReport" %>

<asp:Content ID="formSection" ContentPlaceHolderID="formSection" runat="server">
    <form id="form1" runat="server">
<br />

        <div class="row">
            <div class="col-md-2">
                Service Line:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" CssClass="form-control" ID="drpServiceLine">

                    <asp:ListItem Value="-1">All</asp:ListItem>
                </asp:DropDownList>

            </div>

        </div>
        <br />
        <br />

        <div class="row">
            <div class="col-lg-1">
                <asp:Button runat="server" ID="btnDownloadReport" Text="Download Report" OnClick="btnDownloadReport_Click" />
            </div>
        </div>
    </form>
    </asp:Content>
