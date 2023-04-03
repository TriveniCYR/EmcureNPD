var objYears = [];
var objMainForm = {};
var objdropdownYearControls_array = ['PackagingTypeId', 'CurrencyId','FinalSelectionId'];
var ColumnObjUpcase = ['PackagingTypeId', 'CommercialBatchSize', 'PriceDiscounting', 'TotalApireq', 'Apireq', 'Suimsvolume', 'MarketGrowth', 'MarketSize', 'PriceErosion', 'FinalSelectionId'];
/*var ColumnObjLowcase = ['packagingTypeId', 'commercialBatchSize', 'priceDiscounting', 'totalApireq', 'apireq', 'suimsvolume', 'marketGrowth', 'marketSize', 'priceErosion', 'finalSelectionId'];*/
var SelectedBUValue = 0;
var selectedStrength = 0;

$(document).ready(function () {
    debugger;
    SetDivReadonly();
    InitializeCurrencyDropdown();
    InitializeFinalSelectionDropdown();    
    $("#AddYearForm").hide();
    IsViewModeCommercial();
    getPIDFAccordion(_PIDFAccordionURL, _PIDFID, "dvPIDFAccrdion");
    getIPDAccordion(_IPDAccordionURL, _EncPIDFID, _PIDFBusinessUnitId, "dvIPDAccrdion");
    SetBU_Strength();
    HideSaveAsDraft();
    InitializeProductTypeDropdown();
});
function HideSaveAsDraft() {    
    if ($('#StatusId').val() == 11)     //[11=CommercialSubmitted , 10= CommercialInProgress]
     $('#btnSaveAsDraft').hide();    
    else
      $('#btnSaveAsDraft').show();
}

function SetBU_Strength() {
    var PIDFBusinessUnitId = $("#PIDFBusinessUnitId").val();
    var PIDFProductStrengthId = $("#PIDFProductStrengthId").val();
    var pidfId = $("#PIDFId").val();

    SelectedBUValue = PIDFBusinessUnitId;
    selectedStrength = PIDFProductStrengthId;

    var StrengthAnchorId = '#BUtab_' + PIDFBusinessUnitId;
    var BUAnchorId = '#Strengthtab_' + PIDFProductStrengthId;

    $(StrengthAnchorId).addClass('active');
    $(BUAnchorId).addClass('active');

    GetCommercialPIDFByBU(pidfId);
    $("#AddYearForm").hide();
    IsShowCancel_Save_buttons(true);
}
function IsViewModeCommercial() {
    if ($("#IsView").val() == '1') {
        SetCommercialFormReadonly();
    }
}

function InitializeCurrencyDropdown() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllCurrency, 'GET', GetCountryListSuccess, GetCountryListError);
}
function GetCountryListSuccess(data) {
    try {
        $('#CurrencyId').append($('<option>').text('--Select--').attr('value','0'));
        $.each(data._object, function (index, object) {
            $('#CurrencyId').append($('<option>').text(object.currencyName).attr('value', object.currencyId));
        });
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetCountryListError(x, y, z) {
    toastr.error(ErrorMessage);
}

function InitializeProductTypeDropdown() {

    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllProductType, 'GET', GetProductTypeListSuccess, GetProductTypeListError);
}
function GetProductTypeListSuccess(data) {
    try {
        $('#PackagingTypeId').append($('<option>').text('--Select--').attr('value', '0'));
        $.each(data._object, function (index, object) {
            $('#PackagingTypeId').append($('<option>').text(object.packagingTypeName).attr('value', object.packagingTypeId));
        });
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetProductTypeListError(x, y, z) {
    toastr.error(ErrorMessage);
}

function InitializeFinalSelectionDropdown() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllFinalSelection, 'GET', GetFSListSuccess, GetFSListError);
}
function GetFSListSuccess(data) {
    try {
        $('#FinalSelectionId').append($('<option>').text('--Select--').attr('value', '0'));
        $.each(data._object, function (index, object) {
            $('#FinalSelectionId').append($('<option>').text(object.finalSelectionName).attr('value', object.finalSelectionId));
        });
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetFSListError(x, y, z) {
    toastr.error(ErrorMessage);
}

function ValidateYearForm() {
    var ArrofInvalid = []
    //var IsValid = true;;
    $.each($('#AddYearForm').serializeArray(), function (_, kv) {

        
        if (jQuery.inArray(kv.name, objdropdownYearControls_array) != -1) {

            if (kv.value == 0) {
                $('#valmsg' + kv.name).text('Required');
                //IsValid = false;
                ArrofInvalid.push(kv.name);
            }
            else {
                $('#valmsg' + kv.name).text('');
            }
        }
        else
        {
            if (kv.value == '' ) {
                $('#valmsg' + kv.name).text('Required');
                //IsValid = false;
                ArrofInvalid.push(kv.name);
            }
            else {
                $('#valmsg' + kv.name).text('');
            }
        }

       
    });
    var status = (ArrofInvalid.length == 0) ? true : false;
    if (!status) {
        var controltobeFocus = ArrofInvalid[ArrofInvalid.length-1];
        $('#' + controltobeFocus).focus();
        toastr.error('Some fields are missing !');
    }
    return status;
}
function ClearValidationForYearForm() {

    $.each($('#AddYearForm').serializeArray(), function (_, kv) {
        $('#valmsg' + kv.name).text('');
    });
}
function ClearValidationForMainForm() {
    var MainFormFeilds = ['MarketSizeInUnit', 'ShelfLife']
    $.each(MainFormFeilds, function (_, kv) {
        $('#valmsg' + kv.name).text('');
    });
}
function ValidateMainForm() {
    var ArrofInvalid = []
    var MainFormFeilds = ['MarketSizeInUnit', 'ShelfLife']
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

function AddYearClick() {
    //var year = $('#AddYearForm').serializeArray();
    var valBUStrength = ValidateBU_Strength();
    if (ValidateYearForm() && valBUStrength && ValidateMainForm()) {
        var entityYear = {};
        //var IsValid = true;;
        $.each($('#AddYearForm').serializeArray(), function (_, kv) {
            if (kv.value == '') {
                $('#valmsg' + kv.name).text('Required');
                //IsValid = false;
                ArrofInvalid.push(kv.name);
            }
            entityYear[kv.name] = kv.value;
        });

        //console.log(entityYear);
        //entityYear['YearIndex'] = objYears.length + 1;
        objYears.push(entityYear);
        UpdateYearTable(ColumnObjUpcase);
        ClearValidationForYearForm();
        $("#AddYearForm").hide();
        IsShowCancel_Save_buttons(true);
        ResetYearFormValues();
    }
}
function AddRow(i) {
    $('#AddYearTBody').append(`<tr id='AddYearRow` + i + `'></tr>`)
    var rowid = 'AddYearRow' + i;
    $('#' + rowid).append('<td>Year' + (i + 1) + '</td>')
}
function AddColToRow(i, tdvalue) {
    var rowid = 'AddYearRow' + i;
    $('#' + rowid).append('<td>' + tdvalue + '</td>')
}

function deleteRow(i) {
    objYears.pop(i);
    UpdateYearTable(ColumnObjUpcase);
}
function AddtblRevenueRow(year, i, columns) {

    var finalSelection = columns[9];
    $('#' + finalSelection).val(year["FinalSelectionId"]);
    var FSSelectedText = $('#' + finalSelection).find(":selected").text();
    var ArrItem = FSSelectedText.split('/');
    var MarketShareUnit = year["MarketShareUnit" + ArrItem[0]];
    var Nsp = year["Nsp" + ArrItem[1]];
    var result = MarketShareUnit * Nsp;
    $('#RevenueTbody').append(`<tr id='RevenueRow` + i + `'></tr>`)
    $('#RevenueRow' + i).append('<td>Year' + (i + 1) + '</td>')
    $('#RevenueRow' + i).append('<td>' + FSSelectedText + '</td>')
    $('#RevenueRow' + i).append('<td>' + result.toFixed() + '</td>')
}

function SetDivReadonly() {
    $("#PIDFScreen").find("input, submit, textarea, a, select").attr("disabled", "disabled");
    $("#PIDFScreen").find("button, submit, a").hide();
    $("#PIDFScreen").find("#collapseButton").show();
    $("#PIDFScreen").find("#PIDFormcollapseButton").show();

    $("#PIDFormcollapseButton").click();
    $("#collapseButton").click();
}
$("#btnSubmit").click(function () {
    if (ValidateMainForm() && ValidateBU_Strength()) {
        if (objYears.length > 0) {
            $.extend(objMainForm, { 'SaveType': 'Sv' });
            SaveCommertialPIDFForm();

        }
        else {
            toastr.error('No Year Data Added');
        }
    }    
});


$("#btnSaveAsDraft").click(function () {
    if (ValidateMainForm() && ValidateBU_Strength()) {
        if (objYears.length > 0) {
            $.extend(objMainForm, { 'SaveType': 'SvDrf' });
            SaveCommertialPIDFForm();
        }
        else {
            toastr.error('No Year Data Added');
        }
    }
    
});

function UpdateYearTable(columns) {
    $("#AddYearTBody tr").remove();
    $("#RevenueTbody tr").remove();
    for (var i = 0; i < objYears.length; i++) {
        AddRow(i);
        $.each(columns, function (item) {
            var cntrlName = columns[item]
            var result = objYears[i][cntrlName]
            //console.log(result);
            if (result != undefined) {
                AddColToRow(i, result)
                //var cntrlid = columns[item]; //this for clear the AddYeareform
                //$('#' + cntrlid).val('');
            }
        });
        AddtblRevenueRow(objYears[i], i, columns);
        $('#AddYearRow' + i).append('<td> <spam><i class="fas fa-trash-alt" id="deleteIconAddyear_' + i + '" onclick="deleteRow(' + i + ')"></i></spam></td>')

        $('#AddYearRow' + i + ' td:eq(10) spam i').prop('id', 'deleteIconAPI_' + i + '');
        $('#AddYearRow' + i + ' td:eq(10) spam i').attr('onclick', 'deleteRowApiDetails(' + i + ')');
    }
}

function SaveCommertialPIDFForm() {

    $.extend(objMainForm, { 'PidfproductStrengthId': selectedStrength });
    $.extend(objMainForm, { 'BusinessUnitId': SelectedBUValue });

    $.extend(objMainForm, { 'Pidfid': $("#Pidfid").val() });
    $.extend(objMainForm, { 'MarketSizeInUnit': $("#MarketSizeInUnit").val() });
    $.extend(objMainForm, { 'ShelfLife': $("#ShelfLife").val() });
    $.extend(objMainForm, { 'encCreatedBy': $("#LoggedInUserId").val() });
    $.extend(objMainForm, { 'PidfCommercialYears': objYears });

    //console.log(JSON.stringify(objMainForm));
    ajaxServiceMethod($('#hdnBaseURL').val() + SaveCommercialPIDF, 'POST', SaveCommertialPIDFFormSuccess, SaveCommertialPIDFFormError, JSON.stringify(objMainForm));

}
function SaveCommertialPIDFFormSuccess(data) {
    try {
        $('#SavePIDFModel').modal('hide');
        if (data._Success === true) {
            //window.location = "/PIDF/PIDFList";
            toastr.success(data._Message);
            $("#AddYearForm").hide();
            IsShowCancel_Save_buttons(true);
            window.location.href = "/PIDF/PIDFList?ScreenId=4";
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveCommertialPIDFFormError(x, y, z) {
    toastr.error(ErrorMessage);
}

function BUtabClick(BUVal, pidfidval) {
    SelectedBUValue = BUVal;
    ClearValidationForYearForm();
    ClearValidationForMainForm();
    GetCommercialPIDFByBU(pidfidval);
    $("#AddYearForm").hide();
    IsShowCancel_Save_buttons(true);
}


function StrengthtabClick(strengthVal, pidfidval) {
    selectedStrength = strengthVal;
    ClearValidationForYearForm();
    ClearValidationForMainForm();
    GetCommercialPIDFByBU(pidfidval);
    $("#AddYearForm").hide();
    IsShowCancel_Save_buttons(true);
}


function GetCommercialPIDFByBU(pidfidval) {
    //SelectedBUValue = (SelectedBUValue === undefined) ? 0 : SelectedBUValue;
    //selectedStrength = (selectedStrength === undefined) ? 0 : selectedStrength;

    ajaxServiceMethod($('#hdnBaseURL').val() + GetCommercialFormData + "/" + pidfidval + "/" + SelectedBUValue + "/" + selectedStrength, 'GET', GetCommercialPIDFByBUSuccess, GetCommercialPIDFByBUError);
    ClearValidationForYearForm();
}
function GetCommercialPIDFByBUSuccess(data) {
    try {

        UpdateFormData(data._object);
        SetDisableForOtherUserBU();

    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetCommercialPIDFByBUError(x, y, z) {
    toastr.error(ErrorMessage);
}
function UpdateFormData(objectForm) {
    $("#MarketSizeInUnit").val(objectForm.marketSizeInUnit);
    $("#ShelfLife").val(objectForm.shelfLife);
    if (objectForm.pidfCommercialYears != undefined && objectForm.pidfCommercialYears != null) {
        objYears = [];
        objYears = objectForm.pidfCommercialYears;
        // console.log('objYears')
        // console.log(objYears)
        var TempObjYears = [];

        for (var i = 0; i < objYears.length; i++) {
            var entityYear = {};
            $.each(objYears[i], function (key, value) {
                //console.log(key + ": " + value);
                var newKey = key.charAt(0).toUpperCase() + key.slice(1);
                entityYear[newKey] = value;
            });
            TempObjYears.push(entityYear);
            entityYear = {};
        }
        // console.log('TempObjYears')
        //console.log(TempObjYears);
        objYears = TempObjYears;
        UpdateYearTable(ColumnObjUpcase);
    }
    else {
        ResetYearForm();
        objYears = [];
        UpdateYearTable(ColumnObjUpcase);
    }
}

function ResetYearForm() {
    // $("#AddYearForm").find("input,textarea").val('');
    // $("#AddYearForm").find("select").val('1');
}
function ResetMainFormForm() {
    $("#MarketSizeInUnit").val('');
    $("#ShelfLife").val('');
}
function SetCommercialFormReadonly() {
    $("#mainDivCommercial").find("input, button, submit, textarea, select").prop('disabled', true);
    $("[id^='deleteIconAddyear']").hide();
    $("#btnCommercialCancel").prop('disabled', false);
    $("#AddyeartableCollapseButton").prop('disabled', false);
}
function SetDisableForOtherUserBU() {
    var UserwiseBusinessUnit = $('#BusinessUnitsByUser').val().split(',');
    var BU_VALUE = SelectedBUValue;
    var status = UserwiseBusinessUnit.indexOf(BU_VALUE);
    var IsViewInMode = ($("#IsView").val() == '1')
    if (status == -1 || IsViewInMode) {
        SetCommercialFormReadonly();
    }
    else {
        $("#mainDivCommercial").find("input, button, submit, textarea, select").prop('disabled', false);
        $("[id^='deleteIconAddyear']").show();
        $("#AddyeartableCollapseButton").prop('disabled', false);
    }
}

// ----------Calulations Methods--------------
//----------- Market Size-----------------------
$('#MarketGrowth').focusout(function () {
    var result = 0;
    var MarketSizeAsLaunch;
    var MarketGrowth;
    var objyearLen = objYears.length;

    if (objyearLen == 0) {
        MarketSizeAsLaunch = $('#MarketSizeInUnit').val();
        MarketGrowth = $('#MarketGrowth').val();
        result = MarketSizeAsLaunch * (1 + (MarketGrowth / 100))
        $('#MarketSize').val(result.toFixed());
    }
    if (objyearLen > 0) {
        MarketSizeAsLaunch = objYears[objyearLen - 1]['MarketSize']
        MarketGrowth = $('#MarketGrowth').val();
        result = MarketSizeAsLaunch * (1 + (MarketGrowth / 100))
        $('#MarketSize').val(result.toFixed());
    }
    // $('#MarketGrowth').val(MarketGrowth + '%');
});
//--------- Estimated Market Share Units--------------------
$('#MarketSharePercentageLow').focusout(function () {
    EstimatedMarketShareUnits('Low');
});
$('#MarketSharePercentageMedium').focusout(function () {
    EstimatedMarketShareUnits('Medium');
});
$('#MarketSharePercentageHigh').focusout(function () {
    EstimatedMarketShareUnits('High');
});
function EstimatedMarketShareUnits(variable) {
    var MarketSize = $('#MarketSize').val();
    var MarketShare = $('#MarketSharePercentage' + variable).val();
    var result = MarketSize * MarketShare / 100;
    $('#MarketShareUnit' + variable).val(result.toFixed());
}
//---------NSP------------------------------------
$('#NspunitsLow').focusout(function () {
    NSP('Low');
});
$('#NspunitsMedium').focusout(function () {
    NSP('Medium');
});
$('#NspunitsHigh').focusout(function () {
    NSP('High');
});
function NSP(variable) {
    var result = 0;
    var Nspunits;
    var PriceErosion = $('#PriceErosion').val();
    var objyearLen = objYears.length;
    if (objyearLen == 0) {
        Nspunits = $('#Nspunits' + variable).val();
    }
    if (objyearLen > 0) {
        Nspunits = objYears[objyearLen - 1]['Nsp' + variable];
    }
    result = Nspunits * (1 + (PriceErosion / 100))
    $('#Nsp' + variable).val(result.toFixed(3));
}
function ShowPopUpCommercial() {
    $("#CancelModelCommercial").find("button, submit, a").show();
    $('#CancelModel').modal('show');
}
function ShowYearForm() {
    if (ValidateBU_Strength()) {
        $("#AddYearForm").show();
        IsShowCancel_Save_buttons(false);
    }
    var Count_Of_Next_year = objYears.length + 1;
    var SaveButtonText = 'Save Year';
    $("#btnSaveYear").text(SaveButtonText + Count_Of_Next_year);
}
function IsShowCancel_Save_buttons(flag) {
    if (flag)
        $("#dvMainButton").show();
    else
        $("#dvMainButton").hide();
}

$('#btnCancelYearForm').click(function () {
    $("#AddYearForm").hide();
    IsShowCancel_Save_buttons(true);
    ResetYearFormValues();
});
