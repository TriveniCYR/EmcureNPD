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

function Viewlog(log, createdDate, createdBy) {
    const date = new Date(createdDate);
    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const year = date.getFullYear().toString().slice(-2);

    $('#bodyElement').html('')
    $('#headerElement').html('')
    $('#headerElement').append('<div class="col-12 mb-4"><div class="d-flex"><div class="me-4"><label for="LoggedInUser">LoggedIn User:</label><input id="LoggedInUser" disabled="disabled" value=' + createdBy + '  /><label for="createdDate">Created Date:</label><input id="createdDate" disabled="disabled" value=' + `${day}/${month}/${year}` + ' /></div></div ></div > ')
    $(log).each(function (index, element) {
            if (element.hasOwnProperty('Values')) {
                $('#bodyElement').append('<hr />' +'<h3>PropertyName'+'('+element.PropertyName+')'+'</h3>')
                $(element.Values).each(function (indexs, elements) {
                    $('#bodyElement').append('<div class="col-12 mb-4"><div class="d-flex"><div class="me-4"><label class="m-0" for="propertyName">PropertyName</label><input type="text" disabled="disabled" id="propertyName" value=' + (elements.DisplayName == null || "" ? elements.PropertyName : elements.DisplayName) + ' /></div><div class="me-4"><label class= "m-0" for="oldValue">OldValue</label><input type="text" disabled="disabled" id="oldValue" value=' + (elements.OldValue != "" ? (elements.OldValue == "True" ? "Yes" : (elements.OldValue == "False" ? "No" : elements.OldValue)) : null) + ' /></div><div class="me-4"><label class="m-0" for= "newValue">NewValue</label><input type="text" disabled="disabled" id="newValue" value=' + (elements.NewValue != "" ? (elements.NewValue == "True" ? "Yes" : (elements.NewValue == "False" ? "No" : elements.NewValue)) : null) + ' /></div></div></div>');
                })
            }
            else {
                $('#bodyElement').append('<div class="col-12 mb-4"><div class="d-flex"><div class="me-4"><label class="m-0" for="propertyName">PropertyName</label><input type="text" disabled="disabled" id="propertyName" value=' + (element.DisplayName == null || "" ? element.PropertyName : element.DisplayName) + ' /></div><div class="me-4"><label class= "m-0" for="oldValue">OldValue</label><input type="text" disabled="disabled" id="oldValue" value=' + (element.OldValue != "" ? (element.OldValue == "True" ? "Yes" : (element.OldValue == "False" ? "No" : element.OldValue)) : null) + ' /></div><div class="me-4"><label class="m-0" for= "newValue">NewValue</label><input type="text" disabled="disabled" id="newValue" value=' + (element.NewValue != "" ? (element.NewValue == "True" ? "Yes" : (element.NewValue == "False" ? "No" : element.NewValue)) : null) + ' /></div></div></div>');
            }
    })

}