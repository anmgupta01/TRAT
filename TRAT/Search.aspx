﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="TRAT.Search" %>

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
    <form runat="server">
        <div class="container">
            <h3>Search Project</h3>
            <div class="row">
                <div class="col-lg-2">Project Name</div>
                <div class="col-lg-4">
                    <asp:TextBox runat="server" ID="txtProjectName"></asp:TextBox>
                    <asp:Button runat="server" ID="btnSearchProject" Text="Search" OnClick="btnSearchProject_Click" />
                </div>
            </div>
            <div class="row">&nbsp;</div>

            <div id="alert" runat="server" class="alert alert-success fade in" visible="false">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <label id="lblAlert" runat="server"> </label>
            </div>
            <div class="row">
                <asp:GridView ID="gvProjects" runat="server" CssClass="table" AutoGenerateColumns="false" GridLines="None" DataKeyNames="Id" OnRowCommand="gvProjects_RowCommand" OnRowDeleting="gvProjects_RowDeleting">
                    <Columns>
                        <asp:BoundField HeaderText="Project" DataField="Name" />
                        <asp:BoundField HeaderText="Partner" DataField="USerID" />
                        <asp:BoundField HeaderText="Office" DataField="Office" />
                        <asp:CommandField EditText="View/Edit" ShowEditButton="true" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="Replicate" runat="server" Text="Replication Of Project" CommandArgument='<%#Eval("Id")%>'
                                    CommandName="Replicate"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="DeleteButton" runat="server" Text="Disable" CommandArgument='<%#Eval("Id")%>'
                                    CommandName="Delete" OnClientClick="return confirm('Are you sure you want to disable this project?');" />
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


