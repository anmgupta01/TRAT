<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditOrDeleteUser.aspx.cs" Inherits="TRAT.EditOrDeleteUser" %>
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
            <h3>Edit or Deactivate Resource</h3>
            <div class="row">
                <div class="col-lg-2">Resource Name</div>
                <div class="col-lg-4">
                    <asp:TextBox runat="server" ID="txtResourceName"></asp:TextBox>
                    <asp:Button runat="server" ID="btnSearchProject" Text="Search" OnClick="btnSearchProject_Click"/>
                </div>
            </div>
            <div id="alert" runat="server" class="alert alert-success fade in" visible="false">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <label id="lblAlert" runat="server"> </label>
            </div><br />
            <div class="row">
                <asp:GridView ID="gvProjects" runat="server" CssClass="table" AutoGenerateColumns="false" GridLines="None" DataKeyNames="Id" OnRowCommand="gvProjects_RowCommand" OnRowDeleting="gvProjects_RowDeleting">
                    <Columns>
                        <asp:BoundField HeaderText="Employee Code" DataField="EmpID" />
                        <asp:BoundField HeaderText="Employee Name" DataField="Employee_Name" />
                        <asp:BoundField HeaderText="Department" DataField="Department_Name" />
                        <asp:BoundField HeaderText="Email" DataField="Email" />
                        <asp:BoundField HeaderText="Designation" DataField="Designation" />
                        <asp:BoundField HeaderText="Is Active" DataField="isActive" />

                        <asp:CommandField EditText="Edit" ShowEditButton="true" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="DeleteButton" runat="server" Text="De-activate" CommandArgument='<%#Eval("Id")%>'
                                    CommandName="Delete" OnClientClick="return confirm('Are you sure you want to de-activate this employee?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="ActivateButton" runat="server" Text="Activate" CommandArgument='<%#Eval("Id")%>'
                                    CommandName="Activate" OnClientClick="return confirm('Are you sure you want to Activate this employee?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Records Found!
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </form>
</asp:Content>
