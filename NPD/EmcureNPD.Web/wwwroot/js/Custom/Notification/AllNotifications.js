let tableId = "AllNotificationTable";
$(document).ready(function () {
    //$.getJSON($('#hdnBaseURL').val() + GetFilteredNotifications + '/CreatedDate/DESC/0/100', function (data) {
    //$.getJSON($('#hdnBaseURL').val() + GetAllNotification, function (data) {
    //    $('#' + tableId).DataTable({
    //        "data": data.data,
    //        columns: [
    //            {
    //                "data": "notificationId"
    //            },
    //            {
    //                "data": "notificationTitle"
    //            },
    //            {
    //                "data": "createdByName", "name": "Created By"
    //            },
    //            {
    //                "data": "createdDate", "render": function (data, type, row, meta) {
    //                    return moment(data).format("DD MMM YYYY h:m");
    //                }
    //            }
    //        ],
    //        dom: 'Bfrtip',
    //        buttons: [
    //            'copy', 'csv', 'excel', 'pdf', 'print'
    //        ],
    //    });
    //});
  //  debugger;
    InitializeNotificationList();
});

function InitializeNotificationList() {
    var setDefaultOrder = [0, 'asc'];
    var ajaxObject = {
        "url": $('#hdnBaseURL').val() + GetAllNotification,
        "type": "POST",
        "data": function (d) {
            var pageNumber = $('#' + tableId).DataTable().page.info();
            d.PageNumber = pageNumber.page;
        },
        "datatype": "json"
    };
    var columnObject = [
        {
            "data": "notificationId"
        },
        {
            "data": "notificationTitle"
        },
        {
            "data": "createdByName", "name": "Created By"
        },
        {
            "data": "createdDate", "render": function (data, type, row, meta) {
                return moment(data).format("DD MMM YYYY h:m");
            }
        }
    ]

    IntializingDataTable(tableId, setDefaultOrder, ajaxObject, columnObject);
}