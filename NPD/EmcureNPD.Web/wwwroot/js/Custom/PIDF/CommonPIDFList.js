var tableId = "PIDFTable";
var objApprRejList = [];
$(document).ready(function () {
    debugger;
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
            className: 'dt-control',
            orderable: false,
            defaultContent: '',
            "data": null,
            'render': function (data, type, row, meta) {              
                    //return '<input type="button"  id="chk_' + row.pidfid + '" onclick="chkClick(this,' + row.pidfid + ');" value="' + $('<div/>').text(data).html() + '">';
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
                "data": "packagingTypeName", "name": "Product Packaging Name"
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
                    html += '<a title="Edit" class="btn btn-primary" href="/PIDF/PIDFCommerciaLDetails?pidfid=' + row.encpidfid + '&bui=' + row.encbud + '"><i class="fa fa-fw fa-edit mr-1"></i></a> ';
                    html += '<a title="View" class="btn btn-primary" href="/PIDF/PIDFCommerciaLDetails?pidfid=' + row.encpidfid + '&bui=' + row.encbud + '"><i class="fa fa-fw fa-eye mr-1"></i></a> ';
                    return html;
                }
            },
        ];

        IntializingDataTable(tableId, setDefaultOrder, ajaxObject, columnObject);
    }

function chkClick(cb, pidfId) {
    if (cb.checked) {
        objApprRejList.push({ pidfId: pidfId })
    }
    else {
        var ind1 = xyz.findIndex(o => o.pidfId == pidfId);
        objApprRejList.splice(ind1, 1);
    }
}