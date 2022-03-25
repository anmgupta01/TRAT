<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkOrderUsers.aspx.cs" Inherits="TRAT.WorkOrderUsers" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


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
                <div class="col-lg-2">Work Order</div>
                <div class="col-lg-4">
                    <asp:TextBox runat="server" ID="txtWorkOrderName"></asp:TextBox>
                    <asp:Button runat="server" ID="btnWorkOrder" Text="Search" OnClick="btnWorkOrder_Click" />
                </div>
            </div>
            <div class="row">&nbsp;</div>

            <div id="alert" runat="server" class="alert alert-success fade in" visible="false">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <label id="lblAlert" runat="server"></label>
            </div>
            <div class="row">
                <asp:GridView ID="gvWorkOrder" runat="server" CssClass="table" AutoGenerateColumns="false" GridLines="None" DataKeyNames="Id" OnRowCommand="gvWorkOrder_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="WorkOrder" DataField="WOLogNumber" />
                        <asp:BoundField HeaderText="DIJV Name" DataField="DIJVApprover" />
                        <asp:BoundField HeaderText="MF Partner" DataField="MFApprover" />
                        <asp:TemplateField HeaderText="Approved">
                            <ItemTemplate>
                                <%# Eval("isDIJVApproved").ToString() == "True" ? "Yes" : "No" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" Enabled=' <%# Eval("isDIJVApproved").ToString() == "False" ? true : String.IsNullOrEmpty(Eval("isDIJVApproved").ToString())?true:false %>' CommandArgument='<%#Eval("Id")%>' CommandName="Approve" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CommandArgument='<%#Eval("Id")%>' CommandName="Reject" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnReset" runat="server" Text="Reset" CommandArgument='<%#Eval("Id")%>' CommandName="Reset" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="true" HeaderText="Do not send Mail">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkMail" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>                        
                    </Columns>
                    <EmptyDataTemplate>
                        No Records Found!
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>

        <div style="display: none">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewerWO" runat="server">
                <LocalReport ReportPath="Report/WorkOrderDetails.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </form>
</asp:Content>
