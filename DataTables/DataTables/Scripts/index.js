$(document).ready(function () {

    $('#myDataTable').dataTable({
        "bServerSide": true,
        "sAjaxSource": "Home/AjaxHandler",
        "bProcessing": true,
        "aoColumns": [
            {
                "sName": "ID",
                "bSearchable": false,
                "bSortable": false,
                "fnRender": function (oObj) {
                    return '<a href=\"Details/' +
                        oObj.aData[0] + '\">View</a>';
                }
            },
            { "sName": "COMPANY_NAME" },
            { "sName": "ADDRESS" },
            { "sName": "TOWN" }
        ]
    });
}); 