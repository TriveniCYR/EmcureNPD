var tableId = "APIListTable";
$(document).ready(function () {
    InitializeAPIList();
});

function InitializeAPIList() {
    var setDefaultOrder = [0, 'asc'];
    var ajaxObject = {
        "url": $('#hdnBaseURL').val() + AllAPIList,
        "type": "POST",
        "data": function (d) {
            var pageNumber = $('#' + tableId).DataTable().page.info();
            d.PageNumber = pageNumber.page;
        },
        "datatype": "json"
    };

    var columnObject = [
        {
            "data": "pidfno", "name": "PIDFNo"
        },
        {
            "data": "moleculeName", "name": "ProjectName"
        },
        {
            "data": "brandName", "name": "BrandName"
        },
        {
            "data": "Action", "name": "Action", "render": function (data, type, row, meta) {
                var html = '';

                html += '<a class="btn btn-primary" href="/PIDF/PIDFManage?PIDFId=' + row.pidfid + '"><i class="fa fa-fw fa-edit mr-1"></i>Edit</a>';
                html += '<a class="btn btn-danger ml-2" data-toggle="modal" data-target="#DeletePIDFModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationPIDFUser(' + row.pidfid + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i>Delete</a>';
                return html;
            }
        }
    ];

    IntializingDataTable(tableId, setDefaultOrder, ajaxObject, columnObject);
}

//#region Delete APIList
function ConfirmationDeleteAPIList(id) {
    $('#DeleteAPIListModel #PIDFID').val(id);
}
function DeleteAPIList() {
    var tempInAtiveID = $('#DeleteAPIListModel #PIDFID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteAPIListByIdUrl + "/" + tempInAtiveID, 'POST', DeleteAPIListByIdSuccess, DeleteAPIListByIdError);
}
function DeleteAPIListByIdSuccess(data) {
    try {
        if (data._Success === true) {
            toastr.success(RecordDelete);
            $('#' + tableId).DataTable().draw();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteAPIListByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}

//#endregion