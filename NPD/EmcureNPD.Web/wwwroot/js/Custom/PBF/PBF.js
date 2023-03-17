var SelectedBUValue = 0;
var UserwiseBusinessUnit;
var _PIDFPBFId = 0;
var _mode = 0;
$(document).ready(function () {

    try {
        _PIDFPBFId = parseInt($('#hdnPIDFId').val());
        _mode = $('#hdnIsView').val(); //parseInt($('#hdnPIDFId').val());
    } catch (e) {
        _mode = getParameterByName("IsView");
        _PIDFPBFId = parseInt(getParameterByName("PIDFId"));
    }

    if (_mode == 1) {
        readOnlyForm();
    }
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
            $.each(data.MasterBusinessUnit, function (index, object) {
                $('#MarketMappingId').append($('<option>').text(object.businessUnitName).attr('value', object.businessUnitId));
            });
            $('#MarketMappingId').select2();
            BindBusinessUnit(data.MasterBusinessUnit);         
            BindStrength(data.PIDFStrengthEntity)
            $('#ProjectName').val(data.PIDFEntity[0].moleculeName);
            $('#BrandName').val(data.PIDFEntity[0].rfdBrand);
            
            $('#hdnPbfRFDCountryId').val(data.PIDFEntity[0].rfdCountryId);
            $('#RFDCountryId').val(data.PIDFEntity[0].rfdCountryId);
            $('#RFDApplicant').val(data.PIDFEntity[0].rfdApplicant);
            $('#RFDIndication').val(data.PIDFEntity[0].rfdIndication);
           /* $('#PatentStatus').val(data.PIDFIPDEntity[0].patentStatus);*/
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
            $('#GeneralFormulationId').append(_emptyOption);
            $('#GeneralAnalyticalId').append(_emptyOption);

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
                $('#PbfRFDCountryId').append('<option value="' + item.countryId + '">' + item.countryName + '</option>');
            });
            $(data.MasterProductType).each(function (index, item) {
                $('#ProductTypeId').append('<option value="' + item.productTypeId + '">' + item.productTypeName + '</option>');
            });          
            $(data.MasterProductType).each(function (index, item) {
                $('#GeneralProductTypeId').append('<option value="' + item.productTypeId + '">' + item.productTypeName + '</option>');
            });
            $(data.MasterFormulationGL).each(function (index, item) {
                $('#GeneralFormulationGlId').append('<option value="' + item.UserId + '">' + item.fullName + '</option>');
            });
            $(data.MasterAnalyticalGL).each(function (index, item) {
                $('#GeneralAnalyticalGlId').append('<option value="' + item.UserId + '">' + item.fullName + '</option>');
            });
            $(data.MasterTestLicense).each(function (index, item) {
                $('#testlicence').append('&nbsp;<input type="checkbox" name="TestLicenseAvailability" class="License' + item.testLicenseId + '" value="' + item.testLicenseId + '">&nbsp;' + item.testLicenseName);
            });
           
            try {
                if (_PIDFPBFId > 0) {
                    $('#BERequirementId').val($('#hdnBERequirementId').val());
                    $('#PbfDosageFormId').val($('#hdnPbfDosageFormId').val());
                    $('#PlantId').val($('#hdnPlantId').val());
                    $('#WorkflowId').val($('#hdnWorkflowId').val());
                    $('#FillingTypeId').val($('#hdnFillingTypeId').val());
                    $('#PbfFormRNDDivisionId').val($('#hdnPbfFormRNDDivisionId').val());
                    $('#PbfPackagingTypeId').val($('#hdnPbfPackagingTypeId').val());
                    $('#PbfManufacturingId').val($('#hdnPbfManufacturingId').val());
                    $('#PbfRFDCountryId').val(data.PIDFEntity.rfdCountryId);
                    $('#ProductTypeId').val($('#hdnProductTypeId').val());
                    $('#GeneralProductTypeId').val($('#hdnGeneralProductTypeId').val());
                    $('#GeneralFormulationId').val($('#hdnGeneralFormulationGlId').val());
                    $('#GeneralAnalyticalId').val($('#hdnGeneralAnalyticalGlId').val());
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

    window.location.href = 'PBF?pidfid=' + btoa(pidfidval) + '&bui=' + btoa(BUVal);

}

function SetBU_Tab() {  
    var PIDFBusinessUnitId = 0;      

    if ($("#BusinessUnitId").val() > 0)
        PIDFBusinessUnitId = $("#BusinessUnitId").val();
    else
        PIDFBusinessUnitId = $("#PIDFBusinessUnitId").val();
    SelectedBUValue = PIDFBusinessUnitId;
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
    var strengthHTML = "";
    $.each(data, function (index, item) {
        strengthHTML += '<li class="nav-item col-6 p-0">\
            <a class="nav-link '+ (item.businessUnitId == _selectBusinessUnit ? "active" : "") + ' px-2" href="#custom-tabs-' + item.pidfProductStrengthId + '" data-toggle="pill" aria-selected="true" id="custom-tabs-two-' + item.pidfProductStrengthId + '-tab">' + item.strength + '</a></li>';
        //<a class="nav-link '+ (item.businessUnitId == _selectBusinessUnit ? " active" : "") + ' px-2" href = "#custom-tabs-' + item.pidfProductStrengthId + '" data - toggle="pill" aria - selected="true" onclick = "StrengthtabClick(' + _PIDFID + ', ' + item.pidfProductStrengthId + ')" id = "custom-tabs-two-' + item.pidfProductStrengthId + '-tab" > ' + item.strength + '</a ></li > ';

    });
    $('#ProductStrengthTabs').html(strengthHTML);
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
    window.location.href = 'PBF?pidfid=' + btoa(pidfval) + '&bui=' + btoa(SelectedBUValue) + '&strength=' + btoa(strengthVal);
}