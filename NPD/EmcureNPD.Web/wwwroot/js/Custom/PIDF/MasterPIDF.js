var objApprRejList = [];
var _mode = 0;
var _PIDFId = 0;
$(document).ready(function () {
    try {
        _PIDFId = parseInt($('#hdnPIDFId').val());
        //_mode = $('#hdnIsView').val(); //parseInt($('#hdnPIDFId').val());
        _mode = getParameterByName("IsView");
    } catch (e) {
        _mode = getParameterByName("IsView");
        _PIDFId = parseInt(getParameterByName("PIDFId"));
    }
    
    if (_mode == 1) {
        readOnlyForm();
    }
    if (_PIDFId == 0) {
        $('#InHouses').prop("checked", true).val(true);
    }
    GetPIDFDropdown();
    SetChildRowDeleteIcon();

    var uri = document.getElementById("PIDFID").value;
    var status = $('#dvPIDFContainer').find("#StatusId").val();
    if (uri > 0 & status != 1 & status != 2) {
        readOnlyForm();
    }

    $('#BusinessUnitId').change(function (e) {
        if ($(this).val() != "") {
            if (parseInt($(this).val()) > 0) {
                ajaxServiceMethod($('#hdnBaseURL').val() + getCountryByBusinessUnitId + "/" + $(this).val(), 'GET', GetCountryByBusinessUnitSuccess, GetCountryByBusinessUnitError);
            }
        }
    });
    $('#InhouseDropdownId').change(function (e) {
        var _selected = ($(this).val() == "1" ? true : false);
        $('#InHouses').prop("checked", _selected).val(_selected);
    });
});

$("[id*='].Strength']").blur(function () {
    var strengthval = $(this).val();
    $(this).val($.trim(strengthval));
})
$("[id*='__Apiname']").blur(function () {
    var textboxval = $(this).val();
    $(this).val($.trim(textboxval));
})
function GetCountryByBusinessUnitSuccess(data) {
    try {
        $('#RFDCountryId').find('option').remove()
        if (data._object != null && data._object.length > 0) {
            var _emptyOption = '<option value="">-- Select --</option>';
            $('#RFDCountryId').append(_emptyOption);
            $(data._object).each(function (index, item) {
                $('#RFDCountryId').append('<option value="' + item.countryId + '">' + item.countryName + '</option>');
            });

            try {
                if (_PIDFId > 0) {
                    $('#RFDCountryId').val($('#hdnRFDCountryId').val());
                }
            } catch (e) {

            }

        }
    }
    catch (e) {
        toastr.error(ErrorMessage);
    }
}
function GetCountryByBusinessUnitError(x, y, z) {
    toastr.error(ErrorMessage);
}
//function LoadData() {
//    var options = $('#productStrengthUnit_0 option');
//    $('#productStrengthBody tr').each(function (index, element) {
//        $('#productStrengthUnit_' + index + '').append(options);
//    });
//}

function GetPIDFDropdown() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllPIDF + "/" + $('#LogInId').val(), 'GET', GetPIDFDropdownSuccess, GetPIDFDropdownError);
}
function GetPIDFDropdownSuccess(data) {
    try {
        if (data != null) {
            var _emptyOption = '<option value="">-- Select --</option>';
            $('#OralId').append(_emptyOption);
            $('#UnitofMeasurementId').append(_emptyOption);
            $('#productApiSourcing').append(_emptyOption);
            $('#DosageFormId').append(_emptyOption);
            $('#PackagingTypeId').append(_emptyOption);
            $('#BusinessUnitId').append(_emptyOption);
            $('#MarketExtenstionId').append(_emptyOption);
            $('#Diaid').append(_emptyOption);
            $('.productStrengthUnit').append(_emptyOption);
            $('.productApiSourcing').append(_emptyOption);

            $(data.MasterOrals).each(function (index, item) {
                $('#OralId').append('<option value="' + item.oralId + '">' + item.oralName + '</option>');
            });
            $(data.MasterUnitofMeasurements).each(function (index, item) {
                $('#UnitofMeasurementId').append('<option value="' + item.unitofMeasurementId + '">' + item.unitofMeasurementName + '</option>');
                $('.productStrengthUnit').append('<option value="' + item.unitofMeasurementId + '">' + item.unitofMeasurementName + '</option>');
            });
            $(data.MasterAPISourcing).each(function (index, item) {
                $('.productApiSourcing').append('<option value="' + item.apiSourcingId + '">' + item.apiSourcingName + '</option>');
            });
            $(data.MasterDosageForms).each(function (index, item) {
                $('#DosageFormId').append('<option value="' + item.dosageFormId + '">' + item.dosageFormName + '</option>');
            });
            $(data.MasterPackagingTypes).each(function (index, item) {
                $('#PackagingTypeId').append('<option value="' + item.packagingTypeId + '">' + item.packagingTypeName + '</option>');
            });
            $(data.MasterBusinessUnits).each(function (index, item) {
                $('#BusinessUnitId').append('<option value="' + item.businessUnitId + '">' + item.businessUnitName + '</option>');
            });
            //$(data.MasterCountrys).each(function (index, item) {
            //    $('#RFDCountryId').append('<option value="' + item.countryId + '">' + item.countryName + '</option>');
            //});
            $(data.MarketExtensions).each(function (index, item) {
                $('#MarketExtenstionId').append('<option value="' + item.marketExtenstionId + '">' + item.marketExtenstionName + '</option>');
            });
            $(data.InHouses).each(function (index, item) {
                $('#InhouseDropdownId').append('<option value="' + item.inHouseId + '">' + item.inHouseName + '</option>');
            });
            $(data.MasterDIAs).each(function (index, item) {
                $('#Diaid').append('<option value="' + item.diaId + '">' + item.diaName + '</option>');
            });

            try {
                if (_PIDFId > 0) {
                    $('#OralId').val($('#hdnOralId').val());
                    $('#UnitofMeasurementId').val($('#hdnUnitofMeasurementId').val());
                    $('#DosageFormId').val($('#hdnDosageFormId').val());
                    $('#PackagingTypeId').val($('#hdnPackagingTypeId').val());
                    $('#BusinessUnitId').val($('#hdnBusinessUnitId').val()).trigger("change");
                    $('#MarketExtenstionId').val($('#hdnMarketExtenstionId').val());
                    $('#Diaid').val($('#hdnDiaid').val());
                    $('#InhouseDropdownId').val($('#hdnInhouseDropdownId').val()).trigger("change");

                    $(".productStrengthUnit").each(function () {
                        $(this).val($(this).prev("#hdnProductStrengthUnit").val());
                    });
                    $(".productApiSourcing").each(function () {
                        $(this).val($(this).prev("#hdnAPISourcing").val());
                    });
                }
            } catch (e) {

            }
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPIDFDropdownError(x, y, z) {
    toastr.error(ErrorMessage);
}

function SavePIDFForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SavePIDF, 'POST', SavePIDFFormSuccess, SavePIDFFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SavePIDFFormSuccess(data) {
    try {
        //$('#SavePIDFModel').modal('hide');
        if (data._Success === true) {
            window.location = "/PIDF/PIDFList?ScreenId=1";
            toastr.success(RecordInsertUpdate);
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SavePIDFFormError(x, y, z) {
    toastr.error(ErrorMessage);
}

function addRowProductStrength(j) {
    var table = $('#productStrengthBody');
    var node = $('#productStrengthRow_0').clone(true);
    table.find('tr:last').after(node);
    table.find('tr:last').find("input").val("");
    SetChildRowDeleteIcon();
}
function deleteRowProductStrength(j, element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
}

function approveRejDeleteData(type) {
    if (type == "A")
        $('#ApproveModel').modal('show');
    else if (type == "R")
        $('#RejectModel').modal('show');
    else if (type == "D")
        $('#DeleteModel').modal('show');
}
function approveRejDeleteConfirm(type, id) {
    if (id != undefined && id != 0) {
        objApprRejList.push({ pidfId: id })
        var objIds = {
            saveType: type,
            pidfIds: objApprRejList
        };
        ajaxServiceMethod($('#hdnBaseURL').val() + ApproveRejectDeletePidf, 'POST', SavePIDFFormSuccess, SaveApprRejFormError, JSON.stringify(objIds));
    }
    if (type == "A")
        $('#ApproveModel').modal('hide');
    else if (type == "R")
        $('#RejectModel').modal('hide');
    else if (type == "D")
        $('#DeleteModel').modal('hide');
}

function SaveApprRejFormError(x, y, z) {
    toastr.error(ErrorMessage);
}

function addRowApiDetails(j) {
    var table = $('#apiDetailsBody');
    var node = $('#apiDetailsRow_0').clone(true);
    table.find('tr:last').after(node);
    table.find('tr:last').find("input").val("");
    SetChildRowDeleteIcon();
}
function deleteRowApiDetails(j, element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
}


function readOnlyForm() {
    $('#dvPIDFContainer').find('input').attr('readonly', true).attr('disabled', true);
    //$('button').attr('readonly', true).attr('disabled', true);
    $('#dvPIDFContainer').find('select').attr('readonly', true).attr('disabled', true).trigger("change");
    $('#dvPIDFContainer').find('.operationButton').hide();
    $('#btnPIDFCancel').show(); 
}

function SaveClick() {
    $('#SaveType').val('submit');
    SetChildRows();
}
function SaveDraftClick() {
    $('#SaveType').val('draft');
    SetChildRows();
}
function SetChildRows() {
    $.each($('#APIDetailsTable tbody tr'), function (index, value) {
        $(this).find("td:first input").attr("name", "pidfApiDetailEntities[" + index.toString() + "].Apiname");
        $(this).find("td:eq(1) select").attr("name", "pidfApiDetailEntities[" + index.toString() + "].ApisourcingId");
        $(this).find("td:eq(2) input").attr("name", "pidfApiDetailEntities[" + index.toString() + "].Apivendor");
    });
    $.each($('#ProductStrengthTable tbody tr'), function (index, value) {
        $(this).find("td:first input").attr("name", "pidfProductStregthEntities[" + index.toString() + "].Strength");
        $(this).find("td:eq(1) select").attr("name", "pidfProductStregthEntities[" + index.toString() + "].UnitofMeasurementId");
    });
}
function SetChildRowDeleteIcon() {
    if ($('#APIDetailsTable tbody tr').length > 1 && _mode != 1) {
        $('.apiDeleteIcon').show();
    } else {
        $('.apiDeleteIcon').hide();
    }

    if ($('#ProductStrengthTable tbody tr').length > 1 && _mode != 1) {
        $('.strengthDeleteIcon').show();
    } else {
        $('.strengthDeleteIcon').hide();
    }
}