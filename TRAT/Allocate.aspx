<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Allocate.aspx.cs" Inherits="TRAT.Allocate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function scrollToResources() {
            document.getElementById('<%= divResources.ClientID %>').scrollIntoView();
        }
        function scrollToAllocate() {
            document.getElementById('<%= divAllocate.ClientID %>').scrollIntoView();
        }
        function disableF5(e) { if ((e.which || e.keyCode) == 116) e.preventDefault(); };
        // To disable f5
        /* jQuery < 1.7 */
        $(document).bind("keydown", disableF5);
        /* OR jQuery >= 1.7 */
        $(document).on("keydown", disableF5);

        // To re-enable f5
        /* jQuery < 1.7 */
        $(document).unbind("keydown", disableF5);
        /* OR jQuery >= 1.7 */
        $(document).off("keydown", disableF5);

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="formSection" runat="server">
    <form runat="server">
        <asp:ScriptManager ID="sc1" runat="server"></asp:ScriptManager>
        <div class="container">
            <h3>Search Project</h3>
            <div class="row">
                <div class="col-lg-2">Project Name</div>
                <%--<div class="col-lg-4">--%>

                <asp:TextBox runat="server" ID="txtProjectName"></asp:TextBox>
                <asp:Button runat="server" ID="btnSearchProject" Text="Search" OnClick="btnSearchProject_Click" />


            </div>
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <asp:GridView ID="gvProjects" ClientIDMode="Static" runat="server" CssClass="table" AutoGenerateColumns="false" GridLines="None" DataKeyNames="Id,startDate,EndDate" OnRowCommand="gvProjects_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="Project Code" DataField="ProjectCode" />
                    <asp:BoundField HeaderText="Project" DataField="Name" />
                    <asp:BoundField HeaderText="Partner" DataField="USerID" />
                    <asp:BoundField HeaderText="Office" DataField="Office" />
                    <asp:CommandField SelectText="View Resources" ShowSelectButton="true" />
                </Columns>
                <EmptyDataTemplate>
                    No Projects Found!
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        <div class="row">&nbsp;</div>
        <div class="row" id="divResources" runat="server" visible="false">
            Allocated Resources for
            <asp:Label ID="lblProjectNameAllocate" runat="server"></asp:Label>:
            <br />
            <asp:GridView ID="gvResources" runat="server" CssClass="table" AutoGenerateColumns="false" GridLines="None">
                <Columns>
                    <asp:BoundField HeaderText="Resource" DataField="EmpEmail" />
                    <asp:TemplateField HeaderText="From">
                        <ItemTemplate>
                            <asp:Label ID="lblFromdate" runat="server"
                                Text='<%#Eval("Fromdate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="To">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server"
                                Text='<%#Eval("ToDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Allocated By" DataField="CreatedByEmail" />
                    <asp:TemplateField HeaderText="Allocated On">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server"
                                Text='<%#Eval("createddate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    No Resources Found!
                </EmptyDataTemplate>

            </asp:GridView>
        </div>
        <hr />
        <div id="divAllocate" runat="server" visible="false">
            <h3>Allocate Resource</h3>
            <div class="row">
                <div class="col-lg-2">Project Name</div>
                <div class="col-lg-4">
                    <asp:Label ID="lblProjName" runat="server"></asp:Label>
                </div>
            </div>

            <div class="row">&nbsp;</div>

            <div class="row">
                <div class="col-lg-2">Project Start Date</div>
                <div class="col-lg-4">
                    <asp:Label ID="lblStartDateDispaly" runat="server"></asp:Label>
                </div>
            </div>
            <%-----------------------------------------------------------%>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-lg-2">
                    Employee Name                  
                </div>
                <div class="col-lg-4">
                    <asp:DropDownList runat="server" ID="ddlEmpList" CssClass="form-control" style="width: 200px">
                        <asp:ListItem Value="-1">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="valMemberFirm" runat="server" Display="Dynamic"
                    ControlToValidate="ddlEmpList" InitialValue="-1"
                    ErrorMessage="Required" ValidationGroup="vgSubmit"
                    ForeColor="Red">
                </asp:RequiredFieldValidator>
                </div>
            </div>
            <%-----------------------------------------------------------%>

            <%--<div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-lg-2">Resource Email<label style="color: red">*</label></div>
                <div class="col-lg-4">
                    <asp:TextBox ID="txtResourceName" class="form-control" runat="server" Style="width: 200px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvtxtResourceName" runat="server" ErrorMessage="Required"
                        ForeColor="Red" ValidationGroup="vgSubmit" Display="Dynamic"
                        ControlToValidate="txtResourceName">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ErrorMessage="Invalid Email" ControlToValidate="txtResourceName" ValidationGroup="vgSubmit" Display="Dynamic"
                        ForeColor="Red"
                        SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@deloitte*\.\w+([-.]\w+)*">
                    </asp:RegularExpressionValidator>
                    
                    
                </div>
            </div>--%>
            <asp:HiddenField ID="hfProjectId" runat="server" />
                    <asp:HiddenField ID="hfStartDate" runat="server" />
                    <asp:HiddenField ID="hfEndDate" runat="server" />
            <div class="row">&nbsp;</div>



            <div class="row">
                <div class="col-lg-2">
                    From Date
                    <label style="color: red">*</label>
                </div>
                <div class="col-lg-4">
                    <input type="date" id="txtFromDate" class="form-control" runat="server" style="width: 200px" />
                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server"
                        ControlToValidate="txtFromDate" ErrorMessage="Required"
                        ForeColor="Red" ValidationGroup="vgSubmit" Display="Dynamic"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>

                </div>
            </div>

            <div class="row">&nbsp;</div>

            <div class="row">
                <div class="col-lg-2">To Date<label style="color: red">*</label></div>
                <div class="col-lg-4">
                    <input type="date" class="form-control" id="txtTodate" runat="server" style="width: 200px" />
                    <asp:RequiredFieldValidator ID="rfvToDate" runat="server"
                        ControlToValidate="txtFromDate" ErrorMessage="Required"
                        ForeColor="Red" ValidationGroup="vgSubmit"
                        SetFocusOnError="True"></asp:RequiredFieldValidator>

                </div>
            </div>


            <div class="row">&nbsp;</div>

            <div class="row">
                <div class="col-lg-3 text-right">
                    <asp:Button Text="Allocate" runat="server" ID="btnAllocate" OnClick="btnAllocate_Click" CausesValidation="true" ValidationGroup="vgSubmit" />
                </div>

            </div>

        </div>
    </form>
</asp:Content>


