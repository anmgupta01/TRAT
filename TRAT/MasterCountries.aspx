<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterCountries.aspx.cs" Inherits="TRAT.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {


            setTimeout(function () {
                $(".alert").alert('close');
            }, 5000);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="formSection" runat="server">
    <form runat="server" defaultbutton="btnSearch">
        <div class="container">
            <div id="divSearch" runat="server">
            <h3>    <button type="submit" class="btn btn-default btn-sm pull-right" runat="server" onserverclick="btnAddNew_Click">
                        <span class="glyphicon glyphicon-plus"></span>Add New Country</button>  </h3>
            <h3>Search Country</h3>
            <div class="row">
                <div class="col-lg-2">Country Name</div>
                <div class="col-lg-4">
                    <asp:TextBox runat="server" ID="txtCountryName"></asp:TextBox>
                    <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                </div>
               
            </div>
            <div class="row">&nbsp;</div>

            <div id="alert" runat="server" class="alert alert-success fade in" visible="false">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <label id="lblAlert" runat="server"></label>
            </div>
            <div class="row">
                <asp:GridView ID="gvCountry" ShowFooter="true" runat="server" CssClass="table" AutoGenerateColumns="false" GridLines="None" DataKeyNames="Id" OnRowCommand="gvCountry_RowCommand" OnRowDeleting="gvCountry_RowDeleting" OnRowCancelingEdit="gvCountry_RowCancelingEdit" OnRowEditing="gvCountry_RowEditing" OnRowUpdated="gvCountry_RowUpdated" OnRowUpdating="gvCountry_RowUpdating">
                    <Columns>

                        <asp:BoundField HeaderText="Country" DataField="Name" />
                        <asp:CommandField EditText="Edit" ShowEditButton="true" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>                                
                                <asp:LinkButton ID="DeleteButton" runat="server" Text="Disable" CommandArgument='<%#Eval("Id")%>'
                                    CommandName="Delete" OnClientClick="return confirm('Are you sure you want to disable this project?');" />
                            </ItemTemplate>

                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No Records Found!<br />
                                                                
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
          </div>
        <div id="divAddNew" runat="server" visible="false">
         <h3>Add New Country</h3>
            <div class="row" runat="server" id="divAddSect" >
                <div class="col-lg-2">Country Name:   </div>
                <div class="col-lg-6">
                    <asp:TextBox runat="server" ID="txtNewName" Width="300px"></asp:TextBox>
                    <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnSaveNew_Click" />
                 
                </div>
            </div>
             <div class="row"></div>
            <div class="row">
                 <div class="col-lg-2">
                    <asp:LinkButton runat="server" ID="lbBack" Text="<< Back" OnClick="btnAddBack_Click" /></div>
                
            </div><br /><br />
        </div>
</div>
          
        
    </form>
</asp:Content>



