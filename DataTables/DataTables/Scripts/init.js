$(document).ready(function () {
               
    // Setup JQuery UI date picker format to dd/mm/yy
    $.datepicker.regional[""].dateFormat = 'dd/mm/yy';
    $.datepicker.setDefaults($.datepicker.regional['']);

    $('#myDataTable').dataTable({
        "bServerSide": true,
        "sAjaxSource": "/ColumnFilter/DataProviderAction",
        "bJQueryUI": true
    }).columnFilter({
        "aoColumns": [
            { "type": "number-range" },
            { "type": "text" },
            { "type": "select", values: ["London", "Essex", "Lothian", "Leicester", "Buckinghamshire"] },
            { "type": "date-range" }
        ]
    });
});