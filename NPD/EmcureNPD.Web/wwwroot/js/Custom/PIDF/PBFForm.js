﻿
$(document).ready(function () {   
    GetPBFDropdown();
    SetDivReadonly();    
    hideForms();
});
function GetPBFDropdown() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllPBF, 'GET', GetPBFDropdownSuccess, GetPBFDropdownError);
}
function GetPBFDropdownSuccess(data) {
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
                $('#PbfDosageFormId').append('<option value="' + item.dosageFormId + '">' + item.dosageFormName + '</option>');
            });
            $(data.MasterPackagingTypes).each(function (index, item) {
                $('#PbfPackagingTypeId').append('<option value="' + item.packagingTypeId + '">' + item.packagingTypeName + '</option>');
            });
            $(data.MasterBusinessUnits).each(function (index, item) {
                $('#BusinessUnitId').append('<option value="' + item.businessUnitId + '">' + item.businessUnitName + '</option>');
            });
            $(data.MasterCountrys).each(function (index, item) {
                $('#PbfRFDCountryId').append('<option value="' + item.countryId + '">' + item.countryName + '</option>');
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
            // 
            $(data.MasterBERequirements).each(function (index, item) {
                $('#BERequirementId').append('<option value="' + item.beRequirementId + '">' + item.beRequirementName + '</option>');
            });
            $(data.MasterProductType).each(function (index, item) {
                $('#ProductTypeId').append('<option value="' + item.productTypeId + '">' + item.productTypeName + '</option>');
            });
            $(data.MasterPlantService).each(function (index, item) {
                $('#PlantId').append('<option value="' + item.plantId + '">' + item.plantNameName + '</option>');
            });
            $(data.MasterWorkflowService).each(function (index, item) {
                $('#WorkflowId').append('<option value="' + item.workflowId + '">' + item.workflowName + '</option>');
            });
            $(data.MasterFormRNDDivisionService).each(function (index, item) {
                $('#FormRNDDivisionId').append('<option value="' + item.formRNDDivisionId + '">' + item.formRNDDivisionName + '</option>');
            });
            $(data.MasterFormulationService).each(function (index, item) {
                $('#PbfRndFormulationId').append('<option value="' + item.formulationId + '">' + item.formulationName + '</option>');
            });
            $(data.MasterAnalyticalGLService).each(function (index, item) {
                $('#PbfRndAnalyticalId').append('<option value="' + item.analyticalId + '">' + item.analyticalName + '</option>');
            });
            $(data.MasterFormulationService).each(function (index, item) {
                $('#PbfAnalFormulationId').append('<option value="' + item.formulationId + '">' + item.formulationName + '</option>');
            });
            $(data.MasterAnalyticalGLService).each(function (index, item) {
                $('#PbfAnalAnalyticalId').append('<option value="' + item.analyticalId + '">' + item.analyticalName + '</option>');
            });
            $(data.MasterFormulationService).each(function (index, item) {
                $('#PbfClinicalFormulationId').append('<option value="' + item.formulationId + '">' + item.formulationName + '</option>');
            });
            $(data.MasterAnalyticalGLService).each(function (index, item) {
                $('#PbfClinicalAnalyticalId').append('<option value="' + item.analyticalId + '">' + item.analyticalName + '</option>');
            });
            $(data.MasterFormRNDDivisionService).each(function (index, item) {
                $('#PbfManfFormRNDDivisionId').append('<option value="' + item.formRNDDivisionId + '">' + item.formRNDDivisionName + '</option>');
            });
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPBFDropdownError(x, y, z) {
    toastr.error(ErrorMessage);
}

function SavePBFForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SavePBFRnD, 'POST', SavePBFFormSuccess, SavePBFFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SavePBFFormSuccess(data) {
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

function tabClick(action, val, pidfidval) {
    if (action == 'PBFRnDForm')
        var url = "/PBF/PBFRnDForm?pidfid=" + pidfidval + "&bui=" + val;
    if (action == 'PBFAnalyticalForm')
        var url = "/PBF/PBFAnalyticalForm?pidfid=" + pidfidval + "&bui=" + val;
    if (action == 'PBFClinicalForm')
        var url = "/PBF/PBFClinicalForm?pidfid=" + pidfidval + "&bui=" + val;
    window.location.href = url;
}

function openPBFForm(evt, formName) {
			var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
			}
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
			}
    document.getElementById(formName).style.display = "block";
    evt.currentTarget.className += " active";
}

function hideForms() {
    var i, tabcontent;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
}

function SetDivReadonly() {
    $("#PIDFFormTemplate").find("input, submit, textarea, a, select").attr("disabled", "disabled");
    $("#PIDFFormTemplate").find("button, submit, a").hide();
    $("#PIDFFormTemplate").find("#collapseButton").show();
    $("#collapseButton").click();
}
