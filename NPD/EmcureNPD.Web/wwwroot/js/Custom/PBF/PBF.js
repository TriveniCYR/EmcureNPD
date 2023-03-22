﻿var SelectedBUValue = 0;
var UserwiseBusinessUnit;
var _PIDFPBFId = 0;
var _mode = 0;
var _strengthArray = [];

$(document).ready(function () {
    try {
        _PIDFPBFId = parseInt($('#hdnPIDFPBFId').val());
        _mode = $('#hdnIsView').val(); //parseInt($('#hdnPIDFId').val());
    } catch (e) {
        _mode = getParameterByName("IsView");
        _PIDFPBFId = parseInt(getParameterByName("Pidfpbfid"));
    }

    if (_mode == 1) {
        readOnlyForm();
    }
    $(".clinicalcalculatecost").on("change", function () {
        Calculate_Clinical_total();
    });
    GetPBFDropdown();
    UserwiseBusinessUnit = UserWiseBUList.split(',');
    SetBU_Tab();
    /*SetDisableForOtherUserBU(); */
});

function GetPBFDropdown() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllPBF + "/" + _PIDFID, 'GET', GetPBFDropdownSuccess, GetPBFDropdownError);
}
function GetPBFDropdownSuccess(data) {
    console.log(data);
    try {
        if (data != null) {
            $.each(data.MasterBusinessUnit, function (index, object) {
                $('#MarketMappingId').append($('<option>').text(object.businessUnitName).attr('value', object.businessUnitId));
            });
            $('#MarketMappingId').select2();
            var _emptyOption = '<option value="">-- Select --</option>';
            $('#BERequirementId').append(_emptyOption);
            $('#PbfDosageFormId').append(_emptyOption);
            $('#PlantId').append(_emptyOption);
            $('#WorkflowId').append(_emptyOption);
            $('#FillingTypeId').append(_emptyOption);
            $('#PbfFormRNDDivisionId').append(_emptyOption);
            $('#PbfPackagingTypeId').append(_emptyOption);
            $('#PbfManufacturingId').append(_emptyOption);
            $('#PbfRFDCountryId').append(_emptyOption);
            $('#ProductTypeId').append(_emptyOption);
            $('#GeneralProductTypeId').append(_emptyOption);
            $('#GeneralFormulationGLId').append(_emptyOption);
            $('#GeneralAnalyticalGLId').append(_emptyOption);
            $(data.MasterBERequirement).each(function (index, item) {
                $('#BERequirementId').append('<option value="' + item.beRequirementId + '">' + item.beRequirementName + '</option>');
            });
            $(data.MasterDosage).each(function (index, item) {
                $('#PbfDosageFormId').append('<option value="' + item.dosageId + '">' + item.dosageName + '</option>');
            });
            $(data.MasterPlant).each(function (index, item) {
                $('#PlantId').append('<option value="' + item.plantId + '">' + item.plantNameName + '</option>');
            });
            $(data.MasterWorkflow).each(function (index, item) {
                $('#WorkflowId').append('<option value="' + item.workflowId + '">' + item.workflowName + '</option>');
            });
            $(data.MasterFilingType).each(function (index, item) {
                $('#FillingTypeId').append('<option value="' + item.filingTypeId + '">' + item.filingTypeName + '</option>');
            });
            $(data.MasterFormRnDDivision).each(function (index, item) {
                $('#PbfFormRNDDivisionId').append('<option value="' + item.formRnDDivisionId + '">' + item.formRnDDivisionName + '</option>');
            });
            $(data.MasterPackagingType).each(function (index, item) {
                $('#PbfPackagingTypeId').append('<option value="' + item.packagingTypeId + '">' + item.packagingTypeName + '</option>');
            });
            $(data.MasterManufacturing).each(function (index, item) {
                $('#PbfManufacturingId').append('<option value="' + item.manufacturingId + '">' + item.manufacturingName + '</option>');
            });
            $(data.MasterCountry).each(function (index, item) {
                $('#PbfRFDCountryId').append('<option value="' + item.countryID + '">' + item.countryName + '</option>');
            });
            $(data.MasterProductType).each(function (index, item) {
                $('#ProductTypeId').append('<option value="' + item.productTypeId + '">' + item.productTypeName + '</option>');
            });
            $(data.MasterProductType).each(function (index, item) {
                $('#GeneralProductTypeId').append('<option value="' + item.productTypeId + '">' + item.productTypeName + '</option>');
            });
            $(data.MasterFormulationGL).each(function (index, item) {
                $('#GeneralFormulationGLId').append('<option value="' + item.userId + '">' + item.fullName + '</option>');
            });
            $(data.MasterAnalyticalGL).each(function (index, item) {
                $('#GeneralAnalyticalGLId').append('<option value="' + item.userId + '">' + item.fullName + '</option>');
            });
            $(data.MasterTestLicense).each(function (index, item) {
                $('#testlicence').append('&nbsp;<input type="checkbox" name="TestLicenseAvailability" id="License' + item.testLicenseId + '" value="' + item.testLicenseId + '">&nbsp;' + item.testLicenseName);
            });

            try {
                if (_PIDFID > 0) {
                    if ($('#ProjectName').val() == "") {
                        $('#ProjectName').val(data.PIDFEntity[0].moleculeName);
                    }
                    if ($('#PatentStatus').val() == "") {
                        /*$('#PatentStatus').val(data.PIDFIPDEntity[0].patentStatus);*/
                        $('#PatentStatus').val('active');
                    }
                    $('#BrandName').val(data.PIDFEntity[0].rfdBrand);
                    $('#hdnPbfRFDCountryId').val(data.PIDFEntity[0].rfdCountryId);
                    $('#PbfRFDCountryId').val($('#hdnPbfRFDCountryId').val());
                    $('#RFDCountryId').val(data.PIDFEntity[0].rfdCountryId);
                    $('#RFDApplicant').val(data.PIDFEntity[0].rfdApplicant);
                    $('#RFDIndication').val(data.PIDFEntity[0].rfdIndication);

                }
                if (_PIDFPBFId > 0) {
                    $("#Pidfpbfid").val(_PIDFPBFId);
                    $("#PBFGeneralId").val($("#hdnPBFGeneralId").val());


                    $('#BERequirementId').val($('#hdnBERequirementId').val());
                    $('#PbfDosageFormId').val($('#hdnPbfDosageFormId').val());
                    $('#PlantId').val($('#hdnPlantId').val());
                    $('#WorkflowId').val($('#hdnWorkflowId').val());
                    $('#FillingTypeId').val($('#hdnFillingTypeId').val());
                    $('#PbfFormRNDDivisionId').val($('#hdnPbfFormRNDDivisionId').val());
                    $('#PbfPackagingTypeId').val($('#hdnPbfPackagingTypeId').val());
                    $('#PbfManufacturingId').val($('#hdnPbfManufacturingId').val());
                    $('#PbfRFDCountryId').val($('#hdnPbfRFDCountryId').val());
                    $('#ProductTypeId').val($('#hdnProductTypeId').val());
                    $('#GeneralProductTypeId').val($('#hdnGeneralProductTypeId').val());
                    $('#GeneralFormulationGLId').val($('#hdnGeneralFormulationGLId').val());
                    $('#GeneralAnalyticalGLId').val($('#hdnGeneralAnalyticalGLId').val());
                    $("#MarketMappingId").val($("#hdnMarketMappingIds").val().split(',')).trigger('change');
                    //var mappingid = $("#hdnMarketMappingIds").val().split(',');
                    //$.each(mappingid, function (index, item) {
                    //    $("#MarketMappingId").val($("#hdnMarketMappingIds").val().split(',')).trigger('change');
                    //});
                    var license = $("#TestLicenseAvailability").val().split(',');
                    $.each(license, function (index, item) {
                        $("#License" + item.trim()).prop("checked", true);
                    });
                    Calculate_Clinical_total();

                }
            } catch (e) {

            }

            _strengthArray = data.PIDFStrengthEntity;
            BindBusinessUnit(data.MasterBusinessUnit);
            BindStrength(data.PIDFPBFGeneralStrength)
            BindClinical(data.PBFClinicalEntity)
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPBFDropdownError(x, y, z) {
    toastr.error(ErrorMessage);
}

function getStrengthName(strengthId) {
    var _filteredStrength = $.grep(_strengthArray, function (n, i) {
        return n.pidfProductStrengthId === strengthId;
    });
    if (_filteredStrength != null && _filteredStrength != undefined && _filteredStrength.length > 0) {
        return _filteredStrength[0].strength + " " + _filteredStrength[0].unitofMeasurementName;
    } else { return ""; }
}
function getValueFromStrengthId(data, strengthId, propertyName) {
    var _filteredStrength = $.grep(data, function (n, i) {
        return n.strengthId === strengthId;
    });
    if (_filteredStrength != null && _filteredStrength != undefined && _filteredStrength.length > 0) {
        if (_filteredStrength[0][propertyName] != null && _filteredStrength[0][propertyName] != undefined) {
            return _filteredStrength[0][propertyName];
        } else { return ""; }
    } else { return ""; }
}

function SetDisableForOtherUserBU() {
    var BU_VALUE = SelectedBUValue;
    var status = UserwiseBusinessUnit.indexOf(BU_VALUE);
    var IsViewInMode = ($("#hdnPBFIsView").val() == '1')
    if (status == -1 || IsViewInMode) {
        SetPBFFormReadonly();
    }
    else {
        $("#dvPBFContainer").find("input, button, submit, textarea, select,a,i").prop('disabled', false);
    }
}
function SetPBFFormReadonly() {
    $("#dvPBFContainer").find("input, button, submit, textarea, select,a,i").prop('disabled', true);
}
function BUtabClick(pidfidval, BUVal) {
    SelectedBUValue = 0;
    var i, tabcontent, butab;

    SelectedBUValue = BUVal;
    $("#BusinessUnitId").val(SelectedBUValue);
    $("#PbfFormEntities_BusinessUnitId").val(SelectedBUValue);
    butab = document.getElementsByClassName("BUtab");
    for (i = 0; i < butab.length; i++) {
        butab[i].className = butab[i].className.replace(" active", "");
    }
    var BUAnchorId = '#BUtab_' + BUVal;
    $(BUAnchorId).addClass('active');
    window.location.href = 'PBF?pidfid=' + btoa(pidfidval) + '&bui=' + btoa(BUVal);
}
function SetBU_Tab() {
    var PIDFBusinessUnitId = 0;

    if ($("#BusinessUnitId").val() > 0)
        PIDFBusinessUnitId = $("#BusinessUnitId").val();
    else
        PIDFBusinessUnitId = $("#PIDFBusinessUnitId").val();
    SelectedBUValue = PIDFBusinessUnitId;
    $("#Pidfid").val(_PIDFID);
    var pidfId = $("#PIDFId").val();
    var BUAnchorId = '#BUtab_' + PIDFBusinessUnitId;
    $(BUAnchorId).addClass('active');
    //window.location.href = 'PBFClinicalDetailsForm?pidfid=' + btoa(pidfId) + '&bui=' + btoa(SelectedBUValue) + '&strength=' + btoa(selectedStrength);

}

function BindBusinessUnit(data) {
    var businessUnitHTML = "";
    var businessUnitPanel = "";
    $.each(data, function (index, item) {
        businessUnitHTML += '<li class="nav-item p-0">\
            <a class="nav-link '+ (item.businessUnitId == _selectBusinessUnit ? "active" : "") + ' px-2" href="#custom-tabs-' + item.businessUnitId + '" data-toggle="pill" aria-selected="true" onclick="BUtabClick(' + _PIDFID + ', ' + item.businessUnitId + ')" id="custom-tabs-two-' + item.businessUnitId + '-tab">' + item.businessUnitName + '</a></li>';
    });
    $('#custom-tabs-two-tab').html(businessUnitHTML);
}
function BindStrength(data) {
    var strengthHTML = '<thead class="bg-primary"><tr>';
    $.each(_strengthArray, function (index, item) {
        strengthHTML += '<td><input type="hidden" class="control-label" id="GeneralStrengthEntities[' + index + '].StrengthId" name="GeneralStrengthEntities[' + index + '].StrengthId" value="' + item.pidfProductStrengthId + '" /><b>' + getStrengthName(item.pidfProductStrengthId) + '</b></td>';
    });
    strengthHTML += "</tr></thead>";
    strengthHTML += "<tbody><tr>";
    for (var count = 0; count < _strengthArray.length; count++) {
        strengthHTML += '<td><input type="text" class="form-control" id="GeneralStrengthEntities[' + [count] + '].ProjectCode" name="GeneralStrengthEntities[' + [count] + '].ProjectCode" placeholder="Project Code" value="' + (getValueFromStrengthId(data, _strengthArray[count].pidfProductStrengthId, "projectCode")) + '" /></td>';
    }
    strengthHTML += '</tr>';
    strengthHTML += "<tr>";
    for (var count = 0; count < _strengthArray.length; count++) {
        strengthHTML += '<td> <input type="text" class="form-control" id="GeneralStrengthEntities[' + [count] + '].ImprintingEmbossingCode" name="GeneralStrengthEntities[' + [count] + '].ImprintingEmbossingCode" placeholder="Imprinting Embossing Code" value="' + (getValueFromStrengthId(data, _strengthArray[count].pidfProductStrengthId, "imprintingEmbossingCode")) + '" /></td>';
    }
    strengthHTML += '</tr></tbody>';

    $('#tableGeneralStrength').html(strengthHTML);
}

function SavePBFForm(_SaveType) {
    setlicense();
    $('#SaveType').val(_SaveType);
}
function setlicense() {
    var selected = new Array();
    $.each($("input[name='TestLicenseAvailability']:checked"), function () {
        selected.push($(this).val());
    });
    $("#TestLicenseAvailability").val(selected.join(","))
}

function CreateClinicalTable(data, bioStudyTypeId) {
    var objectname = "";
    var tableTitle = "";
    var fastingOrFed = "";
    if (bioStudyTypeId == 1 || bioStudyTypeId == 3) {
        fastingOrFed = "Fasting";
    } else {
        fastingOrFed = "Fed";
    }
    if (bioStudyTypeId == 1 || bioStudyTypeId == 2) {
        tableTitle = "Pilot Bio";
    } else {
        tableTitle = "Pivotal Bio";
    }

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (_strengthArray.length + 2) + '">' + tableTitle + " " + fastingOrFed + '</td>';

    var _iterator = (bioStudyTypeId - 1) * _strengthArray.length;


    objectname += "<tr><td>" + fastingOrFed +"</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td><input type="hidden" id="ClinicalEntities[' + [(i + _iterator)] + '].BioStudyTypeId" name="ClinicalEntities[' + [(i + _iterator)] + '].BioStudyTypeId" value="' + bioStudyTypeId + '" /><input type="hidden" id="ClinicalEntities[' + [(i + _iterator)] + '].StrengthId" name="ClinicalEntities[' + [(i + _iterator)] + '].StrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control clinicalcalculatecost" id="ClinicalEntities[' + [(i + _iterator)] + '].FastingOrFed" name="ClinicalEntities[' + [(i + _iterator)] + '].FastingOrFed" placeholder="' + fastingOrFed + '" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "fastingOrFed")) + '" /></td>';
    }
    objectname += "<td><input type='number' class='form-control totalClinical' readonly='readonly' /></td></tr>";

    objectname += "<tr><td>Number of Volunteers</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td><input type="number" class="form-control clinicalcalculatecost" id="ClinicalEntities[' + [(i + _iterator)] + '].NumberofVolunteers" name="ClinicalEntities[' + [(i + _iterator)] + '].NumberofVolunteers" placeholder="Number of Volunteers" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "numberofVolunteers")) + '"  /></td>';
    }
    objectname += "<td></td></tr>";

    objectname += '<tr><td>Clinical Cost/Vol.</td>';
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td> <input type="number" class="form-control clinicalcalculatecost" id="ClinicalEntities[' + [(i + _iterator)] + '].ClinicalCostAndVolume" name="ClinicalEntities[' + [(i + _iterator)] + '].ClinicalCostAndVolume" placeholder="Clinical Cost And Volume" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "clinicalCostAndVolume")) + '" /></td>';

    }
    objectname += "<td></td></tr>";

    objectname += "<tr><td>Bio analytical Cost/Vol.</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td> <input type="number" class="form-control clinicalcalculatecost" id="ClinicalEntities[' + [(i + _iterator)] + '].BioAnalyticalCostAndVolume" name="ClinicalEntities[' + [(i + _iterator)] + '].BioAnalyticalCostAndVolume" placeholder="Bio Analytical Cost And Volume" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "bioAnalyticalCostAndVolume")) + '" /></td>';

    }
    objectname += "<td></td></tr>";

    objectname += "<tr><td>Doc. Cost/study</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td> <input type="number" class="form-control clinicalcalculatecost" id="ClinicalEntities[' + [(i + _iterator)] + '].DocCostandStudy" name="ClinicalEntities[' + [(i + _iterator)] + '].DocCostandStudy" placeholder="Doc Costand Study" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "docCostandStudy")) + '"/></td>';

    }
    objectname += "<td></td></tr>";

    objectname += "<tr><td class='text-bold'>Total Cost</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += "<td><input type='number' class='form-control totalClinical' readonly='readonly' /></td>";
    }
    objectname += "<td><input type='number' class='form-control totalClinical' readonly='readonly' /></td></tr>";

    return objectname;
}
function BindClinical(data) {
    console.log(data);
    var bioStudyHTML = '<thead class="bg-primary text-bold"><tr><td>Bio Study Cost</td>';
    $.each(_strengthArray, function (index, item) {
        bioStudyHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + '</td>';
    });
    bioStudyHTML += '<td>Total</td></tr></thead><tbody>';

    for (var i = 1; i < 5; i++) {
        bioStudyHTML += CreateClinicalTable($.grep(data, function (n, x) { return n.bioStudyTypeId == i; }), i);
    }

    bioStudyHTML += '<tr><td class="text-bold">Total Bio Study Cost</td>';
    $.each(_strengthArray, function (index, item) {
        bioStudyHTML += "<td><input type='number' class='form-control totalClinical' readonly='readonly' /></td>";
    });
    bioStudyHTML += "<td><input type='number' class='form-control totalClinical' readonly='readonly' /></td></tr></tbody>";
    $('#tableclinical').html(bioStudyHTML);
}

function Calculate_Clinical_total() {

    $('#tableclinicalpilotfasting').tableTotal({      
        totalCol: true,
        bold: true
    });
    $('#tableclinicalpilotfed').tableTotal({
        totalCol: true,
        bold: true
    });
    $('#tableclinicalpivotalfasting').tableTotal({
        totalCol: true,
        bold: true
    });
    $('#tableclinicalpivotalfed').tableTotal({
        totalCol: true,
        bold: true
    });
}

