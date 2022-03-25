﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterAdmin.aspx.cs" Inherits="TRAT.MasterAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Global site tag (gtag.js) - Google Analytics -->
    
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
        <h3>Admins</h3>
        <div class="row">&nbsp;</div>
        <div class="container" id="divAdminList">
            <div id="alert" runat="server" class="alert alert-success fade in" visible="false">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <label id="lblAlert" runat="server"></label>
            </div>
            <div class="row">
                <asp:GridView ID="gvProjects" ShowFooter="true" runat="server" CssClass="table" AutoGenerateColumns="false" GridLines="None" DataKeyNames="Id"
                    OnRowCommand="gvProjects_RowCommand" OnRowDeleting="gvProjects_RowDeleting"
                    OnRowEditing="gvProjects_RowEditing" OnRowCancelingEdit="gvProjects_RowCancelingEdit" OnRowUpdating="gvProjects_RowUpdating">
                    <Columns>
                        <asp:BoundField HeaderText="Employee Name" DataField="Employee_Name" />
                        <asp:BoundField HeaderText="Department Name" DataField="Department_Name" />

                        
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="DeleteButton" runat="server" Text="Disable" CommandArgument='<%#Eval("Id")%>' 
                                    CommandName="Delete" OnClientClick="return confirm('Are you sure you want to remove the employee from admin list?');" />
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
               
            </div>
            <hr/> <br />
            <div class="row" runat="server" id="divAddSect" >
                <div class="col-lg-2">Add New Admin:</div>
                <div class="col-lg-6">
                    <asp:DropDownList runat="server" ID="drpEmployeeNames" DataTextField="Employee_Name" DataValueField="Id" Width="300px"></asp:DropDownList>&nbsp;
                    <asp:Button runat="server" ID="btnAdd" Text="Add" />
                </div>
            </div><br /><br />
        </div>
               
    </form>
</asp:Content>