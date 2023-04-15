var tableId = "AuditLogTable";
$(document).ready(function () {
    InitializeAuditLogList();
});

function InitializeAuditLogList() {
    var setDefaultOrder = [1, 'desc'];
    var ajaxObject = {
        "url": $('#hdnBaseURL').val() + AllAuditLog,
        "type": "POST",
        //"data": function (d) {
        //    var pageNumber = $('#' + tableId).DataTable().page.info();
        //    d.PageNumber = pageNumber.page;
        //},
        "datatype": "json"
    };

    var columnObject = [
        {
            "data": "moduleName", "name": "Module Name"
        },
        //{
        //    "data": "actionType", "name": "Action Type"
        //},
        {
            "data": "createdDate", "Created Date": "Action", "render": function (data, type, row, meta) {
                return moment(data).format("DD MMM YYYY h:m");
            }
        },
        {
            "data": "createdBy", "name": "Created By"
        },
        {
            "data": "Action", "name": "Action", "render": function (data, type, row, meta) {
                var html = '';
                html += '<a type="button" data-toggle="modal" data-target="#AuditLogViewModel" data-backdrop="static" data-keyboard="false" onclick=\'Viewlog(' + row.log + ',' + '"' + row.createdDate + '"' + ',' + '"' + row.createdBy + '")\' class="ml-1 large-font" ><i class="fa fa-fw fa-eye mr-1"></i></a>';
                return html;
            }
        }
    ];

    IntializingDataTable(tableId, setDefaultOrder, ajaxObject, columnObject);
}

function Viewlog(log, createdDate, createdBy) {
    //const date = new Date(createdDate);
    //const day = date.getDate().toString().padStart(2, '0');
    //const month = (date.getMonth() + 1).toString().padStart(2, '0');
    //const year = date.getFullYear().toString().slice(-2);
    //var formateddate = formatDate(createdDate)
    log = JSON.stringify(log);

    $.ajax({
        url: "/AuditLog/AuditLogPartialView",
        type: "POST",
        data: { CreatedDate: createdDate, CreatedBy: createdBy, log: log },
        success: function (result) {
            $("#AuditLogViewModel").find(".modal-body").html(result);
            $("#AuditLogViewModel").modal('show');
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log("Error: " + textStatus + " - " + errorThrown);
        }
    });
}