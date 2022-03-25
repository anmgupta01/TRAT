<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterUpdateProjectDetails.aspx.cs" Inherits="TRAT.MasterUpdateProjectDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="formSection" runat="server">
    <form runat="server">
        <div class="container">
            <h3>Search Project</h3>
        </div>
        <div class="row">
            <div class="col-lg-2">Project Name</div>
            <div class="col-lg-4">
                <asp:TextBox runat="server" ID="txtProjectName"></asp:TextBox>
                <asp:Button runat="server" ID="btnSearchProject" Text="Search" OnClick="btnSearchProject_Click" />
            </div>
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <asp:GridView ID="gvProjects" runat="server" CssClass="table" AutoGenerateColumns="false" GridLines="None" DataKeyNames="Id" OnRowCommand="gvProjects_RowCommand" OnRowDeleting="gvProjects_RowDeleting"
                OnRowEditing="gvProjects_RowEditing">
                <Columns>
                    <asp:BoundField HeaderText="Project Code" DataField="ProjectCode" />
                    <asp:BoundField HeaderText="Project" DataField="Name" />
                    <asp:BoundField HeaderText="Partner" DataField="USerID" />
                    <asp:BoundField HeaderText="Office" DataField="Office" />
                    <asp:CommandField EditText="View" ShowEditButton="true" />

                </Columns>
                <EmptyDataTemplate>
                    No Records Found!
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        <div class="row">&nbsp;</div>

        <div id="dvProjectData" class="row" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList"
            EnableClientScript="true" ForeColor="Red"
            HeaderText="You must enter a value in the following fields:" />
        <br />
        <asp:HiddenField runat="server" ID="hidProjectID" />
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
                        Referring Member Location:<label id="lblFirmOffice" runat="server" style="color: red">*</label>
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

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ErrorMessage="Invalid Email" ControlToValidate="txtPartnerMemfirm"
                        ForeColor="Red"
                        SetFocusOnError="True"
                        ValidationExpression="\w+([-+.']\w+)*@deloitte*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
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
                    <asp:RegularExpressionValidator ID="regexEngManFirm" runat="server"
                        ErrorMessage="Invalid Email" ControlToValidate="txtManagerMemFirm"
                        SetFocusOnError="True" ForeColor="Red"
                        ValidationExpression="\w+([-+.']\w+)*@deloitte*\.\w+([-.]\w+)*">
                    </asp:RegularExpressionValidator>
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
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-2">
                Chargeable:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" Style="width: 360px" class="form-control" ID="drpChargeable" OnSelectedIndexChanged="drpChargeable_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="-1"> --Select-- </asp:ListItem>
                    <asp:ListItem Value="1"> Y </asp:ListItem>
                    <asp:ListItem Value="0"> N </asp:ListItem>
                </asp:DropDownList>

            </div>
            <asp:RequiredFieldValidator ID="valChargeNoncharge" runat="server" Display="None"
                ControlToValidate="drpChargeable" InitialValue="-1" ErrorMessage="Chargeable / Non Chargeable is required"
                ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>
            <div class="col-md-2">
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
                SetFocusOnError="True"></asp:RequiredFieldValidator>
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
                    Phase/Task Code:<label runat="server" id="lblPhaseCode" style="color: red">*</label>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" class="form-control" ID="txtPhaseTaskCode"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="valPhaseCode" runat="server" Display="None"
                    ControlToValidate="txtPhaseTaskCode" ErrorMessage="Phase/Task Code is required"
                    ForeColor="Red"
                    SetFocusOnError="True"></asp:RequiredFieldValidator>
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
                <asp:Button runat="server" align="center" Text="Save" ID="btnSave" OnClick="btnSave_Click" />

            </div>
        </div>
    </form>

</asp:Content>
