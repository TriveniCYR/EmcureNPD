var SelectedBUValue = 0;
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

    $(document).on("change", ".AnalyticalTestTypeId", function () {
        var _selectedTestType = $(this).val();
        if (_selectedTestType != "" && _selectedTestType != "0") {
            var returnFromFunction = 0;
            for (var i = 1; i < 4; i++) {
                if ($(this).parent().parent().hasClass("analyticalActivity" + i + "")) {
                    $.each($('.analyticalActivity' + i + '').find(".AnalyticalTestTypeId"), function () {
                        if ($(this).val() == _selectedTestType) {
                            returnFromFunction++;
                        }
                    });
                    if (returnFromFunction > 1) {
                        $(this).val("0");
                        toastr.error("Test type is already selected, please select another");
                        return false;
                    }
                }
            }
        }
        $(this).parent().parent().find('.analyticalRsTest').val($(this).find(':selected').attr('data-testTypePrice'));
        $(this).parent().parent().find('.analyticalPrototypeDevelopment').val($(this).find(':selected').text());
        $(this).parent().parent().find('.analyticalNumberOfTest').val("1");
    });
    $(document).on("change", ".rndPackagingTypeId", function () {
        var _selectedTestType = $(this).val();
        if (_selectedTestType != "" && _selectedTestType != "0") {
            var returnFromFunction = 0;
            for (var i = 1; i < 4; i++) {
                if ($(this).parent().parent().hasClass("packagingActivity" + i + "")) {
                    $.each($('.packagingActivity' + i + '').find(".rndPackagingTypeId"), function () {
                        if ($(this).val() == _selectedTestType) {
                            returnFromFunction++;
                        }
                    });
                    if (returnFromFunction > 1) {
                        $(this).val("0");
                        toastr.error("Packaging type is already selected, please select another");
                        return false;
                    }
                }
            }
        }
    });
   
    $(document).on("change", ".rndFillingExpensesRegionId", function () {
        var _selectedTestType = $(this).val();
        if (_selectedTestType != "" && _selectedTestType != "0") {
            var returnFromFunction = 0;
            for (var i = 1; i < 4; i++) {
                if ($(this).parent().parent().hasClass("FillingExpensesActivity" + i + "")) {
                    $.each($('.FillingExpensesActivity' + i + '').find(".rndFillingExpensesRegionId"), function () {
                        if ($(this).val() == _selectedTestType) {
                            returnFromFunction++;
                        }
                    });
                    if (returnFromFunction > 1) {
                        $(this).val("0");
                        toastr.error("Region is already selected, please select another");
                        return false;
                    }
                }
            }
        }
    });
    $(document).on("change", ".calcFastingOrFed, .calcNoOfVolunteers, .calcClinicalCost, .calcBioAnalyticalCost, .calcDocCostandStudy", function () {
        var _BioStudyTypeId = $(this).parent().parent().attr("data-biostudytypeid");
        var _StrengthId = $(this).parent().attr("data-strengthid");

        var _ClinicalRows = $('.clinicalcal_' + _BioStudyTypeId + '').find("[data-strengthid=" + _StrengthId + "]");

        var FastingOrFed = 0;
        var NoOfVol = 0;
        var ClinicalCost = 0;
        var BioAnalyticalCost = 0;
        var DocCostStudy = 0;

        FastingOrFed = parseFloat(_ClinicalRows.find(".calcFastingOrFed").val());
        NoOfVol = parseFloat(_ClinicalRows.find(".calcNoOfVolunteers").val());
        ClinicalCost = parseFloat(_ClinicalRows.find(".calcClinicalCost").val());
        BioAnalyticalCost = parseFloat(_ClinicalRows.find(".calcBioAnalyticalCost").val());
        DocCostStudy = parseFloat(_ClinicalRows.find(".calcDocCostandStudy").val());

        var _Sum = 0;
        $.each($(this).parent().parent().find("input[type=number]"), function (index, item) {
            if ($(item).attr("class").indexOf("TotalStrength") === -1) {
                _Sum += parseFloat($(item).val());
            }
        });

        // set total for the row
        $(this).parent().parent().find(".TotalStrength").val(_Sum);

        // formula to calculate the total cost for one strength (all the properties) (one column)
        var totalCostFastingFed = FastingOrFed * (NoOfVol * (ClinicalCost + BioAnalyticalCost) + DocCostStudy);

        // set total for all the property in the table for one strength
        $('.clinicalcal_' + _BioStudyTypeId + 'Total').find("[data-strengthid=" + _StrengthId + "]").find(".calcTotalCostForStrength").val(totalCostFastingFed);

        var _TotalSum = 0;
        $.each($('.clinicalcal_' + _BioStudyTypeId + 'Total').find(".calcTotalCostForStrength"), function (index, item) {
            _TotalSum += parseFloat($(item).val());
        });

        // set total for all the property in the table
        $('.clinicalcal_' + _BioStudyTypeId + 'Total').find(".calcTotalCostForStrengthTotal").val(_TotalSum);

    });
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
                if ($('#ProjectName').val() == "") {
                    $('#ProjectName').val(data.PIDFEntity[0].moleculeName);
                }
                if ($('#PatentStatus').val() == "") {
                    if (data.PIDFIPDEntity.length > 0) {
                        $('#PatentStatus').val(data.PIDFIPDEntity[0].patentStatus);
                        //$('#PatentStatus').val('active');
                    }
                }
                $('#BrandName').val(data.PIDFEntity[0].rfdBrand);
                $('#hdnPbfRFDCountryId').val(data.PIDFEntity[0].rfdCountryId);
                $('#PbfRFDCountryId').val($('#hdnPbfRFDCountryId').val());
                $('#RFDCountryId').val(data.PIDFEntity[0].rfdCountryId);
                $('#RFDApplicant').val(data.PIDFEntity[0].rfdApplicant);
                $('#RFDIndication').val(data.PIDFEntity[0].rfdIndication);

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
                    var license = $("#TestLicenseAvailability").val().split(',');
                    $.each(license, function (index, item) {
                        $("#License" + item.trim()).prop("checked", true);
                    });
                }
            } catch (e) {

            }
            _strengthArray = data.PIDFStrengthEntity;
            BindBusinessUnit(data.MasterBusinessUnit);

            GetPBFTabDetails();
            UserwiseBusinessUnit = UserWiseBUList.split(',');
            SetBU_Tab();
            /*SetDisableForOtherUserBU(); */

        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPBFDropdownError(x, y, z) {
    toastr.error(ErrorMessage);
}

function GetPBFTabDetails() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetPBFAllTabDetails + "/" + _PIDFID + "/" + _selectBusinessUnit, 'GET', GetPBFTabDetailsSuccess, GetPBFTabDetailsError);
}
function GetPBFTabDetailsSuccess(data) {
    try {
        if (data != null) {

            BindStrength(data.PIDFPBFGeneralStrength);
            BindClinical(data.PBFClinicalEntity);
            BindAnalytical(data.PBFAnalyticalEntity, data.PBFAnalyticalCostEntity);
            BindRNDExicipient(data.PBFRNDExicipientEntity);
            BindRNDPackaging(data.PBFRNDPackagingEntity);
            BindRNDBatchSize(data.PBFRNDBatchSize, data.PBFRNDMasterEntity);
            BindRNDAPIRequirement(data.PBFRNDAPIRequirement);
            BindRNDToolingchangepart(data.PBFRNDToolingChangePart);
            BindRNDCapexMiscExpenses(data.PBFRNDCapexMiscExpenses);
            BindRNDPlantSupportCost(data.PBFRNDPlantSupportCost);
            BindRNDReferenceProductDetail(data.PBFRNDReferenceProductDetail);
            BindRNDFillingExpenses(data.PBFRNDFillingExpenses);
            BindRNDManPowerCost(data.PBFRNDManPowerCost)
            $(data.MasterTestType).each(function (index, item) {
                $('.AnalyticalTestTypeId').append('<option value="' + item.testTypeId + '" data-TestTypeCode="' + item.testTypeCode + '" data-TestTypePrice="' + item.testTypePrice + '">' + item.testTypeCode + ": " + item.testTypeName + '</option>');
            });
            $(_strengthArray).each(function (index, item) {
                $('.AMVstrengths').append('<option value="' + item.pidfProductStrengthId + '">' + getStrengthName(item.pidfProductStrengthId) + '</option>');
            });
            $(data.MasterPackagingType).each(function (index, item) {
                $('.rndPackagingTypeId').append('<option value="' + item.packagingTypeId + '">' + item.packagingTypeName + '</option>');
            });
            $(data.MasterBusinessUnit).each(function (index, item) {
                $('.rndFillingExpensesRegionId').append('<option value="' + item.businessUnitId + '">' + item.businessUnitName + '</option>');
            });


            var _availableStrength = $('.AMVstrengths').next().val();
            if (_availableStrength != "") {
                $('.AMVstrengths').val(_availableStrength.split(","));
            }

            $('.AMVstrengths').select2();

            $.each($('.AnalyticalTestTypeId'), function (index, item) {
                if ($(this).next().val() != undefined && $(this).next().val() != null) {
                    $(this).val($(this).next().val());
                }
            });
            $.each($('.rndPackagingTypeId'), function (index, item) {
                if ($(this).next().val() != undefined && $(this).next().val() != null) {
                    $(this).val($(this).next().val());
                }
            });
            var _emptyOption = '<option value="">-- Select --</option>';
            $('#RNDBatchSizeId').append(_emptyOption);
            $(data.MasterBatchSize).each(function (index, item) {
                $('#RNDBatchSizeId').append('<option value="' + item.batchSizeNumberId + '">' + item.batchSizeNumberName + '</option>');
            });
            if (data.PBFRNDMasterEntity.length > 0) {
                if (data.PBFRNDMasterEntity[0].batchSizeId > 0) {
                    $('#RNDBatchSizeId').val(data.PBFRNDMasterEntity[0].batchSizeId);
                }
            }
           
            $.each($('.rndFillingExpensesRegionId'), function (index, item) {
                if ($(this).next().val() != undefined && $(this).next().val() != null) {
                    $(this).val($(this).next().val());
                }
            });
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPBFTabDetailsError(x, y, z) {
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
function getValueFromStrengthTestTypeId(data, strengthId, propertyName, testTypeId) {
    var _filteredStrength = $.grep(data, function (n, i) {
        return n.strengthId === strengthId && n.testTypeId == testTypeId;
    });
    if (_filteredStrength != null && _filteredStrength != undefined && _filteredStrength.length > 0) {
        if (_filteredStrength[0][propertyName] != null && _filteredStrength[0][propertyName] != undefined) {
            return _filteredStrength[0][propertyName];
        } else { return ""; }
    } else { return ""; }
}
function getValueFromStrengthPackagingTypeId(data, strengthId, propertyName, packagingTypeId) {
    var _filteredStrength = $.grep(data, function (n, i) {
        return n.strengthId === strengthId && n.packagingTypeId == packagingTypeId;
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
    SetRNDChildRows();
    SetAnalyticalChildRows();
    $('#SaveType').val(_SaveType);
    return false;
}
function setlicense() {
    var selected = new Array();
    $.each($("input[name='TestLicenseAvailability']:checked"), function () {
        selected.push($(this).val());
    });
    $("#TestLicenseAvailability").val(selected.join(","))
}
//Clinical Start
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

    objectname += "<tr class='clinicalcal_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId +"'><td>" + fastingOrFed + "</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId +'"><input type="hidden" class="hdnBioStudyTypeId" id="ClinicalEntities[' + [(i + _iterator)] + '].BioStudyTypeId" name="ClinicalEntities[' + [(i + _iterator)] + '].BioStudyTypeId" value="' + bioStudyTypeId + '" /><input type="hidden" id="ClinicalEntities[' + [(i + _iterator)] + '].StrengthId" name="ClinicalEntities[' + [(i + _iterator)] + '].StrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control calcFastingOrFed" id="ClinicalEntities[' + [(i + _iterator)] + '].FastingOrFed" name="ClinicalEntities[' + [(i + _iterator)] + '].FastingOrFed" placeholder="' + fastingOrFed + '" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "fastingOrFed")) + '" /></td>';
    }
    objectname += "<td><input type='number' class='form-control TotalStrength' readonly='readonly' min='0' /></td></tr>";

    objectname += "<tr class='clinicalcal_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId +"'><td>Number of Volunteers</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId +'"><input type="number" class="form-control calcNoOfVolunteers" id="ClinicalEntities[' + [(i + _iterator)] + '].NumberofVolunteers" name="ClinicalEntities[' + [(i + _iterator)] + '].NumberofVolunteers" placeholder="Number of Volunteers" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "numberofVolunteers")) + '"  /></td>';
    }
    objectname += "<td><input type='number' class='form-control TotalStrength' readonly='readonly' min='0' /></td></tr>";

    objectname += "<tr class='clinicalcal_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId +"'><td>Clinical Cost/Vol.</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId +'"> <input type="number" class="form-control calcClinicalCost" id="ClinicalEntities[' + [(i + _iterator)] + '].ClinicalCostAndVolume" name="ClinicalEntities[' + [(i + _iterator)] + '].ClinicalCostAndVolume" placeholder="Clinical Cost And Volume" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "clinicalCostAndVolume")) + '" /></td>';

    }
    objectname += "<td><input type='number' class='form-control TotalStrength' readonly='readonly' min='0' /></td></tr>";

    objectname += "<tr  class='clinicalcal_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId +"'><td>Bio analytical Cost/Vol.</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId +'"> <input type="number" class="form-control calcBioAnalyticalCost" id="ClinicalEntities[' + [(i + _iterator)] + '].BioAnalyticalCostAndVolume" name="ClinicalEntities[' + [(i + _iterator)] + '].BioAnalyticalCostAndVolume" placeholder="Bio Analytical Cost And Volume" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "bioAnalyticalCostAndVolume")) + '" /></td>';

    }
    objectname += "<td><input type='number' class='form-control TotalStrength' readonly='readonly' min='0' /></td></tr>";

    objectname += "<tr class='clinicalcal_" + bioStudyTypeId + "' data-biostudytypeid='" + bioStudyTypeId +"'><td>Doc. Cost/study</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td data-strengthid="' + _strengthArray[i].pidfProductStrengthId +'"> <input type="number" class="form-control calcDocCostandStudy" id="ClinicalEntities[' + [(i + _iterator)] + '].DocCostandStudy" name="ClinicalEntities[' + [(i + _iterator)] + '].DocCostandStudy" placeholder="Doc Cost and Study" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "docCostandStudy")) + '"/></td>';

    }
    objectname += "<td><input type='number' class='form-control TotalStrength' readonly='readonly' min='0' /></td></tr>";

    objectname += "<tr class='clinicalcal_" + bioStudyTypeId + "Total' data-biostudytypeid='" + bioStudyTypeId +"'><td class='text-bold'>Total Cost</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += "<td data-strengthid='" + _strengthArray[i].pidfProductStrengthId + "'><input type='number' class='form-control calcTotalCostForStrength' readonly='readonly' min='0' /></td>";
    }
    objectname += "<td><input type='number' class='form-control calcTotalCostForStrengthTotal' readonly='readonly'  min='0' /></td></tr>";

    return objectname;
}
function BindClinical(data) {
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
//Clinical End
//Analytical Start
function CreateAnalyticalTable(data, activityTypeId) {

    var objectname = "";
    var tableTitle = "";

    if (activityTypeId == 1) {
        tableTitle = "Prototype";
    } else if (activityTypeId == 2) {
        tableTitle = "Scale Up";
    } else {
        tableTitle = "Exhibit Batch";
    }
    //var _iterator = (activityTypeId - 1) * _strengthArray.length;

    var _counter = (data.length == 0 ? 1 : data.length);

    var _testType = [];

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (6 + _strengthArray.length) + '">' + tableTitle + '</td>';
    for (var a = 0; a < _counter; a++) {
        if (data.length > 0) {
            if (_testType.indexOf(data[a].testTypeId) !== -1) {
                continue;
            }
        }
        objectname += '<tr  id="analyticalRow" class="analyticalactivity analyticalActivity' + (activityTypeId) + '">'
            + '<td><select class="form-control readOnlyUpdate AnalyticalTestTypeId"><option value = "0" > --Select --</option ></select><input type="hidden" value="' + (data.length > 0 ? data[a].testTypeId : "") + '" /></td>'
            + '<td><input type="number" class="form-control totalAnalytical analyticalNumberOfTest" min="0" value="' + (data.length > 0 ? data[a].numberoftests : "") + '"  /></td>'
            + '<td><input type="text" class="form-control totalAnalytical analyticalPrototypeDevelopment" value="' + (data.length > 0 ? data[a].prototypeDevelopment : "") + '"  /></td>'
            + '<td><input type="number" class="form-control totalAnalytical analyticalRsTest" min="0" value="' + (data.length > 0 ? data[a].costPerTest : "") + '"  /></td>'
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += '<td><input class="analyticalTypeId" type="hidden" value="' + activityTypeId + '" /><input type="hidden" class="analyticalStrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control analyticalStrengthValue" min="0" value="' + (data.length > 0 ? getValueFromStrengthTestTypeId(data, _strengthArray[i].pidfProductStrengthId, "prototypeCost", data[a].testTypeId) : "") + '" /></td>';
        }
        objectname += "<td><input type='number' class='form-control' readonly='readonly' /></td><td> <i class='fa-solid fa-circle-plus nav-icon text-success operationButton' id='addIcon' onclick='addRowanalytical(this);'></i> <i class='fa-solid fa-trash nav-icon text-red strengthDeleteIcon operationButton DeleteIcon' onclick='deleteRowanalytical(this);' ></i></td></tr>";

        if (data.length > 0) {
            _testType.push(data[a].testTypeId);
        }

    }

    return objectname;
}
function BindAnalytical(data, costData) {
    var analyticalactivityHTML = '<thead class="bg-primary text-bold"><tr>'
        + '<td width="15%">Test Type</td>'
        + '<td width="8%">Number of tests</td>'
        + '<td width="20%">Prototype Development</td>'
        + '<td width="10%">Rs /test</td>'
    $.each(_strengthArray, function (index, item) {
        analyticalactivityHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + ' (Prototype Cost)</td>';
    });
    analyticalactivityHTML += '<td width="12%">Total</td><td width="7%">Action</td></tr></thead><tbody id="tblanalyticalBody">';

    for (var i = 1; i < 4; i++) {
        analyticalactivityHTML += CreateAnalyticalTable($.grep(data, function (n, x) { return n.activityTypeId == i; }), i);
    }

    analyticalactivityHTML += '<tr>';
    analyticalactivityHTML += '<td colspan="2"><input type="number" class="form-control totalAnalytical" id="AMVCosts.TotalAmvcost" name="AMVCosts.TotalAmvcost" placeholder="Total AMV Cost" min="0" value="' + (costData.length > 0 ? costData[0].totalAMVCost : "") + '" /></td><td class="text-bold" colspan="2"><textarea id="remark" class="form-control" id="AMVCosts.Remark" name="AMVCosts.Remark" placeholder="Remark">' + (costData.length > 0 ? costData[0].remark : "") + '</textarea></td>  <td colspan="' + (_strengthArray.length) + '"><input type="hidden" id="AMVCosts.StrengthId" name="AMVCosts.StrengthId" /><select class="form-control readOnlyUpdate AMVstrengths" multiple="multiple" name="AMVCosts.StrengthId"></select><input type="hidden" value="' + (costData.length > 0 ? costData[0].strengthIds : "") + '" /> </td></tr>';
    analyticalactivityHTML += '<tr><td colspan="4" class="text-bold">Total Cost</td>';

    $.each(_strengthArray, function (index, item) {
        analyticalactivityHTML += "<td><input type='number' class='form-control' readonly='readonly' min='0'/></td>";
    });

    analyticalactivityHTML += "<td><input type='number' class='form-control' readonly='readonly' min='0' /><td></td></tr></tbody>";

    $('#tableanalytical').html(analyticalactivityHTML);

    SetChildRowDeleteIcon();

}
function addRowanalytical(element) {
    //var table = $('#tblanalyticalBody');
    var node = $(element).parent().parent().clone(true);

    node.find("input.form-control").val("");
    $(element).parent().parent().after(node);
    //table.find('#analyticalRow').after(node);
    //table.find('#analyticalRow' + j).find("input.form-control").val("");
    SetChildRowDeleteIcon();
}
function deleteRowanalytical(element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
}
function SetChildRowDeleteIcon() {
    for (var i = 1; i < 4; i++) {
        if ($('#tableanalytical tbody tr.analyticalActivity' + i + '').length > 1) {
            $('.analyticalActivity' + i + '').find(".DeleteIcon").show();
        } else {
            $('.analyticalActivity' + i + '').find(".DeleteIcon").hide();
        }
        if ($('#tablerndexicipientrequirement tbody tr.exicipientActivity' + i + '').length > 1) {
            $('.exicipientActivity' + i + '').find(".DeleteIcon").show();
        } else {
            $('.exicipientActivity' + i + '').find(".DeleteIcon").hide();
        }
        if ($('#tablerndpackagingmaterialrequirement tbody tr.packagingActivity' + i + '').length > 1) {
            $('.packagingActivity' + i + '').find(".DeleteIcon").show();
        } else {
            $('.packagingActivity' + i + '').find(".DeleteIcon").hide();
        }

        if ($('#tablerndtoolingchangepart tbody tr.ToolingChangePartActivity' + i + '').length > 1) {
            $('.ToolingChangePartActivity' + i + '').find(".DeleteIcon").show();
        } else {
            $('.ToolingChangePartActivity' + i + '').find(".DeleteIcon").hide();
        }
        if ($('#tablerndcapexmiscellaneousexpenses tbody tr.CapexMiscActivity' + i + '').length > 1) {
            $('.CapexMiscActivity' + i + '').find(".DeleteIcon").show();
        } else {
            $('.CapexMiscActivity' + i + '').find(".DeleteIcon").hide();
        }
        if ($('#tablerndplantsupportcost tbody tr.PlantSupportCostActivity' + i + '').length > 1) {
            $('.PlantSupportCostActivity' + i + '').find(".DeleteIcon").show();
        } else {
            $('.PlantSupportCostActivity' + i + '').find(".DeleteIcon").hide();
        }
        if ($('#tablerndfilingexpenses tbody tr.FillingExpensesActivity' + i + '').length > 1) {
            $('.FillingExpensesActivity' + i + '').find(".DeleteIcon").show();
        } else {
            $('.FillingExpensesActivity' + i + '').find(".DeleteIcon").hide();
        }


    }

}
function SetAnalyticalChildRows() {

    var _AnalyticalArray = [];

    for (var i = 1; i < 4; i++) {
        $.each($('#tableanalytical tbody tr.analyticalActivity' + i + ''), function () {

            var TestTypeId = $(this).find(".AnalyticalTestTypeId").val();
            var ActivityTypeId = $(this).find(".analyticalTypeId").val();
            var Numberoftests = $(this).find(".analyticalNumberOfTest").val();
            var PrototypeDevelopment = $(this).find(".analyticalPrototypeDevelopment").val();
            var CostPerTest = $(this).find(".analyticalRsTest").val();

            $.each($(this).find(".analyticalStrengthId"), function (index, item) {
                var _AnalyticalObject = new Object();
                _AnalyticalObject.StrengthId = $(this).val();
                _AnalyticalObject.PrototypeCost = $(this).parent().find(".analyticalStrengthValue").val();
                _AnalyticalObject.TestTypeId = TestTypeId;
                _AnalyticalObject.ActivityTypeId = ActivityTypeId;
                _AnalyticalObject.Numberoftests = Numberoftests;
                _AnalyticalObject.PrototypeDevelopment = PrototypeDevelopment;
                _AnalyticalObject.CostPerTest = CostPerTest;
                if (_AnalyticalObject.PrototypeCost != "" && _AnalyticalObject.TestTypeId != "0") {
                    _AnalyticalArray.push(_AnalyticalObject);
                }
            });
        });

        $('#hdnAnalyticalData').val(JSON.stringify(_AnalyticalArray));
    }
}
//Analytical End

//R&D Start
//Exicipient Requirement table start
function CreateRNDExicipientTable(data, activityTypeId) {
    var objectname = "";
    var tableTitle = "";

    if (activityTypeId == 1) {
        tableTitle = "Exicipient Protoype";
    } else if (activityTypeId == 2) {
        tableTitle = "Exicipient Scale Up";
    } else {
        tableTitle = "Exicipient Exhibit";
    }

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (6 + _strengthArray.length) + '">' + tableTitle + '</td>';

    var _counter = (data.length == 0 ? 1 : data.length);

    var _testType = [];
    for (var a = 0; a < _counter; a++) {
        if (data.length > 0) {
            if (_testType.indexOf(data[a].activityTypeId) !== -1) {
                continue;
            }
        }
        objectname += '<tr  id="ExicipientRow" class="exicipientactivity exicipientActivity' + (activityTypeId) + '">'
            + '<td><input type="number" class="form-control rndExicipientPrototype" min="0" value="' + (data.length > 0 ? data[a].exicipientPrototype : "") + '"  /></td>'
            + '<td><input type="number" class="form-control rndExicipientRsperkg" min="0" value="' + (data.length > 0 ? data[a].rsPerKg : "") + '"  /></td>'
            + '<td><input type="number" class="form-control rndExicipientMGperUnitDosage" min="0" value="' + (data.length > 0 ? data[a].mgPerUnitDosage : "") + '"  /></td>';
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += '<td><input class="rndExicipientTypeId" type="hidden" value="' + activityTypeId + '" /><input type="hidden" class="rndExicipientStrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control rndExicipientStrengthValue" min="0" value="' + (data.length > 0 ? getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "exicipientDevelopment") : "") + '" /></td>';
        }
        objectname += "<td><input type='number' class='form-control' readonly='readonly' /></td><td> <i class='fa-solid fa-circle-plus nav-icon text-success operationButton' id='addIcon' onclick='addRowRNDExicipient(this);'></i> <i class='fa-solid fa-trash nav-icon text-red strengthDeleteIcon operationButton DeleteIcon' onclick='deleteRowRNDExicipient(this);' ></i></td></tr>";

        if (data.length > 0) {
            _testType.push(data[a].activityTypeId);
        }

    }

    return objectname;
}
function BindRNDExicipient(data) {

    var ExicipientActivityHTML = '<thead class="bg-primary text-bold"><tr>'
        + '<td>Exicipient Prototype</td>'
        + '<td>Rs / Kg</td>'
        + '<td>Mg Per Unit Dosage</td>';
    $.each(_strengthArray, function (index, item) {
        ExicipientActivityHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + '</td>';
    });
    ExicipientActivityHTML += '<td>Total</td><td>Action</td></tr></thead><tbody id="tblExicipientBody">';

    for (var i = 1; i < 4; i++) {
        ExicipientActivityHTML += CreateRNDExicipientTable($.grep(data, function (n, x) { return n.activityTypeId == i; }), i);
    }
    ExicipientActivityHTML += '<tr><td class="text-left text-bold bg-light" colspan="10"></td></tr>';
    ExicipientActivityHTML += '<tr><td class="text-bold">Total Exicipient Costs</td>';
    ExicipientActivityHTML += '<tr><td class="text-bold">Total Cost</td>';
    ExicipientActivityHTML += "<td><input type='number' class='form-control' readonly='readonly' /></td><td><input type='number' class='form-control' readonly='readonly' /></td><td><input type='number' class='form-control' readonly='readonly' /></td><td><input type='number' class='form-control' readonly='readonly' /></td></tr></tbody>";
    $('#tablerndexicipientrequirement').html(ExicipientActivityHTML);
    SetChildRowDeleteIcon();
}
function addRowRNDExicipient(element) {
    var node = $(element).parent().parent().clone(true);
    node.find("input.form-control").val("");
    $(element).parent().parent().after(node);
    SetChildRowDeleteIcon();
}
function deleteRowRNDExicipient(element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon(j);
}
//Exicipient Requirement table end
//Packaging Material table start
function CreateRNDPackagingTable(data, activityTypeId) {

    var objectname = "";
    var tableTitle = "";

    if (activityTypeId == 1) {
        tableTitle = "Packaging Protoype";
    } else if (activityTypeId == 2) {
        tableTitle = "Packaging Scale Up";
    } else {
        tableTitle = "Packaging Exhibit";
    }
    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (6 + _strengthArray.length) + '">' + tableTitle + '</td>';
    var _counter = (data.length == 0 ? 1 : data.length);
    var _packagingType = [];
    for (var a = 0; a < _counter; a++) {
        if (data.length > 0) {
            if (_packagingType.indexOf(data[a].packagingTypeId) !== -1) {
                continue;
            }
        }

        objectname += '<tr  id="PackagingRow" class="packagingactivity packagingActivity' + (activityTypeId) + '">'
            + '<td><select class="form-control readOnlyUpdate rndPackagingTypeId"><option value = "0" > --Select --</option ></select><input type="hidden" value="' + (data.length > 0 ? data[a].packagingTypeId : "") + '" /></td>'
            + '<td><input type="number" class="form-control rndPackagingUnitofMeasurement" min="0"  value="' + (data.length > 0 ? data[a].unitOfMeasurement : "") + '"/></td>'
            + '<td><input type="number" class="form-control rndPackagingRsperUnit" min="0"  value="' + (data.length > 0 ? data[a].rsPerUnit : "") + '" /></td>'
            + '<td><input type="number" class="form-control rndPackagingQuantity" min="0" value="' + (data.length > 0 ? data[a].quantity : "") + '"  /></td>';
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += '<td><input class="rndPackagingActivityId" type="hidden" value="' + activityTypeId + '" /><input type="hidden" class="rndPackagingStrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control rndPackagingStrengthValue" min="0" value="' + (data.length > 0 ? getValueFromStrengthPackagingTypeId(data, _strengthArray[i].pidfProductStrengthId, "packagingDevelopment", data[a].packagingTypeId) : "") + '"/></td>';
        }
        objectname += "<td><input type='number' class='form-control' readonly='readonly' /></td><td> <i class='fa-solid fa-circle-plus nav-icon text-success operationButton' id='addIcon' onclick='addRowRNDPackaging(this);'></i> <i class='fa-solid fa-trash nav-icon text-red strengthDeleteIcon operationButton DeleteIcon'  onclick='deleteRowRNDPackaging(this);' ></i></td></tr>";

        if (data.length > 0) {
            _packagingType.push(data[a].packagingTypeId);
        }

    }

    return objectname;
}
function BindRNDPackaging(data) {
    var PackagingActivityHTML = '<thead class="bg-primary text-bold"><tr>'
        + '<td>Packaging Type</td>'
        + '<td>Unit of Measurement</td>'
        + '<td>Rs / Unit</td>'
        + '<td>Quantity</td>';
    $.each(_strengthArray, function (index, item) {
        PackagingActivityHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + '</td>';
    });
    PackagingActivityHTML += '<td>Total</td><td>Action</td></tr></thead><tbody id="tblPackagingBody">';

    for (var i = 1; i < 4; i++) {
        PackagingActivityHTML += CreateRNDPackagingTable($.grep(data, function (n, x) { return n.activityTypeId == i; }), i);
    }
    PackagingActivityHTML += '<tr><td class="text-left text-bold bg-light" colspan="10"></td></tr>';
    PackagingActivityHTML += '<tr><td class="text-bold">Total Packaging Costs</td>';
    PackagingActivityHTML += '<tr><td class="text-bold">Total Cost</td>';
    PackagingActivityHTML += "<td><input type='number' class='form-control' readonly='readonly' /></td><td><input type='number' class='form-control' readonly='readonly' /></td><td><input type='number' class='form-control' readonly='readonly' /></td><td><input type='number' class='form-control' readonly='readonly' /></td></tr></tbody>";
    $('#tablerndpackagingmaterialrequirement').html(PackagingActivityHTML);
    SetChildRowDeleteIcon();
}
function addRowRNDPackaging(element) {
    var node = $(element).parent().parent().clone(true);
    node.find("input.form-control").val("");
    $(element).parent().parent().after(node);
    SetChildRowDeleteIcon();
}
function deleteRowRNDPackaging(element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon(j);
}
//Packaging Material table end
//Batch size table start
function CreateBatchsizeTable(data, bioStudyTypeId) {
    var objectname = "";


    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (_strengthArray.length + 2) + '">Batch Size</td>';

    var _iterator = (bioStudyTypeId - 1) * _strengthArray.length;


    objectname += "<tr><td>Prototype Formulation</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td><input type="hidden" id="RNDBatchSizes[' + [(i + _iterator)] + '].StrengthId" name="RNDBatchSizes[' + [(i + _iterator)] + '].StrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control" id="RNDBatchSizes[' + [(i + _iterator)] + '].PrototypeFormulation" name="RNDBatchSizes[' + [(i + _iterator)] + '].PrototypeFormulation" placeholder="Prototype Formulation" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "prototypeFormulation")) + '" /></td>';
    }
    objectname += "<td><input type='number' class='form-control totalBatchSize' readonly='readonly' /></td></tr>";

    objectname += "<tr><td>Scale-up batch</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td><input type="number" class="form-control totalBatchSize" id="RNDBatchSizes[' + [(i + _iterator)] + '].ScaleUpbatch" name="RNDBatchSizes[' + [(i + _iterator)] + '].ScaleUpbatch" placeholder="ScaleUp batch" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "scaleUpbatch")) + '"  /></td>';
    }
    objectname += "<td></td></tr>";

    objectname += '<tr><td>Exhibit Batch 1</td>';
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td> <input type="number" class="form-control totalBatchSize" id="RNDBatchSizes[' + [(i + _iterator)] + '].ExhibitBatch1" name="RNDBatchSizes[' + [(i + _iterator)] + '].ExhibitBatch1" placeholder="Exhibit Batch 1" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "exhibitBatch1")) + '" /></td>';

    }
    objectname += "<td></td></tr>";

    objectname += "<tr><td>Exhibit Batch 2</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td> <input type="number" class="form-control totalBatchSize" id="RNDBatchSizes[' + [(i + _iterator)] + '].ExhibitBatch2" name="RNDBatchSizes[' + [(i + _iterator)] + '].ExhibitBatch2" placeholder="Exhibit Batch 2" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "exhibitBatch2")) + '" /></td>';

    }
    objectname += "<td></td></tr>";

    objectname += "<tr><td>Exhibit Batch 3</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td> <input type="number" class="form-control totalBatchSize" id="RNDBatchSizes[' + [(i + _iterator)] + '].ExhibitBatch3" name="RNDBatchSizes[' + [(i + _iterator)] + '].ExhibitBatch3" placeholder="Exhibit Batch 3" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "exhibitBatch3")) + '"/></td>';

    }
    objectname += "<td></td></tr>";

    //objectname += "<tr><td class='text-bold'>Total Cost</td>";
    //for (var i = 0; i < _strengthArray.length; i++) {
    //    objectname += "<td><input type='number' class='form-control totalBatchSize' readonly='readonly' /></td>";
    //}
    //objectname += "<td><input type='number' class='form-control totalBatchSize' readonly='readonly' /></td></tr>";

    return objectname;
}
function BindRNDBatchSize(data, rndmasterdata) {
    var batchSizeHTML = '<thead class="bg-primary text-bold"><tr><td>Batch Size</td>';
    $.each(_strengthArray, function (index, item) {
        batchSizeHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + '</td>';
    });
    batchSizeHTML += '<td>Total</td></tr></thead><tbody>';

    batchSizeHTML += CreateBatchsizeTable(data, 1);

    batchSizeHTML += '<tr><td class="text-bold">Total Cost</td>';
    $.each(_strengthArray, function (index, item) {
        batchSizeHTML += "<td><input type='number' class='form-control totalBatchSize' readonly='readonly' min='0'/></td>";
    });
    batchSizeHTML += "<td><input type='number' class='form-control totalBatchSize' readonly='readonly' min='0' /></td></tr></tbody>";
    $('#tablerndbatchsize').html(batchSizeHTML);
    if (rndmasterdata.length > 0) {
        $("#RNDMasterEntities_ApirequirementMarketPrice").val(rndmasterdata[0].apiRequirementMarketPrice);
        $("#RNDMasterEntities_PlanSupportCostRsPerDay").val(rndmasterdata[0].planSupportCostRsPerDay);
        $("#RNDMasterEntities_ManHourRate").val(rndmasterdata[0].manHourRate);
    }
   
}
//Batch size table end
//API requirement table start
function CreateAPIrequirementTable(data, bioStudyTypeId) {
    var objectname = "";


    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (_strengthArray.length + 2) + '">API Requirement</td>';

    var _iterator = (bioStudyTypeId - 1) * _strengthArray.length;


    objectname += "<tr><td>Prototype</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td><input type="hidden" id="RNDApirequirements[' + [(i + _iterator)] + '].StrengthId" name="RNDApirequirements[' + [(i + _iterator)] + '].StrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control" id="RNDApirequirements[' + [(i + _iterator)] + '].Prototype" name="RNDApirequirements[' + [(i + _iterator)] + '].Prototype" placeholder="Prototype" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "prototype")) + '" /></td>';
    }
    objectname += "<td><input type='number' class='form-control totalAPIReq' readonly='readonly' /></td></tr>";

    objectname += "<tr><td>Scale Up </td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td><input type="number" class="form-control totalAPIReq" id="RNDApirequirements[' + [(i + _iterator)] + '].ScaleUp" name="RNDApirequirements[' + [(i + _iterator)] + '].ScaleUp" placeholder="ScaleUp" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "scaleUp")) + '"  /></td>';
    }
    objectname += "<td></td></tr>";

    objectname += '<tr><td>Exhibit Batch 1</td>';
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td> <input type="number" class="form-control totalAPIReq" id="RNDApirequirements[' + [(i + _iterator)] + '].ExhibitBatch1" name="RNDApirequirements[' + [(i + _iterator)] + '].ExhibitBatch1" placeholder="Exhibit Batch 1" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "exhibitBatch1")) + '" /></td>';

    }
    objectname += "<td></td></tr>";

    objectname += "<tr><td>Exhibit Batch 2</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td> <input type="number" class="form-control totalAPIReq" id="RNDApirequirements[' + [(i + _iterator)] + '].ExhibitBatch2" name="RNDApirequirements[' + [(i + _iterator)] + '].ExhibitBatch2" placeholder="Exhibit Batch 2" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "exhibitBatch2")) + '" /></td>';

    }
    objectname += "<td></td></tr>";

    objectname += "<tr><td>Exhibit Batch 3</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td> <input type="number" class="form-control totalAPIReq" id="RNDApirequirements[' + [(i + _iterator)] + '].ExhibitBatch3" name="RNDApirequirements[' + [(i + _iterator)] + '].ExhibitBatch3" placeholder="Exhibit Batch 3" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "exhibitBatch3")) + '"/></td>';

    }
    objectname += "<td></td></tr>";
    objectname += "<tr><td>Prototype Cost</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td> <input type="number" class="form-control totalAPIReq" id="RNDApirequirements[' + [(i + _iterator)] + '].PrototypeCost" name="RNDApirequirements[' + [(i + _iterator)] + '].PrototypeCost" placeholder="Prototype Cost" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "prototypeCost")) + '"/></td>';

    }
    objectname += "<td></td></tr>";
    objectname += "<tr><td>Scale Up Cost</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td> <input type="number" class="form-control totalAPIReq" id="RNDApirequirements[' + [(i + _iterator)] + '].ScaleUpCost" name="RNDApirequirements[' + [(i + _iterator)] + '].ScaleUpCost" placeholder="Scale Up Cost" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "scaleUpCost")) + '"/></td>';

    }
    objectname += "<td></td></tr>";
    objectname += "<tr><td>Exhibit Batch Cost</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td> <input type="number" class="form-control totalAPIReq" id="RNDApirequirements[' + [(i + _iterator)] + '].ExhibitBatchCost" name="RNDApirequirements[' + [(i + _iterator)] + '].ExhibitBatchCost" placeholder="Exhibit Batch Cost" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "exhibitBatchCost")) + '"/></td>';

    }
    objectname += "<td></td></tr>";
    //objectname += "<tr><td class='text-bold'>Total Cost</td>";
    //for (var i = 0; i < _strengthArray.length; i++) {
    //    objectname += "<td><input type='number' class='form-control totalAPIReq' readonly='readonly' /></td>";
    //}
    //objectname += "<td><input type='number' class='form-control totalAPIReq' readonly='readonly' /></td></tr>";

    return objectname;
}
function BindRNDAPIRequirement(data) {

    var APIRequirementHTML = '<thead class="bg-primary text-bold"><tr><td>Batch Size</td>';
    $.each(_strengthArray, function (index, item) {
        APIRequirementHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + '</td>';
    });
    APIRequirementHTML += '<td>Total</td></tr></thead><tbody>';

    APIRequirementHTML += CreateAPIrequirementTable(data, 1);

    APIRequirementHTML += '<tr><td class="text-bold">Total Cost</td>';
    $.each(_strengthArray, function (index, item) {
        APIRequirementHTML += "<td><input type='number' class='form-control totalAPIReq' readonly='readonly' min='0' /></td>";
    });
    APIRequirementHTML += "<td><input type='number' class='form-control totalAPIReq' readonly='readonly' min='0' /></td></tr></tbody>";
    $('#tablerndapirequirement').html(APIRequirementHTML);
}
//API requirement table end
//Tooling change part table start
function CreateToolingchangepartTable(data, activityTypeId) {
    var objectname = "";
    var tableTitle = "Tooling and Change Part Cost";

    //var _iterator = (activityTypeId - 1) * _strengthArray.length;

    var _counter = (data.length == 0 ? 1 : data.length);

    var _activityType = [];

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (8 + _strengthArray.length) + '">' + tableTitle + '</td></tr>';
    for (var a = 0; a < _counter; a++) {
        if (data.length > 0) {
            if (_activityType.indexOf(data[a].activityTypeId) !== -1) {
                continue;
            }
        }
        objectname += '<tr  id="ToolingChangePartRow" class="toolingchangepartactivity ToolingChangePartActivity' + (activityTypeId) + '">'
            + '<td><input type="number" class="form-control totalTCP rndToolingChangePartPrototypeDevelopment" min="0" value="' + (data.length > 0 ? data[a].prototypeDevelopment : "") + '" /></td>'
            + '<td><input type="number" class="form-control totalTCP rndToolingChangePartTotalCost" min="0" value="' + (data.length > 0 ? data[a].totalCost : "") + '"  /></td>'
            + '<td><input type="number" class="form-control totalTCP rndToolingChangePartCost" min="0" value="' + (data.length > 0 ? data[a].cost : "") + '"  /></td>'
            + '<td><input type="number" class="form-control totalTCP rndToolingChangePartPrototype" min="0" value="' + (data.length > 0 ? data[a].prototype : "") + '"  /></td>'
            + '<td><input type="number" class="form-control totalTCP rndToolingChangePartScaleupExhibit" min="0" value="' + (data.length > 0 ? data[a].scaleUpExhibitBatch : "") + '"  /></td>'
            + '<td><input type="number" class="form-control totalTCP rndToolingChangePartTotalScaleupExhibit" min="0" value="' + (data.length > 0 ? data[a].totalScaleUpExhibitBatch : "") + '"  /></td>'
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += '<td><input type="hidden" class="rndToolingChangePartStrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control ToolingChangePartStrengthValue" min="0" value="' + (data.length > 0 ? getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "toolingChangepartDevelopment") : "") + '" /></td>';
        }
        objectname += "<td><input type='number' class='form-control totalTCP rndToolingChangePartFinalCost' readonly='readonly' /></td><td> <i class='fa-solid fa-circle-plus nav-icon text-success operationButton' id='addIcon' onclick='addRowToolingChangePart(this);'></i> <i class='fa-solid fa-trash nav-icon text-red strengthDeleteIcon operationButton DeleteIcon' onclick='deleteRowToolingChangePart(this);' ></i></td></tr>";
        if (data.length > 0) {
            _activityType.push(data[a].activityTypeId);
        }
    }

    return objectname;
}
function BindRNDToolingchangepart(data) {
    
    var toolingchangepartHTML = '<thead class="bg-primary text-bold"><tr>'
        + '<td>Prototype Development</td>'
        + '<td>Total Cost</td>'
        + '<td>Cost</td>'
        + '<td>Prototype</td>'
        + '<td>Scale up / Exhibit Batch</td>'
        + '<td>Total Scale up / Exhibit Batch</td>'
    $.each(_strengthArray, function (index, item) {
        toolingchangepartHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + ' (Cost)</td>';
    });
    toolingchangepartHTML += '<td>Total</td><td>Action</td></tr></thead><tbody id="tblToolingChangePartBody">';
    for (var i = 1; i < 2; i++) {
        toolingchangepartHTML += CreateToolingchangepartTable($.grep(data, function (n, x) { return n.activityTypeId == i; }), i);
    }

    toolingchangepartHTML += '<tr><td class="text-bold">Total Cost</td>';

    $.each(_strengthArray, function (index, item) {
        toolingchangepartHTML += "<td><input type='number' class='form-control' readonly='readonly' min='0'/></td>";
    });

    toolingchangepartHTML += "<td><input type='number' class='form-control' readonly='readonly' min='0' /><td></td></tr></tbody>";

    $('#tablerndtoolingchangepart').html(toolingchangepartHTML);

    SetChildRowDeleteIcon();

}
function addRowToolingChangePart(element) {
    var node = $(element).parent().parent().clone(true);
    node.find("input.form-control").val("");
    $(element).parent().parent().after(node);
    SetChildRowDeleteIcon();
}
function deleteRowToolingChangePart(element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
}
//Tooling change part table end
//Capex and Misc Exp table start
function CreateCapexMiscTable(data, activityTypeId) {
    var objectname = "";
    var tableTitle = "Capex and Miscellaneous Expenses";

    //var _iterator = (activityTypeId - 1) * _strengthArray.length;

    var _counter = (data.length == 0 ? 1 : data.length);

    var _activityType = [];

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (6 + _strengthArray.length) + '">' + tableTitle + '</td>';
    for (var a = 0; a < _counter; a++) {
        if (data.length > 0) {
            if (_activityType.indexOf(data[a].activityTypeId) !== -1) {
                continue;
            }
        }
        objectname += '<tr  id="CapexMiscRow" class="CapexMiscactivity CapexMiscActivity' + (activityTypeId) + '">'
            + '<td><input type="number" class="form-control totalCapexMisc rndCapexMiscMiscellaneous"  min="0" value="' + (data.length > 0 ? data[a].miscellaneousDevelopment : "") + '" /></td>'
            + '<td><input type="number" class="form-control totalCapexMisc rndCapexMiscLicensing" min="0" value="' + (data.length > 0 ? data[a].licensing : "") + '"  /></td>'
            + '<td><input type="number" class="form-control totalCapexMisc rndCapexMiscCapex1" min="0" value="' + (data.length > 0 ? data[a].capex1 : "") + '"  /></td>'
            + '<td><input type="number" class="form-control totalCapexMisc rndCapexMiscCapex2" min="0" value="' + (data.length > 0 ? data[a].capex2 : "") + '"  /></td>'
            + '<td><input type="number" class="form-control totalCapexMisc rndCapexMiscTotalCost" min="0" value="' + (data.length > 0 ? data[a].totalCost : "") + '"  /></td>'
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += '<td><input type="hidden" class="rndCapexMiscStrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control CapexMiscStrengthValue" min="0" value="' + (data.length > 0 ? getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "capexMiscellaneousExpensesDevelopment") : "") + '" /></td>';
        }
        objectname += "<td><input type='number' class='form-control' readonly='readonly' min='0' /></td><td> <i class='fa-solid fa-circle-plus nav-icon text-success operationButton' id='addIcon' onclick='addRowCapexMisc(this);'></i> <i class='fa-solid fa-trash nav-icon text-red strengthDeleteIcon operationButton DeleteIcon' onclick='deleteRowCapexMisc(this);' ></i></td></tr>";
        if (data.length > 0) {
            _activityType.push(data[a].activityTypeId);
        }
    }

    return objectname;
}
function BindRNDCapexMiscExpenses(data) {
    var capexmiscexpensesHTML = '<thead class="bg-primary text-bold"><tr>'
        + '<td>Miscellaneous</td>'
        + '<td>Licensing</td>'
        + '<td>Capex 1</td>'
        + '<td>Capex 2</td>'
        + '<td>Total Cost</td>'
    $.each(_strengthArray, function (index, item) {
        capexmiscexpensesHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + ' (Cost)</td>';
    });
    capexmiscexpensesHTML += '<td>Total</td><td>Action</td></tr></thead><tbody id="tblCapexMiscBody">';
    for (var i = 1; i < 2; i++) {
        capexmiscexpensesHTML += CreateCapexMiscTable($.grep(data, function (n, x) { return n.activityTypeId == i; }), i);
    }

    capexmiscexpensesHTML += '<tr><td class="text-bold">Total Cost</td>';

    $.each(_strengthArray, function (index, item) {
        capexmiscexpensesHTML += "<td><input type='number' class='form-control' readonly='readonly'  min='0' /></td>";
    });

    capexmiscexpensesHTML += "<td><input type='number' class='form-control' readonly='readonly' min='0'/><td></td></tr></tbody>";

    $('#tablerndcapexmiscellaneousexpenses').html(capexmiscexpensesHTML);

    SetChildRowDeleteIcon();

}
function addRowCapexMisc(element) {
    var node = $(element).parent().parent().clone(true);
    node.find("input.form-control").val("");
    $(element).parent().parent().after(node);
    SetChildRowDeleteIcon();
}
function deleteRowCapexMisc(element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
}
//Capex and Misc Exp table end
//plant support cost table start
function CreatePlantSupportCostTable(data, activityTypeId) {
    var objectname = "";
    var tableTitle = "Plant Support Cost";

    //var _iterator = (activityTypeId - 1) * _strengthArray.length;

    var _counter = (data.length == 0 ? 1 : data.length);

    var _testType = [];

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (6 + _strengthArray.length) + '">' + tableTitle + '</td>';
    for (var a = 0; a < _counter; a++) {
        if (data.length > 0) {
            if (_testType.indexOf(data[a].activityTypeId) !== -1) {
                continue;
            }
        }
        objectname += '<tr  id="PlantSupportCostRow" class="PlantSupportCostactivity PlantSupportCostActivity' + (activityTypeId) + '">'
            + '<td><input type="number" class="form-control totalPlantSupportCost rndPlantSupportCostScaleUp"  min="0" value="' + (data.length > 0 ? data[a].scaleUp : "") + '" /></td>'
            + '<td><input type="number" class="form-control totalPlantSupportCost rndPlantSupportCostExhibitBatch"  min="0" value="' + (data.length > 0 ? data[a].exhibitBatch : "") + '"  /></td>'
            + '<td><input type="number" class="form-control totalPlantSupportCost rndPlantSupportCostTotalCost" min="0" value="' + (data.length > 0 ? data[a].totalCost : "") + '"  /></td>'
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += '<td><input type="hidden" class="rndPlantSupportCostStrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control PlantSupportCostStrengthValue" min="0" value="' + (data.length > 0 ? getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "plantSupportDevelopment") : "") + '" /></td>';
        }
        objectname += "<td><input type='number' class='form-control' readonly='readonly' /></td><td> <i class='fa-solid fa-circle-plus nav-icon text-success operationButton' id='addIcon' onclick='addRowPlantSupportCost(this);'></i> <i class='fa-solid fa-trash nav-icon text-red strengthDeleteIcon operationButton DeleteIcon' onclick='deleteRowPlantSupportCost(this);' ></i></td></tr>";
        if (data.length > 0) {
            _testType.push(data[a].activityTypeId);
        }
    }

    return objectname;
}
function BindRNDPlantSupportCost(data) {
    var plantsupportcostHTML = '<thead class="bg-primary text-bold"><tr>'
        + '<td>Scale Up</td>'
        + '<td>Exhibit Batch</td>'
        + '<td>Total Cost</td>'
    $.each(_strengthArray, function (index, item) {
        plantsupportcostHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + ' (Cost)</td>';
    });
    plantsupportcostHTML += '<td>Total</td><td>Action</td></tr></thead><tbody id="tblPlantSupportCostBody">';
    for (var i = 1; i < 2; i++) {
        plantsupportcostHTML += CreatePlantSupportCostTable($.grep(data, function (n, x) { return n.activityTypeId == i; }), i);
    }

    plantsupportcostHTML += '<tr><td class="text-bold">Total Cost</td>';

    $.each(_strengthArray, function (index, item) {
        plantsupportcostHTML += "<td><input type='number' class='form-control' readonly='readonly' min='0'/></td>";
    });

    plantsupportcostHTML += "<td><input type='number' class='form-control' readonly='readonly' min='0'/><td></td></tr></tbody>";

    $('#tablerndplantsupportcost').html(plantsupportcostHTML);

    SetChildRowDeleteIcon();

}
function addRowPlantSupportCost(element) {
    var node = $(element).parent().parent().clone(true);
    node.find("input.form-control").val("");
    $(element).parent().parent().after(node);
    SetChildRowDeleteIcon();
}
function deleteRowPlantSupportCost(element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
}
//plant support cost table end
//Reference Product Detail table start
function CreateReferenceProductDetailTable(data, bioStudyTypeId) {
    var objectname = "";


    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (_strengthArray.length + 2) + '">Reference Product Detail</td>';

    var _iterator = (bioStudyTypeId - 1) * _strengthArray.length;


    objectname += "<tr><td>Unit Cost Of Reference Product</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td><input type="hidden" id="RNDReferenceProductDetails[' + [(i + _iterator)] + '].StrengthId" name="RNDReferenceProductDetails[' + [(i + _iterator)] + '].StrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control" id="RNDReferenceProductDetails[' + [(i + _iterator)] + '].UnitCostOfReferenceProduct" name="RNDReferenceProductDetails[' + [(i + _iterator)] + '].UnitCostOfReferenceProduct" placeholder="UnitCostOfReferenceProduct" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "unitCostOfReferenceProduct")) + '" /></td>';
    }
    objectname += "<td><input type='number' class='form-control totalRPD' readonly='readonly' min='0' /></td></tr>";

    objectname += "<tr><td>Formulation Development</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td><input type="number" class="form-control totalRPD" id="RNDReferenceProductDetails[' + [(i + _iterator)] + '].FormulationDevelopment" name="RNDReferenceProductDetails[' + [(i + _iterator)] + '].FormulationDevelopment" placeholder="Formulation Development" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "formulationDevelopment")) + '"  /></td>';
    }
    objectname += "<td></td></tr>";

    objectname += '<tr><td>Pilot Be</td>';
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td> <input type="number" class="form-control totalRPD" id="RNDReferenceProductDetails[' + [(i + _iterator)] + '].PilotBe" name="RNDReferenceProductDetails[' + [(i + _iterator)] + '].PilotBe" placeholder="Pilot Be" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "pilotBE")) + '" /></td>';

    }
    objectname += "<td></td></tr>";

    objectname += "<tr><td>Pharmasuitical Equivalence</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td> <input type="number" class="form-control totalRPD" id="RNDReferenceProductDetails[' + [(i + _iterator)] + '].PharmasuiticalEquivalence" name="RNDReferenceProductDetails[' + [(i + _iterator)] + '].PharmasuiticalEquivalence" placeholder="Pharmasuitical Equivalence" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "pharmasuiticalEquivalence")) + '" /></td>';

    }
    objectname += "<td></td></tr>";

    objectname += "<tr><td>Pivotal Bio</td>";
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td> <input type="number" class="form-control totalRPD" id="RNDReferenceProductDetails[' + [(i + _iterator)] + '].PivotalBio" name="RNDReferenceProductDetails[' + [(i + _iterator)] + '].PivotalBio" placeholder="Pivotal Bio" min="0" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "pivotalBio")) + '"/></td>';

    }
    objectname += "<td></td></tr>";
    //objectname += "<tr><td>Total Cost </td>";
    //for (var i = 0; i < _strengthArray.length; i++) {
    //    objectname += '<td> <input type="number" class="form-control totalRPD" id="RNDReferenceProductDetails[' + [(i + _iterator)] + '].TotalCost" name="RNDReferenceProductDetails[' + [(i + _iterator)] + '].TotalCost" placeholder="Total Cost" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "totalCost")) + '"/></td>';

    //}
    //objectname += "<td></td></tr>";

    return objectname;
}
function BindRNDReferenceProductDetail(data) {
    var RPDHTML = '<thead class="bg-primary text-bold"><tr><td>Reference Product Detail</td>';
    $.each(_strengthArray, function (index, item) {
        RPDHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + '</td>';
    });
    RPDHTML += '<td>Total</td></tr></thead><tbody>';

    RPDHTML += CreateReferenceProductDetailTable(data, 1);

    RPDHTML += '<tr><td class="text-bold">Total Cost</td>';
    $.each(_strengthArray, function (index, item) {
        RPDHTML += "<td><input type='number' class='form-control totalRPD' readonly='readonly' min='0'/></td>";
    });
    RPDHTML += "<td><input type='number' class='form-control totalRPD' readonly='readonly' min='0'/></td></tr></tbody>";
    $('#tablerndreferenceproductdetail').html(RPDHTML);
}
//RPD table end
// Filling Expenses table Start
function CreateFillingExpensesTable(data, activityTypeId) {
    var objectname = "";
    var tableTitle = "Filling Expenses";

    //var _iterator = (activityTypeId - 1) * _strengthArray.length;

    var _counter = (data.length == 0 ? 1 : data.length);

    var _activityType = [];

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (6 + _strengthArray.length) + '">' + tableTitle + '</td>';
    for (var a = 0; a < _counter; a++) {

        if (data.length > 0) {
            if (_activityType.indexOf(data[a].businessUnitId) !== -1) {
                continue;
            }
        }

        objectname += '<tr  id="FillingExpensesRow" class="FillingExpensesactivity FillingExpensesActivity' + (activityTypeId) + '">'
            + '<td><input type="number" class="form-control totalFillingExpenses rndFillingExpensesTotalCost" value="2300000" min="0"/></td>'
            + '<td><select class="form-control readOnlyUpdate rndFillingExpensesRegionId"><option value = "0" > --Select --</option ></select><input type="hidden" value="' + (data.length > 0 ? data[a].businessUnitId : "") + '"/></td>'
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += '<td> <div stylle="display: inline-block;" ><input type="hidden" class="rndFillingExpensesStrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="checkbox" id="rndFillingExpensesStrengthIsChecked' + _strengthArray[i].pidfProductStrengthId + '" class="rndFillingExpensesStrengthIsChecked' + _strengthArray[i].pidfProductStrengthId + '"  ></select> &nbsp; <input type="number" class="FillingExpensesStrengthValue" readonly="readonly" disabled="true" min="0" /></div></td>';
        }
        objectname += "<td><input type='number' class='form-control' readonly='readonly' min='0' /></td><td> <i class='fa-solid fa-circle-plus nav-icon text-success operationButton' id='addIcon' onclick='addRowFillingExpenses(this);'></i> <i class='fa-solid fa-trash nav-icon text-red strengthDeleteIcon operationButton DeleteIcon' onclick='deleteRowFillingExpenses(this);' ></i></td></tr>";
        if (data.length > 0) {
            _activityType.push(data[a].businessUnitId);
        }
    }

    return objectname;
}
function BindRNDFillingExpenses(data) {
   
    var FillingExpensesexpensesHTML = '<thead class="bg-primary text-bold"><tr>'
        + '<td width="15%">Total Cost</td>'
        + '<td>Region</td>'
    $.each(_strengthArray, function (index, item) {
        FillingExpensesexpensesHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + ' (Cost)</td>';
    });
    FillingExpensesexpensesHTML += '<td>Total</td><td>Action</td></tr></thead><tbody id="tblFillingExpensesBody">';
    for (var i = 1; i < 2; i++) {
        FillingExpensesexpensesHTML += CreateFillingExpensesTable($.grep(data, function (n, x) { return n.pbfGeneralId; }), i);
    }

    //FillingExpensesexpensesHTML += '<tr><td class="text-bold">Total Cost</td>';

    //$.each(_strengthArray, function (index, item) {
    //    FillingExpensesexpensesHTML += "<td><input type='number' class='form-control' readonly='readonly' /></td>";
    //});

    //FillingExpensesexpensesHTML += "<td><input type='number' class='form-control' readonly='readonly' /><td></td></tr></tbody>";
    $('#tablerndfilingexpenses').html(FillingExpensesexpensesHTML);
    SetChildRowDeleteIcon();
   

}
function addRowFillingExpenses(element) {
    var node = $(element).parent().parent().clone(true);
    node.find("input.form-control").val("");
    $(element).parent().parent().after(node);
    SetChildRowDeleteIcon();
}
function deleteRowFillingExpenses(element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
}
// Filling Expenses table End

//MPC table start
function CreateRNDManPowerCostTable(data) {
    var objectname = "";

    objectname += '<tr><td class="text-left text-bold bg-light" colspan="' + (8+_strengthArray.length + 2) + '">Project Activities</td></tr>';
    var _counter = (data.length == 0 ? 1 : data.length);
    var _projectActivities = [];
    for (var a = 0; a < _counter; a++) {
        if (data.length > 0) {
            if (_projectActivities.indexOf(data[a].projectActivitiesId) !== -1) {
                continue;
            }
        }
        objectname += '<tr class="MPCActivity"><td><input type="number" class="form-control totalMPC rndMPCDurationInDays" min="0" value="' + (data.length > 0 ? data[a].durationInDays : "") + '"   /></td><td><input type="hidden" class="rndMPCProjectActivitiesId" value="' + data[a].projectActivitiesId + '" />' + data[a].projectActivitiesName + '</td><td><input type="number" class="form-control totalMPC rndMPCManPowerInDays" min="0" value="' + (data.length > 0 ? data[a].manPowerInDays : "") + '" /></td>';
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += '<td><input type="hidden" class="rndMPCStrengthId"  value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control rndMPCStrengthValue" readonly="readonly" min="0"/></td>';
        }
        objectname += "<td><input type='number' class='form-control totalRPD' readonly='readonly' min='0' /></td><td><input type='number' class='form-control totalRPD' readonly='readonly' min='0' /></td><td><input type='number' class='form-control totalRPD' readonly='readonly' min='0' /></td><td><input type='number' class='form-control totalRPD' readonly='readonly' min='0' /></td><td><input type='number' class='form-control totalRPD' readonly='readonly' min='0' /></td></tr>";
        if (data.length > 0) {
            _projectActivities.push(data[a].projectActivitiesId);
        }
    }

    //objectname += "<tr><td>Total Cost </td>";
    //for (var i = 0; i < _strengthArray.length; i++) {
    //    objectname += '<td> <input type="number" class="form-control totalRPD" id="RNDReferenceProductDetails[' + [(i + _iterator)] + '].TotalCost" name="RNDReferenceProductDetails[' + [(i + _iterator)] + '].TotalCost" placeholder="Total Cost" value="' + (getValueFromStrengthId(data, _strengthArray[i].pidfProductStrengthId, "totalCost")) + '"/></td>';

    //}
    //objectname += "<td></td></tr>";

    return objectname;
}
function BindRNDManPowerCost(data) {
    console.log(data)
    var RPDHTML = '<thead class="bg-primary text-bold"><tr><td>Duration in days </td><td>Project Activities </td><td>Man Power in days </td>';
    $.each(_strengthArray, function (index, item) {
        RPDHTML += '<td>' + getStrengthName(item.pidfProductStrengthId) + '</td>';
    });
    RPDHTML += '<td>Total</td><td>4 Strength</td><td>4 RLD</td><td>NON RLD</td><td>RLD</td></tr></thead><tbody>';

    RPDHTML += CreateRNDManPowerCostTable(data);

    RPDHTML += "</tbody>";
    $('#tablerrndmanpowercostprojectduration').html(RPDHTML);
}
//MPC table end
function SetRNDChildRows() {

    var _RNDexicipientArray = [];
    var _RNDpackagingArray = [];
    var _RNDToolingChangePartArray = [];
    var _RNDCapexMiscArray = [];
    var _RNDPlantSupportCostArray = [];
    var _RNDFillingExpensesArray = [];
    var _RNDMenPowerCostArray = [];
    for (var i = 1; i < 4; i++) {
        $.each($('#tablerndexicipientrequirement tbody tr.exicipientActivity' + i + ''), function () {

            var ExicipientPrototype = $(this).find(".rndExicipientPrototype").val();
            var RsPerKg = $(this).find(".rndExicipientRsperkg").val();
            var MgPerUnitDosage = $(this).find(".rndExicipientMGperUnitDosage").val();
            var ActivityTypeId = $(this).find(".rndExicipientTypeId").val();

            $.each($(this).find(".rndExicipientStrengthId"), function (index, item) {
                var _rndExicipientObject = new Object();
                _rndExicipientObject.StrengthId = $(this).val();
                _rndExicipientObject.ExicipientDevelopment = $(this).parent().find(".rndExicipientStrengthValue").val();
                _rndExicipientObject.ExicipientPrototype = ExicipientPrototype;
                _rndExicipientObject.ActivityTypeId = ActivityTypeId;
                _rndExicipientObject.RsPerKg = RsPerKg;
                _rndExicipientObject.MgPerUnitDosage = MgPerUnitDosage;
                if (_rndExicipientObject.ExicipientDevelopment != "") {
                    _RNDexicipientArray.push(_rndExicipientObject);
                }
            });
        });

        $('#hdnrndExicipientData').val(JSON.stringify(_RNDexicipientArray));

        $.each($('#tablerndpackagingmaterialrequirement tbody tr.packagingActivity' + i + ''), function () {

            var PackagingTypeId = $(this).find(".rndPackagingTypeId").val();
            var ActivityTypeId = $(this).find(".rndPackagingActivityId").val();
            var UnitOfMeasurement = $(this).find(".rndPackagingUnitofMeasurement").val();
            /*  var PrototypeDevelopment = $(this).find(".rndPackagingPrototype").val();*/
            var RsPerUnit = $(this).find(".rndPackagingRsperUnit").val();
            var Quantity = $(this).find(".rndPackagingQuantity").val();


            $.each($(this).find(".rndPackagingStrengthId"), function (index, item) {
                var _rndPackagingObject = new Object();
                _rndPackagingObject.StrengthId = $(this).val();
                _rndPackagingObject.PackagingDevelopment = $(this).parent().find(".rndPackagingStrengthValue").val();
                _rndPackagingObject.PackagingTypeId = PackagingTypeId;
                _rndPackagingObject.ActivityTypeId = ActivityTypeId;
                /* _rndPackagingObject.PrototypeDevelopment = PrototypeDevelopment;*/
                _rndPackagingObject.RsPerUnit = RsPerUnit;
                _rndPackagingObject.UnitOfMeasurement = UnitOfMeasurement;
                _rndPackagingObject.Quantity = Quantity;

                if (_rndPackagingObject.PackagingDevelopment != "" && _rndPackagingObject.PackagingTypeId != "0") {
                    _RNDpackagingArray.push(_rndPackagingObject);
                }
            });
        });

        $('#hdnrndPackagingData').val(JSON.stringify(_RNDpackagingArray));

        if (i == 1) {
            var toolcount = i;
            $.each($('#tablerndtoolingchangepart tbody tr.ToolingChangePartActivity' + i + ''), function () {
                var PrototypeDevelopment = $(this).find(".rndToolingChangePartPrototypeDevelopment").val();
                var TotalCost = $(this).find(".rndToolingChangePartTotalCost").val();
                var Cost = $(this).find(".rndToolingChangePartCost").val();
                var Prototype = $(this).find(".rndToolingChangePartPrototype").val();
                var ScaleUpExhibitBatch = $(this).find(".rndToolingChangePartScaleupExhibit").val();
                var TotalScaleUpExhibitBatch = $(this).find(".rndToolingChangePartTotalScaleupExhibit").val();
                var FinalCost = $(this).find(".rndToolingChangePartFinalCost").val();


                $.each($(this).find(".rndToolingChangePartStrengthId"), function (index, item) {
                    var _rndToolingChangePartObject = new Object();
                    _rndToolingChangePartObject.StrengthId = $(this).val();
                    _rndToolingChangePartObject.ToolingChangepartDevelopment = $(this).parent().find(".ToolingChangePartStrengthValue").val();
                    _rndToolingChangePartObject.PrototypeDevelopment = PrototypeDevelopment;
                    _rndToolingChangePartObject.TotalCost = TotalCost;
                    _rndToolingChangePartObject.Cost = Cost;
                    _rndToolingChangePartObject.Prototype = Prototype;
                    _rndToolingChangePartObject.ScaleUpExhibitBatch = ScaleUpExhibitBatch;
                    _rndToolingChangePartObject.TotalScaleUpExhibitBatch = TotalScaleUpExhibitBatch;
                    _rndToolingChangePartObject.FinalCost = FinalCost;

                    if (_rndToolingChangePartObject.ToolingChangepartDevelopment != "") {
                        _rndToolingChangePartObject.ActivityTypeId = toolcount;
                        _RNDToolingChangePartArray.push(_rndToolingChangePartObject);
                    }
                });
                toolcount++;
            });

            $('#hdnrndToolingChangePartData').val(JSON.stringify(_RNDToolingChangePartArray));
            var misccount = i
            $.each($('#tablerndcapexmiscellaneousexpenses tbody tr.CapexMiscActivity' + i + ''), function () {

                var MiscellaneousDevelopment = $(this).find(".rndCapexMiscMiscellaneous").val();
                var Licensing = $(this).find(".rndCapexMiscLicensing").val();
                var Capex1 = $(this).find(".rndCapexMiscCapex1").val();
                var Capex2 = $(this).find(".rndCapexMiscCapex2").val();
                var TotalCost = $(this).find(".rndCapexMiscTotalCost").val();
                //var TotalScaleUpExhibitBatch = $(this).find(".rndToolingChangePartTotalScaleupExhibit").val();
                //var FinalCost = $(this).find(".rndToolingChangePartFinalCost").val();


                $.each($(this).find(".rndCapexMiscStrengthId"), function (index, item) {
                    var _rndCapexMiscObject = new Object();
                    _rndCapexMiscObject.StrengthId = $(this).val();
                    _rndCapexMiscObject.CapexMiscellaneousExpensesDevelopment = $(this).parent().find(".CapexMiscStrengthValue").val();
                    _rndCapexMiscObject.MiscellaneousDevelopment = MiscellaneousDevelopment;
                    _rndCapexMiscObject.Licensing = Licensing;
                    _rndCapexMiscObject.Capex1 = Capex1;
                    _rndCapexMiscObject.Capex2 = Capex2;
                    _rndCapexMiscObject.TotalCost = TotalCost;
                    //_rndCapexMiscObject.TotalScaleUpExhibitBatch = TotalScaleUpExhibitBatch;
                    //_rndCapexMiscObject.FinalCost = FinalCost;

                    if (_rndCapexMiscObject.CapexMiscellaneousExpensesDevelopment != "") {
                        _rndCapexMiscObject.ActivityTypeId = misccount;
                        _RNDCapexMiscArray.push(_rndCapexMiscObject);

                    }
                });
                misccount++;
            });

            $('#hdnrndCapexMiscellaneousExpensesData').val(JSON.stringify(_RNDCapexMiscArray));

            var plantsupportcount = i
            $.each($('#tablerndplantsupportcost tbody tr.PlantSupportCostActivity' + i + ''), function () {

                var ScaleUp = $(this).find(".rndPlantSupportCostScaleUp").val();
                var ExhibitBatch = $(this).find(".rndPlantSupportCostExhibitBatch").val();
                //var Capex1 = $(this).find(".rndPlantSupportCostTotalCost").val();
                //var Capex2 = $(this).find(".rndCapexMiscCapex2").val();
                var TotalCost = $(this).find(".rndPlantSupportCostTotalCost").val();
                //var TotalScaleUpExhibitBatch = $(this).find(".rndToolingChangePartTotalScaleupExhibit").val();
                //var FinalCost = $(this).find(".rndToolingChangePartFinalCost").val();


                $.each($(this).find(".rndPlantSupportCostStrengthId"), function (index, item) {
                    var _rndPlantSupportObject = new Object();
                    _rndPlantSupportObject.StrengthId = $(this).val();
                    _rndPlantSupportObject.PlantSupportDevelopment = $(this).parent().find(".PlantSupportCostStrengthValue").val();
                    _rndPlantSupportObject.ScaleUp = ScaleUp;
                    _rndPlantSupportObject.ExhibitBatch = ExhibitBatch;
                    //_rndCapexMiscObject.Capex1 = Capex1;
                    //_rndCapexMiscObject.Capex2 = Capex2;
                    _rndPlantSupportObject.TotalCost = TotalCost;
                    //_rndCapexMiscObject.TotalScaleUpExhibitBatch = TotalScaleUpExhibitBatch;
                    //_rndCapexMiscObject.FinalCost = FinalCost;

                    if (_rndPlantSupportObject.PlantSupportDevelopment != "") {
                        _rndPlantSupportObject.ActivityTypeId = plantsupportcount;
                        _RNDPlantSupportCostArray.push(_rndPlantSupportObject);

                    }
                });
                plantsupportcount++;
            });

            $('#hdnrndPlantSupportCostData').val(JSON.stringify(_RNDPlantSupportCostArray));

            var fillingcount = i
            $.each($('#tablerndfilingexpenses tbody tr.FillingExpensesActivity' + i + ''), function () {

                var BusinessUnitId = $(this).find(".rndFillingExpensesRegionId").val();

                $.each($(this).find(".rndFillingExpensesStrengthId"), function (index, item) {
                    var _rndFillingExpensesObject = new Object();
                    _rndFillingExpensesObject.StrengthId = $(this).val();
                    _rndFillingExpensesObject.ExpensesStrengthValue = $(this).parent().find(".FillingExpensesStrengthValue").val();
                    _rndFillingExpensesObject.IsChecked = $(this).parent().find('#rndFillingExpensesStrengthIsChecked' + $(this).val()).is(":checked");
                    _rndFillingExpensesObject.BusinessUnitId = BusinessUnitId;
                    if (_rndFillingExpensesObject.IsChecked == true) {
                        //_rndPlantSupportObject.ActivityTypeId = fillingcount;
                        _RNDFillingExpensesArray.push(_rndFillingExpensesObject);

                    }
                });
                fillingcount++;
            });

            $('#hdnrndFillingExpensesData').val(JSON.stringify(_RNDFillingExpensesArray));
            var MPCcount = i
            $.each($('#tablerrndmanpowercostprojectduration tbody tr.MPCActivity'), function () {

                var DurationInDays = $(this).find(".rndMPCDurationInDays").val();
                var ProjectActivitiesId = $(this).find(".rndMPCProjectActivitiesId").val();
                var ManPowerInDays = $(this).find(".rndMPCManPowerInDays").val();
                
                $.each($(this).find(".rndMPCStrengthId"), function (index, item) {
                    var _rndMPCObject = new Object();
                    _rndMPCObject.StrengthId = $(this).val();
                    _rndMPCObject.MCPStrengthValue = $(this).parent().find(".rndMPCStrengthValue").val();
                    _rndMPCObject.DurationInDays = DurationInDays;
                    _rndMPCObject.ProjectActivitiesId = ProjectActivitiesId;
                    _rndMPCObject.ManPowerInDays = ManPowerInDays;
                    //if (_rndFillingExpensesObject.ExpensesStrengthValue == true) {
                    _RNDMenPowerCostArray.push(_rndMPCObject);

                    //}
                });
                fillingcount++;
            });

            $('#hdnrndManPowerCostProjectDuration').val(JSON.stringify(_RNDMenPowerCostArray));
        }


    }
}
//R&D End
