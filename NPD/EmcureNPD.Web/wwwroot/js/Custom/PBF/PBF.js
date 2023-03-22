var SelectedBUValue = 0;
var UserwiseBusinessUnit;
var _PIDFPBFId = 0;
var _mode = 0;
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
        alert('hi from clinicalcalculatecost ');
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
    try {
        if (data != null) {
            clearclinicaltables();
            BindBusinessUnit(data.MasterBusinessUnit);
            BindStrength(data.PIDFStrengthEntity)
            BindClinical(data.PIDFStrengthEntity, data.PBFClinicalEntity)
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
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPBFDropdownError(x, y, z) {
    toastr.error(ErrorMessage);
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
    clearclinicaltables();
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
    $.each(data, function (index, item) {
        strengthHTML += '<td><input type="hidden" class="control-label" id="GeneralStrengthEntities[' + index + '].StrengthId" name="GeneralStrengthEntities[' + index + '].StrengthId" value="' + item.pidfProductStrengthId + '" /><b>' + item.strength + item.unitofMeasurementName + '</b></td>';
    });
    strengthHTML += "</tr></thead>";
    strengthHTML += "<tbody><tr>";
    for (var count = 0; count < data.length; count++) {
        strengthHTML += '<td><input type="text" class="form-control" id="GeneralStrengthEntities[' + [count] + '].ProjectCode" name="GeneralStrengthEntities[' + [count] + '].ProjectCode" placeholder="Project Code" value="' + (data[count].projectCode != null && data[count].projectCode != undefined ? data[count].projectCode : "") + '" /></td>';
    }
    strengthHTML += '</tr>';
    strengthHTML += "<tr>";
    for (var count = 0; count < data.length; count++) {
        strengthHTML += '<td> <input type="text" class="form-control" id="GeneralStrengthEntities[' + [count] + '].ImprintingEmbossingCode" name="GeneralStrengthEntities[' + [count] + '].ImprintingEmbossingCode" placeholder="Imprinting Embossing Code" value="' + (data[count].imprintingEmbossingCode != null && data[count].imprintingEmbossingCode != undefined ? data[count].imprintingEmbossingCode : "") + '" /></td>';
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
function createclinicaltables(data, tablefor, type, tablename, iteration, biostudytype, result) {
    var objectname = '<thead class="bg-primary"><tr><td>' + tablefor + '</td>';
    $.each(data, function (index, item) {
        objectname += '<td><input type="hidden" class="control-label" id="ClinicalEntities[' + parseInt(index + iteration) + '].StrengthId" name="ClinicalEntities[' + parseInt(index + iteration) + '].StrengthId" value="' + item.pidfProductStrengthId + '" /><b>' + item.strength + item.unitofMeasurementName + '</b></td>';
    });
    objectname += '</tr></thead>';
    objectname += '<tbody><tr><td>' + type + '</td>';
    for (var count = iteration; count < data.length + iteration; count++) {
        objectname += '<td>'
            + '<input type="hidden" class="control-label" id="ClinicalEntities[' + [count] + '].BioStudyTypeId" name="ClinicalEntities[' + [count] + '].BioStudyTypeId" value="' + (result[count].bioStudyTypeId != null && result[count].bioStudyTypeId != undefined ? result[count].bioStudyTypeId : biostudytype) + '" />'
            + '<input type="text" class="form-control clinicalcalculatecost" id="ClinicalEntities[' + [count] + '].FastingOrFed" name="ClinicalEntities[' + [count] + '].FastingOrFed" placeholder="' + type + '" value="' + (result[count].fastingOrFed != null && result[count].fastingOrFed != undefined ? result[count].fastingOrFed : "") + '" /></td>';

    }
    /* objectname += '<td> <input type="text" class="form-control readonlyUpdate" value="0" disabled="true"/> </td></tr>';*/
    objectname += '</tr>';
    objectname += "<tr><td>Number of Volunteers</td>";
    for (var count = iteration; count < data.length + iteration; count++) {
        objectname += '<td> <input type="text" class="form-control clinicalcalculatecost" id="ClinicalEntities[' + [count] + '].NumberofVolunteers" name="ClinicalEntities[' + [count] + '].NumberofVolunteers" placeholder="Number of Volunteers" value="' + (result[count].numberofVolunteers != null && result[count].numberofVolunteers != undefined ? result[count].numberofVolunteers : "") + '"  /></td>';

    }
    /*objectname += '<td> <input type="text" class="form-control readonlyUpdate" value="0" disabled="true" /> </td></tr>';*/
    objectname += '</tr>';
    objectname += '<tr><td>ClinicalCostAndVolume</td>';
    for (var count = iteration; count < data.length + iteration; count++) {
        objectname += '<td> <input type="text" class="form-control clinicalcalculatecost" id="ClinicalEntities[' + [count] + '].ClinicalCostAndVolume" name="ClinicalEntities[' + [count] + '].ClinicalCostAndVolume" placeholder="Clinical Cost And Volume" value="' + (result[count].clinicalCostAndVolume != null && result[count].clinicalCostAndVolume != undefined ? result[count].clinicalCostAndVolume : "") + '" /></td>';

    }
    /* objectname += '<td> <input type="text" class="form-control readonlyUpdate" value="0" disabled="true" /> </td></tr>';*/
    objectname += '</tr>';
    objectname += "<tr><td>BioAnalyticalCostAndVolume</td>";
    for (var count = iteration; count < data.length + iteration; count++) {
        objectname += '<td> <input type="text" class="form-control clinicalcalculatecost" id="ClinicalEntities[' + [count] + '].BioAnalyticalCostAndVolume" name="ClinicalEntities[' + [count] + '].BioAnalyticalCostAndVolume" placeholder="Bio Analytical Cost And Volume" value="' + (result[count].bioAnalyticalCostAndVolume != null && result[count].bioAnalyticalCostAndVolume != undefined ? result[count].bioAnalyticalCostAndVolume : "") + '" /></td>';

    }
    /*objectname += '<td> <input type="text" class="form-control readonlyUpdate" value="0" disabled="true" /> </td></tr>';*/
    objectname += '</tr>';
    objectname += "<tr><td>DocCostandStudy</td>";
    for (var count = iteration; count < data.length + iteration; count++) {
        objectname += '<td> <input type="text" class="form-control clinicalcalculatecost" id="ClinicalEntities[' + [count] + '].DocCostandStudy" name="ClinicalEntities[' + [count] + '].DocCostandStudy" placeholder="Doc Costand Study" value="' + (result[count].docCostandStudy != null && result[count].docCostandStudy != undefined ? result[count].docCostandStudy : "") + '"/></td>';

    }
    /*objectname += '<td> <input type="text" class="form-control readonlyUpdate" value="0" disabled="true" /> </td></tr></tbody>';*/
    objectname += '</tr></tbody>';
    $('#' + tablename).html(objectname);
}

function BindClinical(data, result) {
    //pilot fasting table start
    createclinicaltables(data, 'Pilot Bio Fasting', 'Fasting', 'tableclinicalpilotfasting', 0, 1, result);
    //end

    //pilot fed table start
    createclinicaltables(data, 'Pilot Bio Fed', 'Fed', 'tableclinicalpilotfed', 2, 2, result);
    //pilot fed end

    //pivotal bio fasting table start
    createclinicaltables(data, 'Pivotal Bio Fasting', 'Fasting', 'tableclinicalpivotalfasting', 4, 3, result);

    //end

    //pivotal fed table start
    createclinicaltables(data, 'Pivotal Bio Fed', 'Fed', 'tableclinicalpivotalfed', 6, 4, result);
    //pivotal fed end
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
function clearclinicaltables() {
    $("#tableclinicalpilotfasting").find("tr:gt(0)").remove();
    $("#tableclinicalpilotfed").find("tr:gt(0)").remove();
    $("#tableclinicalpivotalfasting").find("tr:gt(0)").remove();
    $("#tableclinicalpivotalfed").find("tr:gt(0)").remove();
    //$('#tableclinicalpilotfasting tbody').empty();
    //$('#tableclinicalpilotfed tbody').empty();
    //$('#tableclinicalpivotalfasting tbody').empty();
    //$('#tableclinicalpivotalfed tbody').empty();
}


//function StrengthtabClick(strengthVal, pidfval) {  
//    //selectedStrength = 0;
//    var i, tabcontent, strengthtab;
//    selectedStrength = strengthVal;
//    /*$("#PidfPbfClinicals_StrengthId").val(strengthVal);*/
//    //var StrengthAnchorId = '#Strengthtab_' + strengthVal;
//    //$(StrengthAnchorId).className.add(" active", "");
//    strengthtab = document.getElementsByClassName("strengthtab");
//    for (i = 0; i < strengthtab.length; i++) {
//        strengthtab[i].className = strengthtab[i].className.replace(" active", "");
//    }
//    var StrengthAnchorId = '#Strengthtab_' + strengthVal;
//    $(StrengthAnchorId).addClass("active");
//    window.location.href = 'PBF?pidfid=' + btoa(pidfval) + '&bui=' + btoa(SelectedBUValue) + '&strength=' + btoa(strengthVal);
//}