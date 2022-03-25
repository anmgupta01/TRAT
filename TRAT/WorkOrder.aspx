<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkOrder.aspx.cs" Inherits="TRAT.WorkOrder" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="formSection" ContentPlaceHolderID="formSection" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            calculateHours();
        });
        function scrollToWorkOrder() {
            document.getElementById('<%= divWorkOrder.ClientID %>').scrollIntoView();
        }
        function calculateHours() {

            var sum = 0;
            //iterate through each textboxes and add the values
            //$(".sum1").each(function () {
            //    //add only if the value is number
            //    if (!isNaN(this.value) && this.value.length != 0) {
            //        sum += parseFloat(this.value);
            //    }

            //});
            sum = (document.getElementById('txtADHours').value != "" ? parseFloat(document.getElementById('txtADHours').value) : 0)
                + (document.getElementById('txtManagerHours').value != "" ? parseFloat(document.getElementById('txtManagerHours').value) : 0)
                + (document.getElementById('txtDirectorHours').value != "" ? parseFloat(document.getElementById('txtDirectorHours').value) : 0)
                + (document.getElementById('txtDMHours').value != "" ? parseFloat(document.getElementById('txtDMHours').value) : 0)
                + (document.getElementById('txtAMHours').value != "" ? parseFloat(document.getElementById('txtAMHours').value) : 0)
                + (document.getElementById('txtExecutiveHours').value != "" ? parseFloat(document.getElementById('txtExecutiveHours').value) : 0)
                + (document.getElementById('txtExec1').value != "" ? parseFloat(document.getElementById('txtExec1').value) : 0);
            $('#txtTotal').text(sum);
            $("#txtTotal").html(sum.toFixed(2));

        }
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

        <div class="container">
            <h3>Search Project for Work Order Generation</h3>
            <div class="row">
                <div class="col-lg-2">Project Name</div>
                <div class="col-lg-4">
                    <asp:TextBox runat="server" ID="txtProjectName"></asp:TextBox>
                    <asp:Button runat="server" ID="btnSearchProject" CausesValidation="false" Text="Search" OnClick="btnSearchProject_Click" />
                </div>
            </div>
            <div class="row">&nbsp;</div>

            <%--<div id="alert" runat="server" class="alert alert-success fade in" visible="false">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <label id="lblAlert" runat="server"></label>
            </div>--%>
            <div class="row">
                <asp:GridView ID="gvProjects" runat="server" CssClass="table" AutoGenerateColumns="false" GridLines="None"
                    DataKeyNames="Id" OnRowCommand="gvProjects_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="Work Order Number" DataField="WorkOrderNumber" />
                        <asp:BoundField HeaderText="Project Code" DataField="ProjectCode" />

                        <asp:BoundField HeaderText="Project" DataField="Name" />
                        <asp:BoundField HeaderText="Partner" DataField="USerID" />
                        <asp:BoundField HeaderText="Office" DataField="Office" />
                        <asp:TemplateField HeaderText="WO Status">
                            <ItemTemplate>
                                <%# Eval("IsWOSubmitted").ToString() == "True" ? "Submitted" : "Not Submitted" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DIJV Approved">
                            <ItemTemplate>
                                <%# Eval("isDIJVApproved").ToString() == "True" ? "Yes" : "No" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MF Approved">
                            <ItemTemplate>
                                <%# Eval("isMFApproved").ToString() == "True" ? "Yes" : "No" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="GenerateWO" runat="server" Text="Generate Work Order" CommandName="GenerateWO"
                                    CommandArgument='<%#Eval("Id")%>' CausesValidation="false"
                                    Enabled='<%#Convert.IsDBNull(Eval("WorkOrderNumber")) || Eval("WorkOrderNumber").ToString() == ""%>'>

                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="ViewEdit" CausesValidation="false" runat="server" Text="View\Edit" CommandName="edt" CommandArgument='<%#Eval("Id")%>'
                                    Enabled='<%#Eval("WorkOrderNumber").ToString() != ""%>'>
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="Revise" CausesValidation="false" runat="server" Text="Revise Work Order" CommandName="Revise" CommandArgument='<%#Eval("Id")%>'
                                    Visible='<%# Convert.IsDBNull(Eval("isMFApproved")) ?  false : Convert.ToBoolean(Eval("isMFApproved"))%>'>
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
                <div class="row" style="background-color: greenyellow">
                    <span><b>Important Notice</b></span>
                </div>
                <br />
                <div class="row">
                    This Work Order incorporates the agreed terms and conditions of the Master Service Agreement (the "MSA") between the Member Firm ("MF") and 		
                    Deloitte India Joint Venture ("DIJV").	
                </div>
                <br />
                <div class="row">
                    In accordance with the terms of the Master Services Agreement between DGFA and your Deloitte Member Firm “You”, DGFA will undertake all work procedures in accordance 		
                    with your instructions under your guidance and as an extension of your engagement team. Notwithstanding the support provided by DGFA, You retain ultimate responsibility for 		
                    the review, assessment and acceptance of all work (“Outputs”) performed by DGFA as part of your engagement (which should include as a minimum your own quality control 		
                    procedures) and assume all responsibility for determining the suitability of the Outputs for your purposes.  You will remain solely responsible to your Client for any Deliverable 		
                    provided by you to them which may include the Outputs from our services.
                </div>
                <br />
                <div class="row" style="background-color: greenyellow">
                    <span><b>General information</b></span>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-2"><b>Work Order Version: </b></div>
                    <div class="col-lg-4">
                        <asp:DropDownList runat="server" ID="drpVersion" ReadOnly="true" Enabled="false" Width="200px">
                            <asp:ListItem Value="Initial">Initial</asp:ListItem>
                            <asp:ListItem Value="Revise">Revise</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-2"><b>DIJV conflict check number: </b></div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="txtConflictCheck" ReadOnly="true" Enabled="false" Width="200px"></asp:TextBox>
                    </div>
                    <div class="col-lg-2"><b>DIJV work order number: </b></div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="txtWONumber" ReadOnly="true" Enabled="false" Width="200px"></asp:TextBox>
                    </div>
                </div>
                <div class="row">&nbsp;</div>

                <div class="row">
                    <div class="col-lg-2"><b>DIJV project code: </b></div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="txtProjectCode" ReadOnly="true" Enabled="false" Width="200px"></asp:TextBox>
                    </div>

                </div>
                <div class="row">&nbsp;</div>

                <div class="row">
                    <div class="col-lg-2"><b>Referring Member Firm: </b></div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="txtMemberFirm" ReadOnly="true" Enabled="false" Width="200px"></asp:TextBox>
                    </div>
                    <div class="col-lg-2"><b>Referring Location/Office: </b></div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="txtRefOffice" ReadOnly="true" Enabled="false" Width="200px"></asp:TextBox>
                    </div>

                </div>
                <div class="row">&nbsp;</div>

                <div class="row">
                    <div class="col-lg-2"><b>Engagement Project Name: </b></div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="txtEngmntName" ReadOnly="true" Enabled="false" Width="200px"></asp:TextBox>
                    </div>
                    <div class="col-lg-2"><b>Engagement Type: </b></div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="txtEngmntType" ReadOnly="true" Enabled="false" Width="200px"></asp:TextBox>
                    </div>

                </div>
                <div class="row">&nbsp;</div>

                <div class="row">
                    <div class="col-lg-2"><b>MF Engagement Partner: </b></div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="txtEngmntPartner" ReadOnly="true" Enabled="false" Width="200px"></asp:TextBox>
                    </div>
                    <div class="col-lg-2"><b>MF Engagement Manager: </b></div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="txtEngmntManager" ReadOnly="true" Enabled="false" Width="200px"></asp:TextBox>
                    </div>
                </div>
                <div class="row">&nbsp;</div>

                <div class="row">
                    <div class="col-lg-2"><b>DIJV Engagement Partner: </b></div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="txtDIJVPartner" ReadOnly="true" Enabled="false" Width="200px"></asp:TextBox>
                    </div>

                </div>
                <div class="row">&nbsp;</div>


                <div class="row" style="display: none">
                    <div class="col-lg-2"><b>Client Name:<label style="color: red">*</label></b></div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="txtClientName" Width="200px"></asp:TextBox>
                    </div>
                    <%--<asp:RequiredFieldValidator ID="valClientName" runat="server" Display="None"
                        ControlToValidate="txtClientName"
                        ErrorMessage="Client name is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>--%>
                </div>
                <div class="row">&nbsp;</div>

                <div class="row">
                    <div class="col-lg-2"><b>Job Code: </b></div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="txtJobCode" ReadOnly="true" Enabled="false" Width="200px"></asp:TextBox>
                    </div>

                </div>
                <div class="row">&nbsp;</div>

                <div class="row">
                    <div class="col-lg-2"><b>Proposed Project Start Date: </b></div>
                    <div class="col-lg-4">
                        <input type="Date" id="dtStartDate" runat="server" readonly="true" style="width: 200px" />
                    </div>

                    <div class="col-lg-2">
                        <b>Estimated End Date:<label style="color: red">*</label>
                        </b>
                    </div>
                    <div class="col-lg-4">
                        <input type="Date" id="dtEndDt" runat="server" style="width: 200px" />
                    </div>
                    <asp:RequiredFieldValidator ID="valStartDate" runat="server" Display="None"
                        ControlToValidate="dtEndDt" ErrorMessage="Project end date is required"
                        ForeColor="Red"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <div class="row">&nbsp;</div>

                <div class="row" style="background-color: greenyellow">
                    <span><b>Compliance/Risk Matters</b></span>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-6"><b>MF Conflict Check information forwarded to DIJV and MF confirmed their conditions have been met : </b></div>
                    <div class="col-lg-3">
                        <asp:TextBox runat="server" ID="txtMFConflictCheck" ToolTip="MF Engagement Manager Name" Width="200px"></asp:TextBox>
                    </div>
                    <div class="col-lg-3">
                        <asp:CheckBox runat="server" ID="chkCC" Visible="false"></asp:CheckBox>
                        <input type="Date" id="dtMFConflictCheck" runat="server" style="width: 200px" />
                    </div>
                </div>


                <div class="row">
                    <div class="col-lg-6"><b>DIJV TS internal risk confirmation obtained? : </b></div>
                    <div class="col-lg-3">
                        <asp:TextBox runat="server" ID="txtInternalRisk" ToolTip="Internal Risk Confirmation Obtained" Width="200px"></asp:TextBox>
                    </div>
                    <div class="col-lg-3">
                        <asp:CheckBox runat="server" ID="chkInternal" Visible="false"></asp:CheckBox>
                        <input type="Date" id="dtInternalRisk" runat="server" style="width: 200px" />
                    </div>
                </div>


                <%--<div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-lg-6"><b>DRMS/TOP/CAEA forwarded to DIJV : </b></div>
                    <div class="col-lg-3">
                        <asp:TextBox runat="server" ID="txtDRMS" Width="200px"></asp:TextBox>
                    </div>
                    <div class="col-lg-3">
                        <asp:CheckBox runat="server" ID="chkDRMS" Visible="false"></asp:CheckBox>
                        <input type="Date" id="dtDRMS" runat="server" style="width: 200px" />
                    </div>

                </div>--%>
                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-lg-6"><b>DIJV conflict check submitted : </b></div>
                    <div class="col-lg-3">
                        <asp:TextBox runat="server" ID="txtDIJVSubmitted" ToolTip="DIJV Process Lead" Width="200px"></asp:TextBox>
                    </div>
                    <div class="col-lg-3">
                        <asp:CheckBox runat="server" ID="chkDIJV" Visible="false"></asp:CheckBox>
                        <input type="Date" id="dtDIJVSubmitted" runat="server" style="width: 200px" />
                    </div>

                </div>
                <div class="row">&nbsp;</div>

                <div class="row">
                    <div class="col-lg-6"><b>DIJV Conflict Check approved by DCCS and DIJV confirmed their conditions have been met : </b></div>
                    <div class="col-lg-3">
                        <asp:TextBox runat="server" ID="txtCCCompleted" ToolTip="DIJV Process Lead" Width="200px"></asp:TextBox>
                    </div>
                    <div class="col-lg-3">
                        <asp:CheckBox runat="server" ID="chkCCCompleted" Visible="false"></asp:CheckBox>
                        <input type="Date" id="dtCCCompleted" runat="server" style="width: 200px" />
                    </div>
                </div>
                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-lg-6"><b>Data sharing confirmed ok by MF (i.e. EL is on standard MF T&C's, NDA signed by MF considered, etc) : </b></div>
                    <div class="col-lg-3">
                        <asp:TextBox runat="server" ID="txtDataSharing" ToolTip="MF Engagemen Manager" Width="200px"></asp:TextBox>
                    </div>
                    <div class="col-lg-3">
                        <asp:CheckBox runat="server" ID="chkDataSharing" Visible="false"></asp:CheckBox>
                        <input type="Date" id="dtDataSharing" runat="server" style="width: 200px" />
                    </div>

                </div>
                <%--6 Newly added field on dated 4 May 2020 as per Shruti Mantri instruction By Vijay Bhagat--%>
                <%----------------------------------------------------------------------------------------------------%>
                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-lg-6"><b>Does the transaction relate to Government/Public or defence/aerospace sectors: </b></div>
                    <div class="col-lg-3">
                        <asp:TextBox runat="server" ID="txtTransactionRelate" ToolTip="Does the transaction relates" Width="200px"></asp:TextBox>
                    </div>
                    <div class="col-lg-3">
                        <asp:CheckBox runat="server" ID="ChkTransactionRelate" Visible="false"></asp:CheckBox>
                        <input type="Date" id="DtTransactionRelate" runat="server" style="width: 200px" />
                    </div>
                </div>

                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-lg-6"><b>Is the MF working for another member firm (in which case they have an agreement with them to use the DIJV): </b></div>
                    <div class="col-lg-3">
                        <asp:TextBox runat="server" ID="txtAreYouWorking" ToolTip="Are you working" Width="200px"></asp:TextBox>
                    </div>
                    <div class="col-lg-3">
                        <asp:CheckBox runat="server" ID="ChkAreYouWorking" Visible="false"></asp:CheckBox>
                        <input type="Date" id="dtAreYouWorking" runat="server" style="width: 200px" />
                    </div>
                </div>

                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-lg-6"><b>Is there a confidentiality agreement/NDA clauses relevant to the DIJV: </b></div>
                    <div class="col-lg-3">
                        <asp:TextBox runat="server" ID="txtIsThereConfidentiality" ToolTip="Is There an confidentiality" Width="200px"></asp:TextBox>
                    </div>
                    <div class="col-lg-3">
                        <asp:CheckBox runat="server" ID="ChkIsThereConfidentiality" Visible="false"></asp:CheckBox>
                        <input type="Date" id="dtIsThereConfidentiality" runat="server" style="width: 200px" />
                    </div>
                </div>

                <div class="row">&nbsp;</div>
                <%----------------------------------------------------------------------------------------------------%>
                <div class="row">
                    <div class="col-lg-6"></div>
                    <div class="col-lg-3">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" Visible="true" />
                    </div>
                    <div class="col-lg-3">
                    </div>
                </div>

                <div class="row">&nbsp;</div>
                <div class="row" style="background-color: greenyellow">
                    <span><b>Engagement Information / Services required</b></span>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-4">
                        <b>Overview of Engagement:<label style="color: red">*</label>
                        </b>
                    </div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="txtOverview" ToolTip="Overview of Engagement" Width="600px" TextMode="MultiLine"
                            Rows="2"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="valOverview" runat="server" Display="None"
                        ControlToValidate="txtOverview"
                        ErrorMessage="Overview of Engagement is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>

                </div>
                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-lg-4">
                        <b>Overview of Services required from DIJV:<label style="color: red">*</label>
                        </b>
                    </div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="txtServicesRequired" Width="600px" TextMode="MultiLine"
                            Rows="3"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="valServicesRequired" runat="server" Display="None"
                        ControlToValidate="txtServicesRequired"
                        ErrorMessage="Overview of Services required from DIJV is a required field."
                        ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-lg-7 "><b>Briefing of DIJV team has taken place including: (refer also to Best Practice in User Guide): </b></div>
                    <div class="col-lg-2">
                        <asp:TextBox runat="server" ID="txtBriefingOfDIJVTeam" Width="200px"></asp:TextBox>
                    </div>
                    <div class="col-lg-2" style="float: left !important; padding-left: 49px;">
                        <input type="Date" id="dtBriefingOfDIJVTeam" runat="server" style="width: 200px" />
                    </div>
                    <div class="row">
                        <div class="col-lg-12" style="padding-left: 30px !important"><b>- description of envisaged work areas to be performed by DIJV team;</b></div>
                        <div class="col-lg-12" style="padding-left: 30px !important"><b>- overview of specific focus areas, risks or difficulties connected with DIJV tasks; </b></div>
                        <div class="col-lg-12" style="padding-left: 30px !important"><b>- description of planned communication, deadlines and specific filing / version control guidance</b></div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-lg-4"><b>Number of hours required:</b></div>

                    </div>
                    <div class="row">&nbsp;</div>

                    <div class="row">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-2">
                            <asp:Label ID="lblDirector" runat="server">Director</asp:Label><label style="color: red">*</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox runat="server" class="sum1" min="0" placeholder="0" onchange="calculateHours()" ClientIDMode="Static" ID="txtDirectorHours" type="number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>

                    <div class="row">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-2">
                            <asp:Label ID="lblAD" runat="server">Assistant Director</asp:Label><label style="color: red">*</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox runat="server" class="sum1" min="0" placeholder="0" onchange="calculateHours()" ClientIDMode="Static" ID="txtADHours" type="number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>

                    <div class="row">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-2">
                            <asp:Label ID="lblManager" runat="server">Manager</asp:Label><label style="color: red">*</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox runat="server" placeholder="0" min="0" onchange="calculateHours()" ID="txtManagerHours" class="sum1" ClientIDMode="Static" type="number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>

                    <div class="row">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-2">
                            <asp:Label ID="lblDM" runat="server">Deputy Manager</asp:Label><label style="color: red">*</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox runat="server" class="sum1" placeholder="0" min="0" onchange="calculateHours()" ClientIDMode="Static" ID="txtDMHours" type="number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>

                    <div class="row">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-2">
                            <asp:Label ID="lblAM" runat="server">Assistant Manager</asp:Label><label style="color: red">*</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox runat="server" class="sum1" min="0" placeholder="0" onchange="calculateHours()" ClientIDMode="Static" ID="txtAMHours" type="number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>

                    <div class="row">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-2">
                            <asp:Label ID="lblExec" runat="server">Executive</asp:Label><label style="color: red">*</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox runat="server" class="sum1" placeholder="0" min="0" onchange="calculateHours()" ClientIDMode="Static" ID="txtExecutiveHours" type="number"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">&nbsp;</div>

                    <div class="row">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-2">
                            <asp:Label ID="lblExec1" runat="server">Executive</asp:Label><label style="color: red">*</label>
                        </div>
                        <div class="col-lg-4">
                            <asp:TextBox runat="server" class="sum1" placeholder="0" min="0" onchange="calculateHours()" ClientIDMode="Static" ID="txtExec1" type="number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-2">Total</div>
                        <div class="col-lg-4">
                            <span id="txtTotal"></span>
                        </div>
                    </div>


                    <div class="row" style="visibility: hidden">
                        <div class="col-lg-4"><b>Shared Drive Path:</b></div>
                        <div class="col-lg-4">
                            <asp:TextBox runat="server" ID="txtSharedDrive" Width="600px"></asp:TextBox>
                        </div>

                    </div>
                    <div class="row">&nbsp;</div>

                    <div class="row">
                        <div class="col-lg-4"><b>Names and contact details of DIJV team members:<label style="color: red">*</label></b></div>
                        <div class="col-lg-4">
                            DIJV Senior Team Lead
                        <asp:TextBox runat="server" ID="txtContact1" Width="200px" ReadOnly="true" Enabled="false"></asp:TextBox><div class="row">&nbsp;</div>
                            DIJV Process Lead
                        <asp:TextBox runat="server" ID="txtContact2" Width="200px"></asp:TextBox><div class="row">&nbsp;</div>
                            DIJV Other
                        <asp:TextBox runat="server" ID="txtContact3" Width="200px"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                        ControlToValidate="txtContact2" ErrorMessage="DIJV contact email is required"
                        ForeColor="Red"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>--%>

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ErrorMessage="Invalid Email" ControlToValidate="txtContact2"
                                SetFocusOnError="True" ForeColor="Red"
                                ValidationExpression="\w+([-+.']\w+)*@deloitte*\.\w+([-.]\w+)*">
                            </asp:RegularExpressionValidator>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                        ControlToValidate="txtContact3" ErrorMessage="DIJV contact email is required"
                        ForeColor="Red"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>--%>

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ErrorMessage="Invalid Email" ControlToValidate="txtContact3"
                                SetFocusOnError="True" ForeColor="Red"
                                ValidationExpression="\w+([-+.']\w+)*@deloitte*\.\w+([-.]\w+)*">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <span><b>The above are estimated hours – actual hours advised / invoiced to the on-shore Project / Central Teams as required (and recorded to Member Firm SAP/SWIFT if agreed). 
              Please approve works order for DIJV to commence work.
An updated works order with actual hours can be issued if required at end of project
                        </b></span>
                    </div>

                    <div class="row">&nbsp;</div>

                    <div class="row" style="background-color: greenyellow">
                        <span><b>Signatures</b></span>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-7" style="background-color: aquamarine">
                            <span><b>DIJV Approver</b></span>
                        </div>
                        <div class="col-lg-1"></div>
                        <div class="col-lg-4" style="background-color: aquamarine">
                            <span><b>Member Firm Engagement Partner</b></span>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>

                    <div class="row">
                        <div class="col-lg-2"><b>Name:<label style="color: red">*</label></b></div>
                        <div class="col-lg-5">
                            <asp:DropDownList runat="server" ID="drpDIJVPartner" Width="200px"></asp:DropDownList>
                        </div>
                        <div class="col-lg-1"></div>
                        <div class="col-lg-2"><b>Name:<label style="color: red">*</label></b></div>
                        <div class="col-lg-2">
                            <asp:TextBox runat="server" ID="txtMemberFirmPartnerAppr" ReadOnly="true" Enabled="false"></asp:TextBox>
                            <%--<asp:RegularExpressionValidator ID="regexPartnerEmail" runat="server"
                                ErrorMessage="Invalid Email" ControlToValidate="txtMemberFirmPartnerAppr"
                                SetFocusOnError="True" ForeColor="Red"
                                ValidationExpression="\w+([-+.']\w+)*@deloitte*\.\w+([-.]\w+)*">
                            </asp:RegularExpressionValidator>--%>
                        </div>
                        <asp:RequiredFieldValidator ID="valPartnerEmail" runat="server" Display="None"
                            ControlToValidate="txtMemberFirmPartnerAppr" ErrorMessage="Engagement Manager email is required"
                            ForeColor="Red"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                    <div class="row">&nbsp;</div>

                    <div class="row">
                        <div class="col-lg-2"><b>Date:<label style="color: red">*</label></b></div>
                        <div class="col-lg-5">
                            <asp:TextBox runat="server" ID="dtAppOrReject" Width="200px" ReadOnly="true" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-lg-1"></div>
                        <div class="col-lg-2"><b>Date:<label style="color: red">*</label></b></div>
                        <div class="col-lg-2">
                            <asp:TextBox runat="server" ID="dtPartnerApprRej" ReadOnly="true" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>

                    <div class="row">
                        <div class="col-lg-2"><b>Approval:<label style="color: red">*</label></b></div>
                        <div class="col-lg-5">
                            <asp:TextBox runat="server" ID="txtStatusDIJV" Width="200px" ReadOnly="true" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-lg-1"></div>
                        <div class="col-lg-2"><b>Approval:<label style="color: red">*</label></b></div>
                        <div class="col-lg-2">
                            <asp:TextBox runat="server" ID="txtStatusPartner" ReadOnly="true" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">&nbsp;</div>
                    <div class="row">
                        <span><b>Please refer to relevant notes on accompanying approval request email.</b></span>

                    </div>

                    <div class="row">
                        <span><b>EPCC Stage 1.</b></span>

                    </div>

                    <div class="row">&nbsp;</div>

                    <div class="row">
                        <div class="row">
                        </div>
                        <div class="col-lg-1"></div>
                    </div>

                    <hr />
                    <div class="row" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />

                    </div>
                    <div class="row">&nbsp;</div>

                    <div id="divDIJVApprReject" class="row" align="center">
                        <asp:Button ID="btnDIJVAppr" runat="server" Text="Approve" OnClick="btnDIJVAppr_Click" />
                        <asp:Button ID="btnDIJVReject" CausesValidation="false" runat="server" Text="Reject" OnClick="btnDIJVReject_Click" />

                    </div>
                    <div class="row">&nbsp;</div>

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
