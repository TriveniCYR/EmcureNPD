var tableId = "PIDFTable";
var objApprRejList = [];
$(document).ready(function () {
    InitializePIDFList();
});

function InitializePIDFList() {
    var setDefaultOrder = [2, 'desc'];
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
            "data": null
        },
        {
            orderable: false,
            "data": null,
            'render': function (data, type, row, meta) {
                //if (row.status == 'PIDF Created' || row.status == 'PIDF Pending Approval') {
                //    return '<input type="checkbox" id="chk_' + row.pidfid + '" name="id[]" onclick="chkClick(this,' + row.pidfid + ');" value="' + $('<div/>').text(data).html() + '">';
                //}
                //else {
                //    return '<input type="checkbox" disabled id="chk_' + row.pidfid + '" name="id[]" onclick="chkClick(this,' + row.pidfid + ');" value="' + $('<div/>').text(data).html() + '">';
                //}
                return '<input type="checkbox" class="ml-2 custom-list-checkbox" id="chk_' + row.pidfid + '" name="id[]" onclick="chkClick(this,' + row.pidfid + ');" value="' + $('<div/>').text(data).html() + '" ' + (row.pidfStatusID != 2 ? "disabled" : "") + '>';
            }
        },
        {
            "data": "pidfNo", "name": "PIDF No"
        },
        {
            "data": "moleculeName", "name": "Project Name"
        },
        {
            "data": "brandName", "name": "Brand Name"
        },
        {
            "data": "dosageFormName", "name": "Dosage Form"
        },
        {
            "data": "businessUnitName", "name": "Dosage Form"
        },
        {
            "data": "oralName", "name": "Dosage Form"
        },
        {
            "data": "inHouses", "name": "Dosage Form", "render": function (data, type, row, meta) {
                return (data ? "Yes" : "No");
            }
        },
        {
            "data": "ipd", "name": "IPD", "render": function (data, type, row, meta) {
                return '<a class="small-button btn btn-' + (row.ipd ? "success" : "danger") +'"><i class="fa ' + (row.ipd ? "fa-check" : "fa-remove") + '"></i></a>';
            }
        },
        {
            "data": "medical", "name": "medical", "render": function (data, type, row, meta) {
                return '<a class="small-button btn btn-' + (row.medical ? "success" : "danger") + '"><i class="fa ' + (row.medical ? "fa-check" : "fa-remove") + '"></i></a>';
            }
        },
        {
            "data": "commercial", "name": "Commercial", "render": function (data, type, row, meta) {
                return '<a class="small-button btn btn-' + (row.commercial ? "success" : "danger") + '"><i class="fa ' + (row.commercial ? "fa-check" : "fa-remove") + '"></i></a>';
            }
        },
        {
            "data": "pbf", "name": "PBF", "render": function (data, type, row, meta) {
                return '<a class="small-button btn btn-' + (row.pbf ? "success" : "danger") + '"><i class="fa ' + (row.pbf ? "fa-check" : "fa-remove") + '"></i></a>';
            }
        },
        {
            "data": "finance", "name": "Finance", "render": function (data, type, row, meta) {
                return '<a class="small-button btn btn-' + (row.finance ? "success" : "danger") + '"><i class="fa ' + (row.finance ? "fa-check" : "fa-remove") + '"></i></a>';
            }
        },
        {
            "data": "management", "name": "Management", "render": function (data, type, row, meta) {
                return '<a class="small-button btn btn-' + (row.management ? "success" : "danger") + '"><i class="fa ' + (row.management ? "fa-check" : "fa-remove") + '"></i></a>';
            }
        },
        {
            "data": "marketExtension", "name": "Market Extension"
        },
        {
            "data": "productPackagingName", "name": "Product Packaging"
        },
        {
            "data": "rfdBrand", "name": "RFDBrand"
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
            "data": "createdBy", "name": "Created By"
        },
        {
            "data": "status", "name": "Status"
        },
        {
            "data": "Action", "name": "Action", "render": function (data, type, row, meta) {
                var html = '';
                var _PIDFForm = '/PIDF/PIDF?PIDFId=' + row.pidfid + '';
                html += '<a class="large-font" href="' + ((row.pidfStatusID == 1 || row.pidfStatusID == 2) ? _PIDFForm : "#") +'"><i class="fa fa-fw fa-edit mr-1"></i></a>';
                html += '<a class="ml-1 large-font" href="/PIDF/PIDF?PIDFId=' + row.pidfid + '"><i class="fa fa-fw fa-eye mr-1"></i></a>';
                //if (row.status == 'PIDF Created' || row.status == 'PIDF Pending Approval') {
                //    html += '<a class="btn btn-primary" href="/PIDF/PIDF?PIDFId=' + row.pidfid + '"><i class="fa fa-fw fa-edit mr-1"></i>Edit</a>';
                //    html += '<a class="btn btn-primary disabled" href="/PIDF/PIDF?PIDFId=' + row.pidfid + '"><i class="fa fa-fw fa-edit mr-1"></i>View</a>';
                //} else {
                //    html += '<a class="btn btn-primary disabled" href="/PIDF/PIDF?PIDFId=' + row.pidfid + '"><i class="fa fa-fw fa-edit mr-1"></i>Edit</a>';
                //    html += '<a class="btn btn-primary" href="/PIDF/PIDF?PIDFId=' + row.pidfid + '"><i class="fa fa-fw fa-edit mr-1"></i>View</a>';
                //}
                return html;
            }
        },
    ];

    var dataTableInst = IntializingDataTable(tableId, setDefaultOrder, ajaxObject, columnObject);

    // Add event listener for opening and closing details
    $('#' + tableId +' tbody').on('click', 'td.dt-control', function () {
        var tr = $(this).closest('tr');
        var row = dataTableInst.row(tr);

        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        } else {
            // Open this row
            row.child(CustomizeChildContent(row.data())).show();
            tr.addClass('shown');
        }
    });
}
/* Formatting function for row details - modify as you need */
function CustomizeChildContent(d) {
    // `d` is the original data object for the row
    var _psHTML = "";
    var _paHTML = "";
    if (d.productStrength != null) {
        var _productStrength = JSON.parse(d.productStrength);
        $.each(_productStrength, function (index, value) {
            _psHTML += "<tr><td>" + value.Strength + "</td>" + "<td>" + value.UnitofMeasurementName + "</td></tr>";
        });
    }
    if (d.productAPIDetail != null) {
        var _productAPI = JSON.parse(d.productAPIDetail);
        $.each(_productAPI, function (index, value) {
            _paHTML += "<tr><td>" + value.APIName + "</td>" + "<td>" + value.APISourcingName + "</td><td>" + value.APIVendor + "</td></tr>";
        });
    }
    return (
        '<table><thead><tr><th>Strength</th><th>Unit</th></tr></thead><tbody>' + _psHTML + '</tbody></table><div class="clearfix">&nbsp;</div><table><thead><tr><th>API Name</th><th>Sourcing Name</th><th>Vendor</th></tr></thead><tbody>' + _paHTML +'</tbody></table>'
    );
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
