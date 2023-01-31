var tableId = "PIDFTable";
var objApprRejList = [];
$(document).ready(function () {
    debugger;
    if (SaveStatus != '' && SaveStatus != undefined) {
        if (SaveStatus == 'Saved successfully.')
            toastr.success(SaveStatus);
        else
            toastr.error(SaveStatus);
    }

    InitializePIDFList();
});

function InitializePIDFList() {
    var setDefaultOrder = [0, 'asc'];
    var ajaxObject = {
        "url": $('#hdnBaseURL').val() + GetCommonPIDFList +"?ScreenName=" + ScreenName,
        "type": "POST",
        "data": function (d) {
            var pageNumber = $('#' + tableId).DataTable().page.info();
            d.PageNumber = pageNumber.page;
        },
        "datatype": "json"
    };

    var columnObject = [ 
        {
            "data": null,
            'render': function (data, type, row, meta) {
                return '<input type="checkbox" id="chk_' + row.pidfid + '" name="id[]" onclick="chkClick(this,' + row.pidfid + ');" value="' + $('<div/>').text(data).html() + '">';
            }
        },
        {
            "data": "pidfno", "name": "PIDF No"
        },
        {
            "data": "moleculeName", "name": "Project Name"
        },
        {
            "data": "brandName", "name": "Brand Name"
        },
        {
            "data": "dosageFormName", "name": "DosageForm Name"
        },
        {
            "data": "countryName", "name": "Country Name"
        },
        {
            "data": "packagingTypeName", "name": "Product Packaging Name"
        },
        {
            "data": "createdBy", "name": "Created By"
        },
        {
            "data": "status", "name": "status"
        },
        {
            "data": "Action", "name": "Action", "render": function (data, type, row, meta) {
                var html = '';
                html += '<a class="btn btn-primary m-1" href="/PBF/APIIPDDetailsForm?pidfid=' + row.encpidfid + '&bui=' + row.encbud + '"><i class="fa fa-fw fa-plus mr-1"></i> API IPD Details </a> ';
                html += '<a class="btn btn-primary m-1" href="/PIDF/PBFForm?pidfid=' + row.encpidfid + '&bui=' + row.encbud + '"><i class="fa fa-fw fa-plus mr-1"></i> API Rnd Details </a> ';
                html += '<a class="btn btn-primary m-1" href="/PIDF/PBFForm?pidfid=' + row.encpidfid + '&bui=' + row.encbud + '"><i class="fa fa-fw fa-plus mr-1"></i> API Charter Details </a> ';
                html += '<a class="btn btn-primary m-1" href="/PIDF/PBFForm?pidfid=' + row.encpidfid + '&bui=' + row.encbud + '"><i class="fa fa-fw fa-plus mr-1"></i> API Charter Summary </a> ';
                html += '<a class="btn btn-primary m-1" href="/PIDF/PBFForm?pidfid=' + row.encpidfid + '&bui=' + row.encbud + '"><i class="fa fa-fw fa-plus mr-1"></i> PBF Details </a> ';
                html += '<a class="btn btn-primary m-1" href="/PIDF/PBFForm?pidfid=' + row.encpidfid + '&bui=' + row.encbud + '"><i class="fa fa-fw fa-plus mr-1"></i> PBF Charter Summary </a> ';
                return html;
            }
        }
    ];

    IntializingDataTable(tableId, setDefaultOrder, ajaxObject, columnObject);
}

function chkClick(cb, pidfId) {
    console.log("Clicked, new value = " + cb.checked + " ---pidfId::" + pidfId);
    if (cb.checked) {
        objApprRejList.push({ pidfId: pidfId })
    }
    else {
        var ind1 = xyz.findIndex(o => o.pidfId == pidfId);
        objApprRejList.splice(ind1, 1);
    }
}
function approveRejDeleteData(type) {
    if (objApprRejList != undefined && objApprRejList.length > 0) {
        if (type == "A")
            $('#ApproveModel').modal('show');
        else if (type == "R")
            $('#RejectModel').modal('show');
        else if (type == "D")
            $('#DeleteModel').modal('show');
    }
    else
        toastr.error("Select Pidf No");
}
function approveRejDeleteConfirm(type) {
    if (objApprRejList != undefined && objApprRejList.length > 0) {
        var objIds = {
            saveType: type,
            pidfIds: objApprRejList
        };
        ajaxServiceMethod($('#hdnBaseURL').val() + CommonApproveRejectDeletePidf + "?ScreenName=" + ScreenName, 'POST', SaveAppRejSuccess, SaveApprRejFormError, JSON.stringify(objIds));

    }
    if (type == "A")
        $('#ApproveModel').modal('hide');
    else if (type == "R")
        $('#RejectModel').modal('hide');
    else if (type == "D")
        $('#DeleteModel').modal('hide');
}
function SaveAppRejSuccess(data) {
    try {
        if (data._Success === true) {

            toastr.success(data._Message);
            objApprRejList = [];
            $("#PIDFTable").dataTable().fnDestroy();
            InitializePIDFList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveApprRejFormError(x, y, z) {
    toastr.error(ErrorMessage);
}