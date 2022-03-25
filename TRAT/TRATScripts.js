$(function()
{
    $("li").click(function () {
        
        $("li").removeClass('active');
        $(this).addClass('active');

    });

});
function checkAll(obj) {
    debugger;
    var grid = document.getElementsByTagName('input');
    
    if (grid.rows.length > 0) {
        for (i = 1; i < grid.rows.length; i++) {
            cell = grid.rows[i].cells[0];

            for (j = 0; j < cell.childNodes.length; j++) {
                //if childNode type is CheckBox                 
                if (cell.childNodes[j].type == "checkbox") {
                    if (grid.rows[0].cells[0].checked == true) {
                        cell.childNodes[j].checked = false;
                    }
                    else {
                        cell.childNodes[j].checked == true;
                    }
                }
            }
        }

    }
}
