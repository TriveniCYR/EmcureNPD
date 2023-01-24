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
                html += '<button type="button" data-toggle="modal" data-target="#AuditLogViewModel" data-backdrop="static" data-keyboard="false" onclick=Viewlog(' + row.log + ') class="btn btn-primary mr-2" >View</button>';
                return html;
            }
        }
    ];

    IntializingDataTable(tableId, setDefaultOrder, ajaxObject, columnObject);
}

function Viewlog(log) {
    items = [];
    $(log).each(function (index, element) {
        if (index == 0) {
            items.push({
                "id": "jstree_" + element.PropertyName, "parent": "#", "text": element.PropertyName,
                'state': {
                    'opened': true,
                    'selected': true
                },
            });
        }
        else
            items.push({ "id": "jstree_" + element.PropertyName, "parent": "#", "text": element.PropertyName });

        items.push({ 'id': "jstree_eln_" + index, 'parent': "jstree_" + element.PropertyName, 'text': "" + element.NewValue });
        items.push({ 'id': "jstree_elo_" + index, 'parent': "jstree_" + element.PropertyName, 'text': "" + element.OldValue });
    });
    $('#jstree').jstree("destroy");
    $('#jstree').jstree({
        "themes": {
            "responsive": true
        },
        'core': {
            'data': items

        }
    })

}