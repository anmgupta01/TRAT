<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetMFPartnerWOStatus.aspx.cs" Inherits="TRAT.SetMFPartnerWOStatus" %>
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
            <div class="row">
                <h3>Set Member Firm Work Order Approval Status</h3><br />
                <span>Setting member firm status from this location should only be done in case member firm approval or rejection over email has been sent but could not be updated in TRAT system.</span><br />
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
                    Created Work Orders: 
                </div>
                <div class="col-lg-10">
                    <%--<asp:DropDownList runat="server" Style="width: 360px" ID="ddlWO" CssClass="form-control" OnSelectedIndexChanged="ddlWO_SelectedIndexChanged" AutoPostBack ="True">
                        <asp:ListItem Value="-1">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqEmpList" runat="server" Display="Dynamic"
                        ControlToValidate="ddlWO" InitialValue="-1"
                        ErrorMessage="Required" ValidationGroup="vgSubmit"
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>--%>
                     <asp:TextBox runat="server" ID="txtWorkOrderName"></asp:TextBox>
                    <asp:Button runat="server" ID="btnWorkOrder" Text="Search" OnClick="btnWorkOrder_Click" />
                </div>
            </div>
            <div class="row">&nbsp;</div>

            <%--<div id="alert" runat="server" class="alert alert-success fade in" visible="false">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <label id="lblAlert" runat="server"></label>
            </div>--%>
            <div class="row">
                <asp:GridView ID="gvProjects" runat="server" CssClass="table" AutoGenerateColumns="false" GridLines="None" OnRowCommand="gvProjects_RowCommand"
                    DataKeyNames="Id">
                    <Columns>
                         <asp:BoundField HeaderText="WorkOrder" DataField="WOLogNumber" />
                        <asp:BoundField HeaderText="DIJV Name" DataField="DIJVApprover" />
                        <asp:BoundField HeaderText="MF Partner" DataField="MFApprover" />
                        <asp:TemplateField HeaderText="DIJV Approved">
                            <ItemTemplate>
                                <%# Eval("isDIJVApproved").ToString() == "True" ? "Yes" : "No" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MF Approved">
                            <ItemTemplate>
                                <%# Eval("isMFApproved").ToString() == "True" ? "Yes" : String.IsNullOrEmpty(Eval("isMFApproved").ToString()) ? "No" : "Yes" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                      <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnApprove" runat="server" Text="Approve"  CommandArgument='<%#Eval("Id")%>' CommandName="Approve" />
                            </ItemTemplate>
                        </asp:TemplateField>  
                         <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CommandArgument='<%#Eval("Id")%>' CommandName="Reject" />
                            </ItemTemplate>
                        </asp:TemplateField>  
                       
                    </Columns>
                    <EmptyDataTemplate>
                        No Records Found!
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
            <br />
            <%--<div class="row">
                <div class="col-lg-2">
                    Member Status:                 
                </div>
                <div class="col-lg-10">
                    <asp:DropDownList runat="server" Style="width: 360px" ID="ddlStatus" CssClass="form-control">
                        <asp:ListItem Value="-1">--Select--</asp:ListItem>
                        <asp:ListItem Value="1">APPROVED</asp:ListItem>
                        <asp:ListItem Value="0">REJECTED</asp:ListItem>

                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="reqddlRights" runat="server" Display="Dynamic"
                        ControlToValidate="ddlStatus" InitialValue="-1"
                        ErrorMessage="Required" ValidationGroup="vgSubmit"
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>
            </div>--%>
            <%--<br />
            <div class="row">
                <div class="col-lg-2">
                    <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                </div>
            </div><br />--%>
            <span><i>P.S. - Before setting the status from here please verify member firm approval\rejection mail sent to the Indian partner.</i></span>
        </div>
        <br />
    </form>
</asp:Content>
