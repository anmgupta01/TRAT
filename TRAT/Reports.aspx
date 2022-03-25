<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="TRAT.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="formSection" runat="server">
    <script src="TRATScripts.js"></script>
    <script type="text/javascript">
        function RadioCheck(rb) {
            var gv = document.getElementById("<%=gvProjects.ClientID%>");
            var rbs = gv.getElementsByTagName("input");

            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }
        function SelectAll(id) {
            //get reference of GridView control
            var grid = document.getElementById("<%= gvProjects.ClientID %>");
            //variable to contain the cell of the grid
            var cell;

            if (grid.rows.length > 0) {
                //loop starts from 1. rows[0] points to the header.
                for (i = 1; i < grid.rows.length; i++) {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];

                    //loop according to the number of childNodes in the cell
                    for (j = 0; j < cell.childNodes.length; j++) {
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type == "checkbox") {
                            //assign the status of the Select All checkbox to the cell 
                            //checkbox within the grid
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }

        function Validate(sender, args) {
            var gridView = document.getElementById("<%=gvProjects.ClientID %>");
            if (gridView.rows.length > 0) {
                var checkBoxes = gridView.getElementsByTagName("input");

                for (var i = 0; i < checkBoxes.length; i++) {
                    if (checkBoxes[i].type == "RadioButton" && checkBoxes[i].checked) {
                        args.IsValid = true;
                        return;
                    }
                }
                args.IsValid = false;

            }
            else {
                return true;
            }

        }
        setTimeout(function () {
            $(".alert").alert('close');
        }, 5000);
    </script>

    <form runat="server">
        <div class="container">
            <br />
            <div id="alert" runat="server" class="alert alert-success fade in" visible="false">

                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong>No data available for the filters done !</strong><br />
            </div>
            <h3>Download Report</h3>
            <hr />
            <br />
            <div class="row">
                <div class="col-md-1">Start Date</div>
                <div class="col-md-2">
                    <input runat="server" class="form-control" id="dtStartDate" type="date" />
                </div>
                <div class="col-md-1">End Date</div>
                <div class="col-md-2">
                    <input runat="server" class="form-control" id="dtEndDate" type="date" />
                </div>
            </div>
            <div class="row">
                <br />
            </div>
            <div class="row">
                <div class="col-lg-2">Frequency :</div>
                <div class="row">&nbsp;</div>
                <div class="col-lg-2">
                    <asp:RadioButton runat="server" ID="rdDay" value="rdDay" GroupName="rdSelect" />
                    Day
                </div>
                <div class="col-lg-2">
                    <asp:RadioButton Checked="true" ID="rdWeek" value="rdWeek" runat="server" GroupName="rdSelect" />
                    Week
                </div>
                <div class="col-lg-2">
                    <asp:RadioButton runat="server" ID="rdMonth" value="rdMonth" GroupName="rdSelect" />
                    Monthly

                </div>
                <div class="col-lg-2">
                    <asp:RadioButton runat="server" ID="rdQuat" value="rdQuater" GroupName="rdSelect" />                    
                    Quarterly
                </div>
            </div>
            <div class="row">
                <br />
            </div>
            <div class="row">
                <div class="col-md-2">
                    Service Line:<label style="color: red">*</label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" CssClass="form-control" ID="drpServiceLine" OnSelectedIndexChanged="drpServiceLine_SelectedIndexChanged" AutoPostBack="true">
                        <%--DataValueField="Id" DataTextField="Name" ID="drpServiceLine" OnSelectedIndexChanged="drpServiceLine_SelectedIndexChanged"
                    AutoPostBack="true"--%>
                        <asp:ListItem Value="-1">All</asp:ListItem>
                    </asp:DropDownList>

                </div>
                <%--<asp:RequiredFieldValidator ID="valServiceLine" runat="server" Display="None"
                ControlToValidate="drpServiceLine" InitialValue="-1" ErrorMessage="Service line is required"
                ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
            </div>
            <div class="row">
                <br />
            </div>
            <div class="row">
                <div class="col-md-2">
                    Geography:<label style="color: red">*</label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" CssClass="form-control" ID="drpCountry">
                    </asp:DropDownList>

                </div>
                <%--<asp:RequiredFieldValidator ID="valServiceLine" runat="server" Display="None"
                ControlToValidate="drpServiceLine" InitialValue="-1" ErrorMessage="Service line is required"
                ForeColor="Red"
                SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
            </div>
            <div class="row">
                <br />
            </div>

            <div class="row">
                <div class="col-md-2">
                    Department                
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="drpDepartment" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged">
                        <asp:ListItem Value="-1">All</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <br />
            </div>

            <div class="row">
                <div class="col-md-2">
                    Sub Department                  
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" CssClass="form-control" ID="drpSubDepartment">
                        <asp:ListItem Value="-1">All</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <br />
            </div>

            <div class="row">
                <div class="col-md-2">
                    Employee Name                  
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlEmpList">
                        <asp:ListItem Value="-1">All</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <br />
            </div>
            <div class="row">
                <div class="col-md-2">Project Name</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtProjectName" placeholder="Enter project name or code here"></asp:TextBox>

                    <%--<a href="#" id="clrSelect" onServerClick="clrSelect_Click">Clear Selection</a>--%>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:Button runat="server" ID="btnSearchProject" Text="Search" OnClick="btnSearchProject_Click" CausesValidation="false" />
                    <asp:LinkButton runat="server" ID="btnClrSelect" Text="Clear Selection" OnClick="btnClrSelect_Click" CausesValidation="false" />
                </div>

            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <asp:GridView ID="gvProjects" runat="server" CssClass="table" AutoGenerateColumns="false" GridLines="None" DataKeyNames="Id" OnRowDataBound="gvProjects_RowDataBound">
                    <Columns>

                        <asp:TemplateField>
                            <%--<HeaderTemplate>
                                <asp:CheckBox ID="allchk" runat="server" onclick="SelectAll(this);" />
                            </HeaderTemplate>--%>
                            <%--<ItemTemplate>
                                <asp:CheckBox ID="chkProject" runat="server"></asp:CheckBox>
                            </ItemTemplate>--%>
                            <ItemTemplate>
                                <asp:RadioButton ID="rdProjectSelect" value="rdProjectSel" runat="server" GroupName="rdProject" onclick="RadioCheck(this)"></asp:RadioButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Project Code" DataField="ProjectCode" />
                        <asp:BoundField HeaderText="Project" DataField="Name" />
                        <asp:BoundField HeaderText="Partner" DataField="USerID" />
                        <asp:BoundField HeaderText="Office" DataField="Office" />

                    </Columns>
                    <%--<EmptyDataTemplate>
                        No Records Found!
                    </EmptyDataTemplate>--%>
                </asp:GridView>

                <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please select at least one record."
                    ClientValidationFunction="Validate" ForeColor="Red"></asp:CustomValidator>--%>
                <br />
            </div>
            <div class="row">
                <div class="col-lg-2">
                    <asp:Label runat="server" ID="lblNoRecords" Visible="false">No Records Found!</asp:Label>

                </div>
            </div>
            <div class="row">
                <br />
            </div>



            <%--<div id="alert" runat="server" class="alert alert-success fade in" visible="false">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <strong>No data available for the filters done !</strong>
            </div>--%>
            <div class="row">
                <div class="col-lg-1">
                    <asp:Button runat="server" ID="btnDownloadReport" Text="Download Report" OnClick="btnDownloadReport_Click" />
                </div>
            </div>
            <div class="row">
                <h4>Please note:- Report data is scheduled to refresh twice daily at 0800 Hours and 1600 Hours. Report extracted will be as current as the last refresh time. Please time your data exports to ensure you get the most recent updated data.
                    </h4>
            </div>
        </div>
    </form>
</asp:Content>
