<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page.aspx.cs" Inherits="TRAT.page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      
  <%--    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous" />
   <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.15/css/bootstrap-multiselect.css" type="text/css" />
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-multiselect/0.9.15/js/bootstrap-multiselect.min.js"></script>
   --%> 
    
  <link href="multiselect.css" rel="stylesheet"/>
    <script src="multiselect.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>
    
<script>
    $('#testSelect1').multiselect({
        includeSelectAllOption: true
    });
   // document.multiselect('#testSelect1');
    $(document).ready(function () {
        $('#companyName').select2('close');
   

    $(function () {
        $('.select2-multiple').select2MultiCheckboxes({
            placeholder: "Choose multiple elements",
        })
        $('.select2-multiple2').select2MultiCheckboxes({
            formatSelection: function (selected, total) {
                return "Selected " + selected.length + " of " + total;
            }
        })
        $('.select2-original').select2({
            placeholder: "Choose elements",
            width: "100%"
        })
    });
    var select = $('select');

    function formatSelection(state) {
        return state.text;
    }

    function formatResult(state) {
        console.log(state)
        if (!state.id) return state.text; // optgroup
        var id = 'state' + state.id.toLowerCase();
        var label = $('<label></label>', { for: id })
            .text(state.text);
        var checkbox = $('<input type="checkbox">', { id: id });

        return checkbox.add(label);
    }

    select.select2({
        closeOnSelect: false,
        formatResult: formatResult,
        formatSelection: formatSelection,
        escapeMarkup: function (m) {
            return m;
        },
        matcher: function (term, text, opt) {
            return text.toUpperCase().indexOf(term.toUpperCase()) >= 0 || opt.parent("optgroup").attr("label").toUpperCase().indexOf(term.toUpperCase()) >= 0
        }
    });
    });
</script>
<%--//$(document).ready(function () {
             $(function () {
                 $(function () {
                     $('#drpTool1').multiselect({
                         includeSelectAllOption: true
                     });
                 });
           //  });--%>
<%--     <script>
         //$(document).ready(function () {
             $(function () {
                 $(function () {
                     $('#drpTool1').multiselect({
                         includeSelectAllOption: true
                     });
                 });
           //  });
             </script>--%>
    <%-- <script type="text/javascript">
                 $(function () {
                     $('#drpTool1').multiselect({
                         includeSelectAllOption: true
                     });
                 });
     </script>--%>


  <%--  <script type="text/javascript">
        $(document).ready(function () {
            let branch_all = [];

            function formatResult(state) {
                if (!state.id) {
                    var btn = $('<div class="text-right"><button id="all-branch" style="margin-right: 10px;" class="btn btn-default">Select All</button><button id="clear-branch" class="btn btn-default">Clear All</button></div>')
                    return btn;
                }

                branch_all.push(state.id);
                var id = 'state' + state.id;
                var checkbox = $('<div class="checkbox"><input id="' + id + '" type="checkbox" ' + (state.selected ? 'checked' : '') + '><label for="checkbox1">' + state.text + '</label></div>', { id: id });
                return checkbox;
            }

            function arr_diff(a1, a2) {
                var a = [], diff = [];
                for (var i = 0; i < a1.length; i++) {
                    a[a1[i]] = true;
                }
                for (var i = 0; i < a2.length; i++) {
                    if (a[a2[i]]) {
                        delete a[a2[i]];
                    } else {
                        a[a2[i]] = true;
                    }
                }
                for (var k in a) {
                    diff.push(k);
                }
                return diff;
            }

            let optionSelect2 = {
                templateResult: formatResult,
                closeOnSelect: false,
                width: '100%'
            };

            let $select2 = $("#country").select2(optionSelect2);

            var scrollTop;

            $select2.on("select2:selecting", function (event) {
                var $pr = $('#' + event.params.args.data._resultId).parent();
                scrollTop = $pr.prop('scrollTop');
            });

            $select2.on("select2:select", function (event) {
                $(window).scroll();

                var $pr = $('#' + event.params.data._resultId).parent();
                $pr.prop('scrollTop', scrollTop);

                $(this).val().map(function (index) {
                    $("#state" + index).prop('checked', true);
                });
            });

            $select2.on("select2:unselecting", function (event) {
                var $pr = $('#' + event.params.args.data._resultId).parent();
                scrollTop = $pr.prop('scrollTop');
            });

            $select2.on("select2:unselect", function (event) {
                $(window).scroll();

                var $pr = $('#' + event.params.data._resultId).parent();
                $pr.prop('scrollTop', scrollTop);

                var branch = $(this).val() ? $(this).val() : [];
                var branch_diff = arr_diff(branch_all, branch);
                branch_diff.map(function (index) {
                    $("#state" + index).prop('checked', false);
                });
            });

            $(document).on("click", "#all-branch", function () {
                $("#country > option").not(':first').prop("selected", true);// Select All Options
                $("#country").trigger("change")
                $(".select2-results__option").not(':first').attr("aria-selected", true);
                $("[id^=state]").prop("checked", true);
                $(window).scroll();
            });

            $(document).on("click", "#clear-branch", function () {
                $("#country > option").not(':first').prop("selected", false);
                $("#country").trigger("change");
                $(".select2-results__option").not(':first').attr("aria-selected", false);
                $("[id^=state]").prop("checked", false);
                $(window).scroll();
            });
        });
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                  <asp:ListBox ID="drpTool1" runat="server" SelectionMode="Multiple"  class="form-control">
             <asp:Listitem Text ="Gauri" Value="1"/>
                <asp:Listitem Text ="Bibek" Value="1"/>
                <asp:Listitem Text ="Priya" Value="1"/>
                </asp:ListBox>
        </div>

         <div class="mt-5 pt-5 col-md-5 offset-md-3">
                <select id="country" placeholder="Select Text" multiple>
                    <option disabled>Select Text</option>
                    <option value="php">Php</option>
                    <option value="bootstrap">Bootstrap</option>
                    <option value="sql">sql</option>
                    <option value="nodejs">Node Js</option>
                    <option value="laravel">Laravel</option>
                    <option value="Jquery">Jquery</option>
                </select>
            </div>

    <select id="companyName" class="form-control__" style="width:100%;height: 34px;" onchange="" tabindex="4">
        <option value="">Select </option>
    </select>

        <div class="row">
  <select name="sel-01" id="sel-01" class="select2-multiple">
    <option></option>
    <option value="AL">Alabama</option>
    <option value="CA">California</option>
    <option value="NY">New York</option>
    <option value="TX">Texas</option>
    <option value="WY">Wyoming</option>
  </select>
</div>
 <div class="row">
  <select name="sel-02" id="sel-02" class="select2-multiple2">
    <option></option>
    <option value="AL">Alabama</option>
    <option value="CA">California</option>
    <option value="NY">New York</option>
    <option value="TX">Texas</option>
    <option value="WY">Wyoming</option>
  </select>
</div>
<div class="row">
  <select name="sel-03" id="sel-03" class="select2-original" multiple>
    <option></option>
    <option value="AL">Alabama</option>
    <option value="CA">California</option>
    <option value="NY">New York</option>
    <option value="TX">Texas</option>
    <option value="WY">Wyoming</option>
  </select>
</div>
        <select multiple style="width: 300px;">
    <optgroup label="Alaskan/Hawaiian Time Zone">
        <option value="AK">Alaska</option>
        <option value="HI">Hawaii</option>
    </optgroup>
    <optgroup label="Pacific Time Zone">
        <option value="CA">California</option>
        <option value="NV">Nevada</option>
        <option value="OR">Oregon</option>
        <option value="WA">Washington</option>
    </optgroup>
    <optgroup label="Mountain Time Zone">
        <option value="AZ">Arizona</option>
        <option value="CO">Colorado</option>
        <option value="ID">Idaho</option>
        <option value="MT">Montana</option>
        <option value="NE">Nebraska</option>
        <option value="NM">New Mexico</option>
        <option value="ND">North Dakota</option>
        <option value="UT">Utah</option>
        <option value="WY">Wyoming</option>
    </optgroup>
    <optgroup label="Central Time Zone">
        <option value="AL">Alabama</option>
        <option value="AR">Arkansas</option>
        <option value="IL">Illinois</option>
        <option value="IA">Iowa</option>
        <option value="KS">Kansas</option>
        <option value="KY">Kentucky</option>
        <option value="LA">Louisiana</option>
        <option value="MN">Minnesota</option>
        <option value="MS">Mississippi</option>
        <option value="MO">Missouri</option>
        <option value="OK">Oklahoma</option>
        <option value="SD">South Dakota</option>
        <option value="TX">Texas</option>
        <option value="TN">Tennessee</option>
        <option value="WI">Wisconsin</option>
    </optgroup>
    <optgroup label="Eastern Time Zone">
        <option value="CT">Connecticut</option>
        <option value="DE">Delaware</option>
        <option value="FL">Florida</option>
        <option value="GA">Georgia</option>
        <option value="IN">Indiana</option>
        <option value="ME">Maine</option>
        <option value="MD">Maryland</option>
        <option value="MA">Massachusetts</option>
        <option value="MI">Michigan</option>
        <option value="NH">New Hampshire</option>
        <option value="NJ">New Jersey</option>
        <option value="NY">New York</option>
        <option value="NC">North Carolina</option>
        <option value="OH">Ohio</option>
        <option value="PA">Pennsylvania</option>
        <option value="RI">Rhode Island</option>
        <option value="SC">South Carolina</option>
        <option value="VT">Vermont</option>
        <option value="VA">Virginia</option>
        <option value="WV">West Virginia</option>
    </optgroup>
</select>


        <select id='testSelect1' multiple>
            <option value='1'>Item 1</option>
            <option value='2'>Item 2</option>
            <option value='3'>Item 3</option>
            <option value='4'>Item 4</option>
            <option value='5'>Item 5</option>
        </select>
    </form>
</body>
</html>
