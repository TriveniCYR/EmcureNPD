var tableId = "AuditLogTable";
$(document).ready(function () {
    InitializeAuditLogList();
});

function InitializeAuditLogList() {
    var setDefaultOrder = [2, 'desc'];
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
        {
            "data": "actionType", "name": "Action Type"
        },
        {
            "data": "createdDate", "name": "Created Date"
        },
        {
            "data": "createdBy", "name": "Created By"
        },
        {
            "data": "Action", "name": "Action", "render": function (data, type, row, meta) {
                var html = '';
                html += '<button type="button" data-toggle="modal" data-target="#AuditLogViewModel" data-backdrop="static" data-keyboard="false" onclick=\'Viewlog(' + row.log + ',' + '"' + row.createdDate + '"' + ',' + '"' + row.createdBy + '")\' class="btn btn-primary mr-2" >View</button>';
                return html;
            }
        }
    ];

    IntializingDataTable(tableId, setDefaultOrder, ajaxObject, columnObject);
}

function Viewlog(log,createdDate,createdBy) {
    $('#bodyElement').html('')
    $('#headerElement').html('')
    $('#headerElement').append('<div class="col-12 mb-4"><div class="d-flex"><div class="me-4"><label for="LoggedInUser">LoggedIn User:</label><input id="LoggedInUser" readonly="readonly" value=' + createdBy + '  /><label for="createdDate">Created Date:</label><input id="createdDate" readonly="readonly" value=' + createdDate + ' /></div></div ></div > ')
    $(log).each(function (index, element) {
        if (index != 0) {
            $('#bodyElement').append('<div class="col-12 mb-4"><div class="d-flex"><div class="me-4"><label class="m-0" for="propertyName">PropertyName</label><input type="text" readonly="readonly" id="propertyName" value=' + element.PropertyName + ' /></div><div class="me-4"><label class= "m-0" for="oldValue">OldValue</label><input type="text" readonly="readonly" id="oldValue" value=' + (element.OldValue != "" ? element.OldValue : null) + ' /></div><div class="me-4"><label class="m-0" for= "newValue">NewValue</label><input type="text" readonly="readonly" id="newValue" value=' + (element.NewValue!=""?element.NewValue:null) + ' /></div></div></div>');   
        }
    })

}