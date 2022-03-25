<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterSAPCode.aspx.cs" Inherits="TRAT.MAsterSAPCode" %>

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
            <h3>Add SAP Codes</h3>
            <div id="alert" runat="server" class="alert alert-success fade in" visible="false">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <label id="lblAlert" runat="server"></label>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-2">
                    Referring Member Firm:<label style="color: red">*</label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpMemberFirm" OnSelectedIndexChanged="drpMemberFirm_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem> --Select-- </asp:ListItem>

                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="valMemberFirm" runat="server" Display="None"
                        ControlToValidate="drpMemberFirm" InitialValue="-1"
                        ErrorMessage="Member Firm is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-2">
                    Referring Member Location:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" Style="width: 360px" CssClass="form-control" ID="drpMemLocation" DataValueField="Id" DataTextField="Office"
                        AutoPostBack="true" OnSelectedIndexChanged="drpMemLocation_SelectedIndexChanged">
                        <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                    </asp:DropDownList>



                </div>


            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-2">
                    Service Line:<label style="color: red">*</label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" Style="width: 360px" class="form-control" DataValueField="Id" DataTextField="Name" ID="drpServiceLine" OnSelectedIndexChanged="drpServiceLine_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>

                </div>
                <asp:RequiredFieldValidator ID="valServiceLine" runat="server" Display="None"
                    ControlToValidate="drpServiceLine" InitialValue="-1" ErrorMessage="Service line is required"
                    ForeColor="Red"
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
            </div>

            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-2">
                    Sub-Service Line:
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" AutoPostBack="true" Style="width: 360px" class="form-control" DataValueField="SubServiceLineId" DataTextField="Name" ID="drpSubServiceLine" OnSelectedIndexChanged="drpSubServiceLine_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">&nbsp;</div>

            <div class="row">
                <div class="col-md-2">
                    Chargeable:<label style="color: red">*</label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpChargeable" OnSelectedIndexChanged="drpChargeable_SelectedIndexChanged" AutoPostBack="true">
                        <%--<asp:ListItem Value="-1"> --Select-- </asp:ListItem>--%>
                        <asp:ListItem Value="1"> Y </asp:ListItem>
                        <asp:ListItem Value="0"> N </asp:ListItem>
                    </asp:DropDownList>

                </div>
                <%--<asp:RequiredFieldValidator ID="valChargeNoncharge" runat="server" Display="None"
                    ControlToValidate="drpChargeable" InitialValue="1" ErrorMessage="Chargeable / Non Chargeable is required"
                    ForeColor="Red"
                    SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
            </div>
            <div class="row">&nbsp;</div>

            <div class="row">   
                <asp:GridView ID="gvSapCodes" ShowFooter="true" runat="server" CssClass="table" AutoGenerateColumns="false" GridLines="None" DataKeyNames="Id"
                    OnRowCommand="gvSapCodes_RowCommand" OnRowDeleting="gvSapCodes_RowDeleting"
                    OnRowEditing="gvSapCodes_RowEditing" OnRowCancelingEdit="gvSapCodes_RowCancelingEdit" OnRowUpdating="gvSapCodes_RowUpdating">
                    <Columns>
                        <asp:BoundField HeaderText="SAP Code" DataField="ChargeCode" />
                        <asp:CommandField EditText="Edit" ShowEditButton="true"/>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="DeleteButton" runat="server" Text="Disable" CommandArgument='<%#Eval("Id")%>' 
                                    CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this SAP Code?');" />
                            </ItemTemplate>                            
                        </asp:TemplateField>                                                
                    </Columns>                    
                    <EmptyDataTemplate>                        
                        No Records Found!
                    </EmptyDataTemplate>                    
                </asp:GridView>                
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">&nbsp;</div>

           <div class="row" runat="server" id="divAddSect" >
                <div class="col-lg-2">Sap Code:</div>
                <div class="col-lg-6">
                    <asp:TextBox runat="server" ID="txtSapCode" Width="300px"></asp:TextBox>
                    <asp:Button runat="server" ID="btnAdd" CausesValidation="false" Text="Add New SAP Code" OnClick="btnAdd_Click" />
                </div>
            </div><br /><br />
            <br />
            <br />
            <div class="row">&nbsp;</div>
        </div>
    </form>
</asp:Content>
