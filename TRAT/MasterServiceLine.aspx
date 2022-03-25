﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterServiceLine.aspx.cs" Inherits="TRAT.MasterServiceLine" %>

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
            <h3>Add Service Line</h3>
            <div id="alert" runat="server" class="alert alert-success fade in" visible="false">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <label id="lblAlert" runat="server"></label>
            </div>
            <div class="row">&nbsp;</div>
            
            <div class="row">   
                <asp:GridView ID="gvProjects" ShowFooter="true" runat="server" CssClass="table" AutoGenerateColumns="false" GridLines="None" DataKeyNames="Id"
                    OnRowCommand="gvProjects_RowCommand" OnRowDeleting="gvProjects_RowDeleting"
                    OnRowEditing="gvProjects_RowEditing" OnRowCancelingEdit="gvProjects_RowCancelingEdit" OnRowUpdating="gvProjects_RowUpdating">
                    <Columns>
                        <asp:BoundField HeaderText="Service Line" DataField="Name" />
                        <asp:CommandField EditText="Edit" ShowEditButton="true"/>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="DeleteButton" runat="server" Text="Disable" CommandArgument='<%#Eval("Id")%>' 
                                    CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this service line?');" />
                            </ItemTemplate>                            
                        </asp:TemplateField>                                                
                    </Columns>                    
                    <EmptyDataTemplate>                        
                        No Records Found!
                    </EmptyDataTemplate>                    
                </asp:GridView>                
            </div>
            <div class="row" runat="server" id="divAddSect" >
                <div class="col-lg-2">Service Line:</div>
                <div class="col-lg-6">
                    <asp:TextBox runat="server" ID="txtSectorName" Width="300px"></asp:TextBox>
                    <asp:Button runat="server" ID="btnAdd" Text="Add New Service Line" OnClick="btnAdd_Click" />
                </div>
            </div><br /><br />
        </div>
    </form>
</asp:Content>
