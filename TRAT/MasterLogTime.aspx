<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MasterLogTime.aspx.cs" Inherits="TRAT.MasterLogTime" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="TRATScripts.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script type="text/javascript">

        $(document).ready(function () {
            Footer();
            setTimeout(function () {
                $(".alert").alert('close');
            }, 5000);

            // hide the comment popup initially
            var modal = document.getElementById('myModal');
            modal.style.display = "none";

            //check if the textbox for entering hours is enabled, if not, hide the comments button next to it
            var buttonList = document.getElementsByClassName('Check');
            for (var count = 0; count < buttonList.length; count++) {
                if ($(buttonList[count]).prev().is(':disabled') == true) {
                    $(buttonList[count]).css('visibility', 'hidden');
                }
                else {
                    $(buttonList[count]).css('visibility', 'visible');
                }
            }
        });

        window.onclick = function (event) {
            // When the user clicks anywhere outside of the modal, close it and clear all the values pertaining to the modal popup
            var modal = document.getElementById('myModal');
            if (event.target == modal) {
                modal.style.display = "none";
                projectId = '';
                commentDay = '';
                $('#txtComment').val('');
            }
        }

        // Numeric only control handler
        jQuery.fn.ForceNumericOnly =
        function () {
            return this.each(function () {
                $(this).keydown(function (e) {
                    var key = e.charCode || e.keyCode || 0;
                    //alert(e.charCode);
                    // allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
                    // home, end, period, and numpad decimal
                    return (
                        key == 8 ||
                        key == 9 ||
                        key == 13 ||
                        key == 46 ||
                        key == 110 ||
                        key == 190 ||
                        (key >= 35 && key <= 40) ||
                        (key >= 48 && key <= 57) ||
                        (key >= 96 && key <= 105));
                });
            });
        };


        $("#d1").ForceNumericOnly();
        $("#d2").ForceNumericOnly();
        $("#d3").ForceNumericOnly();
        $("#d4").ForceNumericOnly();
        $("#d5").ForceNumericOnly();
        $("#d6").ForceNumericOnly();
        $("#d7").ForceNumericOnly();

        function Footer() {

            var taxgrid = document.getElementById('<%=gvMain.ClientID %>');
            var taxip = taxgrid.getElementsByTagName('input');
            var hours = 0.00;
            var d1, d2, d3, d4, d5, d6, d7;
            d1 = d2 = d3 = d4 = d5 = d6 = d7 = 0.00;
            for (i = 0; i < taxip.length; i++) {
                if (!isNaN(parseFloat(taxip[i].value))) {
                    switch (taxip[i].id.substring(21, 19)) {
                        case 'd1':
                            //case 'formSection_gvMain_d1_'+i:
                            d1 = d1 + parseFloat(taxip[i].value);
                            break;
                        case 'd2':
                            d2 = d2 + parseFloat(taxip[i].value);
                            break;
                        case 'd3':
                            d3 = d3 + parseFloat(taxip[i].value);
                            break;
                        case 'd4':
                            d4 = d4 + parseFloat(taxip[i].value);
                            break;
                        case 'd5':
                            d5 = d5 + parseFloat(taxip[i].value);
                            break;
                        case 'd6':
                            d6 = d6 + parseFloat(taxip[i].value);
                            break;
                        case 'd7':
                            d7 = d7 + parseFloat(taxip[i].value);
                            break;

                    }
                }
                else { taxip[i].value = ""; }
            }


            var labels = taxgrid.getElementsByTagName('label');
            for (i = 0; i < labels.length; i++) {
                switch (labels[i].id) {
                    case 'd1Sum':
                        labels[i].innerHTML = d1;
                        if (d1 > 24)
                        { labels[i].style.color = "red"; }
                        else { labels[i].style.color = "black"; }
                        break;
                    case 'd2Sum':
                        labels[i].innerHTML = d2;
                        if (d2 > 24)
                        { labels[i].style.color = "red"; }
                        else { labels[i].style.color = "black"; }
                        break;
                    case 'd3Sum':
                        labels[i].innerHTML = d3;
                        if (d3 > 24)
                        { labels[i].style.color = "red"; }
                        else { labels[i].style.color = "black"; }
                        break;
                    case 'd4Sum':
                        labels[i].innerHTML = d4;
                        if (d4 > 24)
                        { labels[i].style.color = "red"; }
                        else { labels[i].style.color = "black"; }
                        break;
                    case 'd5Sum':
                        labels[i].innerHTML = d5;
                        if (d5 > 24)
                        { labels[i].style.color = "red"; }
                        else { labels[i].style.color = "black"; }
                        break;
                    case 'd6Sum':
                        labels[i].innerHTML = d6;
                        if (d6 > 24)
                        { labels[i].style.color = "red"; }
                        else { labels[i].style.color = "black"; }
                        break;
                    case 'd7Sum':
                        labels[i].innerHTML = d7;
                        if (d7 > 24)
                        { labels[i].style.color = "red"; }
                        else { labels[i].style.color = "black"; }
                        break;
                }

            }
        }


        function myFunction() {

            var taxgrid = document.getElementById('<%=gvMain.ClientID %>');
            var labels = taxgrid.getElementsByTagName('label');

            var taxip = taxgrid.getElementsByTagName('input');
            var hours = 0.00;
            var d1, d2, d3, d4, d5, d6, d7;
            d1 = d2 = d3 = d4 = d5 = d6 = d7 = 0.00;
            for (i = 0; i < taxip.length; i++) {
                if (!isNaN(parseFloat(taxip[i].value))) {
                    switch (taxip[i].id.substring(21, 19)) {
                        case 'd1':
                            //case 'formSection_gvMain_d1_'+i:
                            d1 = d1 + parseFloat(taxip[i].value);
                            break;
                        case 'd2':
                            d2 = d2 + parseFloat(taxip[i].value);
                            break;
                        case 'd3':
                            d3 = d3 + parseFloat(taxip[i].value);
                            break;
                        case 'd4':
                            d4 = d4 + parseFloat(taxip[i].value);
                            break;
                        case 'd5':
                            d5 = d5 + parseFloat(taxip[i].value);
                            break;
                        case 'd6':
                            d6 = d6 + parseFloat(taxip[i].value);
                            break;
                        case 'd7':
                            d7 = d7 + parseFloat(taxip[i].value);
                            break;

                    }
                }
            }
            for (i = 0; i < labels.length; i++) {
                switch (labels[i].id) {
                    case 'd1Sum':
                        labels[i].innerHTML = d1;
                        if (d1 > 24) {
                            alert('The total number of hours logged is greater than 24.');
                            return false;
                        }

                        break;
                    case 'd2Sum':
                        labels[i].innerHTML = d2;
                        if (d2 > 24) {
                            alert('The total number of hours logged is greater than 24.');
                            return false;
                        }

                        break;
                    case 'd3Sum':
                        labels[i].innerHTML = d3;
                        if (d3 > 24) {
                            alert('The total number of hours logged is greater than 24.');
                            return false;
                        }

                        break;
                    case 'd4Sum':
                        labels[i].innerHTML = d4;
                        if (d4 > 24) {
                            alert('The total number of hours logged is greater than 24.');
                            return false;
                        }

                        break;
                    case 'd5Sum':
                        labels[i].innerHTML = d5;
                        if (d5 > 24) {
                            alert('The total number of hours logged is greater than 24.');
                            return false;
                        }
                        break;
                    case 'd6Sum':
                        labels[i].innerHTML = d6;
                        if (d6 > 24) {
                            alert('The total number of hours logged is greater than 24.');
                            return false;
                        }
                        break;
                    case 'd7Sum':
                        labels[i].innerHTML = d7;
                        if (d7 > 24) {
                            alert('The total number of hours logged is greater than 24.');
                            return false;
                        }
                        break;
                }

            }
            return true;
        }

        var projectId = ''; //global variable to pass project id value between the main form and popup
        var commentDay = ''; // global variable to pass day value between the main form and popup

        // method to get project id and day for which comment is to be written and reads the existing comment value from the respective hidden field
        // also, set the value in the comment textbox as per the value received from code behind
        function OpenPopup(projectName, day, flagValue) {
            console.log('Function: ' + getFuncName() + ' initiated...');
            console.log('Should popup be displayed for '+ projectName +' on '+ day +' : ' + flagValue);
            var modal = document.getElementById('myModal'); // get the div containing the modal popup

            try {
                // check if popup is to be displayed; if yes, get the project id from the projectName, comment value from code behind and display the modal accordingly
                if (flagValue == 'True') {
                    projectId = projectName.split('-')[0];
                    commentDay = day;

                    var hdnFieldIdentifier = projectId + '-' + commentDay;
                    $('#' + hdnFieldIdentifier).val();  
                    var hdnFieldValue = String($('#' + hdnFieldIdentifier).val());
                    if (hdnFieldValue.length > 0) {
                        console.log('Existing comment for project id:' + projectId + ' for ' + commentDay + ': ' + hdnFieldValue);
                        $('#txtComment').val(hdnFieldValue);
                    }
                    else {
                        $('#txtComment').val('');
                    }
                    $('#CommentTitle').text('Add Comment for ProjectId: ' + projectId + ' on ' + commentDay);
                    modal.style.display = "block";
                }
            }
            catch (e) {
                modal.style.display = "none";
                $('#txtComment').val('');
                console.log('Exception at : ' + getFuncName() + ' : Error Description : ' + e.name + ' >> Error Message: ' + e.message);
            }
        }

        // method which is called when "save" button of popup is clicked
        // reads the value from the comments textbox and stores it in the corresponding hidden field
        function AddComment() {
            try {
                console.log('Function: ' + getFuncName() + ' initiated...');
                var hdnFieldValue = String($('#txtComment').val());
                var hdnFieldIdentifier = projectId + '-' + commentDay;
                $('#' + hdnFieldIdentifier).val(hdnFieldValue);
                projectId = '';
                commentDay = '';
                console.log('Comment entered for ' + hdnFieldIdentifier + " : " +$('#' + hdnFieldIdentifier).val());
            }
            catch (e) {
                console.log('Exception at : ' + getFuncName() + ' : Error Description : ' + e.name + ' >> Error Message: ' + e.message);
            }
            var modal = document.getElementById('myModal');
            modal.style.display = "none";
        }

        // clears the value in the comments textbox in modal popup
        function ClearComment() {
            try {
                console.log('Function: ' + getFuncName() + ' initiated...');
                $('#txtComment').val('');
                console.log('Cleared the value of the Comment textbox');
            }
            catch (e) {
                console.log('Exception at : ' + getFuncName() + ' : Error Description : ' + e.name + ' >> Error Message: ' + e.message);
            }
        }

        function ClosePopup() {
            try {
                console.log('Function: ' + getFuncName() + ' initiated...');
                var modal = document.getElementById('myModal');
                modal.style.display = 'none';
                $('#txtComment').val('');
                projectId = '';
                commentDay = '';
            }
            catch (e) {
                console.log('Exception occured at function: ' + getFuncName() + ' : Error Description : ' + e.name + ' >> Error Message: ' + e.message);
            }
        }

        function getFuncName() {
            return getFuncName.caller.name
        }
    </script>

    <style type="text/css">
        .aspNetDisabled {
            display: block;
            width: 100%;
            height: 34px;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: lightgray;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }

        .floating-box {
            display: inline-block;
            width: 150px;
            height: 75px;
            margin: 10px;
            border: 3px solid #73AD21;  
        }

        .btnComment {
            width:0px;
            height:0px;
            margin:5px;
            padding:0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="formSection" runat="server">
    <form runat="server">
        <asp:HiddenField ID="HfStartDate" runat="server" />
        <div class="container">
            <h3>Timesheet </h3>
            <hr />
            <div class="row">
                <div class="col-lg-2">Employee Name</div>
                <div class="col-lg-4">
                     <asp:DropDownList runat="server" ID="ddlEmpList" CssClass="form-control" style="width: 200px" OnSelectedIndexChanged="ddlEmpList_SelectedIndexChanged"
                         AutoPostBack="true">
                        <asp:ListItem Value="-1">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="valMemberFirm" runat="server" Display="Dynamic"
                    ControlToValidate="ddlEmpList" InitialValue="-1"
                    ErrorMessage="Required" ValidationGroup="vgSubmit"
                    ForeColor="Red">
                </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row">
                <div class="col-lg-1">Week</div>
                <div class="col-lg-2">
                    <input type="date" value="01-08-2017" id="calDate" runat="server" />
                </div>
                <div class="col-lg-1">
                    <asp:Button runat="server" Text="Go" ID="btnGo" OnClick="btnGo_Click" />
                </div>

                <div class="col-lg-8">
                    <div style="float: right; align-items: flex-end">
                        <asp:Button runat="server" Text="<<" ID="btnPrev" OnClick="btnGo_Click" /> -
                        <asp:Button runat="server" Text=">>" ID="btnNext" OnClick="btnGo_Click" />
                    </div>
                </div>
            </div>
            <div class="row">&nbsp;</div>

            <div id="alert" runat="server" class="alert alert-success fade in" visible="false">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                <%--<strong>Changes have been successfully saved!</strong>--%>
            </div>
            <hr id="hrHasData" runat="server" />
            <asp:GridView ID="gvMain" runat="server" CssClass="table" GridLines="None" AutoGenerateColumns="false" ShowFooter="true" DataKeyNames="PID">
                <Columns>
                    <asp:BoundField DataField="Project" HeaderText="Project" />
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <%= dates[0] %>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="col-xs-12" style="display:flex;">
                                <asp:TextBox runat="server" ID="d1" type="number" class="form-control" onchange="Footer();" Text='<%# Eval("d1") %>' min="0" max="24" step="0.25" value='<%# Eval("d1") %>' Enabled='<%# (Convert.ToBoolean(Eval("D1Flag").ToString()))  %>' />
                                <button type="button" class="btn Check btnComment" title="Add Comment" onclick='<%# String.Format("javascript:return OpenPopup(\"{0}\",\"{1}\",\"{2}\")", Eval("Project").ToString(), "Monday",(Convert.ToBoolean(Eval("D1Flag").ToString()))) %>'>
                                    <span class="glyphicon glyphicon-file"></span>
                                </button>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div class="col-xs-12">
                                <label id="d1Sum"></label>
                                <%--<asp:TextBox runat="server" type="number" class="form-control" ID="d1Tot" Text="d1Tot" max="24" Enabled="false"></asp:TextBox>--%>
                            </div>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <% =dates[1] %>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="col-xs-12" style="display:flex;">
                                <%--<asp:Textbox runat="server" id="d2" class="form-control" type="number" onchange="Footer();" Text='<%# Eval("d2") %>'  max="24" step="0.25" value='<%# Eval("d2") %>'  />--%>
                                <asp:TextBox runat="server" ID="d2" class="form-control" type="number" onchange="Footer();" Text='<%# Eval("d2") %>' min="0" max="24" step="0.25" value='<%# Eval("d2") %>' Enabled='<%# (Convert.ToBoolean(Eval("D2Flag").ToString()))  %>' />
                                <button type="button" class="btn Check btnComment" title="Add Comment" onclick='<%# String.Format("javascript:return OpenPopup(\"{0}\",\"{1}\",\"{2}\")", Eval("Project").ToString(), "Tuesday",(Convert.ToBoolean(Eval("D2Flag").ToString()))) %>'>
                                    <span class="glyphicon glyphicon-file"></span>
                                </button>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div class="col-xs-12" style="display:flex;">
                                <label id="d2Sum"></label>
                            </div>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <% =dates[2] %>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="col-xs-12" style="display:flex;">
                                <%--<input id="d3" class="form-control"   type="number" onchange="Footer();" max="24" step="0.25" value='<%# Eval("d3") %>' <%# Eval("D3Flag") %>  />--%>
                                <asp:TextBox runat="server" ID="d3" class="form-control" type="number" onchange="Footer();" Text='<%# Eval("d3") %>' min="0" max="24" step="0.25" value='<%# Eval("d3") %>' Enabled='<%# (Convert.ToBoolean(Eval("D3Flag").ToString()))  %>' />
                                <button type="button" class="btn Check btnComment" title="Add Comment" onclick='<%# String.Format("javascript:return OpenPopup(\"{0}\",\"{1}\",\"{2}\")", Eval("Project").ToString(), "Wednesday",(Convert.ToBoolean(Eval("D3Flag").ToString()))) %>'>
                                    <span class="glyphicon glyphicon-file"></span>
                                </button>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div class="col-xs-12">
                                <label id="d3Sum"></label>
                            </div>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <% =dates[3] %>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="col-xs-12" style="display:flex;">
                                <%--<input name="d4ele" id="d4" runat="server" class="form-control" type="number" onchange="Footer();" max="24" step="0.25" value='<%# Eval("d4") %>' <%# Eval("D4Flag")  %>  />--%>
                                <asp:TextBox runat="server" ID="d4" class="form-control" type="number" onchange="Footer();" Text='<%# Eval("d4") %>' min="0" max="24" step="0.25" value='<%# Eval("d4") %>' Enabled='<%# (Convert.ToBoolean(Eval("D4Flag").ToString()))  %>' />
                                <button type="button" class="btn Check btnComment" title="Add Comment" onclick='<%# String.Format("javascript:return OpenPopup(\"{0}\",\"{1}\",\"{2}\")", Eval("Project").ToString(), "Thursday",(Convert.ToBoolean(Eval("D4Flag").ToString()))) %>'>
                                    <span class="glyphicon glyphicon-file"></span>
                                </button>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div class="col-xs-12">
                                <label id="d4Sum"></label>
                            </div>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <% =dates[4] %>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="col-xs-12" style="display:flex;">
                                <%--<input id="d5" class="form-control" type="number" onchange="Footer();" max="24" step="0.25" value="<%# Eval("d5") %>"  />--%>
                                <asp:TextBox runat="server" ID="d5" class="form-control" type="number" onchange="Footer();" Text='<%# Eval("d5") %>' min="0" max="24" step="0.25" value='<%# Eval("d5") %>' Enabled='<%# (Convert.ToBoolean(Eval("D5Flag").ToString()))  %>' />
                                <button type="button" class="btn Check btnComment" title="Add Comment" onclick='<%# String.Format("javascript:return OpenPopup(\"{0}\",\"{1}\",\"{2}\")", Eval("Project").ToString(), "Friday",(Convert.ToBoolean(Eval("D5Flag").ToString()))) %>'>
                                    <span class="glyphicon glyphicon-file"></span>
                                </button>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div class="col-xs-12">
                                <label id="d5Sum"></label>
                            </div>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <% =dates[5] %>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="col-xs-12" style="display:flex;">
                                <%--<input id="d6" class="form-control" type="number" onchange="Footer();" max="24" step="0.25" value="<%# Eval("d6") %>" <%# Eval("D6Flag") %> />--%>
                                <asp:TextBox runat="server" ID="d6" class="form-control" type="number" onchange="Footer();" Text='<%# Eval("d6") %>' min="0" max="24" step="0.25" value='<%# Eval("d6") %>' Enabled='<%# (Convert.ToBoolean(Eval("D6Flag").ToString()))  %>' />
                                <button type="button" class="btn Check btnComment" title="Add Comment" onclick='<%# String.Format("javascript:return OpenPopup(\"{0}\",\"{1}\",\"{2}\")", Eval("Project").ToString(), "Saturday",(Convert.ToBoolean(Eval("D6Flag").ToString()))) %>'>
                                    <span class="glyphicon glyphicon-file"></span>
                                </button>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div class="col-xs-12">
                                <label id="d6Sum"></label>
                            </div>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <% =dates[6] %>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="col-xs-12" style="display:flex;">
                                <%--<input id="d7" class="form-control" type="number" onchange="Footer();" max="24" step="0.25" value="<%# Eval("d7") %>" <%# Eval("D7Flag") %> />--%>
                                <asp:TextBox runat="server" ID="d7" class="form-control" type="number" onchange="Footer();" Text='<%# Eval("d7") %>' min="0" max="24" step="0.25" value='<%# Eval("d7") %>' Enabled='<%# (Convert.ToBoolean(Eval("D7Flag").ToString()))  %>' />
                                <button type="button" class="btn Check btnComment" title="Add Comment" onclick='<%# String.Format("javascript:return OpenPopup(\"{0}\",\"{1}\", \"{2}\")", Eval("Project").ToString(), "Sunday",(Convert.ToBoolean(Eval("D7Flag").ToString()))) %>'>
                                    <span class="glyphicon glyphicon-file"></span>
                                </button>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div class="col-xs-12">
                                <label id="d7Sum"></label>
                            </div>
                        </FooterTemplate>
                    </asp:TemplateField>


                </Columns>
                <EmptyDataTemplate>
                    No Allocation Found For The Week Selected!
                </EmptyDataTemplate>
            </asp:GridView>
            <div>
            </div>
            <div class="modal" id="myModal" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" onclick="javascript: return ClosePopup();" data-dismiss="modal">&times;</button>
                            <h4 id="CommentTitle" class="modal-title">Add Comment</h4>
                        </div>
                        <div class="modal-body">
                            <p>
                                <input type="text" id="txtComment" style="width: 80%;" value="" placeholder="Enter your comments here..." /></p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn" data-dismiss="modal" onclick="javascript: return AddComment();">Save</button>
                            <button type="button" class="btn" onclick="javascript: return ClearComment();">Reset</button>
                        </div>
                    </div>
                </div>
            </div>
            <asp:PlaceHolder ID="hiddenControlContainer" runat="server" />
        </div>

        <div class="row">
            <asp:Button runat="server" align="center" Text="Save" ID="btnSave" OnClick="btnSave_Click" OnClientClick="return myFunction()" />

        </div>

    </form>
</asp:Content>


