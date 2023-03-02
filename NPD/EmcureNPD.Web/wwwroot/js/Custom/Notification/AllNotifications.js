let tableId = "AllNotificationTable";
$(document).ready(function () {
    $('#' + tableId).DataTable({
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
    $('#' + tableId).DataTable().page.info();
});
