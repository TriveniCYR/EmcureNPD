var tableId = "PIDFTable";
var objApprRejList = [];
$(document).ready(function () {
    InitializePIDFList();
});

function InitializePIDFList() {
    var setDefaultOrder = [23, 'desc'];
    var ajaxObject = {
        "url": $('#hdnBaseURL').val() + AllPIDF + "?ScreenId=" + _screenId,
        "type": "POST",
        "data": function (d) {
            var pageNumber = $('#' + tableId).DataTable().page.info();
            d.PageNumber = pageNumber.page;
            d.ScreenId = _screenId;
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
                if (_screenId == "1" || _screenId == "2" || _screenId == "7" || _screenId == "8") {
                    var _flag = false;
                    if (_screenId == "1") {
                        if (row.pidfStatusID == 2) {
                            _flag = true;
                        }
                        $("#DvApproveReject").show();
                    } else if (_screenId == "2") {
                        if (row.pidfStatusID == 6 || (row.ipd && row.pidfStatusID == 9)) {
                            _flag = true;
                        }
                    } else if (_screenId == "7") {
                        if ((row.pidfStatusID == 17 || (row.finance)) && (row.api || row.inHouses == false)) {
                            _flag = true;
                        }
                        $("#DvApproveReject").show();
                    }
                    else if (_screenId == "8") {
                        if ((row.pidfStatusID == 18 || row.pidfStatusID == 20 || row.pidfStatusID == 21) && row.alReadyApproved == false) {
                            _flag = true;
                            isManagement = _flag;
                        }
                    }
                    return '<input type="checkbox" class="ml-2 custom-list-checkbox" id="chk_' + row.pidfid + '" name="id[]" onclick="chkClick(this,' + row.pidfid + ');" value="' + $('<div/>').text(data).html() + '" ' + (_flag ? "" : "disabled") + '>';
                } else {
                    return "";
                }
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
                return '<a class="small-button btn btn-' + (row.ipd ? "success" : "danger") + '"><i class="fa ' + (row.ipd ? "fa-check" : "fa-remove") + '"></i></a>';
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
            "data": "api", "name": "api", "render": function (data, type, row, meta) {
                return '<a class="small-button btn btn-' + (row.api ? "success" : "danger") + '"><i class="fa ' + (row.api ? "fa-check" : "fa-remove") + '"></i></a>';
            }
        },
        {
            "data": "finance", "name": "Finance", "render": function (data, type, row, meta) {
                return '<a class="small-button btn btn-' + (row.finance ? "success" : "danger") + '"><i class="fa ' + (row.finance ? "fa-check" : "fa-remove") + '"></i></a>';
            }
        },
        //{
        //    "data": "management", "name": "Management", "render": function (data, type, row, meta) {
        //        return '<a class="small-button btn btn-' + (row.management ? "success" : "danger") + '"><i class="fa ' + (row.management ? "fa-check" : "fa-remove") + '"></i></a>';
        //    }
        //},
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
            "data": "createdDate", "name": "createdDate", "render": function (data, type, row, meta) {
                return moment(data).format("DD MMM YYYY h:m");
            }
        },
        {
            "data": "status", "name": "Status", "render": function (data, type, row, meta) {
                var html = '';
                html = '<span style="background-color:' + row.statusColor + ';padding:5px;border-radius:10px;">' + data + '</span>';
                return html;
            }
        },
        {
            "data": null, "name": "Action", "render": function (data, type, row, meta) {
                var html = '';
                if (_screenId == "1") {
                    var _PIDFForm = '/PIDF/PIDF?PIDFId=' + row.pidfid;
                    var _enable = (row.pidfStatusID == 1 || row.pidfStatusID == 2);
                    if (IsEditPIDF || IsApprovePIDF) {
                        html += '<a class="large-font" style="color:' + (_enable ? "#007bff" : "grey") + '" href="' + (_enable ? _PIDFForm : "#") + '"><i class="fa fa-fw fa-edit mr-1"></i></a>';
                    }
                    if (IsViewPIDF) {
                        html += '<a class="ml-1 large-font" href="' + _PIDFForm + '&IsView=1"><i class="fa fa-fw fa-eye mr-1"></i></a>';
                    }
                } else if (_screenId == "2") {
                    var _IPDForm = '/IPD/IPD?pidfid=' + row.encpidfid + '&bui=' + row.encbud + '&ipd=' + row.ipd;

                    var _enable = (row.pidfStatusID == 3 || row.pidfStatusID == 5 || row.pidfStatusID == 6 || row.pidfStatusID == 9);
                    if (IsEditIPD || IsAddIPD || IsApproveIPD) {
                        html += '<a class="large-font" style="color:' + (_enable ? "#007bff" : "grey") + '" href="' + (_enable ? _IPDForm : "#") + '"><i class="fa fa-fw fa-edit mr-1"></i></a>';
                    }
                    var _enableView = (row.pidfStatusID > 4);
                    if (IsViewIPD) {
                        html += '<a class="ml-1 large-font" style="color:' + (_enableView ? "#007bff" : "grey") + '" href="' + (_enableView ? _IPDForm : "#") + '&IsView=1"><i class="fa fa-fw fa-eye mr-1"></i></a>';
                    }
                } else if (_screenId == "3") {
                    var _MedicalForm = '/Medical/Medical?pidfid=' + row.encpidfid + '&bui=' + row.encbud;
                    var _enable = (row.pidfStatusID == 3 || row.pidfStatusID == 5 || row.pidfStatusID == 6 || row.pidfStatusID == 9);
                    if (IsEditMedical || IsAddMedical) {
                        html += '<a class="large-font" style="color:' + (_enable ? "#007bff" : "grey") + '" href="' + (_enable ? _MedicalForm : "#") + '"><i class="fa fa-fw fa-edit mr-1"></i></a>';
                    }
                    var _enableView = (row.pidfStatusID > 4);
                    if (IsViewMedical) {
                        html += '<a class="ml-1 large-font" style="color:' + (_enableView ? "#007bff" : "grey") + '" href="' + (_enableView ? _MedicalForm : "#") + '&IsView=1"><i class="fa fa-fw fa-eye mr-1"></i></a>';
                    }
                } else if (_screenId == "4") {
                    var _CommercialForm = '/Commercial/PIDFCommerciaLDetails?pidfid=' + row.encpidfid + '&bui=' + row.encbud + '&commercial=' + row.commercial;
                    var _enable = (row.pidfStatusID == 7 || row.pidfStatusID == 10 || row.pidfStatusID == 11 || row.pidfStatusID == 12 || row.pidfStatusID == 13 || row.pidfStatusID == 14 || row.pidfStatusID == 15 || row.pidfStatusID == 16 || row.pidfStatusID == 17);//|| row.pidfStatusID == 9
                    if (IsEditCommercial || IsAddCommercial) {
                        html += '<a class="large-font" style="color:' + (_enable ? "#007bff" : "grey") + '" href="' + (_enable ? _CommercialForm : "#") + '"><i class="fa fa-fw fa-edit mr-1"></i></a>';
                    }
                    var _enableView = (row.pidfStatusID > 7);
                    if (IsViewCommercial) {
                        html += '<a class="ml-1 large-font" style="color:' + (_enableView ? "#007bff" : "grey") + '" href="' + (_enableView ? _CommercialForm : "#") + '&IsView=1"><i class="fa fa-fw fa-eye mr-1"></i></a>';
                    }
                } else if (_screenId == "5") {
                    var _APIForm = '/API/';
                    var _APIQS = '?pidfid=' + row.encpidfid + '&bui=' + row.encbud + '&api=' + row.api;
                    var _enable = (row.pidfStatusID == 7 || row.pidfStatusID == 10 || row.pidfStatusID == 11 || row.pidfStatusID == 12 || row.pidfStatusID == 13 || row.pidfStatusID == 14 || row.pidfStatusID == 15 || row.pidfStatusID == 16 || row.pidfStatusID == 17);//|| row.pidfStatusID == 9
                    if (IsEditAPIIPD || IsViewAPIIPD || IsAddAPIIPD) { html += '<a class="large-font" title="IPD" style="color:' + (_enable ? "#007bff" : "grey") + '" href="' + (_enable ? _APIForm + "APIIPDDetailsForm" + _APIQS : "#") + '"><i class="fa fa-fw fa-columns mr-1"></i></a>'; }
                    if (IsEditAPIRnD || IsViewAPIRnD || IsAddAPIRnD) { html += '<a class="large-font" title="RnD" style="color:' + (_enable ? "#007bff" : "grey") + '" href="' + (_enable ? _APIForm + "APIRndDetailsForm" + _APIQS : "#") + '"><i class="fa fa-fw fa-flask mr-1"></i></a>'; }
                    if (IsEditAPICharter || IsViewAPICharter || IsAddAPICharter) { html += '<a class="large-font" title="Charter" style="color:' + (_enable ? "#007bff" : "grey") + '" href="' + (_enable ? _APIForm + "APICharterDetailsForm" + _APIQS : "#") + '"><i class="fa fa-fw fa-map-marker mr-1"></i></a>'; }
                    if (IsViewAPICharterSummary) { html += '<a class="large-font" title="Charter Summary" href="' + (_APIForm + "APICharterSummaryDetailsForm" + _APIQS) + '"><i class="fa fa-fw fa-chart-line mr-1"></i></a>'; }
                } else if (_screenId == "6") {
                    //var _PBFForm = '/PIDF/';
                    var _NewPBFForm = '/PBF/';
                    var _PBFQS = '?pidfid=' + row.encpidfid + '&bui=' + row.encbud + '&pbf=' + row.pbf;
                    var _enable = (row.pidfStatusID == 7 || row.pidfStatusID == 10 || row.pidfStatusID == 11 || row.pidfStatusID == 12 || row.pidfStatusID == 13 || row.pidfStatusID == 14 || row.pidfStatusID == 15 || row.pidfStatusID == 16 || row.pidfStatusID == 17);//|| row.pidfStatusID == 9
                    //html += '<a class="large-font" title="Edit" style="color:' + (_enable ? "#007bff" : "grey") + '" href="' + (_enable ? _PBFForm + "PBFForm" + _PBFQS : "#") + '"><i class="fa fa-fw fa-edit mr-1"></i></a>';
                    if (IsEditPBF || IsAddPBF) {
                        html += '<a class="large-font" title="Edit" style="color:' + (_enable ? "#007bff" : "grey") + '" href="' + (_enable ? _NewPBFForm + "PBF" + _PBFQS : "#") + '"><i class="fa fa-fw fa-edit mr-1"></i></a>';
                    }
                    //html += '<a class="large-font" title="Analytical" style="color:' + (_enable ? "#007bff" : "grey") + '" href="' + (_enable ? _NewPBFForm + "PBFFormAnalytical" + _PBFQS : "#") + '"><i class="fa fa-fw fa-flask mr-1"></i></a>';
                    //html += '<a class="large-font" title="Clinical" style="color:' + (_enable ? "#007bff" : "grey") + '" href="' + (_enable ? _NewPBFForm + "PBFClinicalDetailsForm" + _PBFQS : "#") + '"><i class="fa fa-fw fa-map-marker mr-1"></i></a>';

                    var _enableView = (row.pidfStatusID > 7);
                    if (IsViewPBF) {
                        html += '<a class="large-font" title="View" style="color:' + (_enableView ? "#007bff" : "grey") + '" href="' + (_enableView ? _NewPBFForm + "PBF" + _PBFQS + "&IsView=1" : "#") + '"><i class="fa fa-fw fa-eye mr-1"></i></a>';
                    }
                    //    html += '<a class="large-font" title="Charter Summary" style="color:' + (_enable ? "#007bff" : "grey") + '" href="' + (_enable ? _PBFForm + "PBFForm" + _PBFQS : "#") + '"><i class="fa fa-fw fa-chart-line mr-1"></i></a>';
                } else if (_screenId == "7") {
                    var _FinanceForm = '/Finance/PIDFFinance?pidfid=' + row.encpidfid + '&bui=' + row.encbud + '&finance=' + row.finance + '&api=' + row.api + '&inHouses=' + row.inHouses;

                    var _enable = (row.pidfStatusID == 7 || row.pidfStatusID == 10 || row.pidfStatusID == 11 || row.pidfStatusID == 12 || row.pidfStatusID == 13 || row.pidfStatusID == 14 || row.pidfStatusID == 15 || row.pidfStatusID == 16 || row.pidfStatusID == 17)//|| row.pidfStatusID == 9;
                    if (IsEditFinance || IsAddFinance) {
                        html += '<a class="large-font" title="Edit" style="color:' + (_enable ? "#007bff" : "grey") + '" href="' + (_enable ? _FinanceForm : "#") + '"><i class="fa fa-fw fa-edit mr-1"></i></a>';
                    }
                    var _enableView = (row.pidfStatusID > 7);
                    if (IsViewFinance) {
                        html += '<a class="large-font" title="View" style="color:' + (_enableView ? "#007bff" : "grey") + '" href="' + (_enableView ? _FinanceForm + "&IsView=1" : "#") + '"><i class="fa fa-fw fa-eye mr-1"></i></a>';
                    }
                } else if (_screenId == "8") {
                    var _ManagementForm = '/Management/PIDFManagementApproval?pidfid=' + row.encpidfid + '&bui=' + row.encbud;
                    var _enable = (row.pidfStatusID == 18 || row.pidfStatusID == 20 || row.pidfStatusID == 21 || row.pidfStatusID == 22);
                    if (IsViewManagementHOD) {
                        html += '<a class="large-font" title="View" style="color:' + (_enable ? "#007bff" : "grey") + '" href="' + (_enable ? _ManagementForm + "&IsView=1" : "#") + '"><i class="fa fa-fw fa-eye mr-1"></i></a>';
                    }
                }
                else if (_screenId == "9") {
                    var _ProjectManagementForm = '/Project/ProjectManagement?pidfid=' + row.encpidfid + '&bui=' + row.encbud;
                    var _enable = (row.pidfStatusID == 22);
                    if (IsEditProject || IsAddProject || IsViewProject) {
                        html += '<a class="large-font" style="color:' + (_enable ? "#007bff" : "grey") + '" href="' + (_enable ? _ProjectManagementForm : "#") + '"><i class="fa fa-fw fa-edit mr-1"></i></a>';
                    }
                }
                return html;
            }
        },
    ];

    var dataTableInst = IntializingDataTable(tableId, setDefaultOrder, ajaxObject, columnObject, {
        left: 3,
        right: 2
    });

    // Add event listener for opening and closing details
    $('#' + tableId + ' tbody').on('click', 'td.dt-control', function () {
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
    var _phHTML = "";
    try {
        if (d != null && d != undefined) {
            if (d.productStrength != null && d.productStrength != undefined) {
                var _productStrength = JSON.parse(d.productStrength);
                $.each(_productStrength, function (index, value) {
                    _psHTML += "<tr><td>" + value.Strength + "</td>" + "<td>" + value.UnitofMeasurementName + "</td></tr>";
                });
            }
            if (d.productAPIDetail != null && d.productAPIDetail != undefined) {
                var _productAPI = JSON.parse(d.productAPIDetail);
                $.each(_productAPI, function (index, value) {
                    _paHTML += "<tr><td>" + value.APIName + "</td>" + "<td>" + value.APISourcingName + "</td><td>" + value.APIVendor + "</td></tr>";
                });
            }
            if (d.statusHistory != null && d.statusHistory != undefined) {
                var _productStatus = JSON.parse(d.statusHistory);
                $.each(_productStatus, function (index, value) {
                    _phHTML += "<tr><td style='background-color:" + value.StatusColor + "'>" + value.PIDFStatus + "</td> <td>" + value.Remark + "</td> <td>" + moment(value.CreatedDate).format('dddd, MMMM Do YYYY, h:mm') + "</td><td>" + value.FullName + "</td></tr>";
                });
            }
        }
    } catch (e) {
    }

    return (
        '<table class="custom-table-child"><thead><tr><th>Strength</th><th>Unit</th></tr></thead><tbody>' + _psHTML + '</tbody></table><table class="custom-table-child"><thead><tr><th>API Name</th><th>Sourcing Name</th><th>Vendor</th></tr></thead><tbody>' + _paHTML + '</tbody></table><table class="custom-table-child"><thead><tr><th>Status</th><th>Remark</th><th>Date</th><th>By</th></tr></thead><tbody>' + _phHTML + '</tbody></table>'
    );
}

function chkClick(cb, pidfId) {
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
            pidfIds: objApprRejList,
            screenId: _screenId
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
function ApproveRejectPIDF(type, ScreenId, URL) {
    var _selectedPIDFId = "";
    $.each(objApprRejList, function (index, item) {
        _selectedPIDFId += item.pidfId + (index == objApprRejList.length - 1 ? "" : ",");
    });

    ApproveRejectClick(type, _selectedPIDFId, ScreenId, URL);
}