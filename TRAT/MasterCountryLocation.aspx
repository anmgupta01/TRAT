<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterCountryLocation.aspx.cs" Inherits="TRAT.MasterCountryLocation" %>
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
        <h3>Add Country Location</h3>
            <div id="alert" runat="server" class="alert alert-success fade in" visible="false">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <label id="lblAlert" runat="server"></label>
            </div>
            <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList"
            EnableClientScript="true" ForeColor="Red"
            HeaderText="You must enter a value in the following fields:" />
        <br />
        <div class="row">&nbsp;</div>
            <div class="row">
            <div class="col-lg-2">
                Country:<label style="color: red">*</label>
            </div>
            <div class="col-lg-2">
                <asp:DropDownList runat="server" AutoPostBack="true" ID="drpCountry" OnSelectedIndexChanged="drpCountry_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <asp:RequiredFieldValidator ID="valCountry" runat="server" Display="None"
                ControlToValidate="drpCountry" InitialValue="-1" ErrorMessage="Country selection is required"
                ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        </div>
        <div class="row">&nbsp;</div>
            
            <div class="row">   
                <asp:GridView ID="gvProjects" ShowFooter="true" runat="server" CssClass="table" AutoGenerateColumns="false" GridLines="None" DataKeyNames="Id"
                    OnRowCommand="gvProjects_RowCommand" OnRowDeleting="gvProjects_RowDeleting"
                    OnRowEditing="gvProjects_RowEditing" OnRowCancelingEdit="gvProjects_RowCancelingEdit" OnRowUpdating="gvProjects_RowUpdating">
                    <Columns>
                        <asp:BoundField HeaderText="Country Location" DataField="Office" />
                        <asp:CommandField EditText="Edit" ShowEditButton="true"/>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="DeleteButton" runat="server" Text="Disable" CommandArgument='<%#Eval("Id")%>' 
                                    CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this location?');" />
                            </ItemTemplate>                            
                        </asp:TemplateField>                                                
                    </Columns>                    
                    <EmptyDataTemplate>                        
                        No Records Found!
                    </EmptyDataTemplate>                    
                </asp:GridView>                
            </div>
            <hr/> <br />
            <div class="row" runat="server" id="divAddSect" >
                <div class="col-lg-2">Member Firm Location Name:</div>
                <div class="col-lg-6">
                    <asp:TextBox runat="server" ID="txtMemberFirmLocationName" Width="300px"></asp:TextBox>
                    <asp:Button runat="server" ID="btnAdd" Text="Add New Member Firm Location" OnClick="btnAdd_Click" />
                </div>
            </div><br /><br />
        </div>
    </form>
</asp:Content>
