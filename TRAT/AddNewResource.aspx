<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewResource.aspx.cs" Inherits="TRAT.AddNewResource" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function getQueryStringValue(key) {
            return decodeURIComponent(window.location.search.replace(new RegExp("^(?:.*[&\\?]" + encodeURIComponent(key).replace(/[\.\+\*]/g, "\\$&") + "(?:\\=([^&]*))?)?.*$", "i"), "$1"));
        }
        $(document).ready(function () {
            if (getQueryStringValue("id") != '') {
                debugger;
                //alert(getQueryStringValue("id"));
                alert('Employee details saved !');
                var uri = window.location.href.toString();
                if (uri.indexOf("?") > 0) {
                    var clean_uri = uri.substring(0, uri.indexOf("?"));
                    window.history.replaceState({}, document.title, clean_uri);
                }
            }

        });

    </script>
</asp:Content>
<asp:Content ID="formSection" ContentPlaceHolderID="formSection" runat="server">
    <form runat="server">
        <h3>Add New Resource</h3>
        <asp:HiddenField runat="server" ID="hidEmpID" />
        <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList"
            EnableClientScript="true" ForeColor="Red"
            HeaderText="You must enter a value in the following fields:" />
        <br />
        <div class="row">
            <div class="col-md-2">
                Employee Id:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" class="form-control" ID="txtEmpID"></asp:TextBox>
            </div>
            <asp:RequiredFieldValidator ID="valEmpID" runat="server" Display="None"
                ControlToValidate="txtEmpID"
                ErrorMessage="Employee ID is a required field."
                ForeColor="Red">
            </asp:RequiredFieldValidator>

        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-2">
                Employee Name:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" class="form-control" ID="txtEmpName"></asp:TextBox>
            </div>
            <asp:RequiredFieldValidator ID="valEmpName" runat="server" Display="None"
                ControlToValidate="txtEmpName"
                ErrorMessage="Employee Name is a required field."
                ForeColor="Red">
            </asp:RequiredFieldValidator>

        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-2">
                Email Id:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" class="form-control" ID="txtEmpEmail"></asp:TextBox>
            </div>
            <asp:RequiredFieldValidator ID="valEmpEmail" runat="server" Display="None"
                ControlToValidate="txtEmpEmail"
                ErrorMessage="Employee email is a required field."
                ForeColor="Red">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regexManagerIJV" runat="server"
                ErrorMessage="Invalid Email" ControlToValidate="txtEmpEmail"
                SetFocusOnError="True" ForeColor="Red"
                ValidationExpression="\w+([-+.']\w+)*@deloitte*\.\w+([-.]\w+)*">
            </asp:RegularExpressionValidator>

        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-2">
                Deparment Name:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" class="form-control" DataValueField="Id" DataTextField="Name" AutoPostBack="true" ID="drpDepartment" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged">
                </asp:DropDownList>

            </div>
            <asp:RequiredFieldValidator ID="valServiceLine" runat="server" Display="None"
                ControlToValidate="drpDepartment" InitialValue="-1" ErrorMessage="Service line is required"
                ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-2">
                Sub-Department Name:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" class="form-control" DataValueField="Id" DataTextField="Name" ID="drpSubDepartment">
                </asp:DropDownList>

            </div>
            
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-2">
                Designation:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" class="form-control" DataValueField="Id" DataTextField="Name" ID="drpDesignation">
                    <asp:ListItem value="Select">--Select--</asp:ListItem>                    
                    <asp:ListItem value="Executive">Executive</asp:ListItem>
                    <asp:ListItem value="Senior Executive">Senior Executive</asp:ListItem>
                    <asp:ListItem value="Assistant Manager">Assistant Manager</asp:ListItem>
                    <asp:ListItem value="Deputy Manager">Deputy Manager</asp:ListItem>
                    <asp:ListItem value="Senior Manager">Senior Manager</asp:ListItem>
                    <asp:ListItem value="Manager">Manager</asp:ListItem>
                    <asp:ListItem value="Director">Director</asp:ListItem>

                </asp:DropDownList>
            </div>
            <asp:RequiredFieldValidator ID="valDesignation" runat="server" Display="None"
                ControlToValidate="drpDesignation"
                ErrorMessage="Designation is a required field." InitialValue="Select"
                ForeColor="Red">
            </asp:RequiredFieldValidator>

        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-2">
                Enter Australia Email Id:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" class="form-control" ID="txtAusEmailID" ToolTip="Enter member firm Australia email id"></asp:TextBox>          
            
            </div>
            <asp:RequiredFieldValidator ID="valPartnerFirm" runat="server"
                ForeColor="Red" Display="None"
                ControlToValidate="txtAusEmailID" ErrorMessage="Australia email id is required"
                SetFocusOnError="True"></asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator ID="valAusEmailId" runat="server"
                ErrorMessage="Invalid Email" ControlToValidate="txtAusEmailID"
                ForeColor="Red"
                SetFocusOnError="True"
                ValidationExpression="\w+([-+.']\w+)*@deloitte*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-2">
                Enter UK Email Id:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" class="form-control" ID="txtUkEmailId" ToolTip="Enter member firm UK email id"></asp:TextBox>          
            
            </div>
            <asp:RequiredFieldValidator ID="valUkEmailId" runat="server"
                ForeColor="Red" Display="None"
                ControlToValidate="txtUkEmailId" ErrorMessage="UK email id is required"
                SetFocusOnError="True"></asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator ID="regUKEmailId" runat="server"
                ErrorMessage="Invalid Email" ControlToValidate="txtUkEmailId"
                ForeColor="Red"
                SetFocusOnError="True"
                ValidationExpression="\w+([-+.']\w+)*@deloitte*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-2">
                Enter Canada Email Id:<label style="color: red">*</label>
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" class="form-control" ID="txtCanEmailId" ToolTip="Enter member firm Canada email id"></asp:TextBox>          
            
            </div>
            <asp:RequiredFieldValidator ID="valCanEmailId" runat="server"
                ForeColor="Red" Display="None"
                ControlToValidate="txtCanEmailId" ErrorMessage="Canada email id is required"
                SetFocusOnError="True"></asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator ID="regCanEmailId" runat="server"
                ErrorMessage="Invalid Email" ControlToValidate="txtCanEmailId"
                ForeColor="Red"
                SetFocusOnError="True"
                ValidationExpression="\w+([-+.']\w+)*@deloitte*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <asp:Button runat="server" align="center" Text="Add New Resource" ID="btnAddResource" OnClick="btnAddResource_Click"  />            
            

        </div>
     </form>
</asp:Content>
