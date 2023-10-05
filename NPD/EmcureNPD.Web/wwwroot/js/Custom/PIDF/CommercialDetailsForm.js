var ArrMainCommercial = [];
var objYears = [];
var objMainForm = {};
var objdropdownYearControls_array = ['PackagingTypeId', 'CurrencyId', 'FinalSelectionId'];
var NonMandetoryControls = ['FreeOfCost', 'Apireq'];
var ColumnObjUpcase = ['packagingTypeId', 'commercialBatchSize', 'priceDiscounting', 'totalApireq', 'apireq', 'suimsvolume', 'marketGrowth', 'marketSize', 'priceErosion', 'finalSelectionId'];
var SelectedBUValue = 0;
var selectedStrength = 0;
var selectedCountry = 0
var EditIndex = -1;
var MainRowEditIndex = -1;
var PIDFCommercialMaster;
var IsPageLoad = true;
var IsTabClick = false;
var IsCommTabClick = false;
var MainArrCountryList = [];
var mainStrengthData = [];
var IsNoStrengthforSelectedBU = false;
$(document).ready(function () {
    $('#mainDivCommercial').find('label[id^="valmsg"]').hide();
    IsViewModeCommercial();
    SetBU_Strength();
    GetPBFOutsourcingTabDDLdata();
    if ($('#hdnIsPartial').val() != '1') {
        //  getPIDFAccordion(_PIDFAccordionURL, _PIDFID, "dvPIDFAccrdion");
        //  getIPDAccordion(_IPDAccordionURL, _EncPIDFID, _PIDFBusinessUnitId, "dvIPDAccrdion");
    }
    //if(true)
    //$('.PBFDetailsTab').hide();
  

});

function RedirectToPIDF() {
    var arrurl = $(location).attr('href').split('?');
    if (arrurl.length == 2)
        location.href = '/PIDF/PIDF?pidfid=' + _PIDFID + '&bui=' + SelectedBUValue + '&IsCountryAdd=1&ReturnURL=' +arrurl[1];
}

function SetBU_Strength() {
    var pidfId = $("#PIDFId").val();
    GetCommercialPIDFByBU(pidfId);
    IsShowCancel_Save_buttons(true);
}
function IsViewModeCommercial() {
    if ($("#IsView").val() == '1') {
        SetCommercialFormReadonly();
        SetPBFCommercialFormReadonly();
    }
}
function SetCommercialDivReadonly() {
    $("#PIDFScreen").find("input, submit, textarea, a, select").attr("disabled", "disabled");
    $("#PIDFScreen").find("button, submit, a").hide();
    $("#PIDFScreen").find("#collapseButton").show();
    $("#PIDFScreen").find("#PIDFormcollapseButton").show();

    $("#PIDFormcollapseButton").click();
    $("#collapseButton").click();
}

function ValidateYearForm() {
    var ArrofInvalid = []
    $.each($('#AddYearForm').serializeArray(), function (_, kv) {
        if (NonMandetoryControls.indexOf(kv.name) == -1) {

            if (jQuery.inArray(kv.name, objdropdownYearControls_array) != -1) {
                if (kv.value == 0) {
                    /*                $('#valmsg' + kv.name).text('Required');*/
                    $('#' + kv.name).addClass('InvalidBox');
                    //IsValid = false;
                    ArrofInvalid.push(kv.name);
                }
                else {
                    /*                $('#valmsg' + kv.name).text('');*/
                    $('#' + kv.name).removeClass('InvalidBox');
                }
            }
            else {
                if (kv.value == '') {
                    ///*$('#valmsg' + kv.name).text('Required');*/
                    //$('#' + kv.name).addClass('InvalidBox');
                    ////IsValid = false;
                    //ArrofInvalid.push(kv.name);
                    if (!(kv.name == "TotalApireq")) {
                        $('#' + kv.name).addClass('InvalidBox');
                        ArrofInvalid.push(kv.name);
                    }

                }
                else {
                    $('#valmsg' + kv.name).text('').hide();
                    $('#' + kv.name).removeClass('InvalidBox');
                }
            }
        }
    });
    var status = (ArrofInvalid.length == 0) ? true : false;
    if (!status) {
        var controltobeFocus = ArrofInvalid[ArrofInvalid.length - 1];
        $('#' + controltobeFocus).focus();
        toastr.error('Some fields are missing !');
    }
    return status;
}
function ClearValidationForYearForm() {
    $.each($('#AddYearForm').serializeArray(), function (_, kv) {
        $('#valmsg' + kv.name).text('').hide();
    });
}
function ClearValidationForMainForm() {
    var MainFormFeilds = ['MarketSizeInUnit', 'ShelfLife', 'PackSizeId', 'MarketSizeInUnit']
    $.each(MainFormFeilds, function (_, kv) {
        $('#valmsg' + kv).text('').hide();
    });
}
function ValidateMainForm() {
    var ArrofInvalid = []
    var MainFormFeilds = ['MarketSizeInUnit', 'ShelfLife', 'PackSizeId']
    $.each(MainFormFeilds, function (_, kv) {
        if ($('#' + kv).val() == '') {
            $('#valmsg' + kv).text('Required').show();
            ArrofInvalid.push(kv);
        }
        else {
            $('#valmsg' + kv).text('').hide();
        }
    });

    var status = (ArrofInvalid.length == 0) ? true : false;
    if (!status) { toastr.error('Some fields are missing !'); }

    if (ArrMainCommercial != null && ArrMainCommercial != undefined && ArrMainCommercial.length > 0 && $('#hdnPackSizeMode').val() != "1") {
        var _PackSize = $.grep(ArrMainCommercial, function (n, i) {
            return n.packSizeId == $('#PackSizeId option:selected').val() && n.businessUnitId == SelectedBUValue && n.pidfProductStrengthId == selectedStrength && n.countryId == selectedCountry;
        });
        if (_PackSize != null && _PackSize != undefined && _PackSize.length > 0) {
            toastr.error('Pack size is already selected !');
            status = false;
        }
    }
    return status;
}
function ValidateIsInterested() {
    var status = true;
    let IsInterestedYes = $('.IsInterestedYes').prop('checked');
    let IsInterestedNo = $('.IsInterestedNo').prop('checked');
    if (!IsInterestedYes && !IsInterestedNo) {
        $('#valmsgInterested').text('Required').show();
        $('.IsInterestedYes').focus();
        status = false;
        toastr.error('Please Select Is Interested ! ');
    }
    else {
        $('#valmsgInterested').text('').show();
        status = true;
    }
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
function ValidateYearDataExist() {
    var status = true;
    $.each(ArrMainCommercial, function (index, item) {
        if (item.PidfCommercialYears.length <= 0) {
            toastr.error('Please fill year detail for Business Unit: ' + $('#BUtab_' + item.businessUnitId).text() + ' & Strength: ' + $('#Strengthtab_' + item.pidfProductStrengthId).text());
            status = false;
        }
    });
    return status;
}
function ResetYearFormValues() {
    $.each($('#AddYearForm').serializeArray(), function (_, kv) {
        if (jQuery.inArray(kv.name, objdropdownYearControls_array) != -1) {
            $('#' + kv.name).val(0);
        }
        else {
            $('#' + kv.name).val('');
        }
    });
}
function CommercialSubmitClick(saveType) {
    let InterestedStatus = true; // ValidateIsInterested();
    if (InterestedStatus && ValidateBU_Strength()) {
        if (ArrMainCommercial.length > 0) {
            if (ValidateYearDataExist()) {
                $.extend(objMainForm, { 'SaveType': saveType });
                SaveCommertialPIDFForm();
            }
        } else {
            toastr.error('No Data Added');
        }
    }
}
function CommercialSaveAsDraftClickClick(saveType) {

    if (ValidateBU_Strength()) {
        if (ArrMainCommercial.length > 0) {
            $.extend(objMainForm, { 'SaveType': saveType });
            SaveCommertialPIDFForm();
        }
        else {
            if (saveType !='TabClick')
            toastr.error('No Data Added');
        }
    }
}

$(document).on("change", "#mainDivCommercial .InvalidBox", function () {
    if ($(this).val() != '' && $(this).val() != 0) {
        $(this).removeClass("InvalidBox");
    }
});
$('#btnCancelYearForm').click(function () {
    IsShowCancel_Save_buttons(true);
    ResetYearFormValues();
});
$('#PriceDiscounting').focusout(function () {
    var ControlID = $(this).attr('id');
    var cntrlvalue = parseInt($(this).val());
    if (cntrlvalue < 0 || cntrlvalue > 100) {
        $('#valmsg' + ControlID).text('Select value within 0-100').show();
        $(this).val('');
    } else {
        $('#valmsg' + ControlID).text('').hide();
    }
});


function SaveCommertialPIDFForm() {
    $.extend(objMainForm, { 'encCreatedBy': $("#LoggedInUserId").val() });
    $.extend(objMainForm, { 'Pidfid': parseInt($("#PIDFId").val()) });
    /* -------------------------------*/
    // $.extend(objMainForm, { 'Interested': $("#Interested").prop('checked') }); 
    $.extend(objMainForm, { 'Remark': $("#Remark").val() });
    $.extend(objMainForm, { 'MainBusinessUnitId': parseInt(SelectedBUValue) });
    $.extend(objMainForm, { 'MainCountryId': selectedCountry });
    /* ---------------------------------*/
    $.extend(objMainForm, { 'PIDFArrMainCommercial': ArrMainCommercial });
    ajaxServiceMethod($('#hdnBaseURL').val() + SaveCommercialPIDF, 'POST', SaveCommertialPIDFFormSuccess, SaveCommertialPIDFFormError, JSON.stringify(objMainForm));
}
function SaveCommertialPIDFFormSuccess(data) {
    try {
        $('#SavePIDFModel').modal('hide');
        if (data._Success === true) {
            IsShowCancel_Save_buttons(true);
            if (!IsTabClick) {
                toastr.success(data._Message);
                window.location.href = "/PIDF/PIDFList?ScreenId=4";
            }
        }
        else {
            console.log(data._Message);
        }
    } catch (e) {
        console.log('Save Commercial Error:' + e.message);
    }
}
function SaveCommertialPIDFFormError(x, y, z) {
    console.log(ErrorMessage);
}
function BUtabClick(BUVal, pidfidval) {
    $('[id^="BUtab_"]').removeClass('active');
    $('#BUtab_' + BUVal).addClass('active');
    SelectedBUValue = BUVal;
    BussinesUnitInterestedCommercial(pidfidval, SelectedBUValue, 'Commercial');
    ClearValidationForYearForm();
    ClearValidationForMainForm();
    renderCountryTabList(BUVal);
   
    Update_BUstregthPackTable(ArrMainCommercial);
    // Update_IsInterested_Remark();
    SetCommercialDisableForOtherUserBU();
    IsShowCancel_Save_buttons(true);
}
function Update_IsInterested_Remark() {
    if (PIDFCommercialMaster != undefined) {
        var object_CommercialMaster = $.grep(PIDFCommercialMaster, function (n, i) {
            return n.businessUnitId == SelectedBUValue
        });
        if (object_CommercialMaster.length > 0) {
            $('#Remark').val(object_CommercialMaster[0].remark);
            if (object_CommercialMaster[0].interested) {
                $('.IsInterestedYes').prop('checked', true);
                $('.IsInterestedNo').prop('checked', false);
            } else {
                $('.IsInterestedYes').prop('checked', false);
                $('.IsInterestedNo').prop('checked', true);
            }
        }
        else {
            $("#Remark").val('');
            $('.IsInterestedYes').prop('checked', false);
            $('.IsInterestedNo').prop('checked', false);
        }
    }
}
function CountrytabClick(countryVal, pidfidval) {
    $('[id^="Countrytab_"]').removeClass('active');
    $('#Countrytab_' + countryVal).addClass('active');
    selectedCountry = countryVal;

    renderPIDFStrength(MainArrCountryList);
    SetCommercialDisableForOtherUserBU();
}
function StrengthtabClick(strengthVal, pidfidval) {
    $('[id^="Strengthtab_"]').removeClass('active');
    $('#Strengthtab_' + strengthVal).addClass('active');
    selectedStrength = strengthVal;
    ClearValidationForYearForm();
    ClearValidationForMainForm();
    Update_BUstregthPackTable(ArrMainCommercial);
    SetCommercialDisableForOtherUserBU();
    IsShowCancel_Save_buttons(true);
}
function GetCommercialPIDFByBU(pidfidval) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetCommercialFormData + "/" + pidfidval + "/" + SelectedBUValue + "/" + selectedStrength, 'GET', GetCommercialPIDFByBUSuccess, GetCommercialPIDFByBUError);
    ClearValidationForYearForm();
}
function GetCommercialPIDFByBUSuccess(data) {
    try {

        GetCurrencyList(data._object.Currency);
        GetProductTypeList(data._object.PackagingType);
        GetPackSize(data._object.PackSize);
        GetFSList(data._object.FinalSelection);

        renderBusinessUnit(data._object.BusinessUnit);
        MainArrCountryList = data._object.CountryList;
        renderCountryTabList(SelectedBUValue);
        renderPIDFStrength(MainArrCountryList);
        mainStrengthData = data._object.PIDFStrength;
        PIDFCommercialMaster = data._object.PIDFCommercialMaster;
        setCommercialArray(data._object.Commercial, data._object.CommercialYear);
        Update_BUstregthPackTable(ArrMainCommercial);
        UpdatePBFOutSourceData(data._object.PBFOutSourceData);
        // Update_IsInterested_Remark();
        SetCommercialDisableForOtherUserBU();
        if (data._object.PIDF[0].inHouses)
            $('.PBFDetailsTab').hide();
        else
            $('.PBFDetailsTab').show();

    } catch (e) {
        console.log('Get Commercial Error:' + e.message);
    }
}
function GetCommercialPIDFByBUError(x, y, z) {
    console.log(ErrorMessage);
}
function ResetMainFormForm() {
    $("#MarketSizeInUnit").val('');
    $("#ShelfLife").val('');
    //$('.IsInterestedYes').prop('checked', false);
    //$('.IsInterestedNo').prop('checked', false);
    $("#Remark").val("");
}
function SetCommercialFormReadonly() {
    $("#mainDivCommercial").find("input, button, submit, textarea, select").prop('disabled', true);
    /*$("[id^='deleteIconAddyear']").hide();*/
    $("#btnCommercialCancel").prop('disabled', false);
    $("#AddyeartableCollapseButton").prop('disabled', false);
    $("#btnAddyeartableCollapseButton").prop('disabled', false);
    $('#mainDivCommercial').find('.operationButton').hide();
}
function SetCommercialDisableForOtherUserBU() {
    var UserwiseBusinessUnit = $('#BusinessUnitsByUser').val().split(',');
    var status = UserwiseBusinessUnit.indexOf(SelectedBUValue.toString());
    var IsViewInMode = ($("#IsView").val() == '1')
    if (status == -1 || IsViewInMode || IsNoStrengthforSelectedBU) {
        SetCommercialFormReadonly();
    }
    else {
        $("#mainDivCommercial").find("input, button, submit, textarea, select").prop('disabled', false);
        /*$("[id^='deleteIconAddyear']").show();*/
        $('#mainDivCommercial').find('.operationButton').show();
    }
}

function UpdateYearOnMarketSizeUnitEdited() {
    var result = 0;
    /* ----------------Update - marketSize--------------------------*/
    var _foundObject = (ArrMainCommercial[MainRowEditIndex] == null || ArrMainCommercial[MainRowEditIndex] == undefined);
    var MarketSizeAsLaunch = parseFloat(_foundObject ? 0 : ArrMainCommercial[MainRowEditIndex].marketSizeInUnit);
    var MarketGrowth = parseFloat(ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[EditIndex]['marketGrowth']);
    var currentIndex = (EditIndex == -1) ? (_foundObject ? 0 : ArrMainCommercial[MainRowEditIndex].PidfCommercialYears.length) : EditIndex;
    if (currentIndex == 0) {
        result = MarketSizeAsLaunch * (1 + (MarketGrowth / 100))
    }
    if (currentIndex > 0) {
        MarketSizeAsLaunch = ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentIndex - 1]['marketSize']
        result = MarketSizeAsLaunch * (1 + (MarketGrowth / 100))
    }
    ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentIndex]['marketSize'] = result.toFixed();
    /* ---------------End-------------------------*/

    EstimatedMarketShareUnits_OnMarketSizeUnitEdited('Low');
    EstimatedMarketShareUnits_OnMarketSizeUnitEdited('Medium');
    EstimatedMarketShareUnits_OnMarketSizeUnitEdited('High');

    NSP_OnMarketSizeUnitEdited('Low');
    NSP_OnMarketSizeUnitEdited('Medium');
    NSP_OnMarketSizeUnitEdited('High');

}
function EstimatedMarketShareUnits_OnMarketSizeUnitEdited(variable) {
    var MarketSize = ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[EditIndex]['marketSize']
    var MarketShare = ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[EditIndex]['marketSharePercentage' + variable];
    var result = MarketSize * MarketShare / 100;
    ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[EditIndex]['marketShareUnit' + variable] = result.toFixed();
    // $('#MarketShareUnit' + variable).val(result.toFixed());
}
function NSP_OnMarketSizeUnitEdited(variable) {
    var result = 0;
    var Nspunits;
    var PriceErosion = ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[EditIndex]['priceErosion'];
    var _foundObject = (ArrMainCommercial[MainRowEditIndex] == null || ArrMainCommercial[MainRowEditIndex] == undefined);

    var currentIndex = (EditIndex == -1) ? (_foundObject ? 0 : ArrMainCommercial[MainRowEditIndex].PidfCommercialYears.length) : EditIndex;
    if (currentIndex == 0) {
        Nspunits = ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentIndex]['nspunits' + variable];
    }
    if (currentIndex > 0) {
        Nspunits = ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentIndex - 1]['nsp' + variable];
    }
    result = Nspunits * (1 + (PriceErosion / 100))
    ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentIndex]['nsp' + variable] = result.toFixed(5);
    // $('#Nsp' + variable).val(result.toFixed(5));
}

// ----------Calulations Methods----All formulam taken from--"Revenue -Dummy Calculations (1).xlsx"----------
//----------- Market Size-----------------------
$('input[type="number"]').focusout(function () {
    var result = 0;

    var _foundObject = (ArrMainCommercial[MainRowEditIndex] == null || ArrMainCommercial[MainRowEditIndex] == undefined);

    var MarketSizeAsLaunch = parseFloat(_foundObject ? 0 : ArrMainCommercial[MainRowEditIndex].marketSizeInUnit);
    var MarketGrowth = parseFloat($('#MarketGrowth').val());
    var currentIndex = (EditIndex == -1) ? (_foundObject ? 0 : ArrMainCommercial[MainRowEditIndex].PidfCommercialYears.length) : EditIndex;
    if (currentIndex == 0) {
        result = MarketSizeAsLaunch * (1 + (MarketGrowth / 100))
    }
    if (currentIndex > 0) {
        MarketSizeAsLaunch = ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentIndex - 1]['marketSize']
        result = MarketSizeAsLaunch * (1 + (MarketGrowth / 100))
    }
    $('#MarketSize').val(result.toFixed());
});
//--------- Estimated Market Share Units--------------------
$('input[type="number"]').focusout(function () {
    EstimatedMarketShareUnits('Low');
});
$('input[type="number"]').focusout(function () {
    EstimatedMarketShareUnits('Medium');
});
$('input[type="number"]').focusout(function () {
    EstimatedMarketShareUnits('High');
});
function EstimatedMarketShareUnits(variable) {
    var MarketSize = $('#MarketSize').val();
    var MarketShare = $('#MarketSharePercentage' + variable).val();
    var result = MarketSize * MarketShare / 100;
    $('#MarketShareUnit' + variable).val(result.toFixed());
}
//---------NSP------------------------------------
$('input[type="number"]').focusout(function () {
    NSP('Low');
    CalculateAPIRequirment();
});
function CalculateAPIRequirment() {
    var strength_val = 0;
    var marketShareUnitMedium_val = ($('#MarketShareUnitMedium').val() == '') ? 0 : $('#MarketShareUnitMedium').val();
    var packSize_val = 0;
    var _strenthid = 0;
    if (!(ArrMainCommercial[MainRowEditIndex] == null || ArrMainCommercial[MainRowEditIndex] == undefined)) {
         packSize_val = ArrMainCommercial[MainRowEditIndex].packSize;
        _strenthid = ArrMainCommercial[MainRowEditIndex].pidfProductStrengthId;

        var CurrentSelectedStrengthValList = $.grep(MainArrCountryList, function (n, i) {
            return n.pidfProductStrengthId == _strenthid;
        });

        if (CurrentSelectedStrengthValList != null || CurrentSelectedStrengthValList.length > 0)
            strength_val = parseInt(CurrentSelectedStrengthValList[0].strength);
    }
    
    var result = marketShareUnitMedium_val * packSize_val * strength_val/1000000;

    result = isNaN(result) ? 0 : result;
    $('#Apireq').val(result.toFixed(6));
}
$('input[type="number"]').focusout(function () {
    NSP('Medium');
});
$('input[type="number"]').focusout(function () {
    NSP('High');
});
function NSP(variable) {
    var result = 0;
    var Nspunits;
    var PriceErosion = $('#PriceErosion').val();
    var _foundObject = (ArrMainCommercial[MainRowEditIndex] == null || ArrMainCommercial[MainRowEditIndex] == undefined);

    var currentIndex = (EditIndex == -1) ? (_foundObject ? 0 : ArrMainCommercial[MainRowEditIndex].PidfCommercialYears.length) : EditIndex;
    //if (currentIndex == 0) {
    //    Nspunits = $('#Nspunits' + variable).val();
    //}
    //if (currentIndex > 0) {
    //    Nspunits = ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentIndex - 1]['nsp' + variable];
    //}

    Nspunits = $('#Nspunits' + variable).val();
    result = Nspunits * (1 + (PriceErosion / 100))
    $('#Nsp' + variable).val(result.toFixed(5));
}
function UpdateOtherYearData(currentEditingYearIndex) {
    var yearlength = ArrMainCommercial[MainRowEditIndex].PidfCommercialYears.length;

    if (!(yearlength - 1 == currentEditingYearIndex)) {

        var result = 0;

        var _foundObject = (ArrMainCommercial[MainRowEditIndex] == null || ArrMainCommercial[MainRowEditIndex] == undefined);

        var MarketSizeAsLaunch = parseFloat(_foundObject ? 0 : ArrMainCommercial[MainRowEditIndex].marketSizeInUnit);
        var MarketGrowth = parseFloat(ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentEditingYearIndex + 1]['marketGrowth']);

        MarketSizeAsLaunch = ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentEditingYearIndex]['marketSize']
        result = MarketSizeAsLaunch * (1 + (MarketGrowth / 100))

        ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentEditingYearIndex + 1]['marketSize'] = result.toFixed();
        //  $('#MarketSize').val(result.toFixed());
        //-----------------------------------------------
        var MarketSize = result;
        var MarketShare = parseFloat(ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentEditingYearIndex + 1]['marketSharePercentageLow']);
        result = MarketSize * MarketShare / 100;
        ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentEditingYearIndex + 1]['marketShareUnitLow'] = result.toFixed();

        MarketShare = parseFloat(ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentEditingYearIndex + 1]['marketSharePercentageMedium']);
        result = MarketSize * MarketShare / 100;
        ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentEditingYearIndex + 1]['marketShareUnitMedium'] = result.toFixed();

        MarketShare = parseFloat(ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentEditingYearIndex + 1]['marketSharePercentageHigh']);
        result = MarketSize * MarketShare / 100;
        ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentEditingYearIndex + 1]['marketShareUnitHigh'] = result.toFixed();

        ReEditingNSP('Low', currentEditingYearIndex);
        ReEditingNSP('Medium', currentEditingYearIndex);
        ReEditingNSP('High', currentEditingYearIndex);
    }
}
function ReEditingNSP(variable, currentEditingYearIndex) {
    var result = 0;
    var Nspunits;
    var PriceErosion = parseFloat(ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentEditingYearIndex + 1]['priceErosion']);
    Nspunits = ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentEditingYearIndex]['nsp' + variable];

    result = Nspunits * (1 + (PriceErosion / 100))
    ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[currentEditingYearIndex + 1]['nsp' + variable] = result.toFixed(5);

}


function AddYearClick() { //SaveYearClick
    var ArrofInvalid = [];
    //var year = $('#AddYearForm').serializeArray();
    var valBUStrength = ValidateBU_Strength();
    if (ValidateYearForm() && valBUStrength) {
        var entityYear = {};
        //var IsValid = true;;
        $.each($('#AddYearForm').serializeArray(), function (_, kv) {
            if (kv.value == '') {
                //$('#valmsg' + kv.name).text('Required').show();
                ////IsValid = false;
                //ArrofInvalid.push(kv.name);

                if (!(kv.name == "TotalApireq")) {
                $('#valmsg' + kv.name).text('Required').show();
                //IsValid = false;
                ArrofInvalid.push(kv.name);
                }
                //if (!(kv.name == "TotalApireq")) {
                //    $('#valmsg' + kv.name).text('Required').show();
                //    //IsValid = false;
                //    ArrofInvalid.push(kv.name);
                //}
            }
            entityYear[getPropertyName(kv.name)] = kv.value;
        });

        entityYear.businessUnitId = SelectedBUValue;
        entityYear.pidfId = parseInt($("#PIDFId").val());
        entityYear.countryId = selectedCountry;
        entityYear.pidfProductStrengthId = selectedStrength;
        entityYear.packSizeId = ArrMainCommercial[MainRowEditIndex].packSizeId;
        entityYear.yearIndex = (EditIndex + 1);

        if (EditIndex == -1) {
            entityYear.yearIndex = ArrMainCommercial[MainRowEditIndex].PidfCommercialYears.length + 1;
            ArrMainCommercial[MainRowEditIndex].PidfCommercialYears.push(entityYear);
        }
        else {
            ArrMainCommercial[MainRowEditIndex].PidfCommercialYears[EditIndex] = entityYear;
            var yearlength = ArrMainCommercial[MainRowEditIndex].PidfCommercialYears.length;
            for (var k = EditIndex; k < yearlength; k++) {
                UpdateOtherYearData(k);
            }

        }

        Update_BUstregthPackTable(ArrMainCommercial);
        //UpdateYearTable(ColumnObjUpcase);
        ClearValidationForYearForm();
        //$("#AddYearForm").hide();
        IsShowCancel_Save_buttons(true);
        ResetYearFormValues();
        EditIndex = -1;
        MainRowEditIndex = -1;
        $('#dvCommercialAddYear').modal('hide');
    }
}

function AddPackStyle() {
    $('#ShelfLife').val("");
    $('#MarketSizeInUnit').val("");
    $('#hdnPackSizeMode').val("0");
    $('#PackSizeId').val("").removeAttr("disabled");
    ClearValidationForMainForm();
    $('#dvCommercialPackStyle').modal('show');
}

function BUstregthPack_AddButtonClick() {
    if (ValidateMainForm()) {
        var ent_BuStrPack = {};
        $.each($('#frmMainBUstrengthPackCommercial').serializeArray(), function (index, item) {
            ent_BuStrPack[getPropertyName(item.name)] = item.value;
        });
        ent_BuStrPack["packSizeName"] = $("#PackSizeId option:selected").text();
        ent_BuStrPack["packSizeId"] = $("#PackSizeId option:selected").val();
        ent_BuStrPack["packSize"] = $("#PackSizeId option:selected").attr('data-val');
        //var IndexofMaintable = ArrMainCommercial.length;
        //ent_BuStrPack['IndexofMaintable'] = IndexofMaintable;
        ent_BuStrPack['PidfCommercialYears'] = [];
        ent_BuStrPack['businessUnitId'] = SelectedBUValue;
        ent_BuStrPack['countryId'] = selectedCountry;
        ent_BuStrPack['pidfProductStrengthId'] = selectedStrength;
        ent_BuStrPack['pidfId'] = parseInt($("#PIDFId").val());

        if ($('#hdnPackSizeMode').val() == "1") {
            //Find index of specific object using findIndex method.    
            objIndex = ArrMainCommercial.findIndex((obj => obj.packSizeId == ent_BuStrPack.packSizeId && obj.businessUnitId == SelectedBUValue && obj.pidfProductStrengthId == selectedStrength));
            ArrMainCommercial[objIndex].marketSizeInUnit = ent_BuStrPack.marketSizeInUnit;
            ArrMainCommercial[objIndex].packSizeName = ent_BuStrPack.packSizeName;
            ArrMainCommercial[objIndex].shelfLife = ent_BuStrPack.shelfLife;
            //--------Update PidfCommercialYears onject when MarketSizeUnit is Edited----------------------------------------            
            var yearlen = ArrMainCommercial[objIndex].PidfCommercialYears.length;
            if (yearlen > 0) {
                MainRowEditIndex = objIndex;
                for (var k = 0; k < yearlen; k++) {
                    EditIndex = k;
                    UpdateYearOnMarketSizeUnitEdited(k);
                }
                MainRowEditIndex = -1;
                EditIndex = -1;
            }
            //------------------------------------------------------------------------------------------------

        } else {
            ArrMainCommercial.push(ent_BuStrPack);
        }

        Update_BUstregthPackTable(ArrMainCommercial);
        $('#dvCommercialPackStyle').modal('hide');
    }
}
function Update_BUstregthPackTable(_objArrMainCommercial) {


    _objArrMainCommercial = $.grep(_objArrMainCommercial, function (n, i) {
        return n.businessUnitId == SelectedBUValue && n.pidfProductStrengthId == selectedStrength && n.countryId == selectedCountry ;
    });

    $("#tblMainCommercial #mainCommercialRows tr").remove();
    $.each(_objArrMainCommercial, function (index, object) {
        let i = object.packSizeId;
        // Add parent row into table for commercial
        let tableRow = "";
        let tablechildRow = "";

        var editbtn = '<a class="large-font editBtn operationButton" style="" title="Edit" onclick="btnEditBUStrengthPack(' + object.packSizeId + ', ' + object.businessUnitId + ', ' + object.pidfProductStrengthId + ', this)"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a>';

        var deletebtn = '<a class="large-font text-danger deleteBtn operationButton" style="" title="Delete" data-keyboard="false" onclick="btnDeleteBUStrengthPack(' + i + ')"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>';

        let addYearbtn = "";
        if (object.PidfCommercialYears != null && object.PidfCommercialYears != undefined && object.PidfCommercialYears.length < 5) {
            addYearbtn = '<a class="large-font addYearBtn operationButton" name="addYearBtn" style="" title="Add Year Details" onclick="OpenYearForm(' + i + ');return false;"><i class="fa fa-fw fa-plus mr-1"></i> ' + '</a>';
        }

        let ExpandRowBtn = '<a class="large-font" style="" onclick="ShowChildRows(\'Yearclildrows_' + object.packSizeId + '\', this);" title="Show Year Details"><i class="fa fa-minus-circle text-danger icoShowSubRow' + i + '" aria-hidden="true"></i>' + '</a>';

        tableRow = '<tr class="highlight-tr"><td>' + ExpandRowBtn + '</td><td>' + object.packSizeName + '</td><td class="marketSize">' + object.marketSizeInUnit + '</td><td class="shelfLife">' + object.shelfLife + '</td><td>' + editbtn + deletebtn + addYearbtn + '</td> </tr>';

        $('#tblMainCommercial #mainCommercialRows').append(tableRow);
        // end of Add parent row into table for commercial

        var YearInnerHtml = $('#dvYearsTable').clone();
        var RevenueInnerHtml = $('#dvtblRevenue').clone();
        tablechildRow = '<tr style="" class="clildrows_' + i + '" id="Yearclildrows_' + object.packSizeId + '"><td class="p-3" colspan="12" >' + YearInnerHtml.html() + RevenueInnerHtml.html() + '</td> </tr>';

        $('#tblMainCommercial #mainCommercialRows').append(tablechildRow);

        UpdateChildTableDetail(object);
    });
}
function ShowChildRows(id, element) {
    $('#' + id.trim()).toggle();
    if ($('#' + id.trim()).is(":visible")) {
        $(element).find("i").removeClass("fa fa-plus-circle text-success").addClass("fa fa-minus-circle text-danger");
    } else {
        $(element).find("i").removeClass("fa fa-minus-circle text-danger").addClass("fa fa-plus-circle text-success");
    }
}

function btnEditBUStrengthPack(packSizeId, businessUnitId, pidfProductStrengthId, element) {
    $('#ShelfLife').val($(element).parent().parent().find(".shelfLife").text());
    $('#MarketSizeInUnit').val($(element).parent().parent().find(".marketSize").text());
    $('#hdnPackSizeMode').val("1");
    $('#PackSizeId').val(packSizeId).attr("disabled", "disabled");
    ClearValidationForMainForm();
    $('#dvCommercialPackStyle').modal('show');
}
function btnDeleteBUStrengthPack(packSizeId) {
    bootbox.confirm({
        title: 'Delete',
        message: 'Are you sure, you want to delete ?',
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
                ArrMainCommercial = $.grep(ArrMainCommercial, function (n, i) {
                    return n.packSizeId != packSizeId || n.businessUnitId != SelectedBUValue || n.pidfProductStrengthId != selectedStrength || n.countryId != selectedCountry;
                });
                Update_BUstregthPackTable(ArrMainCommercial);
            }
        }
    });
}

function OpenYearForm(packSizeId, yearIndex) {
    if (ValidateBU_Strength()) {
        //$("#AddYearForm").show();
        $('#dvCommercialAddYear').find(".modal-header").find(".modal-title").text("Pack Size: " + $('#PackSizeId option[value=' + packSizeId + ']').text());
        $('#dvCommercialAddYear').modal('show');
        IsShowCancel_Save_buttons(false);
    }
    MainRowEditIndex = ArrMainCommercial.findIndex((obj => obj.packSizeId == packSizeId && obj.businessUnitId == SelectedBUValue && obj.countryId==selectedCountry && obj.pidfProductStrengthId == selectedStrength));
    EditIndex = (yearIndex == null || yearIndex == undefined ? -1 : yearIndex);

    if ((yearIndex == null || yearIndex == undefined)) {
        ClearValidationForYearForm();
        ResetYearFormValues();
    }

    if (ArrMainCommercial[MainRowEditIndex].PidfCommercialYears != null && ArrMainCommercial[MainRowEditIndex].PidfCommercialYears != undefined) {
        var _years = (yearIndex == null || yearIndex == undefined ? ArrMainCommercial[MainRowEditIndex].PidfCommercialYears.length : yearIndex);
        $("#btnSaveYear").text("Save Year" + (_years + 1));
        if (_years == 0) {
            $("#MarketSize").removeAttr('readonly');
            
        } 
        else {
            $("#MarketSize").attr("readonly", true);
            UpdateNSPasperlasteYear(MainRowEditIndex, _years, 'Low');
            UpdateNSPasperlasteYear(MainRowEditIndex, _years, 'Medium');
            UpdateNSPasperlasteYear(MainRowEditIndex, _years, 'High');
        }
    }
}

function UpdateNSPasperlasteYear(mainind, yearind, variable) {
    var nspval = ArrMainCommercial[mainind].PidfCommercialYears[yearind-1]['nsp' + variable];
    $('#Nspunits' + variable).val(nspval);
}

function UpdateChildTableDetail(_cObject) {
    if (_cObject.PidfCommercialYears != null && _cObject.PidfCommercialYears != undefined && _cObject.PidfCommercialYears.length > 0) {
        $.each(_cObject.PidfCommercialYears, function (index, it) {
            var html = "<tr id='AddYearRow'><td>Year" + (index + 1) + "</td>";
            $.each(ColumnObjUpcase, function (i, item) {
                var cntrlName = item;
                var result = it[cntrlName]
                if (cntrlName == 'packagingTypeId')
                    result = $('#dvCommercialForm').find('#PackagingTypeId option[value=' + result + ']').text();
                else if (cntrlName == 'currencyId')
                    result = $("#CurrencyId option[value=" + result + "]").text();
                else if (cntrlName == 'finalSelectionId')
                    result = $("#FinalSelectionId option[value=" + result + "]").text();

                if (result != undefined && result != null) {
                    html += '<td>' + result + '</td>'
                }
            });
            //' +
            //'<span class="text-danger" ><i class="fa fa-fw fa-trash" onclick="deleteCommercialYearRow(' + _cObject.packSizeId + ',' + index + ')"></i></span>
            html += '<td> <span class="text-primary"> <i class="fa fa-fw fa-edit large-font operationButton" onclick="editCommercialYearRow(' + _cObject.packSizeId + ',' + index + ', ' + _cObject.businessUnitId + ', ' + _cObject.pidfProductStrengthId + ')"></i></span></td></tr>';

            $('#tblMainCommercial').find('#Yearclildrows_' + _cObject.packSizeId).find("#YearsTable").find("#AddYearTBody").append(html);


            $('#FinalSelectionId').val(it["finalSelectionId"]);
            var FSSelectedText = $('#FinalSelectionId').find(":selected").text();
            var ArrItem = FSSelectedText.split('/');
            var MarketShareUnit = it["marketShareUnit" + ArrItem[0]];
            var Nsp = it["nsp" + ArrItem[1]];
            var result = MarketShareUnit * Nsp;
            html = '<tr id="RevenueRow"' + index + '><td>Year' + (index + 1) + '</td><td>' + FSSelectedText + '</td><td>' + result.toFixed() + '</td></tr>';

            $('#tblMainCommercial').find('#Yearclildrows_' + _cObject.packSizeId).find("#tblRevenue").find("#RevenueTbody").append(html);
        });
    }
}

function editCommercialYearRow(packSizeId, i, businessUnitId, pidfProductStrengthId) {
    EditIndex = i;
    var _commercialIndex = ArrMainCommercial.findIndex((obj => obj.packSizeId == packSizeId && obj.businessUnitId == businessUnitId && obj.pidfProductStrengthId == pidfProductStrengthId));
    MainRowEditIndex = _commercialIndex;
    OpenYearForm(packSizeId, i);
    var entityYear = ArrMainCommercial[_commercialIndex].PidfCommercialYears[i];
    $.each($('#AddYearForm').serializeArray(), function (_, kv) {
        $('#dvCommercialForm').find('#' + kv.name).val(entityYear[getPropertyName(kv.name)]);
    });
    $('#dvCommercialForm').find('#PackagingTypeId').focus();
}
function deleteCommercialYearRow(packSizeId, index) {
    var _commercialIndex = ArrMainCommercial.findIndex((obj => obj.packSizeId == packSizeId));
    ArrMainCommercial[_commercialIndex].PidfCommercialYears.pop(index);
    Update_BUstregthPackTable(ArrMainCommercial);
}

function renderBusinessUnit(businessUnit) {
    var html = "";
    $.each(businessUnit, function (index, item) {
        html += '<li class="nav-item p-0">\
        <a class="nav-link" onClick="BUtabClick('+ item.businessUnitId + ',' + parseInt($("#PIDFId").val()) + ');" id="BUtab_' + item.businessUnitId + '">' + item.businessUnitName + '</a></li>';
    });
    $('#dvCommercialForm').find("#businessUnitTabs").append(html);

    var PIDFBusinessUnitId = $("#PIDFBusinessUnitId").val();
    SelectedBUValue = PIDFBusinessUnitId
    $('#BUtab_' + PIDFBusinessUnitId).addClass('active');
}
function renderPIDFStrength(pidfStrength) {
    var html = "";
    $('#dvCommercialForm').find("#ProductStrengthTabs").html(html);
    var _StrenthByBU = $.grep(pidfStrength, function (n, i) {
        return n.countryId == selectedCountry && n.businessUnitId == SelectedBUValue
    }); 
    if (_StrenthByBU != null && _StrenthByBU != undefined && _StrenthByBU.length > 0) {
        $.each(_StrenthByBU, function (index, item) {
            html += '<li class="nav-item col-6 p-0">\
    <a class="nav-link" onClick="StrengthtabClick('+ item.pidfProductStrengthId + ',' + parseInt($("#PIDFId").val()) + ');" id="Strengthtab_' + item.pidfProductStrengthId + '">' + item.strength + item.unitofMeasurementName + '</a></li>';
        });

        $('#dvCommercialForm').find("#ProductStrengthTabs").html(html);

        var PIDFProductStrengthId = _StrenthByBU[0].pidfProductStrengthId;
        selectedStrength = PIDFProductStrengthId;
        $('#Strengthtab_' + PIDFProductStrengthId).addClass('active');
        StrengthtabClick(selectedStrength, _PIDFID);
    }    

    if (html == "") {
        IsNoStrengthforSelectedBU = true;
        SetCommercialFormReadonly();
    }
    else {
        IsNoStrengthforSelectedBU = false;
        $("#mainDivCommercial").find("input, button, submit, textarea, select").prop('disabled', false);
        /*$("[id^='deleteIconAddyear']").show();*/
        $('#mainDivCommercial').find('.operationButton').show();
    }
}
//function GetCountryList(selectedBU) {
//    ajaxServiceMethod($('#hdnBaseURL').val() + GetCountryListURL + "/" + selectedBU + "/" + _PIDFID, 'GET', GetCountryListSuccess, GetCountryListError);

//}
//function GetCountryListSuccess(data) {
//    try {
//        renderCountryTabList(data._object)
//        // getParentFormId().find('.SelectCountryPD').val(arr).trigger('change');
//    } catch (e) {
//        toastr.error('Error:' + e.message);
//    }
//}
//function GetCountryListError(x, y, z) {
//    toastr.error(ErrorMessage);
//}
function renderCountryTabList(BuVal) {
    var arrofcountryid = [];
    $('#dvCommercialForm').find("#navCountryTabs").html('');
    var html = "";
    var _CountryListforSelectedBU = $.grep(MainArrCountryList, function (n, i) {
        return n.businessUnitId == BuVal;
    });
    if (_CountryListforSelectedBU != null && _CountryListforSelectedBU != undefined && _CountryListforSelectedBU.length > 0) {
        $.each(_CountryListforSelectedBU, function (index, item) {
            if (arrofcountryid.indexOf(item.countryId) == -1) {
                arrofcountryid.push(item.countryId)
                html += '<li class="nav-item col-6 p-0 pt-1">\
    <a class="nav-link" onClick="CountrytabClick('+ item.countryId + ',' + parseInt($("#PIDFId").val()) + ');" id="Countrytab_' + item.countryId + '">' + item.countryName + '</a></li>';
            }
        });
        $('#dvCommercialForm').find("#navCountryTabs").append(html);

        var _countryId = (_CountryListforSelectedBU[0] == undefined) ? 0 : _CountryListforSelectedBU[0].countryId;
        selectedCountry = _countryId;
        $('#Countrytab_' + _countryId).addClass('active');
    }
    renderPIDFStrength(MainArrCountryList);
}
function setCommercialArray(Commercial, CommercialYear) {
    $.each(Commercial, function (index, item) {
        item.PidfCommercialYears = [];
        var _commercialChild = $.grep(CommercialYear, function (n, i) {
            return n.pidfCommercialId == item.pidfCommercialId;
        });
        $.each(_commercialChild, function (i, t) {
            item.PidfCommercialYears.push(t);
        });
        ArrMainCommercial.push(item);
    });
}
function GetCurrencyList(data) {
    try {
        $('#CurrencyId').append($('<option>').text('--Select--').attr('value', '0'));
        $.each(data, function (index, object) {
            $('#CurrencyId').append($('<option>').text(object.currencyName).attr('value', object.currencyID));
        });
    } catch (e) {
        console.log('Get Currency Error:' + e.message);
    }
}
function GetProductTypeList(data) {
    try {
        $('#dvCommercialForm').find('#PackagingTypeId').append($('<option>').text('--Select--').attr('value', '0'));
        $.each(data, function (index, object) {
            $('#dvCommercialForm').find('#PackagingTypeId').append($('<option>').text(object.packagingTypeName).attr('value', object.packagingTypeId));
        });
    } catch (e) {
        console.log('Get Product Type Error:' + e.message);
    }
}
function GetPackSize(data) {
    try {
        $('#PackSizeId').append($('<option>').text('--Select--').attr('value', ''));
        $.each(data, function (index, object) {
            $('#PackSizeId').append($('<option>').text(object.packSizeName).attr('value', object.packSizeId).attr('data-val', object.packSize));
        });
    } catch (e) {
        console.log('Get Final Selection Error:' + e.message);
    }
}
function GetFSList(data) {
    try {
        $('#FinalSelectionId').append($('<option>').text('--Select--').attr('value', '0'));
        $.each(data, function (index, object) {
            $('#FinalSelectionId').append($('<option>').text(object.finalSelectionName).attr('value', object.finalSelectionId));
        });
    } catch (e) {
        console.log('Get Final Selection Error:' + e.message);
    }
}

function getPropertyName(name) {
    return name.charAt(0).toLowerCase() + name.slice(1);
}
function IsShowCancel_Save_buttons(flag) {
    if (flag)
        $("#dvMainButton").show();
    else
        $("#dvMainButton").hide();
}

/*------PBF OutSourcing-----------------------------------------*/
$("#custom-tabs-BudgetApproval-Finance-tab").click(function () {
    SetPBFDDLValues();
});
function SetPBFDDLValues() {
    if (pbfOutSourceData_Arr != null && pbfOutSourceData_Arr.length > 0) {
        var _pbfworkflowId = pbfOutSourceData_Arr[0].pbfWorkflowId;
        $('#ddlPbfworkflowId').val(_pbfworkflowId);

        var _projectWorkFlowId = pbfOutSourceData_Arr[0].projectWorkflowId;
        $('#ddlProjectWorkflowId').val(_projectWorkFlowId);
    }
}


function GetPBFOutsourcingTabDDLdata() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetPBFOutsourcingTabDropDownData, 'GET', GetPBFOutsourcingTabDDLdataSuccess, GetPBFOutsourcingTabDDLdataError);

}
function GetPBFOutsourcingTabDDLdataSuccess(data) {
    try {
        Bind_ddlProjectWorkflowId(data._object[0]);
        Bind_ddlPbfworkflowId(data._object[1]);

    } catch (e) {
        console.log('Get PBF Outsourcing Error:' + e.message);
    }
}
function GetPBFOutsourcingTabDDLdataError(x, y, z) {
    console.log(ErrorMessage);
}
function Bind_ddlProjectWorkflowId(data) {
    try {
        $('#ddlProjectWorkflowId').append($('<option>').text('--Select--').attr('value', '0'));
        $.each(data, function (index, object) {
            $('#ddlProjectWorkflowId').append($('<option>').text(object.workflowName).attr('value', object.workflowId));
        });
        
    } catch (e) {
        console.log('Get ddlProjectWorkflow Error:' + e.message);
    }
}
function Bind_ddlPbfworkflowId(data) {
    try {
        $('#ddlPbfworkflowId').append($('<option>').text('--Select--').attr('value', '0'));
        $.each(data, function (index, object) {
            $('#ddlPbfworkflowId').append($('<option>').text(object.pbfworkFlowName).attr('value', object.pbfworkFlowId));
        });
       
    } catch (e) {
        console.log('Get ddlPbfworkflow  Error:' + e.message);
    }
}
//--------------------------------------------
var pbfOutSourceData_Arr = [];
$('#ddlPbfworkflowId').change(function () {
    var id = $(this).val();
    if (id != undefined || id != '') {
        var _Objpbfdata = $.grep(pbfOutSourceData_Arr, function (v) {
            return v.pbfWorkflowId == parseInt(id);
        });
        if (_Objpbfdata.length > 0) {
            AddTaskTo_tblPBFOutsourcetask(_Objpbfdata);
        }
        else {
            GetPBFWorkFlowTaskNames(id);
        }
    }
});
function GetPBFWorkFlowTaskNames(_pbfWorkFlowid) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetPBFWorkFlowTaskNamesurl + "/" + _pbfWorkFlowid, 'GET', GetPBFWorkFlowTaskNamesSuccess, GetPBFWorkFlowTaskNamesError);
}
function GetPBFWorkFlowTaskNamesSuccess(data) {
    try {
        //console.log(data);
        if (data._object != null)
            AddTaskTo_tblPBFOutsourcetask(data._object);
    } catch (e) {
        console.log('Get PBF Outsourcing Error:' + e.message);
    }
}
function GetPBFWorkFlowTaskNamesError(x, y, z) {
    console.log(ErrorMessage);
}
function AddTaskTo_tblPBFOutsourcetask(data) {
    var html = '';
    $.each(data, function (ind, item) {
        var _cost = (item.cost == undefined) ? '' : item.cost
        var _Taskname = (item.pbfWorkFlowTaskName == null) ? '' : item.pbfWorkFlowTaskName;
        var _tentative = (item.tentative == undefined) ? '' : item.tentative
        var classname = '';//(item.taskLevel == 1) ? 'fw-bold' : 'text-end';

        html += '<tr class="taskdataRow" id="RowTask_' + ind + '" >'
        html += '<td> <input type="text" class="form-control clspbfWorkFlowTaskName ' + classname + ' " value="' + _Taskname + '" /> </td>'
        html += '<td> <input type="number" class="form-control clscost"   value="' + _cost + '" /> </td>'
        html += '<td> <input type="date" class="form-control clstentative" value="' + _tentative + '" /> </td>'

        html += '<td class="clsAddDeleteIconPBFComm"> <i class="fa-solid fa-circle-plus nav-icon text-success AddIconAPI" id="addIcon_' + ind + '" onclick="addRowParent_PBFOutsource(' + ind +')"></i> '

        html += '<i  class="fa-solid fa-trash nav-icon text-red DeleteIconAPI" id="deleteIconAPI_' + ind + '" onclick="removeRowParent_PBFOutsource(' + ind +', this)"></i>'

        html += '</td> </tr>'
    });
    //if (html != '') {
    //   $("#PBFOutsourcetaskTbody").append(html);
    //}
    $("#PBFOutsourcetaskTbody").html(html);
    PBFOutsourceRowDeleteIcon();
    if ($("#IsView").val() == '1') {
        SetPBFCommercialFormReadonly();
    }
}

function UpdatePBFOutSourceData(data) {
 
    if (data != null && data.length > 0) {
        pbfOutSourceData_Arr = data;
        AddTaskTo_tblPBFOutsourcetask(data);
    }    
}
function IsPBFPageValid() {
    var IsValid = true;
    if ($('#ddlPbfworkflowId').val() == "0") {
        $('#valmsgddlPbfworkflowId').text('Required');
        IsValid = false;
    }
    else {
        $('#valmsgddlPbfworkflowId').text('');
    }
    if ($('#ddlProjectWorkflowId').val() == "0") {
        $('#valmsgddlProjectWorkflowId').text('Required');
        IsValid = false;
    }
    else {
        $('#valmsgddlProjectWorkflowId').text('');
    }
    if(!IsValid)
        toastr.error('Some feilds missing values!'); 

    return IsValid;
}
function SavePBFOutsourceData(saveType) {
  //  IsPBFPageValid = true;
    if (saveType != 'Sv' || (IsPBFPageValid() && ValidateTaskData())) {
        var pbfworkflowId = $('#ddlPbfworkflowId').val();
        var _projectWorkFlowId = $('#ddlProjectWorkflowId').val();
        var PidfPbfOutsourceTask = getPBFTaskDataToSave();

        var MainObj_PBFOutsourceSaveData = {
            'SaveType': saveType,
            'Pidfid': _PIDFID,
            'ProjectWorkflowId': _projectWorkFlowId,
            'PbfWorkFlowId': pbfworkflowId,
            'pidfpbfoutsourceTaskEntityList': PidfPbfOutsourceTask
        };
        ajaxServiceMethod($('#hdnBaseURL').val() + AddUpdatePBFoutsourceDataUrl, 'POST', AddUpdatePBFoutsourceDataSuccess, AddUpdatePBFoutsourceDataError, JSON.stringify(MainObj_PBFOutsourceSaveData));
    }
}
function AddUpdatePBFoutsourceDataSuccess(data) {
    try {
       // $('#SavePIDFModel').modal('hide');
        if (data._Success === true) {
            
            if (!IsTabClick) {
                toastr.success(data._Message);
                window.location.href = "/PIDF/PIDFList?ScreenId=4";
            }
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        console.log('Save PBfOutsource Error:' + e.message);
    }
}
function AddUpdatePBFoutsourceDataError(x, y, z) {
    console.log(ErrorMessage);
}
function getPBFTaskDataToSave() {
   
    var taskdataArr = [];
    $("tr.taskdataRow").each(function () {   
        var taskdataObj = {};
        var pbfWorkFlowTaskName = $(this).find("input.clspbfWorkFlowTaskName").val();
        var cost = $(this).find("input.clscost").val();
        var tentative = $(this).find("input.clstentative").val();

        taskdataObj = {
            'PbfworkFlowTaskName': pbfWorkFlowTaskName,
            'Cost': cost,
            'Tentative': tentative
        };
        taskdataArr.push(taskdataObj);        
    });
    return taskdataArr;
}
function addRowParent_PBFOutsource(j) {
    var table = $('#PBFOutsourcetaskTbody');
    var node = $('#RowTask_' + j).clone(true);
    table.find('tr:last').after((node.length > 1 ? node[0] : node));
    table.find('tr:last').find("input").val("");
    PBFOutsourceRowDeleteIcon();
}
function removeRowParent_PBFOutsource(j, element) {
    $(element).closest("tr").remove();
    PBFOutsourceRowDeleteIcon();
} 
function PBFOutsourceRowDeleteIcon() {
    if ($('#tblPBFOutsourcetask tbody tr').length > 1) {
        $('.DeleteIconAPI').show();
    } else {
        $('.DeleteIconAPI').hide();
    }
}
function ValidateTaskData() {
    isValidIPDForm = true; 
    $('#tblPBFOutsourcetask input').each(function () {
        if ($(this).val() == '') {
            $(this).css("border-color", "red");
            isValidIPDForm = false;
        }
        else {
            $(this).css("border-color", "");
        }
    });
    if(!isValidIPDForm)
        toastr.error('Some Task are missing values !'); 

    return isValidIPDForm;
}
//PBF Save
$('#btnPBFCommercialSubmit').click(function () {
    IsTabClick = false;
    IsCommTabClick = false;
    SavePBFOutsourceData('Sv');
});
$('#btnSaveAsDraftPBFCommercial').click(function () {
    IsTabClick = false;
    IsCommTabClick = false;
    SavePBFOutsourceData('SvDrf');
});
$('#custom-tabs-BudgetApproval-PBF-tab').click(function () {//commercial Tab Click
    IsTabClick = true;
    if ($("#IsView").val() != '1')
    SavePBFOutsourceData('TabClick'); 
})
// Commercial save
$("#custom-tabs-BudgetApproval-Finance-tab").click(function () { // PBF tab click
    IsTabClick = true;
    if (IsPageLoad) {
        IsPageLoad = false;
        SetPBFDDLValues();
    }
    IsCommTabClick = true;
    if ($("#IsView").val() != '1') 
    CommercialSaveAsDraftClickClick('TabClick');


    var IsViewInMode = ($("#IsView").val() == '1');
    if (IsViewInMode) {
        $('.clsAddDeleteIconPBFComm').hide();
    }
    else {
        $('.clsAddDeleteIconPBFComm').show();
    }
});
$('#mainDivCommercial').find("#btnSubmit").click(function () {
    IsTabClick = false;
    CommercialSubmitClick('Sv');
});
$('#mainDivCommercial').find("#btnSaveAsDraft").click(function () {
    IsTabClick = false;
    CommercialSaveAsDraftClickClick('SvDrf');
});
$(document).on("change", ".clstentative", function () {
   var isValidTentativeDate = true;
    var todaysDate = new Date();
    var _originalExpiryDate = new Date($(this).val());
    if (_originalExpiryDate < new Date(todaysDate.getFullYear(), todaysDate.getMonth(), todaysDate.getDate())) {
        $(this).css("border-color", "red");
        $(this).val('');
        isValidTentativeDate = false;
        toastr.error('Can not select Past date !')
    }
    else {
        $(this).css("border-color", "");
        isValidTentativeDate = true;
    }
});
function SetPBFCommercialFormReadonly() {
    $("#custom-tabs-PBFCommercial").find("input, submit, textarea,text, a, select").attr("disabled", "disabled");
    $("#custom-tabs-PBFCommercial").find("#btnSaveAsDraftPBFCommercial,#btnPBFCommercialSubmit").hide();
    $("#btnPBFCommercialCancel").removeAttr("disabled");
}
var CurrentscreenId_Commercial = '';
function BussinesUnitInterestedCommercial(pidfid, buid, screenId) {
    CurrentscreenId_Commercial = screenId;
    ajaxServiceMethod($('#hdnBaseURL').val() + GetIsInterestedByPIDFandBUurlCommercial + "/" + pidfid + "/" + buid, 'GET', BussinesUnitInterestedCommercialSuccess, BussinesUnitInterestedCommercialError);
}
function BussinesUnitInterestedCommercialSuccess(data) {
    var BUTabData_Div = '.clsContentUnderBUTab_' + CurrentscreenId_Commercial;
    var NonIntNote_Div = '#dvNotInterestedBUNote_' + CurrentscreenId_Commercial;
    var NonIntNote_HeadingNote = '#dvNotInterestedBUNoteHeading_' + CurrentscreenId_Commercial;

    DispalyStatusOfBUByInterested(data, BUTabData_Div, NonIntNote_Div, NonIntNote_HeadingNote);

}
function BussinesUnitInterestedCommercialError(x, y, z) {
    toastr.error(ErrorMessage);
}
