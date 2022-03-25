<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="ProjectForm.aspx.cs" Inherits="TRAT.ProjectForm" %>

<asp:Content ID="formSection" ContentPlaceHolderID="formSection" runat="server">
    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }

        .gridview {
            font-family: Arial;
            background-color: transparent;
            border: solid 1px #CCCCCC;
            width: 100%;
            border-collapse: collapse;
            color: #344
        }

        .gridViewHeader {
            padding: 4px;
            padding-top: 4px;
            padding-right: 4px;
            padding-bottom: 4px;
            padding-left: 4px;
            line-height: 1;
            vertical-align: top;
            border-color: black;
            border-top: 1px solid #ddd;
            column-width: 10px;
            width: 50px
        }

        .gridViewRow {
            padding: 4px;
            padding-top: 4px;
            padding-right: 4px;
            padding-bottom: 4px;
            padding-left: 4px;
            line-height: 1;
            vertical-align: top;
            border-top: 1px solid #ddd;
            border-top-width: 1px;
            border-top-style: solid;
            border-color: black;
            border-top-color: rgb(221, 221, 221);
            column-width: 10px;
            width: 50px
        }

        table.DropdownlistValue > tbody > tr > td > label {
            font-weight: 500;
        }

        table.DropdownlistValue1 > tbody > tr > td > label {
            font-weight: 500;
        }
    </style>


    <script>	
        $(document).ready(function () {
            // File Upload code done by Gauri Wagh instructed by sakshi kandelwal	
            $(function () {
                $('#<%=FileUpload2.ClientID %>').change(
                    function () {
                        var uploadControl = document.getElementById('<%= FileUpload2.ClientID %>');
                        if (uploadControl.files.length > 0) {
                            var fileExtension = ['xls', 'xlsx', 'xlsm', 'xlsb'];
                            if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                                $('#<%=btnSave.ClientID %>').attr("disabled", true);
                                alert("'xlsx' formats are allowed.");
                                uploadControl.value = "";
                                return false;
                            }
                            else if (uploadControl.files[0].size > 1048576) {
                                $('#<%=btnSave.ClientID %>').attr("disabled", true);

                                document.getElementById('dvMsg').style.display = "block";
                                uploadControl.value = "";
                                return false;
                            }
                            else {
                                $('#<%=btnSave.ClientID %>').attr("disabled", false);
                                document.getElementById('dvMsg').style.display = "none";
                                return true;
                            }
                        }
                    })
            })
            // hide the comment popup initially	
            var modal = document.getElementById('myModal');
            modal.style.display = 'none';
            var launch = false;
            function launchModal() { // called from server side code	
                console.log('Function: ' + getFuncName() + ' initiated...');
                launch = true;
                console.log('should modal launch? ' + launch);
                // show the modal popup with duplicate project information	
                document.getElementById('myModal').style.display = 'block';
            }
            function pageLoad() { // ajax function which is called after Page_Load()	
                console.log('Function: ' + getFuncName() + ' initiated...');
                return false;
            }
            function ClosePopup() {
                try { // closes the modal popup	
                    console.log('Function: ' + getFuncName() + ' initiated...');
                    var modal = document.getElementById('myModal');
                    modal.style.display = 'none';
                }
                catch (e) {
                    console.log('Exception occured at function: ' + getFuncName() + ' : Error Description : ' + e.name + ' >> Error Message: ' + e.message);
                }
            }
            function getFuncName() { // gets name of the function that called this function	
                return getFuncName.caller.name
            }




            window.onclick = function (event) {
                // Close the modal when the user clicks anywhere outside of the modal	
                var modal = document.getElementById('myModal');
                if (event.target == modal) {
                    modal.style.display = "none";
                }
            }
        });

        function ValidateRadioButtons(sender, args) {
            debugger;

            var checkBoxList = document.getElementById("<%=drpToolchk.ClientID %>");
            var checkboxes = checkBoxList.getElementsByTagName("input");
            var isValid = false;
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    isValid = true;
                    break;
                }
            }
            args.IsValid = isValid;
        }

    </script>


    <br />
    <br />


    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList"
            EnableClientScript="true" ForeColor="Red"
            HeaderText="You must enter a value in the following fields:" />
        <br />
        <asp:HiddenField runat="server" ID="hidProjectID" />
        <asp:HiddenField runat="server" ID="hidConcessionId" />
        <asp:HiddenField runat="server" ID="hiddrpToolUseId" />
        <div class="row">
            <div class="col-md-2">Project Code: </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" class="form-control" ID="txtProjectCode"></asp:TextBox>
            </div>
        </div>
        <div class="row">&nbsp;</div>

        <div class="row">
            <div class="col-md-2">
                Project Name:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" class="form-control" ID="txtPjctName"></asp:TextBox>

            </div>
            <asp:RequiredFieldValidator ID="valProjectName" runat="server" Display="None"
                ControlToValidate="txtPjctName"
                ErrorMessage="Project name is a required field."
                ForeColor="Red">
            </asp:RequiredFieldValidator>


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
                Referring Location/Office:<label id="lblFirmOffice" runat="server" style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" Style="width: 360px" CssClass="form-control" ID="drpMemLocation" DataValueField="Id" DataTextField="Office"
                    AutoPostBack="true" OnSelectedIndexChanged="drpMemLocation_SelectedIndexChanged">
                    <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                </asp:DropDownList>


                <asp:RequiredFieldValidator ID="valMemberOffice" runat="server" Display="None"
                    ControlToValidate="drpMemLocation" InitialValue="-1"
                    ErrorMessage="Member Firm Office is a required field."
                    ForeColor="Red">
                </asp:RequiredFieldValidator>
            </div>
            <div id="divOthers" runat="server">
                <div class="col-md-2">
                    Others:<label id="lblOthers" style="color: red">*</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" class="form-control" ID="txtOthers"></asp:TextBox>

                </div>
                <asp:RequiredFieldValidator ID="valOthers" runat="server"
                    ForeColor="Red" Display="None"
                    ControlToValidate="txtOthers" ErrorMessage="Please enter the name of the office location"
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
            </div>

        </div>

        <div class="row">&nbsp;</div>

        <div class="row">
            <div class="col-md-2">
                Engagement Partner Email - Member Firm:<label style="color: red">*</label>

            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" ID="txtPartnerMemfirm" class="form-control" ToolTip="Enter a valid member firm email id"></asp:TextBox>
                <br />
                <asp:Button runat="server" align="center" CausesValidation="false" Text="Check" ID="btnCheck_false" Visible="false" OnClick="btnCheck_Click" />
                <asp:Button runat="server" align="center" CausesValidation="false" Text="Check For Duplicate" ID="btnCheck" OnClick="btnCheck_Click" />

            </div>
            <asp:RequiredFieldValidator ID="valPartnerFirm" runat="server"
                ForeColor="Red" Display="None"
                ControlToValidate="txtPartnerMemfirm" ErrorMessage="Engagement Partner email is required"
                SetFocusOnError="True"></asp:RequiredFieldValidator>

            <div class="col-md-2">
                Engagement Manager Email - Member Firm:<label style="color: red">*</label>

            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" class="form-control" ID="txtManagerMemFirm" ToolTip="Enter a valid member firm email id"></asp:TextBox>

            </div>
            <asp:RequiredFieldValidator ID="valEngManFirm" runat="server" Display="None"
                ControlToValidate="txtManagerMemFirm" ErrorMessage="Engagement Manager email is required"
                ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>



        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-2">
                Engagement Partner Name - Member Firm:<label style="color: red">*</label>

            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" ID="txtMFPartnerName" class="form-control" ToolTip="Enter Partner Name"></asp:TextBox>


            </div>
            <div class="col-md-2">
                Engagement Manager Name - Member Firm:<label style="color: red">*</label>

            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" class="form-control" ID="txtMFManagerName" ToolTip="Enter Manager Name"></asp:TextBox>

            </div>

        </div>
        <div class="row">&nbsp;</div>

        <div class="row">
            <div class="col-md-2">
                Engagement Partner - DIJV:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" class="form-control" ID="txtPartnerDIJV" ToolTip="Enter a valid Indian email id"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regexEngPartnerIJV" runat="server"
                    ErrorMessage="Invalid Email" ControlToValidate="txtPartnerDIJV"
                    SetFocusOnError="True" ForeColor="Red"
                    ValidationExpression="\w+([-+.']\w+)*@deloitte*\.\w+([-.]\w+)*">
                </asp:RegularExpressionValidator>
            </div>
            <asp:RequiredFieldValidator ID="valPartnerIJV" runat="server" Display="None"
                ControlToValidate="txtPartnerDIJV" ErrorMessage="Engagement Partner - DIJV email is required"
                ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>
            <div class="col-md-2">
                Engagement Manager - DIJV:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" ID="ddlManagerList" CssClass="form-control">
                    <asp:ListItem Value="-1">--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqManager" runat="server" Display="none"
                    ControlToValidate="ddlManagerList" InitialValue="-1"
                    ErrorMessage="Engagement Manager - DIJV email is required"
                    ForeColor="Red">
                </asp:RequiredFieldValidator>
            </div>

        </div>

        <div class="row">&nbsp;</div>

        <div class="row">
            <div class="col-md-2">
                Project Start Date:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <input type="Date" class="form-control" id="dtStartDate" runat="server" />
            </div>
            <asp:RequiredFieldValidator ID="valStartDate" runat="server" Display="None"
                ControlToValidate="dtStartDate" ErrorMessage="Project start date is required"
                ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>
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
            <div class="col-md-2">
                Sub-Service Line:
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" AutoPostBack="true" Style="width: 360px" class="form-control" DataValueField="SubServiceLineId" DataTextField="Name" ID="drpSubServiceLine" OnSelectedIndexChanged="drpSubServiceLine_SelectedIndexChanged">
                </asp:DropDownList>
            </div>


        </div>

        <div class="row">&nbsp;</div>
        <div class="row" id="divConcessionDocument" runat="server">
            <div class="col-md-2">
                Is Concession:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpConcession" OnSelectedIndexChanged="drpConcession_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                    <asp:ListItem Value="1"> Y </asp:ListItem>
                    <asp:ListItem Value="0"> N </asp:ListItem>
                </asp:DropDownList>

            </div>
            <asp:RequiredFieldValidator ID="valdrpConcession" runat="server" Display="None"
                ControlToValidate="drpConcession" InitialValue="-1" ErrorMessage="Is Concession field is required"
                ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>

            <div class="col-md-2" runat="server" id="lblConcession">
                Concession % (0-100)
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" type="Double" min="0" max="100" step="1" class="form-control" ID="txtConcessionRatio" ToolTip="Concession Ratio" Visible="false"></asp:TextBox>
            </div>
            <asp:RangeValidator ID="valRatio" runat="server" Type="Double" ControlToValidate="txtConcessionRatio" MaximumValue="100" MinimumValue="0"
                ValidationGroup="form" ForeColor="Red" ErrorMessage="Ratio must between 0 to 100" />
        </div>

        <div class="row">&nbsp;</div>



        <div class="row" id="divAnalyticsTool" runat="server">
            <div class="col-md-2">
                Will additional analytics tools be used during the course of the project:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpToolUse" OnSelectedIndexChanged="drpToolUse_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                    <asp:ListItem Value="1"> Y </asp:ListItem>
                    <asp:ListItem Value="0"> N </asp:ListItem>
                </asp:DropDownList>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                    ControlToValidate="drpToolUse" InitialValue="-1" ErrorMessage="Are any analytical tools known required"
                    ForeColor="Red"
                    SetFocusOnError="True"></asp:RequiredFieldValidator>

            </div>

            <div runat="server" id="divshowToolKnown">
                <div class="col-md-2" runat="server" id="lblToolKnown">
                    Please select the tool(s) that will be/might be used on the project<label style="color: red">*</label>
                </div>
                <div class="col-md-4">
                    --Select--
                 <div style="overflow-y: scroll; height: 100px;" runat="server" class="DropdownlistValue1">
                     <asp:CheckBoxList ID="drpToolchk" runat="server" CssClass="DropdownlistValue"></asp:CheckBoxList>

                     <asp:CustomValidator runat="server" ID="cvmodulelist"
                         ClientValidationFunction="ValidateRadioButtons" EnableClientScript="true" ForeColor="Red"
                         SetFocusOnError="True" ErrorMessage="Please Select Atleast one Tool"></asp:CustomValidator>
                 </div>
                </div>
            </div>
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-2">
                Nature of work:<label runat="server" id="lblNatureOfWork" style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" Style="width: 360px" class="form-control dropdown" ID="drpNatureOfWork"></asp:DropDownList>
            </div>
            <asp:RequiredFieldValidator ID="valNatureOfWork" runat="server" Display="None"
                ControlToValidate="drpNatureOfWork" InitialValue="-1" ErrorMessage="Nature of work is required"
                ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>
            <div class="col-md-2">
                Chargeable:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpChargeable" OnSelectedIndexChanged="drpChargeable_SelectedIndexChanged">
                    <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                    <asp:ListItem Value="1"> Y </asp:ListItem>
                    <asp:ListItem Value="0"> N </asp:ListItem>
                </asp:DropDownList>

            </div>
            <asp:RequiredFieldValidator ID="valChargeNoncharge" runat="server" Display="None"
                ControlToValidate="drpChargeable" InitialValue="-1" ErrorMessage="Chargeable / Non Chargeable is required"
                ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">

            <%--    <div class="col-md-2">
                Ideal involvement:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList class="form-control" Style="width: 360px" runat="server" ID="drpIdealInvolvement">
                    <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                    <asp:ListItem Value="1"> Y </asp:ListItem>
                    <asp:ListItem Value="0"> N </asp:ListItem>
                </asp:DropDownList>

            </div>
            <asp:RequiredFieldValidator ID="valIdealInvol" runat="server" Display="None"
                ControlToValidate="drpIdealInvolvement" InitialValue="-1" ErrorMessage="Ideal involvement is required"
                ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
        </div>

        <div class="row">&nbsp;</div>

        <div class="row">
            <div class="col-md-2">
                Sector:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" Style="width: 360px" class="form-control" AutoPostBack="true" ID="drpSector" OnSelectedIndexChanged="drpSector_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <asp:RequiredFieldValidator ID="valSector" runat="server" Display="None"
                ControlToValidate="drpSector" InitialValue="-1" ErrorMessage="Sector is required"
                ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>
            <div class="col-md-2">
                Sub-Sector:<label runat="server" id="lblSubSector" style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpSubSector"></asp:DropDownList>
            </div>
            <asp:RequiredFieldValidator ID="valSubSector" runat="server" Display="None"
                ControlToValidate="drpSubSector" InitialValue="-1" ErrorMessage="Sub-Sector is required"
                ForeColor="Red"
                Texr=""
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        </div>
        <div class="row">&nbsp;</div>

        <div class="row">
        </div>
        <div class="row">&nbsp;</div>

        <div class="row">
            <div class="col-md-2">Engagement Code: </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" class="form-control" ID="txtEngagementCode" ToolTip="WBS Code/Mandate Code"></asp:TextBox>
            </div>
            <div class="col-md-2">
                Phase/Task Code:<%--<label runat="server" id="lblPhaseCode" style="color: red">*</label>--%>
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" class="form-control" ID="txtPhaseTaskCode" ToolTip="Valid only for Valuations"></asp:TextBox>
            </div>

        </div>
        <div class="row">&nbsp;</div>

        <div class="row">
            <div class="col-md-2">
                Indian SAP Code:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" class="form-control" ID="txtSAPCode" ToolTip="Refer Indian SAP time entry code list"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                    ControlToValidate="txtSAPCode" ErrorMessage="Indian SAP Code is required"
                    ForeColor="Red"
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
            </div>

        </div>
        <div class="row">&nbsp;</div>

        <div class="row">
            <div class="col-md-2">
                Conflict Check:<label runat="server" id="lblConflict" style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpConflictCheck" OnSelectedIndexChanged="drpConflictCheck_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                    <asp:ListItem Value="1"> Y </asp:ListItem>
                    <asp:ListItem Value="0"> N </asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:RequiredFieldValidator ID="valConflictCheck" runat="server" Display="None"
                ControlToValidate="drpConflictCheck" InitialValue="-1" ErrorMessage="Conflict Check is required"
                ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>
            <div class="col-md-2">Conflict Check No/BD Work order number: </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" class="form-control" ID="txtConflictCheck"></asp:TextBox>
            </div>
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-2">
                Comments :
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" TextMode="MultiLine" class="form-control" ID="txtComments"></asp:TextBox>
            </div>
        </div>
        <div class="row">&nbsp;</div>
        <div class="row" id="divMainDocument" runat="server">
            <div class="col-md-2">
                Set up documents to be received ?:<label runat="server" id="Label1" style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpDocument" OnSelectedIndexChanged="drpDocument_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                    <asp:ListItem Value="1"> Y </asp:ListItem>
                    <asp:ListItem Value="0"> N </asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                    ControlToValidate="drpDocument" InitialValue="-1"
                    ErrorMessage="Set up documents to be received is a required field."
                    ForeColor="Red">
                </asp:RequiredFieldValidator>
            </div>



            <div id="divDocument" runat="server">
                <div class="col-md-2">
                    Reason :<label id="lblReason" style="color: red">*</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" TextMode="MultiLine" class="form-control" ID="txtReason"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="valReason" runat="server"
                    ForeColor="Red" Display="None"
                    ControlToValidate="txtReason" ErrorMessage="Please enter the Reason"
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
            </div>


        </div>
        <div class="row">&nbsp;</div>

        <div class="row">
            <asp:CheckBox runat="server" ID="chkWO" />
            Work Order needs to be generated
        </div>
        <div class="row">&nbsp;</div>

        <div class="row">
            <div class="col-md-2">Work Order Number: </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" class="form-control" ID="txtWorkOrderNumber">
                    
                </asp:TextBox>
            </div>
        </div>
        <br />
        <div id="FileUploadDiv" runat="server">

            <div class="row">
                <div class="row">&nbsp;</div>
                <div class="col-md-6">
                    <asp:FileUpload ID="FileUpload2" runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    Attach Excel Document for Reference.
                </div>
            </div>
        </div>
        <br />

        <div id="dvMsg" style="background-color: Red; color: White; width: 190px; padding: 3px; display: none;">
            Maximum size allowed is 1 MB
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-6">
                <asp:GridView ID="GridView1" runat="server"
                    AutoGenerateColumns="false" CssClass="gridview">
                    <HeaderStyle CssClass="gridViewHeader" />
                    <RowStyle CssClass="gridViewRow" />
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="Row No." ItemStyle-Width="100">
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber" runat="server" />
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
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
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-2">
                <asp:Button runat="server" align="center" Text="Save" ID="btnSave" OnClick="btnSave_Click" />
            </div>
        </div>
        <div class="modal" id="myModal" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" onclick="javascript: return ClosePopup();" data-dismiss="modal">&times;</button>
                        <h4 id="CommentTitle" class="modal-title">Duplicate Project Information</h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="gvProjectList" runat="server" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="ProjectName" HeaderText="Project Name" ItemStyle-Width="150" />
                                <asp:BoundField DataField="CreatedBy" HeaderText="Created By" ItemStyle-Width="250" />
                                <asp:BoundField DataField="ProjectCode" HeaderText="Project Code" ItemStyle-Width="350" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="Button1" runat="server" CausesValidation="false" Text="Close" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
