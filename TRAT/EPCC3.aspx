<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EPCC3.aspx.cs" Inherits="TRAT.EPCC3" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="formSection" ContentPlaceHolderID="formSection" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
        });
        function scrollToWorkOrder() {
            document.getElementById('<%= divWorkOrder.ClientID %>').scrollIntoView();
        }

        $(function () {
            $('#<%=FileUpload1.ClientID %>').change(
                function () {
                    var fileExtension = ['jpeg', 'jpg', 'pdf', 'xlsx', 'msg', 'docx'];
                    if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                        $('#<%=btnUpload.ClientID %>').attr("disabled", true);
                        alert("Only 'jpeg','jpg','pdf','xlsx','word','Outlook email' formats are allowed.");

                    }
                    else {
                        $('#<%=btnUpload.ClientID %>').attr("disabled", false);
                    }

                    var uploadControl = document.getElementById('<%= FileUpload1.ClientID %>');
                    if (uploadControl.files[0].size > 1048576) {
                        $('#<%=btnUpload.ClientID %>').attr("disabled", true);
                        document.getElementById('dvMsg').style.display = "block";
                        return false;
                    }
                    else {
                        $('#<%=btnUpload.ClientID %>').attr("disabled", false);
                        document.getElementById('dvMsg').style.display = "none";
                        return true;
                    }
                })
        })
    </script>

    <form runat="server">
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList"
            EnableClientScript="true" ForeColor="Red"
            HeaderText="You must enter a value in the following fields:" />
        <br />
        <asp:HiddenField runat="server" ID="hidProjectID" />
        <asp:HiddenField runat="server" ID="hidProjectName" />
        <asp:HiddenField runat="server" ID="hidWONumber" />
        <asp:HiddenField runat="server" ID="hidWOID" />
        <asp:HiddenField runat="server" ID="hidMemberFirmID" />
        <asp:HiddenField runat="server" ID="hidEPCC3ID" />


        <div class="container">
            <h3>Search Work Order For EPCC 3</h3> 
            <div class="row">
                <div class="col-lg-2">Project Name</div>
                <div class="col-lg-4">
                    <asp:TextBox runat="server" ID="txtProjectName"></asp:TextBox>
                    <asp:Button runat="server" ID="btnSearchProject" CausesValidation="false" Text="Search" OnClick="btnSearchProject_Click" />
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <asp:GridView ID="gvProjects" runat="server" CssClass="table" AutoGenerateColumns="false" GridLines="None"
                    DataKeyNames="Id" OnRowCommand="gvProjects_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="Work Order Number" DataField="WorkOrderNumber" />
                        <asp:BoundField HeaderText="Project Code" DataField="ProjectCode" />

                        <asp:BoundField HeaderText="Project" DataField="Name" />
                        <asp:BoundField HeaderText="Partner" DataField="USerID" />
                        <asp:BoundField HeaderText="Office" DataField="Office" />
                        <asp:TemplateField HeaderText="EPCC 3 Status">
                            <ItemTemplate>
                                <%# Eval("IsSubmitted").ToString() == "True" ? "Submitted" : "Not Submitted" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DIJV Manager Approved">
                            <ItemTemplate>
                                <%# Eval("DIJVManagerApproval").ToString() == "True" ? "Yes" : "No" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DIJV Partner Approved">
                            <ItemTemplate>
                                <%# Eval("EngagementManagerApproval").ToString() == "True" ? "Yes" : "No" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="GenerateWO" runat="server" Text="Generate EPCC3" CommandName="GenerateWO"
                                    CommandArgument='<%#Eval("Id")%>' CausesValidation="false"
                                    Enabled='<%#Eval("ProjectID").ToString() == ""%>'>
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="ViewEdit" CausesValidation="false" runat="server" Text="View\Edit" CommandName="edt" CommandArgument='<%#Eval("Id")%>'
                                    Enabled='<%#Eval("ProjectID").ToString() != ""%>'>
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        No Records Found!
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
            <hr />
            <label id="lblWorkOrderError" style="color: red" runat="server" visible="false"></label>
            <div class="row" id="divWorkOrder" runat="server">
                <%--=======================================================================================================================================================================--%>
                <br />
                <div class="row" style="background-color: greenyellow">
                    <span><b>Stage 3 Procedures</b></span>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">Transaction Type: </div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" class="form-control" ID="txtTransactionType" ReadOnly="true" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Project Name: </div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" class="form-control" ID="txtProjectNameEPCC" ReadOnly="true" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">Member firm engaged with: </div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" class="form-control" ID="txtMFEngagedWith" ReadOnly="true" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        Initial TRAT work order issued attached:
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" class="form-control" ID="txtInitialWO" ReadOnly="true" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">
                        Final hours to be charged to the Member Firm are communicated to the MF separately:<label runat="server" id="lblFinalHours" style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpFinalHours">
                            <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                            <asp:ListItem Value="1"> Y </asp:ListItem>
                            <asp:ListItem Value="0"> N </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="valFinalHours" runat="server" Display="None"
                        ControlToValidate="drpFinalHours" InitialValue="-1"
                        ErrorMessage="Final Hours is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>

                    <div class="col-md-2">
                        Final work order signed and filed/ appropriate evidence of increased hours documented:<label runat="server" id="lblFinalWO" style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpFinalWO">
                            <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                            <asp:ListItem Value="1"> Y </asp:ListItem>
                            <asp:ListItem Value="0"> N </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="valFinalWO" runat="server" Display="None"
                        ControlToValidate="drpFinalWO" InitialValue="-1"
                        ErrorMessage="Final work order signed is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">
                        If the project required Canadian review process have all checklists been signed off?<label runat="server" id="lblCanadianReviewk" style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpCanadianReview">
                            <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                            <asp:ListItem Value="1"> Y </asp:ListItem>
                            <asp:ListItem Value="0"> N </asp:ListItem>
                            <asp:ListItem Value="2"> NA </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="valCanadianReview" runat="server" Display="None"
                        ControlToValidate="drpCanadianReview" InitialValue="-1"
                        ErrorMessage="Canadian review is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>

                    <div class="col-md-2">
                        If the project required a German Insider list have all of the members who charged time to the project and the engagement partner signed the declaration and been communciated to the German member firm?<label runat="server" id="lblGermanInsider" style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpGermanInsider">
                            <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                            <asp:ListItem Value="1"> Y </asp:ListItem>
                            <asp:ListItem Value="0"> N </asp:ListItem>
                            <asp:ListItem Value="2"> NA </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="valGermanInsider" runat="server" Display="None"
                        ControlToValidate="drpGermanInsider" InitialValue="-1"
                        ErrorMessage="German Insider list signed is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>
                <br />

                <div class="row">
                    <div class="col-md-2">
                        TRAT work order updated attached:<label runat="server" id="lblTRATWO" style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpTRATwo">
                            <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                            <asp:ListItem Value="1"> Y </asp:ListItem>
                            <asp:ListItem Value="0"> N </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="valTRATwo" runat="server" Display="None"
                        ControlToValidate="drpTRATwo" InitialValue="-1"
                        ErrorMessage="TRAT work order updated is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>

                    <div class="col-md-2">
                        DIJV Conflict check closed:<label runat="server" id="lblDIJVConflict" style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpDIJVConflict">
                            <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                            <asp:ListItem Value="1"> Y </asp:ListItem>
                            <asp:ListItem Value="0"> N </asp:ListItem>
                            <asp:ListItem Value="2"> N/A </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="valDIJVConflict" runat="server" Display="None"
                        ControlToValidate="drpDIJVConflict" InitialValue="-1"
                        ErrorMessage="DIJV Conflict is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>

                    
                </div>
                <br />

                <div class="row">
                    <div class="col-md-2">
                        Notes
                    </div>
                     <div class="col-md-4">
                        <asp:TextBox runat="server" TextMode="MultiLine" class="form-control" ID="txtNote"></asp:TextBox>
                    </div>
                    <%--<asp:RequiredFieldValidator ID="valtxtNote" runat="server" Display="None"
                        ControlToValidate="txtNote"
                        ErrorMessage="Notes is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>--%>

                     <div class="col-md-2">
                        Conflicts
                    </div>
                     <div class="col-md-4">
                        <asp:TextBox runat="server" TextMode="MultiLine" class="form-control" ID="txtConflicts"></asp:TextBox>
                    </div>
                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                        ControlToValidate="txtConflicts"
                        ErrorMessage="Conflicts is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>--%>


                </div>
                
                <br />
                <div class="row">
                    <div class="col-md-2">
                        All documents are filed in line with IJV document retention policies:<label runat="server" style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpAllDocument">
                            <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                            <asp:ListItem Value="1"> Y </asp:ListItem>
                            <asp:ListItem Value="0"> N </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="valAllDocument" runat="server" Display="None"
                        ControlToValidate="drpAllDocument" InitialValue="-1"
                        ErrorMessage="All document is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>

                     <div class="col-md-2">
                        Is there any Confidentiality agreement/NDA clauses relevant to the DIJV?<label runat="server" style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpNDA">
                            <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                            <asp:ListItem Value="1"> Y </asp:ListItem>
                            <asp:ListItem Value="0"> N </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="valNDA" runat="server" Display="None"
                        ControlToValidate="drpNDA" InitialValue="-1"
                        ErrorMessage="Confidentiality agreement/NDA is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>   
                </div>                
                <br />                

               <br />
                <div id="FileUploadDiv" runat="server">
                    <div class="row">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </div>
                    <div class="row">
                        Attach only the final work order and screenshot evidencing conflict check closed.
                    </div>
                    <div class="row">
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="Upload" />
                        <span>Max file size is 1MB (Only JPEG,JPG,PDF,Word,Excel and Outlook Email format are allowed)</span>
                    </div>
                </div>
                <br />

                <div id="dvMsg" style="background-color: Red; color: White; width: 190px; padding: 3px; display: none;">
                    Maximum size allowed is 1 MB
                </div>

                <div class="row">
                    <asp:GridView ID="GridView1" runat="server"
                        AutoGenerateColumns="false" CssClass="table">
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="File Name" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="View" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                                        CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Delete" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="DeleteFile"
                                        CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="row">
                    <div class="col-md-8">                        
                        I confirm that the documents and items, where indicated on the attached checklist have been completed and filed on the working papers.  All documents have been filed.
                    </div>
                </div>

                <div class="row">&nbsp;</div>
                <div class="row" style="background-color: greenyellow">
                    <span><b>EPCC Stage 3 reveiwed by (DIJV Manager):</b></span>
                </div>
                <br />                
                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-lg-2"><b>Name:<label style="color: red">*</label></b></div>
                    <div class="col-lg-5">
                        <asp:DropDownList runat="server" ID="drpDIJVMngr" Width="200px"></asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="valManagerReq" runat="server" Display="None"
                        ControlToValidate="drpDIJVMngr" ErrorMessage="DIJV Manager email is required"
                        ForeColor="Red"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <div class="col-lg-1"></div>

                </div>
                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-lg-2"><b>Date:</b></div>
                    <div class="col-lg-5">
                        <asp:TextBox runat="server" ID="dtAppOrReject" Width="200px" ReadOnly="true" Enabled="false"></asp:TextBox>
                    </div>

                </div>
                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-lg-2"><b>Approval:</b></div>
                    <div class="col-lg-5">
                        <asp:TextBox runat="server" ID="txtStatusDIJV" Width="200px" ReadOnly="true" Enabled="false"></asp:TextBox>
                    </div>

                </div>
                <div class="row">&nbsp;</div>
                <div class="row" style="background-color: greenyellow">
                    <span><b>EPCC Stage 3 reveiwed by (DIJV Approver):</b></span>
                </div>
                <br />

                <div class="row">
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-lg-2"><b>Name:<label style="color: red">*</label></b></div>
                        <div class="col-lg-2">
                            <asp:DropDownList runat="server" ID="drpDIJVPartner" Width="200px"></asp:DropDownList>
                        </div>
                        <asp:RequiredFieldValidator ID="valPartnerEmail" runat="server" Display="None"
                            ControlToValidate="drpDIJVPartner" ErrorMessage="DIJV Partner email is required"
                            ForeColor="Red"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <div class="col-lg-1"></div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-lg-2"><b>Date:</b></div>
                        <div class="col-lg-5">
                            <asp:TextBox runat="server" ID="dtPartnerApprRej" ReadOnly="true" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-lg-2"><b>Approval:</b></div>
                        <div class="col-lg-5">
                            <asp:TextBox runat="server" ID="txtStatusPartner" ReadOnly="true" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                </div>
                
                <br />                
                <div class="row" align="center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                </div>
                <div class="row">&nbsp;</div>
                <div id="divDIJVApprReject" runat="server" class="row" align="center">
                    <asp:Button ID="btnDIJVAppr" runat="server" Text="Approve" OnClick="btnDIJVAppr_Click" />
                    <asp:Button ID="btnDIJVReject" CausesValidation="false" runat="server" Text="Reject" OnClick="btnDIJVReject_Click" />

                </div>
            </div>
        </div>
        <div style="display: none">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewerWO" runat="server">
                <LocalReport ReportPath="Report/EPCC3Report.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </div>

    </form>

</asp:Content>
