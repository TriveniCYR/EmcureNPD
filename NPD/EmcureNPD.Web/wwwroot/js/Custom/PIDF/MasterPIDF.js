var objApprRejList = [];
$(document).ready(function () {
    GetPIDFDropdown();
    var uri = document.getElementById("PIDFID").value;    
    var status = document.getElementById("StatusId").value;    
    if (uri > 0 & status != 1 & status != 2) {
        readOnlyForm();
    }    
});
function LoadData() {
    var options = $('#productStrengthUnit_0 option');
    $('#productStrengthBody tr').each(function (index, element) {
        $('#productStrengthUnit_' + index + '').append(options);
    });
}

function GetPIDFDropdown() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllPIDF, 'GET', GetPIDFDropdownSuccess, GetPIDFDropdownError);
}
function GetPIDFDropdownSuccess(data) {
    try {
        if (data != null) {
            $(data.MasterOrals).each(function (index, item) {
                $('#OralId').append('<option value="' + item.oralId + '">' + item.oralName + '</option>');
            });
            $(data.MasterUnitofMeasurements).each(function (index, item) {
                $('#UnitofMeasurementId').append('<option value="' + item.unitofMeasurementId + '">' + item.unitofMeasurementName + '</option>');
                $('#productStrengthUnit_' + index + '').append('<option value="' + item.unitofMeasurementId + '">' + item.unitofMeasurementName + '</option>'); 
            });
            $(data.MasterAPISourcing).each(function (index, item) {
                $('#apiSourcingData_0').append('<option value="' + item.apiSourcingId + '">' + item.apiSourcingName + '</option>');
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
            $(data.MasterCountrys).each(function (index, item) {
                $('#RFDCountryId').append('<option value="' + item.countryId + '">' + item.countryName + '</option>');
            });
            $(data.MarketExtensions).each(function (index, item) {
                $('#MarketExtenstionId').append('<option value="' + item.marketExtenstionId + '">' + item.marketExtenstionName + '</option>');
            });
            $(data.InHouses).each(function (index, item) {
                $('#InhouseDropdownId').append('<option value="' + item.inHouseId + '">' + item.inHouseName + '</option>');
            });
            $(data.MasterDIAs).each(function (index, item) {
                $('#Diaid').append('<option value="' + item.diaId + '">' + item.diaName + '</option>');
            });
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
        $('#SavePIDFModel').modal('hide');
        if (data._Success === true) {
            window.location = "/PIDF/PIDFList";
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
    var i = parseInt(j) + 1;
    var table = $('#productStrengthBody');
    if (i == 1) {
        var node = $('#productStrengthRow_0').clone(true);
        table.append(node);
        $('#productStrengthBody tr:eq(1)').prop('id', 'productStrengthRow_' + i + '');
    }
    else {
        var node = $('#productStrengthRow_' + j + '').clone(true);
        table.append(node);
        $('#productStrengthBody tr:eq(' + i + ')').prop('id', 'productStrengthRow_' + i + '');
    }
    $('#productStrengthRow_' + i + ' td:first input').prop('id', 'pidfProductStregthEntities[' + i + '].Strength');
    $('#productStrengthRow_' + i + ' td:first input').prop('name', 'pidfProductStregthEntities[' + i + '].Strength');
    $('#productStrengthRow_' + i + ' td:eq(1) select').prop('id', 'productStrengthUnit_' + i + '');
    $('#productStrengthRow_' + i + ' td:eq(1) select').prop('name', 'pidfProductStregthEntities[' + i + '].UnitofMeasurementId');
    $('#productStrengthRow_' + i + ' td:eq(2) i').prop('id', 'addIcon_' + i + '');
    $('#productStrengthRow_' + i + ' td:eq(2) i').attr('onclick', 'addRowProductStrength(' + i + ')');
}

//#region Delete PIDF
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

 //#endregion

//#region API Rows PIDF
function addRowApiDetails(j) {   
    var i = 0;
    var table = $('#apiDetailsBody');
    
    var node = $('#apiDetailsRow_' + j + '').clone(true);
    table.append(node);

    var rowCount = $("#apiDetailsBody tr").length;
    //alert(rowCount);
    $('#apiDetailsBody tr:eq(' + (rowCount) + ')').prop('id', 'apiDetailsRow_' + i + '');
    //}
    $('#apiDetailsRow_' + i + ' td:first input').prop('id', 'pidfApiDetailEntities[' + i + '].Apiname');
    $('#apiDetailsRow_' + i + ' td:first input').prop('name', 'pidfApiDetailEntities[' + i + '].Apiname');
    $('#apiDetailsRow_' + i + ' td:first input').val('');

      $('#apiDetailsRow_' + j + ' td:eq(1) select').prop('id', 'apiSourcingData_' + i + '');
      $('#apiDetailsRow_' + j + ' td:eq(1) select').prop('name', 'pidfApiDetailEntities[' + i + '].ApisourcingId');

    $('#apiDetailsRow_' + i + ' td:eq(2) input').prop('id', 'pidfApiDetailEntities[' + i + '].Apivendor');
    $('#apiDetailsRow_' + i + ' td:eq(2) input').prop('name', 'pidfApiDetailEntities[' + i + '].Apivendor');
    $('#apiDetailsRow_' + i + ' td:eq(2) input').val('');

    $('#apiDetailsRow_' + i + ' td:eq(3) i').prop('id', 'addIconAPI_' + i + '');
    $('#apiDetailsRow_' + i + ' td:eq(3) i').attr('onclick', 'addRowApiDetails(' + i + ')');
    $('#apiDetailsRow_' + i + ' td:eq(3) spam i').prop('id', 'deleteIconAPI_' + i + '');
    $('#apiDetailsRow_' + i + ' td:eq(3) spam i').attr('onclick', 'deleteRowApiDetails(' + i + ')');
    $('#apiDetailsRow_' + i + ' td:eq(3) input').prop('id', 'pidfApiDetailEntities[' + i + '].Pidfapiid');
    $('#apiDetailsRow_' + i + ' td:eq(3) input').prop('name', 'pidfApiDetailEntities[' + i + '].Pidfapiid');
    $('#apiDetailsRow_' + i + ' td:eq(3) input').val(0);
}
function deleteRowApiDetails(j) {
    $("#apiDetailsRow_" + j).remove();   
}
 //#endregion


//#region STR Rows

function addRowProductStrength(j) {
    var i = 0;
    var table = $('#productStrengthBody');

    var node = $('#productStrengthRow_' + j + '').clone(true);
    table.append(node);

    var rowCount = $("#productStrengthBody tr").length;
    //alert(rowCount);
    $('#productStrengthBody tr:eq(' + (rowCount) + ')').prop('id', 'productStrengthRow_' + i + '');
    //}
    $('#productStrengthRow_' + i + ' td:first input').prop('id', 'pidfProductStregthEntities[' + i + '].Strength');
    $('#productStrengthRow_' + i + ' td:first input').prop('name', 'pidfProductStregthEntities[' + i + '].Strength');
    $('#productStrengthRow_' + i + ' td:first input').val('');

    $('#productStrengthRow_' + i + ' td:eq(1) select').prop('id', 'productStrengthUnit_' + i + '');
    $('#productStrengthRow_' + i + ' td:eq(1) select').prop('name', 'pidfProductStregthEntities[' + i + '].UnitofMeasurementId');

    $('#productStrengthRow_' + i + ' td:eq(2) i').prop('id', 'addIconProductStrength_' + i + '');
    $('#productStrengthRow_' + i + ' td:eq(2) i').attr('onclick', 'addRowProductStrength(' + i + ')');
    $('#productStrengthRow_' + i + ' td:eq(2) spam i').prop('id', 'deleteIconProductStrength_' + i + '');
    $('#productStrengthRow_' + i + ' td:eq(2) spam i').attr('onclick', 'deleteRowProductStrength(' + i + ')');
    $('#productStrengthRow_' + i + ' td:eq(2) input').prop('id', 'pidfProductStregthEntities[' + i + '].PidfproductStrengthId');
    $('#productStrengthRow_' + i + ' td:eq(2) input').prop('name', 'pidfProductStregthEntities[' + i + '].PidfproductStrengthId');
    $('#productStrengthRow_' + i + ' td:eq(2) input').val(0);
}
function deleteRowProductStrength(j) {
    $("#productStrengthRow_" + j).remove();
}

 //#endregion

function readOnlyForm() {
    //if (getUrlVars()["IsEditMode"] == true) {
        $('input').attr('readonly', true).attr('disabled', true);
        $('button').attr('readonly', true).attr('disabled', true);
        $('select').attr('readonly', true).attr('disabled', true).trigger("change");
    //}
}

function SaveClick() {
    $('#SaveType').val('Sv');  
}

function SaveDraftClick() {
    $('#SaveType').val('Drf');    
}
