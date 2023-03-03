let tableId = "AllNotificationTable";
$(document).ready(function () {
        //$.getJSON($('#hdnBaseURL').val() + GetFilteredNotifications + '/CreatedDate/DESC/0/100', function (data) {
    $.getJSON($('#hdnBaseURL').val() + GetAllNotification, function (data) {  
        $('#' + tableId).DataTable({
            "data": data.data,
            columns: [
                {
                    "data": "notificationId"
                },
             { "data": "notificationTitle" },
             { "data": "pidfNo" },
                {
                    "data": "createdDate", "render": function (data, type, row, meta) {
                        return moment(data).format("DD MMM YYYY h:m");
                    }
                }
            ],
          dom: 'Bfrtip',
          buttons: [
              'copy', 'csv', 'excel', 'pdf', 'print'
          ],
        });
    });
});
