var tableId = "PIDFTable";
var objApprRejList = [];
$(document).ready(function () {
    InitializePIDFList();
});

function InitializePIDFList() {
    var setDefaultOrder = [0, 'asc'];
    var ajaxObject = {
        "url": $('#hdnBaseURL').val() + AllPIDF,
        "type": "POST",
        "data": function (d) {
            var pageNumber = $('#' + tableId).DataTable().page.info();
            d.PageNumber = pageNumber.page;
        },
        "datatype": "json"
    };

    var columnObject = [ 
        {     
            className: 'dt-control',
            orderable: false,
            defaultContent: '',
            "data": null,
            'render': function (data, type, row, meta) {
                if (row.status == 'PIDF Created' || row.status == 'PIDF Pending Approval') {
                    return '<input type="checkbox" id="chk_' + row.pidfid + '" name="id[]" onclick="chkClick(this,' + row.pidfid + ');" value="' + $('<div/>').text(data).html() + '">';
                }
                else {
                    return '<input type="checkbox" disabled id="chk_' + row.pidfid + '" name="id[]" onclick="chkClick(this,' + row.pidfid + ');" value="' + $('<div/>').text(data).html() + '">';
                }
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
            "data": "marketExtension", "name": "Market Extension"
        },
        {
            "data": "productPackagingName", "name": "Product Packaging Name"
        },     
        {
            "data": "applicant", "name": "Applicant"
        },
        {
            "data": "countryName", "name": "Country Name"
        },
        {
            "data": "inidication", "name": "Inidication"
        },
        {
            "data": "diaName", "name": "DIA "
        },
        {
            "data": "transformFormRandDDivision", "name": "Transform Form R&D Division"
        },
        {
            "data": "previousProjectCode", "name": "Previous Project Code"
        },
        {
            "data": "sinkCost", "name": "Sink Cost"
        },
        {
            "data": "createdBy", "name": "Created By"
        },
        
        {
            "data": "status", "name": "Status"
        },
        {
            "data": "Action", "name": "Action", "render": function (data, type, row, meta) {
                var html = '';
                if (row.status == 'PIDF Created' || row.status == 'PIDF Pending Approval') {
                    html += '<a class="btn btn-primary" href="/PIDF/PIDF?PIDFId=' + row.pidfid + '"><i class="fa fa-fw fa-edit mr-1"></i>Edit</a>';
                    html += '<a class="btn btn-primary disabled" href="/PIDF/PIDF?PIDFId=' + row.pidfid + '"><i class="fa fa-fw fa-edit mr-1"></i>View</a>';
                } else {
                    html += '<a class="btn btn-primary disabled" href="/PIDF/PIDF?PIDFId=' + row.pidfid + '"><i class="fa fa-fw fa-edit mr-1"></i>Edit</a>';
                    html += '<a class="btn btn-primary" href="/PIDF/PIDF?PIDFId=' + row.pidfid + '"><i class="fa fa-fw fa-edit mr-1"></i>View</a>';
                }
                return html;
            }
        },
    ];

    IntializingDataTable(tableId, setDefaultOrder, ajaxObject, columnObject);
}

function chkClick(cb, pidfId) {
    console.log("Clicked, new value = " + cb.checked + " ---pidfId::" + pidfId);
    if (cb.checked) {
        objApprRejList.push({ pidfId: pidfId })
    }
    else {
        var ind1 = objApprRejList.findIndex(o => o.pidfId == pidfId);
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
        toastr.error("Select Pidf");
}
function approveRejDeleteConfirm(type) {
    if (objApprRejList != undefined && objApprRejList.length > 0) {
        var objIds = {
            saveType: type,
            pidfIds: objApprRejList
        };
        ajaxServiceMethod($('#hdnBaseURL').val() + ApproveRejectDeletePidf, 'POST', SaveAppRejSuccess, SaveApprRejFormError, JSON.stringify(objIds));

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

$('#PIDFTable tbody').on('click', 'td.dt-control', function () {
    var tr = $(this).closest('tr');
    var row = table.row(tr);

    if (row.child.isShown()) {
        // This row is already open - close it
        row.child.hide();
        tr.removeClass('shown');
    } else {
        // Open this row
        row.child(format(row.data())).show();
        tr.addClass('shown');
    }
});
