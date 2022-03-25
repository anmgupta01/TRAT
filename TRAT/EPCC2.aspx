<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EPCC2.aspx.cs" Inherits="TRAT.EPCC2" %>

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

            $('#<%=btnDIJVAppr.ClientID %>').click(
                function () {
                    document.getElementById("btnDIJVAppr").disabled = true;
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
        <asp:HiddenField runat="server" ID="hidEPCC2ID" />


        <div class="container">
            <h3>Search Work Order For EPCC 2</h3>
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
                        <asp:TemplateField HeaderText="EPCC Status">
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
                                <asp:LinkButton ID="GenerateWO" runat="server" Text="Generate EPCC2" CommandName="GenerateWO"
                                    CommandArgument='<%#Eval("Id")%>' CausesValidation="false"
                                    Enabled='<%#Eval("EPCC_ProjectID").ToString() == ""%>'>
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="ViewEdit" CausesValidation="false" runat="server" Text="View\Edit" CommandName="edt" CommandArgument='<%#Eval("Id")%>'
                                    Enabled='<%#Eval("EPCC_ProjectID").ToString() != ""%>'>
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
                    <span><b>Stage 2 Procedures</b></span>
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
                        Canadian review project:<label runat="server" id="lblCanadian" style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpCanadian">
                            <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                            <asp:ListItem Value="1"> Y </asp:ListItem>
                            <asp:ListItem Value="0"> N </asp:ListItem>
                        </asp:DropDownList>
                    </div>


                    <asp:RequiredFieldValidator ID="valCAnadianReq" runat="server" Display="None"
                        ControlToValidate="drpCanadian" InitialValue="-1"
                        ErrorMessage="Canadian review project is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>

                    <div class="col-md-2">
                        Insider lists:<label runat="server" id="lblInsider" style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpInsider">
                            <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                            <asp:ListItem Value="1"> Y </asp:ListItem>
                            <asp:ListItem Value="0"> N </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="valInsiderReq" runat="server" Display="None"
                        ControlToValidate="drpInsider" InitialValue="-1"
                        ErrorMessage="Insider lists is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>

                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">
                        Work order issued and saved at:<label style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" TextMode="MultiLine" class="form-control" ID="txtWOIssued"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="valWOIssuedRequired" runat="server" Display="None"
                        ControlToValidate="txtWOIssued"
                        ErrorMessage="Work order issued and saved at is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>


                    <div class="col-md-2">
                        Notes [Any unusual circumstances to be written down] :
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" TextMode="MultiLine" class="form-control" ID="txtNotes"></asp:TextBox>
                    </div>


                </div>
                <br />
                <div class="row" style="background-color: greenyellow">
                    <span><b>Conflicts</b></span>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">
                        Have all conditions from the Member Firm conflict check that relate to the DIJV been met and evidence documented on file?<label runat="server" style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpHaveAllCondition">
                            <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                            <asp:ListItem Value="1"> Y </asp:ListItem>
                            <asp:ListItem Value="0"> N </asp:ListItem>
                            <asp:ListItem Value="2"> N/A </asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <asp:RequiredFieldValidator ID="valHavAllReq" runat="server" Display="None"
                        ControlToValidate="drpHaveAllCondition" InitialValue="-1"
                        ErrorMessage="Have all conditions is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>


                    <div class="col-md-2">
                        Does DIJV conflict check include all the parties identified on the Member Firm Conflict Check?<label runat="server" id="Label2" style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpDoesDIJV">
                            <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                            <asp:ListItem Value="1"> Y </asp:ListItem>
                            <asp:ListItem Value="0"> N </asp:ListItem>
                            <asp:ListItem Value="2"> N/A </asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <asp:RequiredFieldValidator ID="valDoesDIJVReq" runat="server" Display="None"
                        ControlToValidate="drpDoesDIJV" InitialValue="-1"
                        ErrorMessage="Does DIJV conflict check is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">
                        Have we complied with the DIJV conflict check result - and this evidenced on file as appropriate:<label runat="server" style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpAreThere">
                            <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                            <asp:ListItem Value="1"> Y </asp:ListItem>
                            <asp:ListItem Value="0"> N </asp:ListItem>
                            <asp:ListItem Value="2"> N/A </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="valAllThereReq" runat="server" Display="None"
                        ControlToValidate="drpAreThere" InitialValue="-1"
                        ErrorMessage="Are there conditions is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>

                    <div class="col-md-2">
                        Has a restricted folder been created?<label runat="server" id="Label1" style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpHasARestricted">
                            <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                            <asp:ListItem Value="1"> Y </asp:ListItem>
                            <asp:ListItem Value="0"> N </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="valHasRestrictedReq" runat="server" Display="None"
                        ControlToValidate="drpHasARestricted" InitialValue="-1"
                        ErrorMessage="restricted folder is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">
                        Is MF confirming checklist/email response attached (mandatory attachment)?<label runat="server" style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpMFConfirming">
                            <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                            <asp:ListItem Value="1"> Y </asp:ListItem>
                            <asp:ListItem Value="0"> N </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="valMFConfirming" runat="server" Display="None"
                        ControlToValidate="drpMFConfirming" InitialValue="-1"
                        ErrorMessage="MF confirming email or checklist attached is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                    <div class="col-md-2">
                        ALL Conflict check(s), other take on data and MF confirming emails / checklist and data saved at:<label style="color: red">*</label>
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" TextMode="MultiLine" class="form-control" ID="txtConflictCheckEPCC"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="valAllConflictReq" runat="server" Display="None"
                        ControlToValidate="txtConflictCheckEPCC"
                        ErrorMessage="ALL Conflict check(s) is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>
                <br />
                <div class="row">
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
                <div id="FileUploadDiv" runat="server">
                    <div class="row">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </div>
                    <div class="row">
                        Attach only latest work order and evidence of data sharing confirmation from MF.
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
                <div class="row">&nbsp;</div>
                <div class="row" style="background-color: greenyellow">
                    <span><b>EPCC Stage 2 reveiwed by (DIJV Manager):</b></span>
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
                <div class="row">
                    <div class="col-md-8">                        
                        I confirm that the documents and items, where indicated on the attached checklist have been completed and filed on the working papers.  All documents have been filed.
                    </div>
                </div>

                <div class="row">&nbsp;</div>
                <div class="row" style="background-color: greenyellow">
                    <span><b>EPCC Stage 2 reveiwed by (DIJV Enagagement Partner):</b></span>
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
                <div id="divDIJVApprReject" class="row" align="center">
                    <asp:Button ID="btnDIJVAppr" runat="server" Text="Approve" OnClick="btnDIJVAppr_Click" />
                    <asp:Button ID="btnDIJVReject" CausesValidation="false" runat="server" Text="Reject" OnClick="btnDIJVReject_Click" />

                </div>
            </div>
        </div>
        <div style="display: none">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewerWO" runat="server">
                <LocalReport ReportPath="Report/EPCC2Report.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </div>

    </form>

</asp:Content>
