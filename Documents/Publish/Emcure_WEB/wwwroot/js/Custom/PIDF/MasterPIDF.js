var objApprRejList = [];
var _mode = 0;
var _PIDFId = 0;
var isValidPIDFForm = true;
var _PIDFBusinessUnitId;
var _SelectedBusinessUnitPIDF;
var _IsInrestedRadioClick = false;
var mode_IsCountryAdd = true;
$(document).ready(function () {
    try {
        _PIDFId = parseInt($('#hdnPIDFId').val());
        _mode = getParameterByName("IsView");
        mode_IsCountryAdd = (getParameterByName("IsCountryAdd")=='1');
        //if (getParameterByName("bui") == null)
        //    $("#TabBusinessUnitId").val(parseInt($('#hdnBusinessUnitId').val()));
        //else
        //   $("#TabBusinessUnitId").val(parseInt(getParameterByName("bui")));
    }
    catch (e) {
        _mode = getParameterByName("IsView");
        _PIDFId = parseInt(getParameterByName("PIDFId"));
    }
    try {
        if (parseInt(getParameterByName("bui")) > 0) {
            _SelectedBusinessUnitPIDF = parseInt(getParameterByName("bui"));
        }
    } catch (e) {

    }

    if (_mode == 1) {
        readOnlyForm();
    }
    if ($('#hdnFlagForm').val() == "0") {
        readOnlyForm();
        removeDisableAttribute();
    }
    if (_PIDFId == 0) {
        $('#InHouses').prop("checked", true).val(true);
    }

    GetPIDFDropdown();

    SetChildRowDeleteIcon();

    var uri = document.getElementById("PIDFID").value;//document.getElementById("PIDFID").value;
    var status = $('#dvPIDFContainer').find("#StatusId").val();
    if (uri > 0 & status != 1 & status != 2 & !mode_IsCountryAdd) {
        readOnlyForm();
    }
    SetForIsExtendCountry(mode_IsCountryAdd);

    $('#BusinessUnitId').change(function (e) {

        var CurrentStatus = ($('#frmPIDF').find("#StatusId").val() == undefined) ? 0 : $('#frmPIDF').find("#StatusId").val();

       if ($(this).val() != "") {
           if (parseInt($(this).val()) > 0) {
               var _businessUnitId = $(this).val();
               if (_PIDFId > 0) {
                   if (_SelectedBusinessUnitPIDF > 0 && parseInt(CurrentStatus)>1) {
                       _businessUnitId = _SelectedBusinessUnitPIDF
                   }
               }
               ajaxServiceMethod($('#hdnBaseURL').val() + getCountryByBusinessUnitId + "/" + _businessUnitId, 'GET', GetCountryByBusinessUnitSuccess, GetCountryByBusinessUnitError);
           }
       }
   });

    $('#InhouseDropdownId').change(function (e) {
        var _selected = ($(this).val() == "1" ? true : false);
        //$('#InHouses').prop("checked", _selected).val(_selected);
        $('.BindIDForInHouses').val(_selected);
    });

    TradeNameRequired_change();

    try {
        if ((_PIDFId > 0 && ($('#frmPIDF').find("#StatusId").val() == "2"))) {
            _PIDFBusinessUnitId = $('#hdnBusinessUnitId').val();
            fnGetActiveBusinessUnit();
        }
    } catch (e) {
        var ex = e;
    }

    InitializeCountryStrength();
    SetPIDFFormDisableForOtherUserBU();
    SetDisableforNonInitiatorBU();
});
function SetDisableforNonInitiatorBU() {
    if (_SelectedBusinessUnitPIDF != _PIDFBusinessUnitId) {
        $('.readonlyOtherBusinessUnit').attr('readonly', true).attr('disabled', true);
    }
}
function SetForIsExtendCountry(mode_IsCountryAdd) {
    if (mode_IsCountryAdd) {
        $('#IsExtendCountry').val(true);
        var stringURL = '';
        if (getParameterByName("commercial") != null)
            stringURL += '&commercial=' + getParameterByName("commercial");

        if (getParameterByName("pbf") != null)
            stringURL += '&pbf=' + getParameterByName("pbf");


        $('#CommercialReturnURL').val(stringURL);
        readOnlyForm();
        $('#productStrengthBody').find('select').removeAttr('disabled');
        $('#productStrengthBody').find('select').removeAttr('readonly');
        $('#productStrengthBody').find('input').removeAttr('disabled');
        $('#productStrengthBody').find('input').removeAttr('readonly');
        $('#productStrengthBody').find('input').removeAttr('readonly');
        $('#productStrengthBody').find('.operationButton').removeAttr('style');
        $('#btnSubmitPIDF').show();


        if ($('#ProductStrengthTable tbody tr').length > 1) {
            $('.strengthDeleteIcon').show();
        } else {
            $('.strengthDeleteIcon').hide();
        }
    }
    else {
        $('#IsExtendCountry').val(false);
    }

}
function SetPIDFFormDisableForOtherUserBU() {
    var _selectBusinessUnit = $('#dvPIDFContainer').find('#hdnSelectedBusinessUnitId').val();
    if (_selectBusinessUnit != "0" && _selectBusinessUnit != "") {
        var UserwiseBusinessUnit = $('#BusinessUnitsByUser_PIDF').val().split(',');
        var BUval = _selectBusinessUnit.toString();
        var status = UserwiseBusinessUnit.indexOf(BUval);
        // var IsViewInMode = ($("#IsView").val() == '1')
        if (status == -1) {
            readOnlyForm(true);            
            $('.pidfInterestedRadio').attr('readonly', true).attr('disabled', true);
        }
        else {
        }
    }
}

function InitializeCountryStrength() {
    try {
        $('#ProductStrengthTable').find(".StrengthCountry").select2({ dropdownAdapter: $.fn.select2.amd.require('select2/selectAllAdapter'), placeholder: "Select Country", });
    }
    catch (e) {
        $('#ProductStrengthTable').find(".StrengthCountry").select2();
    }
}
//function UpdateProductStrengthCountry() {
//    var buid = $("#TabBusinessUnitId").val();
//    if (buid != "") {
//        if (parseInt(buid) > 0) {
//            ajaxServiceMethod($('#hdnBaseURL').val() + getCountryByBusinessUnitId + "/" + buid, 'GET', GetProductStrengthCountryByBusinessUnitSuccess, GetProductStrengthCountryByBusinessUnitError);
//        }
//    }
//}
//function GetProductStrengthCountryByBusinessUnitSuccess(data) {
//    try {
//            $('.clsproductStrengthCountryId').find('option').remove()
//            if (data._object != null && data._object.length > 0) {
//                $(data._object).each(function (index, item) {
//                    $('.clsproductStrengthCountryId').append($('<option>').text(item.countryName).attr('value', item.countryId));
//                });

//                $('.clsproductStrengthCountryId').select2({ dropdownAdapter: $.fn.select2.amd.require('select2/selectAllAdapter') });
//                if (_PIDFId > 0) {
//                   // $('.clsproductStrengthCountryId').prev()
//                   // $("#CountryId").val($("#hdnCountryId").val().split(',')).trigger('change');
//                }
//            }
//    }
//    catch (e) {
//        toastr.error(ErrorMessage);
//    }
//}
//function GetProductStrengthCountryByBusinessUnitError(x, y, z) {
//    toastr.error(ErrorMessage);
//}

$("#TradeNameRequired").change(TradeNameRequired_change);

function TradeNameRequired_change () {
    var chkval = $('#TradeNameRequired').prop('checked');
    if (chkval) {
        $('#TradeNameDate').show();
    }
    else {
        $('#TradeNameDate').hide();
        $('#TradeNameDate').val(null);
    }
}

$('input[type=radio][name=PIDFIsInterested]').change(function () {
    bootbox.confirm({
        title: (this.value == '1' ? 'Interested' : 'Not Interested'),
        message: 'Are you sure, you are <b>' + (this.value == '1' ? 'Interested' : 'Not Interested') +'</b> for this request ?',
        swapButtonOrder: true,
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-danger'
            },
            cancel: {
                label: 'No',
                className: 'btn-success'
            }
        },
        callback: function (result) {
            if (result) {
                var status = $('#dvPIDFContainer').find("#StatusId").val()
                if (status == "2") {
                    _IsInrestedRadioClick = true;
                    $('#btnSubmitPIDF').click();
                } else {
                    _IsInrestedRadioClick = true;
                    $('#btnSaveDraftPIDF').click();
                }
            } else {
                $('input[type=radio][name=PIDFIsInterested]').prop('checked', false);
            }
        }
    });
});

$('#RFDPriceDiscounting').focusout(function () {
    var ControlID = $(this).attr('id');
    var cntrlvalue = parseInt($(this).val());
    if (cntrlvalue < 0 || cntrlvalue > 100) {
       
        $(this).val('');
    }
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
        $('#RFDCountryId').find('option').remove();
        $('#ProductStrengthTable').find(".StrengthCountry").find("option").remove();

        if (data._object != null && data._object.length > 0) {
            if (_PIDFId == 0) {
              //  GetProductStrengthCountryByBusinessUnitSuccess(data);
            }
            var _emptyOption = '<option value="">-- Select --</option>';
            $('#RFDCountryId').append(_emptyOption);
            $(data._object).each(function (index, item) {
                $('#RFDCountryId').append('<option value="' + item.countryId + '">' + item.countryName + '</option>');
            });

            try {
                if (_PIDFId > 0) {
                    $('#RFDCountryId').val($('#hdnRFDCountryId').val());
                }

                if ($('#RFDCountryId').val() == null || $('#RFDCountryId').val() == "") {
                    $('#RFDCountryId').val(data._object[0].countryId).trigger("change");
                }

            } catch (e) {
            }

            // fill country for product strength
            var _allCountries = [];
            $(data._object).each(function (index, item) {
                $('#ProductStrengthTable').find(".StrengthCountry").append('<option value="' + item.countryId + '">' + item.countryName + '</option>');
                _allCountries.push(item.countryId);
            });
            if (_PIDFId <= 0) {
                if (data._object != null) {
                    $('#ProductStrengthTable').find(".StrengthCountry").val(_allCountries).trigger("change");
                }
            } else {
                $.each($('#ProductStrengthTable tbody tr'), function (index, value) {
                    if ($(this).find("#hdnStrengthCountries").val() != "") {
                        $(this).find(".StrengthCountry").val($(this).find("#hdnStrengthCountries").val().split(",")).trigger("change");
                    }
                    if ($(this).find(".StrengthCountry").val() == null || $(this).find(".StrengthCountry").val() == "") {
                        $(this).find(".StrengthCountry").val(_allCountries).trigger("change");
                    }
                });
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

                    var _Inhousevalue = ($('#InHouses').val() == 'True') ? 1 : 2;
                    $('#InhouseDropdownId').val(_Inhousevalue);

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
    var newIndex = j + 1;
    var table = $('#productStrengthBody');
    var node = $('#productStrengthRow_0').clone();
    table.find('tr:last').after(node);
    table.find('tr:last').find("input").val("");

    table.find("tr:last").find(".select2-container").remove();

    try {
        table.find('tr:last').find(".StrengthCountry").select2({ dropdownAdapter: $.fn.select2.amd.require('select2/selectAllAdapter'), placeholder: "Select Country", closeOnSelect:true });
    }
    catch (e) {
        table.find('tr:last').find(".StrengthCountry").select2({ closeOnSelect: true });
    }
    //table.find('tr:last .clsproductStrengthCountryId').attr('id', 'pidfProductStregthEntities_'+newIndex+'__CountryId')
    //table.find('tr:last .clsproductStrengthCountryId').attr('name', 'pidfProductStregthEntities['+newIndex+'].CountryId')

    SetChildRowDeleteIcon();
}
function deleteRowProductStrength(j, element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
}
function addRowIMSData(j) {
    var table = $('#IMSDataTableBody');
    var node = $('#ImsDataRow_0').clone(true);
    table.find('tr:last').after(node);
    table.find('tr:last').find("input").val("");
    SetChildRowDeleteIcon();
}
function deleteRowIMSData(j, element) {
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
    $('.pidfInterestedRadio').removeAttr("readonly").removeAttr("disabled");
}

function EnabledForm_PIDF() {
    $('#dvPIDFContainer').find('input').removeAttr("readonly").removeAttr("disabled");
    //$('button').attr('readonly', true).attr('disabled', true);
    $('#dvPIDFContainer').find('select').removeAttr("readonly").removeAttr("disabled");
}

function removeDisableAttribute() {
    $('#dvPIDFContainer').find('input').removeAttr('disabled');
    $('#dvPIDFContainer').find('select').removeAttr('disabled');

    $('#dvPIDFContainer').find('#BusinessUnitId').attr('disabled', true);
}

function SaveClick() {
    var isValidPIDFForm = true; 
    if (!_IsInrestedRadioClick) {
        isValidPIDFForm = validateDynamicControldDetails();
    }
    if (!isValidPIDFForm) {
        $('#loading-wrapper').hide();
    }
    if (isValidPIDFForm) {
        $('#loading-wrapper').show();
        $('#SaveType').val('submit');
        SetChildRows();        
    }
    if (mode_IsCountryAdd || (_SelectedBusinessUnitPIDF != _PIDFBusinessUnitId)) {
        EnabledForm_PIDF();
    }

    return isValidPIDFForm; 
}


function SaveDraftClick() {
    isValidPIDFForm = true;
    mandateDynamicControl();
    $('#loading-wrapper').show();
    $('#SaveType').val('draft');
    $("#frmPIDF").validate().settings.ignore = "*";
    SetChildRows();
    return isValidPIDFForm;
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
        $(this).find("td:eq(2) select").attr("name", "pidfProductStregthEntities[" + index.toString() + "].CountryId");

        $(this).find("td:eq(2) .clsPidfproductStrengthId").attr("name", "pidfProductStregthEntities[" + index.toString() + "].PidfproductStrengthId");
        
    });
    $.each($('#IMSDataTable tbody tr'), function (index, value) {
        $(this).find("td:first input").attr("name", "IMSDataEntities[" + index.toString() + "].Imsvalue");
        $(this).find("td:eq(1) input").attr("name", "IMSDataEntities[" + index.toString() + "].Imsvolume");
    });

    if ($('#dvPIDFContainer').find('#hdnSelectedBusinessUnitId').val() == "0") {
        $('#dvPIDFContainer').find('#hdnSelectedBusinessUnitId').val($('#dvPIDFContainer').find('#BusinessUnitId').val());
    }
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
    if ($('#IMSDataTable tbody tr').length > 1 && _mode != 1) {
        $('.imsDeleteIcon').show();
    } else {
        $('.imsDeleteIcon').hide();
    }
}


//function validateDynamicControldDetails() {
//    isValidPIDFForm = true;
//    $('select[name$="UnitofMeasurementId"]').each(function () {
//        validatecontrols(this);
//    });
//    $('input[name$="Strength"]').each(function () {
//        validatecontrols(this);
//    });
//    $('input[name$="Imsvalue"]').each(function () {
//        validatecontrols(this);
//    });
//    $('input[name$="Imsvolume"]').each(function () {
//        validatecontrols(this);
//    });
//    $('select[name$="ApisourcingId"]').each(function () {
//        validatecontrols(this);
//    });
//    $('input[name$="Apivendor"]').each(function () {
//        validatecontrols(this);
//    });
//    $('input[name$="Apiname"]').each(function () {
//        validatecontrols(this);
//    });
//    $('.customvalidateformcontrol').each(function () {
//        validatecontrols(this);
//    });
    
//}

function validateDynamicControldDetails() {
    var isValidPIDFForm = true; 
    $('select[name$="UnitofMeasurementId"], input[name$="Strength"], input[name$="Imsvalue"], input[name$="Imsvolume"], select[name$="ApisourcingId"], input[name$="Apivendor"], input[name$="Apiname"], .customvalidateformcontrol').each(function () {
        if (!validatecontrols(this)) {
            isValidPIDFForm = false; 
        }
    });

    return isValidPIDFForm;
}
function validatecontrols(control) {
    try {
        if ($(control).val() == null || $(control).val().trim() === '') {
            $(control).css("border-color", "red");
            $(control).focus();
            return false;
        } else {
            $(control).css("border-color", "");
            return true;
        }
    } catch (e) {
        return false;
    }
}
function mandateDynamicControl() {
    isValidPIDFForm = true;    
    $('.mandatoryformcontrol').each(function () {
        validatecontrols(this);
    });
}


function fnGetActiveBusinessUnit() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetActiveBusinessUnit, 'GET', GetActiveBusinessUnitSuccess, GetActiveBusinessUnitError);
}
function GetActiveBusinessUnitSuccess(data) {
    var businessUnitHTML = "";
    var businessUnitPanel = "";
    var _UserAccessBusinessUnit = $('#hdnUserBusinessUnits').val();
    if (parseInt(getParameterByName("bui")) > 0) {
        _SelectedBusinessUnitPIDF = parseInt(getParameterByName("bui"));
    } else {
        if (_UserAccessBusinessUnit != null && _UserAccessBusinessUnit != undefined && _UserAccessBusinessUnit != "") {
            var _accessBusinessUnitArray = _UserAccessBusinessUnit.split(',');
            if (_accessBusinessUnitArray.indexOf(_PIDFBusinessUnitId) != -1) {
                _SelectedBusinessUnitPIDF = _PIDFBusinessUnitId;
            } else {
                _SelectedBusinessUnitPIDF = _accessBusinessUnitArray[0];
            }
        }
    }
    $.each(data._object.result, function (index, item) {
        businessUnitHTML += '<li class="nav-item p-0">\
            <a class="nav-link '+ (item.businessUnitId == _SelectedBusinessUnitPIDF ? "active" : "") + ' px-2" onclick="PIDFBUtabClick(' + _PIDFId + ', ' + item.businessUnitId + ')" data-toggle="pill" aria-selected="true" id="pidf-custom-tabs-two-' + item.businessUnitId + '-tab">' + item.businessUnitName + '</a></li>';
/*        businessUnitPanel += '<div class="tab-pane ' + ((item.businessUnitId == _SelectedBusinessUnitPIDF ? "fade show active" : "")) + '" id="pidf-custom-tabs-' + item.businessUnitId + '" role="tabpanel" aria-labelledby="pidf-custom-tabs-two-' + item.businessUnitId + '-tab"></div>';*/
    });
    $('#pidf-custom-tabs-two-tab').html(businessUnitHTML);
    if (_SelectedBusinessUnitPIDF != _PIDFBusinessUnitId) {
        $("#frmPIDF").find('.readonlyOtherBusinessUnit').attr("readonly", "true");
        $("#frmPIDF").find('#BusinessUnitId').attr("disabled", true);
    }
/*    $('#pidf-custom-tabs-two-tabContent').html(businessUnitPanel);*/
}
function GetActiveBusinessUnitError(x, y, z) {
    toastr.error("Some error occurred while loading business unit");
}

function PIDFBUtabClick(pidfId, buId) {
    if (_mode > 0) {
        window.location.href = 'PIDF?pidfid=' + pidfId + '&bui=' + buId + '&IsView=' + _mode;
    }
    else {
        window.location.href = 'PIDF?pidfid=' + pidfId + '&bui=' + buId;
    }
}

