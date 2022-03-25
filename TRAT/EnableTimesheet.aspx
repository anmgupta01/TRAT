<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EnableTimesheet.aspx.cs" Inherits="TRAT.EnableTimesheet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="formSection" runat="server">
    <form runat="server">
         <div runat="server" >
                <h3>Enable Timesheet</h3>
                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-lg-2">Resource Email<label style="color: red">*</label></div>
                    <div class="col-lg-2">
                        <%--<asp:TextBox ID="txtResourceName" runat="server"></asp:TextBox>--%>
                        <asp:DropDownList runat="server" class="form-control" ID="drpEMail" Style="width: 360px" OnSelectedIndexChanged="drpEMail_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="-1">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqManager" runat="server" Display="Dynamic"
                            ControlToValidate="drpEMail" InitialValue="-1"
                            ErrorMessage="Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>
                          
                    </div>
                </div>

                <div class="row">&nbsp;</div>
             
                <div class="row">
                    <div class="col-lg-2">
                        Allocated Projects
                        <label style="color: red">*</label>
                    </div>
                    <div class="col-lg-2">
                        <asp:DropDownList runat="server" class="form-control" ID="drpProjects" Style="width: 360px" AutoPostBack="false">
                            <asp:ListItem Value="-1">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqProjects" runat="server" Display="Dynamic"
                            ControlToValidate="drpProjects" InitialValue="-1"
                            ErrorMessage="Required" ForeColor="Red">
                        </asp:RequiredFieldValidator>                          
                    </div>
                </div>

                <div class="row">&nbsp;</div>

                <div class="row">
                    <div class="col-lg-2">From Date <label style="color: red">*</label></div>
                    <div class="col-lg-4">
                        <input type="date" class="form-control" id="txtFromDate"  runat="server"/>
                         <asp:RequiredFieldValidator ID="rfvFromDate" runat="server"
                ControlToValidate="txtFromDate" ErrorMessage="Required"
                ForeColor="Red" ValidationGroup="vgSubmit" Display="Dynamic"
                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        
                    </div>
                </div>

                <div class="row">&nbsp;</div>

                <div class="row">
                    <div class="col-lg-2">To Date<label style="color: red">*</label></div>
                    <div class="col-lg-4">
                        <input type="date" class="form-control" id="txtTodate" runat="server"/>
                         <asp:RequiredFieldValidator ID="rfvToDate" runat="server"
                ControlToValidate="txtFromDate" ErrorMessage="Required"
                ForeColor="Red" ValidationGroup="vgSubmit"
                SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row">&nbsp;</div>

                <div class="row">
                    <div class="col-lg-2"></div>
                    <div class="col-lg-4">
                        <asp:Button Text="Enable Timesheet" runat="server" ID="btnEnable" OnClick="btnEnable_Click" CausesValidation="true" ValidationGroup="vgSubmit"/>
                    </div>

                </div>

            </div>
    </form>
</asp:Content>
