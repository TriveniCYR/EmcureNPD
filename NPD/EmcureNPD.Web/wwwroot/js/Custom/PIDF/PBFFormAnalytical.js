var SelectedBUValue = 0;
var SelectedstrengthValue = 0;
var selectedTab = 'Analytical';
$(document).ready(function () {
    debugger;

    SelectedBUValue = EncQueryBusinessUnit;
    SelectedstrengthValue = EncQueryStrengthId;
    
    $('#Strengthtab_' + QuerystrengthId).addClass('active');
    $('#BUtab_' + QueryBusinessUnit).addClass('active');

    //fnGetActiveBusinessUnit();
    $('.operationButton').hide();
    GetPBFDropdown();
    SetDivReadonly();
    hideForms();

    if ($("#Pidfpbfid").val() > 0);
    {
        Calculate_Analytical_total();
        Calculate_Clinical_total();
    }
    if ($("#PidfPbfAnalyticals_StrengthId").val() > 0 || $("#PidfPbfClinicals_StrengthId").val() > 0) {
        alert($("#PidfPbfAnalyticals_StrengthId").val());
        $('#' + $("#PidfPbfAnalyticals_StrengthId").val()).addClass("active");
    }


    $("#PidfPbfAnalyticals_PIDFID").val($('#Pidfid').val());
    GetProductStrengthById($('#Pidfid').val());
    $(".analyticalcalculatecost").on("change", function () {
        Calculate_Analytical_total();
    });
    $(".clinicalcalculatecost").on("change", function () {
        Calculate_Clinical_total();
    });
    $("#btnSubmit").show();
    $("#btnSaveAsDraft").show();
    $("#btnCancel").show();
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
            $(data.MasterDosage).each(function (index, item) {
                $('#PbfDosageFormId').append('<option value="' + item.dosageId + '">' + item.dosageName + '</option>');
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
                $('#PbfClinicalFormulationId').append('<option value="' + item.formulationId + '">' + item.formulationName + '</option>');
            });
            $(data.MasterAnalyticalGLService).each(function (index, item) {
                $('#PbfClinicalAnalyticalId').append('<option value="' + item.analyticalId + '">' + item.analyticalName + '</option>');
            });
            $(data.MasterFormRNDDivisionService).each(function (index, item) {
                $('#PbfManfFormRNDDivisionId').append('<option value="' + item.formRNDDivisionId + '">' + item.formRNDDivisionName + '</option>');
            });
            //
            $(data.MasterPackagingTypes).each(function (index, item) {
                $('#PbfRndPPPackagingTypeId').append('<option value="' + item.packagingTypeId + '">' + item.packagingTypeName + '</option>');
            });
            $(data.MasterPackagingTypes).each(function (index, item) {
                $('#PbfEndPSUPackagingTypeId').append('<option value="' + item.packagingTypeId + '">' + item.packagingTypeName + '</option>');
            });
            $(data.MasterPackagingTypes).each(function (index, item) {
                $('#PbfRndPEPackagingTypeId').append('<option value="' + item.packagingTypeId + '">' + item.packagingTypeName + '</option>');
            });
            $(data.MasterCountrys).each(function (index, item) {
                $('#PbfRFDFECountryId').append('<option value="' + item.countryId + '">' + item.countryName + '</option>');
            });
            $(data.MasterProductType).each(function (index, item) {
                $('#AnalyticalProductTypeId').append('<option value="' + item.productTypeId + '">' + item.productTypeName + '</option>');
                $('#ClinicalProductTypeId').append('<option value="' + item.productTypeId + '">' + item.productTypeName + '</option>');
            });
            $(data.MasterFormulationService).each(function (index, item) {
                $('#AnalyticalFormulationId').append('<option value="' + item.formulationId + '">' + item.formulationName + '</option>');
                $('#ClinicalFormulationId').append('<option value="' + item.formulationId + '">' + item.formulationName + '</option>');
            });
            $(data.MasterAnalyticalGLService).each(function (index, item) {
                $('#AnalyticalAnalyticalId').append('<option value="' + item.analyticalId + '">' + item.analyticalName + '</option>');
                $('#ClinicalAnalyticalId').append('<option value="' + item.analyticalId + '">' + item.analyticalName + '</option>');
            });
            $(data.MasterTestType).each(function (index, item) {
                $('#AnalyticalPrototypeTestTypeId').append('<option value="' + item.testTypeId + '">' + item.testTypeName + '</option>');
                $('#AnalyticalExhibitTestTypeId').append('<option value="' + item.testTypeId + '">' + item.testTypeName + '</option>');
                $('#AnalyticalScaleUpTestTypeId').append('<option value="' + item.testTypeId + '">' + item.testTypeName + '</option>');
            });
            $(data.MasterTestLicense).each(function (index, item) {
                $('#Analyticallicence').append('&nbsp;<input type="checkbox" name="PidfPbfAnalyticals.TestLicenseAvailability" value="' + item.testLicenseId + '">&nbsp;' + item.testLicenseName);
                $('#Clinicallicence').append('&nbsp;<input type="checkbox" name="PidfPbfClinicals.TestLicenseAvailability" value="' + item.testLicenseId + '">&nbsp;' + item.testLicenseName);

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



function openPBFForm(evt, formName) {
    var i, tabcontent, tablinks;
    selectedTab = 'Analytical';
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
    $('.operationButton').show();
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

function StrengthtabClick(strengthId, pidfidval) {
    debugger;
    SelectedstrengthValue = strengthId;
    tabClick();
}
function BUtabClick(BUVal, pidfidval) {
    debugger;
    SelectedBUValue = BUVal;
    tabClick();
}
function tabClick(){
    
    var url = "/PIDF/PBFFormAnalytical?pidfid=" + $('#encPidfid').val() + "&bui=" + SelectedBUValue + "&strengthId=" + SelectedstrengthValue;
    window.location.href = url;
}
// #region Get ProductStrength By pidfId
function GetProductStrengthById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetPBFReadonlyDataByPIDFId + "/" + id, 'GET', GetProductStrengthByIdSuccess, GetProductStrengthByIdError);
}
function GetProductStrengthByIdSuccess(data) {
    
    try {
        $('#PidfPbfAnalyticals_ProjectName').val(data._object.projectName);
        $('#PidfPbfAnalyticals_SAPProjectProjectCode').val(data._object.sapProjectProjectCode);
        $('#PidfPbfAnalyticals_ImprintingEmbossingCodes').val(data._object.imprintingEmbossingCodes);
        $('#PidfPbfClinicals_ProjectName').val(data._object.projectName);
        $('#PidfPbfClinicals_SAPProjectProjectCode').val(data._object.sapProjectProjectCode);
        $('#PidfPbfClinicals_ImprintingEmbossingCodes').val(data._object.imprintingEmbossingCodes);
        $(data._object.productStrength).each(function (index, item) {
            $('#strengthlblAnalytical').append('<ul class="nav nav-pills" ><li class="nav-item mr-2"><label class="form-control">' + item.strength + '</label></li></ul>');
            $('#strengthlblClinical').append('<ul class="nav nav-pills" ><li class="nav-item mr-2"><label class="form-control">' + item.strength + '</label></li></ul>');
            if ($("#PidfPbfAnalyticals_StrengthId").val() > 0 && $("#PidfPbfAnalyticals_StrengthId").val() == item.pidfproductStrengthId) {
                alert('value not empty');
                $('#StrengthTabsAnalytical').append("<li class='nav-item mr-2'>  <a class='btn btn-outline-primary strengthtab active' id=" + item.pidfproductStrengthId + " onclick='StrengthtabClick(event," + item.pidfproductStrengthId + ");'>" + item.strength + "</a></li>");
                $('#StrengthTabsClinical').append("<li class='nav-item mr-2'>  <a class='btn btn-outline-primary strengthtab active' id=" + item.pidfproductStrengthId + " onclick='StrengthtabClick(event," + item.pidfproductStrengthId + ");'>" + item.strength + "</a></li>");

            } else {
                $('#StrengthTabsAnalytical').append("<li class='nav-item mr-2'>  <a class='btn btn-outline-primary strengthtab' id=" + item.pidfproductStrengthId + " onclick='StrengthtabClick(event," + item.pidfproductStrengthId + ");'>" + item.strength + "</a></li>");
                $('#StrengthTabsClinical').append("<li class='nav-item mr-2'>  <a class='btn btn-outline-primary strengthtab' id=" + item.pidfproductStrengthId + " onclick='StrengthtabClick(event," + item.pidfproductStrengthId + ");'>" + item.strength + "</a></li>");
            }

        });
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetProductStrengthByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//function StrengthtabClick(evt, strengthVal) {
//    var i, tabcontent, strengthtab;
//    SelectedstrengthValue = strengthVal;
//    strengthtab = document.getElementsByClassName("strengthtab");
//    for (i = 0; i < strengthtab.length; i++) {
//        strengthtab[i].className = strengthtab[i].className.replace(" active", "");
//    }
//    document.getElementById(strengthVal).style.display = "block";
//    evt.currentTarget.className += " active";
//}

// #endregion
//Table Binding For Analytical Start
function PrototypeaddRow(j) {
    var table = $('#PrototypeTableBody');
    var node = $('#PrototypeTableRow_0').clone(true);
    table.find('tr:last').after(node);
    table.find('tr:last').find("input").val("");
    SetChildRowDeleteIcon();
}
function PrototypedeleteRow(j, element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
    Calculate_Analytical_total();
}
function ScaleUpaddRow(j) {
    var table = $('#ScaleUpTableBody');
    var node = $('#ScaleUpTableRow_0').clone(true);
    table.find('tr:last').after(node);
    table.find('tr:last').find("input").val("");
    SetChildRowDeleteIcon();
}
function ScaleUpdeleteRow(j, element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
    Calculate_Analytical_total();
}
function ExhibitaddRow(j) {
    var table = $('#ExhibitTableBody');
    var node = $('#ExhibitTableRow_0').clone(true);
    table.find('tr:last').after(node);
    table.find('tr:last').find("input").val("");
    SetChildRowDeleteIcon();
}
function ExhibitdeleteRow(j, element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
    Calculate_Analytical_total();
}
//Table Binding For Analytical End

//Table Binding for Clinical Start
function PilotBioFastingaddRow(j) {
    var table = $('#PilotBioFastingTableBody');
    var node = $('#PilotBioFastingTableRow_0').clone(true);
    table.find('tr:last').after(node);
    table.find('tr:last').find("input").val("");
    SetChildRowDeleteIcon();
}
function PilotBioFastingdeleteRow(j, element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
    Calculate_Clinical_total();
}
function PilotBioFEDaddRow(j) {
    var table = $('#PilotBioFEDTableBody');
    var node = $('#PilotBioFEDTableRow_0').clone(true);
    table.find('tr:last').after(node);
    table.find('tr:last').find("input").val("");
    SetChildRowDeleteIcon();
}
function PilotBioFEDdeleteRow(j, element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
    Calculate_Clinical_total();
}
function PivotalBioFastingaddRow(j) {
    var table = $('#PivotalBioFastingTableBody');
    var node = $('#PivotalBioFastingTableRow_0').clone(true);
    table.find('tr:last').after(node);
    table.find('tr:last').find("input").val("");
    SetChildRowDeleteIcon();
}
function PivotalBioFastingdeleteRow(j, element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
    Calculate_Clinical_total();
}
function PivotalBioFEDaddRow(j) {
    var table = $('#PivotalBioFEDTableBody');
    var node = $('#PivotalBioFEDTableRow_0').clone(true);
    table.find('tr:last').after(node);
    table.find('tr:last').find("input").val("");
    SetChildRowDeleteIcon();
}
function PivotalBioFEDdeleteRow(j, element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
    Calculate_Clinical_total();
}
//Table Binding for Clinical End



function SetChildRowDeleteIcon() {
    //Analytical Table Start
    if ($('#PrototypeTable tbody tr').length > 1) {
        $('.PrototypeDeleteIcon').show();
    } else {
        $('.PrototypeDeleteIcon').hide();
    }

    if ($('#ScaleUpTable tbody tr').length > 1) {
        $('.ScaleUpDeleteIcon').show();
    } else {
        $('.ScaleUpDeleteIcon').hide();
    }

    if ($('#ExhibitTable tbody tr').length > 1) {
        $('.ExhibitDeleteIcon').show();
    } else {
        $('.ExhibitDeleteIcon').hide();
    }
    //Analytical Table End

    //Clinical Table Start
    if ($('#PilotBioFastingTable tbody tr').length > 1) {
        $('.PilotBioFastingDeleteIcon').show();
    } else {
        $('.PilotBioFastingDeleteIcon').hide();
    }
    if ($('#PilotBioFEDTable tbody tr').length > 1) {
        $('.PilotBioFEDDeleteIcon').show();
    } else {
        $('.PilotBioFEDDeleteIcon').hide();
    }
    if ($('#PilotBioFastingTable tbody tr').length > 1) {
        $('.PilotBioFastingDeleteIcon').show();
    } else {
        $('.PilotBioFastingDeleteIcon').hide();
    }
    if ($('#PivotalBioFastingTable tbody tr').length > 1) {
        $('.PivotalBioFastingDeleteIcon').show();
    } else {
        $('.PivotalBioFastingDeleteIcon').hide();
    }
    if ($('#PivotalBioFEDTable tbody tr').length > 1) {
        $('.PivotalBioFEDTableDeleteIcon').show();
    } else {
        $('.PivotalBioFEDTableDeleteIcon').hide();
    }
    //Clinical Table End

}

function SaveClick() {
    $('#SaveSubmitType').val('Save');
    Save();
}
function Save() {
    var selected = new Array();
 
    switch (selectedTab) {
        
        case 'Analytical':
            $.each($("input[name='PidfPbfAnalyticals.TestLicenseAvailability']:checked"), function () {
                selected.push($(this).val());
            });
            $("#PidfPbfAnalyticals_TestLicenseAvailability").val(selected.join(", "))
            $("#PidfPbfAnalyticals_StrengthId").val(SelectedstrengthValue);
            $("#PidfPbfAnalyticals_BusinessUnitId").val(SelectedBUValue);
            $("#PidfPbfAnalyticals_PIDFID").val($('#Pidfid').val());
            if ($("#PidfPbfAnalyticals_BusinessUnitId").val() == 0) {
                toastr.error('Please Select Business Unit');
                preventSubmit();
            }
            if ($("#PidfPbfAnalyticals_StrengthId").val() == 0) {
                toastr.error('Please select Strength');
                preventSubmit();
            }
            if (selected.length == 0) {
                toastr.error('Please Select License Availability');
                preventSubmit();
            }
            SetChildRows(selectedTab);
            break;
        case 'Clinical':
            $.each($("input[name='PidfPbfClinicals.TestLicenseAvailability']:checked"), function () {
                selected.push($(this).val());
            });
            $("#PidfPbfClinicals_TestLicenseAvailability").val(selected.join(", "))
            $("#PidfPbfClinicals_StrengthId").val(SelectedstrengthValue);
            $("#PidfPbfClinicals_BusinessUnitId").val(SelectedBUValue);
            $("#PidfPbfClinicals_PIDFID").val($('#Pidfid').val());
            if ($("#PidfPbfClinicals_BusinessUnitId").val() == 0) {
                toastr.error('Please Select Business Unit');
                preventSubmit();
            }
            if ($("#PidfPbfClinicals_StrengthId").val() == 0) {
                toastr.error('Please select Strength');
                preventSubmit();
            }
            if (selected.length == 0) {
                toastr.error('Please Select License Availability');
                preventSubmit();
            }
            SetChildRows(selectedTab);
            break;
        default:
            toastr.error('Please select Department');
            preventSubmit();
            break;
    }
}
function SaveDraftClick() {
    $('#SaveSubmitType').val('draft');
    Save();
}
function SetChildRows(selectedTab) {
 
    switch (selectedTab) {
        case 'RnD':
            //code need to be impliment
            break;
        case 'Analytical':
            //Analytical Table set data start
            $.each($('#PrototypeTable tbody tr'), function (index, value) {
                $(this).find("td:first select").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalPrototypes[" + index.toString() + "].TestTypeId");
                $(this).find("td:eq(1) input").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalPrototypes[" + index.toString() + "].Numberoftests");
                $(this).find("td:eq(2) input").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalPrototypes[" + index.toString() + "].PrototypeDevelopment");
                $(this).find("td:eq(3) input").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalPrototypes[" + index.toString() + "].Cost");
                $(this).find("td:eq(4) input").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalPrototypes[" + index.toString() + "].PrototypeCost");
            });
            $.each($('#ScaleUpTable tbody tr'), function (index, value) {
                $(this).find("td:first select").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalScaleUps[" + index.toString() + "].TestTypeId");
                $(this).find("td:eq(1) input").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalScaleUps[" + index.toString() + "].Numberoftests");
                $(this).find("td:eq(2) input").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalScaleUps[" + index.toString() + "].PrototypeDevelopment");
                $(this).find("td:eq(3) input").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalScaleUps[" + index.toString() + "].Cost");
                $(this).find("td:eq(4) input").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalScaleUps[" + index.toString() + "].PrototypeCost");

            });
            $.each($('#ExhibitTable tbody tr'), function (index, value) {
                $(this).find("td:first select").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalExhibits[" + index.toString() + "].TestTypeId");
                $(this).find("td:eq(1) input").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalExhibits[" + index.toString() + "].Numberoftests");
                $(this).find("td:eq(2) input").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalExhibits[" + index.toString() + "].PrototypeDevelopment");
                $(this).find("td:eq(3) input").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalExhibits[" + index.toString() + "].Cost");
                $(this).find("td:eq(4) input").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalExhibits[" + index.toString() + "].PrototypeCost");
            });
            //Analytical Table set data End
            break;
        case 'Clinical':
            //Clinical table set data start
            $.each($('#PilotBioFastingTable tbody tr'), function (index, value) {
                $(this).find("td:first input").attr("name", "PidfPbfClinicals.pidfpbfClinicalpilotBioFastingEntity[" + index.toString() + "].Fasting");
                $(this).find("td:eq(1) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalpilotBioFastingEntity[" + index.toString() + "].NumberofVolunteers");
                $(this).find("td:eq(2) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalpilotBioFastingEntity[" + index.toString() + "].ClinicalCostandVol");
                $(this).find("td:eq(3) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalpilotBioFastingEntity[" + index.toString() + "].DocCostandStudy");
                $(this).find("td:eq(4) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalpilotBioFastingEntity[" + index.toString() + "].TotalCost");
            });
            $.each($('#PilotBioFEDTable tbody tr'), function (index, value) {
                $(this).find("td:first input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPilotBioFedEntity[" + index.toString() + "].Fed");
                $(this).find("td:eq(1) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPilotBioFedEntity[" + index.toString() + "].NumberofVolunteers");
                $(this).find("td:eq(2) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPilotBioFedEntity[" + index.toString() + "].ClinicalCostandVol");
                $(this).find("td:eq(3) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPilotBioFedEntity[" + index.toString() + "].DocCostandStudy");
                $(this).find("td:eq(4) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPilotBioFedEntity[" + index.toString() + "].TotalCost");
            });
            $.each($('#PivotalBioFastingTable tbody tr'), function (index, value) {
                $(this).find("td:first input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPivotalBioFastingEntity[" + index.toString() + "].Fasting");
                $(this).find("td:eq(1) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPivotalBioFastingEntity[" + index.toString() + "].NumberofVolunteers");
                $(this).find("td:eq(2) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPivotalBioFastingEntity[" + index.toString() + "].ClinicalCostandVol");
                $(this).find("td:eq(3) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPivotalBioFastingEntity[" + index.toString() + "].DocCostandStudy");
                $(this).find("td:eq(4) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPivotalBioFastingEntity[" + index.toString() + "].TotalCost");
            });
            $.each($('#PivotalBioFEDTable tbody tr'), function (index, value) {
                $(this).find("td:first input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPivotalBioFedEntity[" + index.toString() + "].Fed");
                $(this).find("td:eq(1) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPivotalBioFedEntity[" + index.toString() + "].NumberofVolunteers");
                $(this).find("td:eq(2) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPivotalBioFedEntity[" + index.toString() + "].ClinicalCostandVol");
                $(this).find("td:eq(3) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPivotalBioFedEntity[" + index.toString() + "].DocCostandStudy");
                $(this).find("td:eq(4) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPivotalBioFedEntity[" + index.toString() + "].TotalCost");
            });
            //Clinical table set data End
            break;
        default:
            break;

    }
}

function Calculate_Analytical_total() {

    var totalAMVsum = parseFloat($('#PidfPbfAnalyticals_PidfPbfAnalyticalCosts_TotalAMVCost').val());
    var prototypesum = 0;
    var scaleupsum = 0;
    var exhibitsum = 0;
    var totalsum = 0;

    $.each($('#PrototypeTable tbody tr'), function (index, value) {
        prototypesum += parseFloat($(this).find("td:eq(4) input").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalPrototypes[" + index.toString() + "].PrototypeCost").val());

    });
    $.each($('#ScaleUpTable tbody tr'), function (index, value) {
        scaleupsum += parseFloat($(this).find("td:eq(4) input").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalScaleUps[" + index.toString() + "].PrototypeCost").val());

    });
    $.each($('#ExhibitTable tbody tr'), function (index, value) {
        exhibitsum += parseFloat($(this).find("td:eq(4) input").attr("name", "PidfPbfAnalyticals.PidfPbfAnalyticalExhibits[" + index.toString() + "].PrototypeCost").val());

    });
    totalsum = prototypesum + scaleupsum + exhibitsum + totalAMVsum;
    $('#PidfPbfAnalyticals_PidfPbfAnalyticalCosts_TotalPrototypeCost').val(prototypesum);
    $('#PidfPbfAnalyticals_PidfPbfAnalyticalCosts_TotalScaleUpCost').val(scaleupsum);
    $('#PidfPbfAnalyticals_PidfPbfAnalyticalCosts_TotalExhibitCost').val(exhibitsum);
    $('#txtAMVCost').val(totalAMVsum);
    $('#PidfPbfAnalyticals_PidfPbfAnalyticalCosts_TotalCost').val(totalsum);
}
function Calculate_Clinical_total() {
  
    var pilotbiofastingsum = 0;
    var pilotbiofedsum = 0;
    var pivotalbiofastingsum = 0;
    var pivotalbiofedsum = 0;
    var totalsum = 0;

    $.each($('#PilotBioFastingTable tbody tr'), function (index, value) {
        pilotbiofastingsum += parseFloat($(this).find("td:eq(4) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalpilotBioFastingEntity[" + index.toString() + "].TotalCost").val());

    });
    $.each($('#PilotBioFEDTable tbody tr'), function (index, value) {
        pilotbiofedsum += parseFloat($(this).find("td:eq(4) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPilotBioFedEntity[" + index.toString() + "].TotalCost").val());

    });
    $.each($('#PivotalBioFastingTable tbody tr'), function (index, value) {
        pivotalbiofastingsum += parseFloat($(this).find("td:eq(4) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPivotalBioFastingEntity[" + index.toString() + "].TotalCost").val());

    });
    $.each($('#PivotalBioFEDTable tbody tr'), function (index, value) {
        pivotalbiofedsum += parseFloat($(this).find("td:eq(4) input").attr("name", "PidfPbfClinicals.pidfpbfClinicalPivotalBioFedEntity[" + index.toString() + "].TotalCost").val());

    });

    totalsum = pilotbiofastingsum + pilotbiofedsum + pivotalbiofastingsum + pivotalbiofedsum;

    $('#PidfPbfClinicals_pidfPbfClinicalCost_TotalPilotFastingCost').val(pilotbiofastingsum);
    $('#PidfPbfClinicals_pidfPbfClinicalCost_TotalPilotFEDCost').val(pilotbiofedsum);
    $('#PidfPbfClinicals_pidfPbfClinicalCost_TotalPivotalFastingCost').val(pivotalbiofastingsum);
    $('#PidfPbfClinicals_pidfPbfClinicalCost_TotalPivotalFEDCost').val(pivotalbiofedsum);
    $('#PidfPbfClinicals_pidfPbfClinicalCost_TotalCost').val(totalsum);
}
function preventSubmit() {

    $(document).on('submit', 'form', function (e) {
        e.preventDefault();
        //your code goes here      
        //100% works
        return;
    });
}
// Business Unit Binding Start

function fnGetActiveBusinessUnit() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetActiveBusinessUnit, 'GET', GetActiveBusinessUnitSuccess, GetActiveBusinessUnitError);
}
function GetActiveBusinessUnitSuccess(data) {
    var businessUnitHTML = "";
    var businessUnitPanel = "";
    $.each(data._object, function (index, item) {
        businessUnitHTML += '<li class="nav-item p-0">\
            <a class="nav-link '+ (item.businessUnitId == _selectBusinessUnit ? "active" : "") + ' px-2" href="#custom-tabs-' + item.businessUnitId + '" data-toggle="pill" aria-selected="true" onclick="LoadIPDForm(' + _PIDFID + ', ' + item.businessUnitId + ')" id="custom-tabs-two-' + item.businessUnitId + '-tab">' + item.businessUnitName + '</a></li>';
        businessUnitPanel += '<div class="tab-pane ' + ((item.businessUnitId == _selectBusinessUnit ? "fade show active" : "")) + '" id="custom-tabs-' + item.businessUnitId + '" role="tabpanel" aria-labelledby="custom-tabs-two-' + item.businessUnitId + '-tab"></div>';
    });
    $('#custom-tabs-two-tab').html(businessUnitHTML);
    $('#custom-tabs-two-tabContent').html(businessUnitPanel);

    LoadIPDForm(_PIDFID, _selectBusinessUnit);
}
function GetActiveBusinessUnitError(x, y, z) {
    toastr.error(ErrorMessage);
}
function LoadIPDForm(pidfId, BusinessUnitId) {
    _selectBusinessUnit = BusinessUnitId;
    if ($("#custom-tabs-" + BusinessUnitId).html() == "") {
        $.get(_PBFPartialURL, { pidfid: pidfId, bui: BusinessUnitId }, function (content) {
            $("#custom-tabs-" + BusinessUnitId).html(content);
        });
    }
}
// Business Unit Binding End
