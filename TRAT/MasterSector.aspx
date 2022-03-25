<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterSector.aspx.cs" Inherits="TRAT.AddSectorSubSector" %>

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
        <h3>Add Sector</h3>
        <div class="row">&nbsp;</div>
        <div class="container">
            <div id="alert" runat="server" class="alert alert-success fade in" visible="false">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <label id="lblAlert" runat="server"></label>
            </div>
            <div class="row">
                <asp:GridView ID="gvProjects" ShowFooter="true" runat="server" CssClass="table" AutoGenerateColumns="false" GridLines="None" DataKeyNames="Id"
                    OnRowCommand="gvProjects_RowCommand" OnRowDeleting="gvProjects_RowDeleting"
                    OnRowEditing="gvProjects_RowEditing" OnRowCancelingEdit="gvProjects_RowCancelingEdit" OnRowUpdating="gvProjects_RowUpdating">
                    <Columns>
                        <asp:BoundField HeaderText="Sector Name" DataField="Name" />
                        <asp:CommandField EditText="Edit" ShowEditButton="true"/>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="DeleteButton" runat="server" Text="Disable" CommandArgument='<%#Eval("Id")%>' 
                                    CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this sector?');" />
                            </ItemTemplate>
                            
                            <FooterTemplate>
                                <%--<asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" OnClick="ButtonAdd_Click"/>--%>
                            </FooterTemplate>
                        </asp:TemplateField>
                         
                        
                    </Columns>
                    
                    <EmptyDataTemplate>
                        
                        No Records Found!
                    </EmptyDataTemplate>
                    
                </asp:GridView>
                
               <%-- <table border="0" style="border-collapse: collapse">
                    <tr>
                        <td style="width: 650px">
                            Add New Sector:<br />
                            <asp:TextBox ID="txtName" runat="server" Width="140" />
                        </td>
        
                        <td style="width: 100px">
                            <asp:Button ID="Button1" runat="server" Text="Add" OnClick="Insert" />
                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBConnection %>"
    
                        InsertCommand="INSERT INTO Sector VALUES (@Name, 0,137,GETUTCDATE())">
    
                        <InsertParameters>
                            <asp:ControlParameter Name="Name" ControlID="txtName" Type="String" />
                           
                        </InsertParameters>
                        
                </asp:SqlDataSource>--%>
            </div>
            <hr/> <br />
            <div class="row" runat="server" id="divAddSect" >
                <div class="col-lg-2">Sector Name:</div>
                <div class="col-lg-6">
                    <asp:TextBox runat="server" ID="txtSectorName" Width="300px"></asp:TextBox>
                    <asp:Button runat="server" ID="btnAdd" Text="Add New Sector" OnClick="btnAdd_Click" />
                </div>
            </div><br /><br />
        </div>
    </form>
</asp:Content>
