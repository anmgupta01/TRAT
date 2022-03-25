<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterGrantAdminRights.aspx.cs" Inherits="TRAT.MasterGrantAdminRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            setTimeout(function () {
                $(".alert").alert('close');
            }, 5000);
        });
    </script>
</asp:Content>
<asp:Content ID="formSection" ContentPlaceHolderID="formSection" runat="server">
    <form runat="server">
        <div class="container">
            <div class="row">
                <h3>Edit Admin Rights</h3>
                <div id="alert" runat="server" class="alert alert-success fade in" visible="false">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    <label id="lblAlert" runat="server"></label>
                </div>
                <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList"
                    EnableClientScript="true" ForeColor="Red"
                    HeaderText="You must enter a value in the following fields:" />
                <br />
            </div>
            <div class="row">
                <div class="col-lg-2">
                    Employee Name: 
                </div>
                <div class="col-lg-10">
                    <asp:DropDownList runat="server" Style="width: 360px" ID="ddlEmpList" CssClass="form-control">
                        <asp:ListItem Value="-1">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqEmpList" runat="server" Display="Dynamic"
                        ControlToValidate="ddlEmpList" InitialValue="-1"
                        ErrorMessage="Required" ValidationGroup="vgSubmit"
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-2">
                    Access Rights:                 
                </div>
                <div class="col-lg-10">
                    <asp:DropDownList runat="server" Style="width: 360px" ID="ddlRights" AutoPostBack="true" OnSelectedIndexChanged="ddlRights_SelectedIndexChanged" CssClass="form-control">
                        <asp:ListItem Value="-1">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqddlRights" runat="server" Display="Dynamic"
                        ControlToValidate="ddlRights" InitialValue="-1"
                        ErrorMessage="Required" ValidationGroup="vgSubmit"
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-2">
                    <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button runat="server" ID="btnDele" Text="Delete" OnClick="btnDele_Click" />
                </div>
            </div>
        </div>
        <br />
    </form>
</asp:Content>
