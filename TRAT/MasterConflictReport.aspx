<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterConflictReport.aspx.cs" Inherits="TRAT.MasterConflictReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
        <div class="row">
                <div class="col-md-2">
                    Service Line:<label style="color: red">*</label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" CssClass="form-control" ID="drpServiceLine" OnSelectedIndexChanged="drpServiceLine_SelectedIndexChanged" AutoPostBack="true">
                        <%--DataValueField="Id" DataTextField="Name" ID="drpServiceLine" OnSelectedIndexChanged="drpServiceLine_SelectedIndexChanged"
                    AutoPostBack="true"--%>
                        <asp:ListItem Value="-1">All</asp:ListItem>
                    </asp:DropDownList>

                </div>
                <%--<asp:RequiredFieldValidator ID="valServiceLine" runat="server" Display="None"
                ControlToValidate="drpServiceLine" InitialValue="-1" ErrorMessage="Service line is required"
                ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
            </div>
    </form>
</body>
</html>
