var SelectedBUValue = 0;
var selectedTab = '';
var objMainForm = {};
var selectedStrength = 0;
var UserwiseBusinessUnit;
$(document).ready(function () {
    UserwiseBusinessUnit = UserWiseBUList.split(',');
    GetPBFDropdown();
    GetProductStrengthById($('#Pidfid').val());
    $(".clinicalcalculatecost").on("change", function () {
        Calculate_Clinical_total();
    });
    setLicensevalues()
    SetBU_Strength();
    SetDisableForOtherUserBU()
});
function SetDisableForOtherUserBU() {
    var BU_VALUE = SelectedBUValue;
    var status = UserwiseBusinessUnit.indexOf(BU_VALUE);
    var IsViewInMode = ($("#hdnPBFClinicalIsView").val() == '1')
    if (status == -1 || IsViewInMode) {
        SetClinicalFormReadonly();
    }
    else {
        $("#dvPBFClinicalContainer").find("input, button, submit, textarea, select,a,i").prop('disabled', false);
    }
}
function SetClinicalFormReadonly() {
    $("#dvPBFClinicalContainer").find("input, button, submit, textarea, select,a,i").prop('disabled', true);
}
function setLicensevalues() {
    var straclinical = $('#PbfFormEntities_TestLicenseAvailability').val();
    var strarrayclinical = straclinical.split(',');
    if (strarrayclinical.length > 0) {
       $.each(strarrayclinical, function (index, value) {
            $("#Clinicallicence").find($(".License" + value).prop("checked", true));
        });
    }
}
function SetBU_Strength() {
    var PIDFProductStrengthId = 0; PIDFBusinessUnitId = 0;
    if ($("#StrengthId").val() > 0)
        PIDFProductStrengthId = $("#StrengthId").val();
    else
        PIDFBusinessUnitId = $("#PIDFBusinessUnitId").val();

    if ($("#PbfFormEntities_BusinessUnitId").val() > 0)
        PIDFBusinessUnitId = $("#PbfFormEntities_BusinessUnitId").val();
    else
        PIDFProductStrengthId = $("#PIDFProductStrengthId").val();

    var pidfId = $("#PIDFId").val();

    SelectedBUValue = PIDFBusinessUnitId;
    selectedStrength = PIDFProductStrengthId;
    $("#PbfFormEntities_BusinessUnitId").val(SelectedBUValue);
    $("#BusinessUnitId").val(SelectedBUValue);
    $("#PbfFormEntities_StrengthId").val(selectedStrength);
    $("#StrengthId").val(selectedStrength);

    var StrengthAnchorId = '#BUtab_' + PIDFBusinessUnitId;
    var BUAnchorId = '#Strengthtab_' + PIDFProductStrengthId;

    $(StrengthAnchorId).addClass('active');
    $(BUAnchorId).addClass('active');
    //window.location.href = 'PBFClinicalDetailsForm?pidfid=' + btoa(pidfId) + '&bui=' + btoa(SelectedBUValue) + '&strength=' + btoa(selectedStrength);
}
function IsViewModeClinical() {
    if ($("#hdnPBFClinicalIsView").val() == '1') {
        SetClinicalFormReadonly();
    }
}
function GetPBFDropdown() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllPBF, 'GET', GetPBFDropdownSuccess, GetPBFDropdownError);
}
function GetPBFDropdownSuccess(data) {
    try {
        if (data != null) {
            $(data.MasterDosage).each(function (index, item) {
                $('#PbfDosageFormId').append('<option value="' + item.dosageId + '">' + item.dosageName + '</option>');
            });
            $(data.MasterPackagingTypes).each(function (index, item) {
                $('#PbfPackagingTypeId').append('<option value="' + item.packagingTypeId + '">' + item.packagingTypeName + '</option>');
            });

            $(data.MasterCountrys).each(function (index, item) {
                $('#PbfRFDCountryId').append('<option value="' + item.countryId + '">' + item.countryName + '</option>');
            });

            $(data.MasterFormRNDDivisionService).each(function (index, item) {
                $('#PbfManfFormRNDDivisionId').append('<option value="' + item.formRNDDivisionId + '">' + item.formRNDDivisionName + '</option>');
            });
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
                $('#TransferFormRNDDivisionId').append('<option value="' + item.formRNDDivisionId + '">' + item.formRNDDivisionName + '</option>');
            });

            $(data.MasterProductType).each(function (index, item) {
                $('#ClinicalProductTypeId').append('<option value="' + item.productTypeId + '">' + item.productTypeName + '</option>');
            });
            $(data.MasterFormulationService).each(function (index, item) {
                $('#ClinicalFormulationId').append('<option value="' + item.formulationId + '">' + item.formulationName + '</option>');
            });
            $(data.MasterAnalyticalGLService).each(function (index, item) {
                $('#ClinicalAnalyticalId').append('<option value="' + item.analyticalId + '">' + item.analyticalName + '</option>');
            });

            $(data.MasterTestLicense).each(function (index, item) {
                $('#Clinicallicence').append('&nbsp;<input type="checkbox" name="PidfPbfClinicals.TestLicenseAvailability" class="License' + item.testLicenseId + '" value="' + item.testLicenseId + '">&nbsp;' + item.testLicenseName);
            });
            $(data.MasterFillingType).each(function (index, item) {
                $('#FillingTypeId').append('<option value="' + item.filingTypeId + '">' + item.filingTypeName + '</option>');
            });
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPBFDropdownError(x, y, z) {
    toastr.error(ErrorMessage);
}

function BUtabClick(BUVal, pidfidval) {
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

    window.location.href = 'PBFClinicalDetailsForm?pidfid=' + btoa(pidfidval) + '&bui=' + btoa(BUVal) + '&strength=' + btoa(selectedStrength);
}
// #region Get ProductStrength By pidfId
function GetProductStrengthById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetPBFReadonlyDataByPIDFId + "/" + id, 'GET', GetProductStrengthByIdSuccess, GetProductStrengthByIdError);
}
function GetProductStrengthByIdSuccess(data) {
    try {
        $('#PbfFormEntities_ProjectName').val(data._object.projectName);
        $('#SAPProjectProjectCode').val(data._object.sapProjectProjectCode);
        $('#ImprintingEmbossingCodes').val(data._object.imprintingEmbossingCodes);
        $(data._object.productStrength).each(function (index, item) {
            $('#strengthlblClinical').append('<ul class="nav nav-pills" ><li class="nav-item mr-2"><label class="form-control">' + item.strength + '</label></li></ul>');
        });
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetProductStrengthByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
function StrengthtabClick(strengthVal, pidfval) {
    //selectedStrength = 0;
    var i, tabcontent, strengthtab;
    selectedStrength = strengthVal;
    $("#PidfPbfClinicals_StrengthId").val(strengthVal);
    //var StrengthAnchorId = '#Strengthtab_' + strengthVal;
    //$(StrengthAnchorId).className.add(" active", "");
    strengthtab = document.getElementsByClassName("strengthtab");
    for (i = 0; i < strengthtab.length; i++) {
        strengthtab[i].className = strengthtab[i].className.replace(" active", "");
    }
    var StrengthAnchorId = '#Strengthtab_' + strengthVal;
    $(StrengthAnchorId).addClass("active");
    window.location.href = 'PBFClinicalDetailsForm?pidfid=' + btoa(pidfval) + '&bui=' + btoa(SelectedBUValue) + '&strength=' + btoa(strengthVal);
}

// #endregion

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

function SetChildRows() {
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
        e.stopPropagation()
        return;
    });
}

function ValidateMainForm() {
    var ArrofInvalid = []
    var selected = new Array();
    $.each($("input[name='PidfPbfClinicals.TestLicenseAvailability']:checked"), function () {
        selected.push($(this).val());
    });
    $("#PidfPbfClinicals_TestLicenseAvailability").val(selected.join(", "))
    SetChildRows();

    var MainFormFeilds = ['PidfPbfClinicals_TestLicenseAvailability']
    $.each(MainFormFeilds, function (_, kv) {
        if ($('#' + kv).val() == '') {
            $('#valmsg' + kv).text('Required');
            ArrofInvalid.push(kv);
        }
        else {
            $('#valmsg' + kv).text('');
        }
    });
    var status = (ArrofInvalid.length == 0) ? true : false;
    if (!status) { toastr.error('Some fields are missing !'); }
    return status;
}
function ValidateBU_Strength() {
    var status = true;
    var valMsg = '';
    if (SelectedBUValue == 0) {
        status = false;
        valMsg += ' BusinessUnit ';
    }
    if (selectedStrength == 0) {
        status = false;
        valMsg += ' Strength ';
    }
    if (!status) {
        toastr.error('Please Select : ' + valMsg);
    }
    return status;
}
$("#save").click(function () {
    if (ValidateMainForm() && ValidateBU_Strength()) {
        $('#SaveSubmitType').val('Save');
    }
});
$("#savedraft").click(function () {
    if (ValidateMainForm() && ValidateBU_Strength()) {
        $('#SaveSubmitType').val('draft');
    }
});
//(function () {
//    'use strict'

//    // Fetch all the forms we want to apply custom Bootstrap validation styles to
//    var forms = document.querySelectorAll('.needs-validation')

//    // Loop over them and prevent submission
//    Array.prototype.slice.call(forms)
//        .forEach(function (form) {
//            form.addEventListener('submit', function (event) {
//                if (!form.checkValidity()) {
//                    event.preventDefault()
//                    event.stopPropagation()
//                }

//                form.classList.add('was-validated')
//            }, false)
//        })
//})()