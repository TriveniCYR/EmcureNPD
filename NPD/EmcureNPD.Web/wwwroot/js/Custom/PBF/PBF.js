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
            //BindRNDExicipient(data.PBFAnalyticalEntity);
            //BindRNDPackaging(data.PBFAnalyticalEntity);

            $(data.MasterTestType).each(function (index, item) {
                $('.AnalyticalTestTypeId').append('<option value="' + item.testTypeId + '" data-TestTypeCode="' + item.testTypeCode + '" data-TestTypePrice="' + item.testTypePrice +'">' + item.testTypeCode + ": " + item.testTypeName + '</option>');
            });
            $(_strengthArray).each(function (index, item) {
                $('.AMVstrengths').append('<option value="' + item.pidfProductStrengthId + '">' + getStrengthName(item.pidfProductStrengthId) + '</option>');
            });
            $(data.MasterPackagingType).each(function (index, item) {
                $('.RNDPackagingTypeId').append('<option value="' + item.packagingTypeId + '">' + item.packagingTypeName + '</option>');
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
            + '<td><select class="form-control readOnlyUpdate AnalyticalTestTypeId"><option value = "0" > --Select --</option ></select><input type="hidden" value="' + (data.length > 0 ? data[a].testTypeId : "") +'" /></td>'
            + '<td><input type="number" class="form-control totalAnalytical analyticalNumberOfTest" value="' + (data.length > 0 ? data[a].numberoftests : "")+'"  /></td>'
            + '<td><input type="text" class="form-control totalAnalytical analyticalPrototypeDevelopment" value="' + (data.length > 0 ? data[a].prototypeDevelopment : "") +'"  /></td>'
            + '<td><input type="number" class="form-control totalAnalytical analyticalRsTest" value="' + (data.length > 0 ? data[a].costPerTest : "") +'"  /></td>'
        for (var i = 0; i < _strengthArray.length; i++) {
            objectname += '<td><input class="analyticalTypeId" type="hidden" value="' + activityTypeId + '" /><input type="hidden" class="analyticalStrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control analyticalStrengthValue" value="' + (data.length > 0 ? getValueFromStrengthTestTypeId(data, _strengthArray[i].pidfProductStrengthId, "prototypeCost", data[a].testTypeId) : "") +'" /></td>';
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
    analyticalactivityHTML += '<td colspan="2"><input type="number" class="form-control totalAnalytical" id="AMVCosts.TotalAmvcost" name="AMVCosts.TotalAmvcost" placeholder="Total AMV Cost" value="' + (costData.length > 0 ? costData[0].totalAMVCost : "") + '" /></td><td class="text-bold" colspan="2"><textarea id="remark" class="form-control" id="AMVCosts.Remark" name="AMVCosts.Remark" placeholder="Remark">' + (costData.length > 0 ? costData[0].remark : "") + '</textarea></td>  <td colspan="' + (_strengthArray.length) + '"><input type="hidden" id="AMVCosts.StrengthId" name="AMVCosts.StrengthId" /><select class="form-control readOnlyUpdate AMVstrengths" multiple="multiple" name="AMVCosts.StrengthId"></select><input type="hidden" value="' + (costData.length > 0 ? costData[0].strengthIds : "") +'" /> </td></tr>';
    analyticalactivityHTML += '<tr><td colspan="4" class="text-bold">Total Cost</td>';   

    $.each(_strengthArray, function (index, item) {
        analyticalactivityHTML += "<td><input type='number' class='form-control' readonly='readonly' /></td>";
    });

    analyticalactivityHTML += "<td><input type='number' class='form-control' readonly='readonly' /><td></td></tr></tbody>";

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
    }
    //if ($('#tableclinical tbody tr').length > 1) {
    //    $('.DeleteIcon').show();
    //} else {
    //    $('.DeleteIcon').hide();
    //}
    //if ($('#tablerndexicipientrequirement tbody tr').length > 1) {
    //    $('.DeleteIcon').show();
    //} else {
    //    $('.DeleteIcon').hide();
    //}
    //if ($('#tablerndpackagingmaterialrequirement tbody tr').length > 1) {
    //    $('.DeleteIcon').show();
    //} else {
    //    $('.DeleteIcon').hide();
    //}
    
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
    var _iterator = (activityTypeId - 1) * _strengthArray.length;
    objectname += '<tr><td class="text-left text-bold bg-light" colspan="10">' + tableTitle + '</td>';



    objectname += '<tr  id="ExicipientRow' + _iterator + '" class="exicipientactivity"><td></td>'     
        + '<td><input type="number" class="form-control" /></td>'
        + '<td><input type="number" class="form-control" /></td>'
        + '<td><input type="number" class="form-control" /></td>';
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td><input type="hidden" id="RnDEntities[' + [(i + _iterator)] + '].ActivityTypeId" name="RnDEntities[' + [(i + _iterator)] + '].ActivityTypeId" value="' + activityTypeId + '" /><input type="hidden" id="RnDEntities[' + [(i + _iterator)] + '].StrengthId" name="RnDEntities[' + [(i + _iterator)] + '].StrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control" /></td>';
    }
    objectname += "<td><input type='number' class='form-control' readonly='readonly' /></td><td> <i class='fas fa-plus' id='addIcon' onclick='addRowRNDExicipient(" + _iterator + ");'></i> <i class='fas fa-trash-alt DeleteIcon' id='deleteIcon" + _iterator + "' onclick='deleteRowRNDExicipient(" + _iterator + ",this);' ></i></td></tr>";

    return objectname;
}
function BindRNDExicipient(data) {

    var ExicipientActivityHTML = '<thead class="bg-primary text-bold"><tr><td>Exicipient Requirement</td>'
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

}
function addRowRNDExicipient(j) {
    var table = $('#tblExicipientBody');
    var node = $('#ExicipientRow' + j).clone(true);
    table.find('#ExicipientRow' + j).after(node);
    table.find('#ExicipientRow' + j).find("input").val("");
    SetChildRowDeleteIcon(j);
}
function deleteRowRNDExicipient(j, element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon(j);
}
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
    var _iterator = (activityTypeId - 1) * _strengthArray.length;
    objectname += '<tr><td class="text-left text-bold bg-light" colspan="10">' + tableTitle + '</td>';



    objectname += '<tr  id="PackagingRow' + _iterator + '" class="packagingactivity"><td></td>'
        + '<td><select class="form-control readOnlyUpdate RNDPackagingTypeId"><option value = "0" > --Select --</option ></select></td>'
        + '<td><input type="number" class="form-control" /></td>'
        + '<td><input type="number" class="form-control" /></td>'
        + '<td><input type="number" class="form-control" /></td>';
    for (var i = 0; i < _strengthArray.length; i++) {
        objectname += '<td><input type="hidden" id="AnalyticalEntities[' + [(i + _iterator)] + '].ActivityTypeId" name="AnalyticalEntities[' + [(i + _iterator)] + '].ActivityTypeId" value="' + activityTypeId + '" /><input type="hidden" id="AnalyticalEntities[' + [(i + _iterator)] + '].StrengthId" name="AnalyticalEntities[' + [(i + _iterator)] + '].StrengthId" value="' + _strengthArray[i].pidfProductStrengthId + '" /><input type="number" class="form-control" /></td>';
    }
    objectname += "<td><input type='number' class='form-control' readonly='readonly' /></td><td> <i class='fas fa-plus' id='addIcon' onclick='addRowRNDPackaging(" + _iterator + ");'></i> <i class='fas fa-trash-alt DeleteIcon' id='deleteIcon" + _iterator + "' onclick='deleteRowRNDPackaging(" + _iterator + ",this);' ></i></td></tr>";
    return objectname;
}
function BindRNDPackaging(data) {
    var PackagingActivityHTML = '<thead class="bg-primary text-bold"><tr><td>Packaging Requirement</td>'
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

}
function addRowRNDPackaging(j) {
    var table = $('#tblPackagingBody');
    var node = $('#PackagingRow' + j).clone(true);
    table.find('#PackagingRow' + j).after(node);
    table.find('#PackagingRow' + j).find("input").val("");
    SetChildRowDeleteIcon(j);
}
function deleteRowRNDPackaging(j, element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon(j);
}
//R&D End
