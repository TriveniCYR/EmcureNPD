var tableId = "IPDPIDFTable";
var objApprRejList = [];

$(document).ready(function () {
    InitializePIDFList();
   
});

function InitializePIDFList() {
    var setDefaultOrder = [0, 'asc'];
    var ajaxObject = {
        "url": $('#hdnBaseURL').val() + AllIPDPIDF,
        "type": "POST",
        "data": function (d) {
            var pageNumber = $('#' + tableId).DataTable().page.info();
            d.PageNumber = pageNumber.page;
        },         
        "datatype": "json"
    };

    var objAccess = $('#hdnAccess').val();
    var objAccessEdit = $('#hdnEdit').val();
    var objAccessAdd = $('#hdnAdd').val();
    
    var columnObject = [
        {
            className: 'dt-control',
            orderable: false,
            defaultContent: '',
            "data": null,
            'render': function (data, type, row, meta) {
                if (objAccess!="False") {
                    return '<input type="checkbox" id="chk_' + row.pidfid + '" name="id[]" onclick="chkClick(this,' + row.pidfid + ');" value="' + $('<div/>').text(data).html() + '">';
                    
                }
                else
                    return '';

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
            "data": "packagingTypeName", "name": "Product Packaging Name"
        },
        {
            "data": "approvedGenerics", "name": "Approved Generics"
        },        
        {
            "data": "launchedGenerics", "name": "Launched Generics"
        },
        {
            "data": "applicant", "name": "Applicant"
        },
        {
            "data": "country", "name": "Country"
        },
        {
            "data": "diaName", "name": "diaName"
        },
        {
            "data": "createdBy", "name": "Created By"
        },  
        {
            "data": "Action", "name": "Action", "render": function (data, type, row, meta) {
                if(objAccessAdd == "True" || objAccessEdit == "True")
                {
                    var html = '';
                    html += '<a class="btn btn-primary" href="/PIDForm/PIDForm?pidfid=' + row.encpidfid + '&bui=' + row.encbud + '"><i class="far fa-plus-square mr-1"></i> Add IPD</a> ';
                    html += '<a class="btn btn-primary" href="/PIDForm/Medical?pidfid=' + row.encpidfid + '&bui=' + row.encbud + '"><i class="far fa-plus-square mr-1"></i> Add Medical</a>';
                    return html;

                }
                else
                return "";
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
function approveRejData(type) {    
    if (objApprRejList != undefined && objApprRejList.length > 0) {
        if (type=="A")
            $('#ApproveModel').modal('show');
        else if (type == "R")
            $('#RejectModel').modal('show');
    }
    else
        toastr.error("Select Pidf");
}
function approveRejConfirm(type) {    
    if (objApprRejList != undefined && objApprRejList.length > 0) {
        var objIds = {
            saveType: type,
            pidfIds: objApprRejList
        };
        ajaxServiceMethod($('#hdnBaseURL').val() + ApproveRejectIds, 'POST', SaveAppRejSuccess, SaveApprRejFormError, JSON.stringify(objIds));
       
    }
    if (type == "A")
        $('#ApproveModel').modal('hide');
    else if (type == "R")
        $('#RejectModel').modal('hide');
}
function SaveAppRejSuccess(data) {
    try {
        if (data._Success === true) {

            toastr.success(data._Message);
            objApprRejList = [];
            $("#IPDPIDFTable").dataTable().fnDestroy();
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
//#endregion

function format(data) {
    // `d` is the original data object for the row
    return (
        '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
        '<tr>' +
        '<td>Full name:</td>' +
        '<td>' +
        data.dosageFormName +
        '</td>' +
        '</tr>' +
        '<tr>' +
        '<td>Extension number:</td>' +
        '<td>' +
        data.moleculeName +
        '</td>' +
        '</tr>' +
        '<tr>' +
        '<td>Extra info:</td>' +
        '<td>And any further details here (images etc)...</td>' +
        '</tr>' +
        '</table>'
    );
}

$('#IPDPIDFTable tbody').on('click', 'td.dt-control', function () {
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


