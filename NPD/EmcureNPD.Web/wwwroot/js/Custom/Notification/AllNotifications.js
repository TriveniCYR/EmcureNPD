let tableId = "AllNotificationTable";
$(document).ready(function () {
    InitializeNotificationList();
});

function InitializeNotificationList() {
    var setDefaultOrder = [2, 'desc'];
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