﻿let rowcount = 0;
let selectedCurrencyText = "";
let el = document.querySelectorAll('input[type="number"]');
let isValidSku = false;
var SumOfSales = [];
var SumOfCOGS = []; 
var SumOfGC = [];
var Expiries_Yearwise_Data = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
var AnnualConfirmatoryRelease_Data = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
var Global_Projection_Year_data = [];
var ExpiriestxtboxFocusId = "";
var AnnualConfirmatoryRtxtboxFocusId = "";
var TabKeyPressed_control = '';
var TabKeyPressed_controlAnnualConf = '';
var ScreenName = 'Finance';

$(document).ready(function () {
 /*   console.log("selectedSKUs" + selectedSKUs);*/
    SelectedTabBU_Finance = _selectBusinessUnit;
    GetCurrencyList();
    calculateDealTerm();
    GetDosageFormList();
    GetSelectedMSPercent();
    GetSelectedTargetPriceScanario();
    GetSkus(pidfId);
    fnGetActiveBusinessUnit();
   
    if (isView === "1") {
        $('.readOnlyUpdate').prop('readonly', true);
        $('select.form-control.readOnlyUpdate').attr("disabled", true);
        $('.btnControll').css("display", "none");
        $('.add-rows').css("display", "none");
        $('.del-rows').css("display", "none");
    }
    else {
        $('.readOnlyUpdate').prop('readonly', false);
        $('select.form-control.readOnlyUpdate').attr("disabled", false);
        $('.btnControll').css("display", "inline-block");
        $('.add-rows').css("display", "block");
        $('.del-rows').css("display", "block");
        SetChildRowDeleteIcon();
        //var row_index = $(ele).closest('tr').index();
        $(`.BrandPrice`).attr("readonly", true);
        $(`.GenericListprice`).attr("readonly", true);
        $(`.NetRealisation`).attr("readonly", true);
        $(`.EstMat2020By12units`).attr("readonly", true);
        $(`.Marketinpacks`).attr("readonly", true);
        $(`.BatchsizeinLtrTabs`).attr("readonly", true);
        $(`.PackSizeValue`).attr("readonly", true);
    }

    $("#Currency").select2({
        placeholder: "Select Currency..",
        allowClear: true,
        dropdownAdapter: $.fn.select2.amd.require('select2/selectAllAdapter') 
    });
    
    $(el).attr("step", "any");

    $(document).on("change", "input[type = 'number']", function () {        
     var selectedVal =   $(this).val()
        if (selectedVal == '')
            $(this).val('0');
    });
    //$(document).on("keydown", ".Expiriestxtbox", function (e) {
    //    if (e.keyCode === 9) {
    //        e.preventDefault();
    //        ExpiriestxtboxFocusId = $(this).closest('td').next('td').find('input').attr('id');
    //      //  console.log('This is Keydown: ' + $(this).attr('id'));
    //        $(this).trigger('change');
    //    }
    //});
    //$(document).on("keydown", ".AnnualConfirmatoryRtxtbox", function (e) {
    //    if (e.keyCode === 9) {
    //        e.preventDefault();
    //        AnnualConfirmatoryRtxtboxFocusId = $(this).closest('td').next('td').find('input').attr('id');
    //       // console.log('This is Keydown: ' + $(this).attr('id'));
    //        $(this).trigger('change');
    //    }
    //});
    UpdateProjectionCommercial();   // Expiriestxtbox
    

    $(".Expiriestxtbox").on('keydown', function (e) {

        var keyCode = e.keyCode || e.which;
        if (keyCode == 9) {
            e.preventDefault();
            TabKeyPressed_controlID = $(this).attr('id');
            $(this).trigger('change');
           //alert('Tab Pressed');
        }
    });

    $(".AnnualConfirmatoryRtxtbox").on('keydown', function (e) {
       // alert('Tab Pressed');
        var keyCode = e.keyCode || e.which;
        if (keyCode == 9) {
            e.preventDefault();

            TabKeyPressed_controlAnnualConf = $(this).attr('id');
            $(this).trigger('change');
            //alert('Tab Pressed');
        }
    });
    if ($('#ProjectStartDate').val()!='')
        $('#ProjectStartDate').attr('readonly', true)
    if ($('#BatchManufacturing').val() != '')
        $('#BatchManufacturing').attr('readonly', true)
    if ($('#ExpectedFilling').val() != '')
        $('#ExpectedFilling').attr('readonly', true)
    
   
});
function ExpiriesValueChange(ele, index) {

    ele.value = (ele.value == undefined || ele.value == '') ? 0 : ele.value;
    Expiries_Yearwise_Data[index] = ele.value;
    RenderFinanceProjection();
    if (ExpiriestxtboxFocusId != '') {
      //  console.log('This is onchnage: ' + ExpiriestxtboxFocusId);
        $('#' + ExpiriestxtboxFocusId).focus();
       // ExpiriestxtboxFocusId = '';
    }

   if (index == 0) {
        TabKeyPressed_controlID = 'ProjectionExpiries_0';
    }
   
    $(".Expiriestxtbox").on('keydown', function (e) {
       var keyCode = e.keyCode || e.which;
        if (keyCode == 9) {
            TabKeyPressed_controlID = $(this).attr('id');
           // alert('Tab Pressed1');
        }
    });
    if (TabKeyPressed_controlID != '' && TabKeyPressed_controlID != 'ProjectionExpiries_9') {
        var nextcontrol = $('#' + TabKeyPressed_controlID).parent().next().find('.Expiriestxtbox');
        nextcontrol.focus().select();
        //TabKeyPressed_controlID = '';
    }

}
function AnnualconfirmatoryValueChange(ele, index) {
    ele.value = (ele.value == undefined || ele.value == '') ? 0 : ele.value;
    AnnualConfirmatoryRelease_Data[index] = ele.value;
    RenderFinanceProjection();  
    if (AnnualConfirmatoryRtxtboxFocusId != '') {
        $('#' + AnnualConfirmatoryRtxtboxFocusId).focus();
       // console.log('This is OnChnage: ' + AnnualConfirmatoryRtxtboxFocusId);
       // AnnualConfirmatoryRtxtboxFocusId = '';
    }  

    if (index == 0) {
        TabKeyPressed_controlAnnualConf = 'ProjectionAnnualConfirmatoryRelease_0';
    }
 $(".AnnualConfirmatoryRtxtbox").on('keydown', function (e) {
        var keyCode = e.keyCode || e.which;
        if (keyCode == 9) {
            TabKeyPressed_controlAnnualConf = $(this).attr('id');
            // alert('Tab Pressed1');
        }
    });
    if (TabKeyPressed_controlAnnualConf != '') {
        var nextcontrol = $('#' + TabKeyPressed_controlAnnualConf).parent().next().find('.AnnualConfirmatoryRtxtbox');
        nextcontrol.focus().select();
        //TabKeyPressed_controlAnnualConf = '';
    }
}

function ValidateSKU_dropdown(){
    var ArrayofInvalid = [];
    var ArrayofZeroInvalidValues = [];
    var isValidIPDForm = true;
    $('.batch-size-coating-table').find("select").each(function () {
        var selectedval =   $(this).val()
        if (selectedval == 0) {
            ArrayofInvalid.push($(this).attr('id'));
            $(this).focus();
            $(this).css("border-color", "red");
            isValidIPDForm = false;
            //toastr.error(ExpirydateErrorMsg);
        }
        else {
            $(this).css("border-color", "");
        }
    });
    $('.batch-size-coating-table').find(".Yield").each(function () {
        var selectedval = parseFloat($(this).val());
        if (selectedval.toFixed(3) == '0.000') {
            ArrayofZeroInvalidValues.push($(this).attr('id'));
            $(this).focus();
            $(this).css("border-color", "red");
            isValidIPDForm = false;
           //toastr.error('Yield should not have "0" Value !');
        }
        else {
            $(this).css("border-color", "");
           
        }
    });
    $('.batch-size-coating-table').find(".Batchsize").each(function () {
        var selectedval = parseFloat($(this).val());
        if (selectedval.toFixed(3) == '0.000') {
            ArrayofZeroInvalidValues.push($(this).attr('id'));
            $(this).focus();
            $(this).css("border-color", "red");
            isValidIPDForm = false;
           // toastr.error('Batchsize should not have "0" Value !');
        }

        else {
            $(this).css("border-color", "");            
        }
    });
    if (ArrayofZeroInvalidValues.length > 0) {
        isValidIPDForm = false;
        toastr.error('Yield or Batchsize should not have "0" Value !');
    }
    if (ArrayofInvalid.length > 0) {
        isValidIPDForm = false;
        toastr.error('Some required fields are missing!',"Batch Size Costing Validation");
    }
    return isValidIPDForm;
}

function UpdateProjectionCommercial(){
    RenderCommercialPerPack();
    RenderFinanceProjection();
}
$('.UpdateProjectionCommercial').on('change', function () {
    UpdateProjectionCommercial();
});
function GetSelectedMSPercent()
{
    $("select#MSPersentage option").each(function (index, value) {
        if (this.value === selectedMsId) {
            $("select#MSPersentage").prop('selectedIndex', index);
            return;
        }
    });
}
function GetSelectedTargetPriceScanario() {
    $("select#TargetPriceScenario option").each(function (index, value) {
        if (this.value === selectedTargetPriceScenario) {
            $("select#TargetPriceScenario").prop('selectedIndex', index);
            return;
        }
    });
}
$("#btnApprove").click(function () {
    $('#ApproveModel').modal('show');
    $('#SaveType').val('approved');
    if ($("#HfStatusRemark").val() == null || $("#HfStatusRemark").val() == "") {
        preventSubmit();
    }
})
$("#btnRejects").click(function () {
    $("#RejectModel").modal('show');
    $('#SaveType').val('rejected');
    if ($("#HfStatusRemark").val() == null || $("#HfStatusRemark").val() == "") {
        preventSubmit();
    }
})
function GetCurrencyList() {
      //ajaxServiceMethod($('#hdnBaseURL').val() + AllCurrency, 'GET', GetCurrencyListSuccess, GetCurrencyListError);
        ajaxServiceMethod($('#hdnBaseURL').val() + "api/Currency/GetCurrencyByLoggedInUser", 'GET', GetCurrencyListSuccess, GetCurrencyListError);
}

function GetCurrencyListSuccess(data) {
    try {
        $('#Currency').html('')
        let optionhtml = '';//'<option value = "-1">Select All</option>';
        $.each(data._object, function (index, object) {
            let currencyText = object.currencyCode == null ? object.currencyName : object.currencyCode + "-" + object.currencyName;
            optionhtml += '<option value="' +
                object.currencyId + '" title="' + object.currencyCode+'">' + currencyText + '</option>';
        });
        $("#Currency").append(optionhtml);

        let arrCur = JSON.parse(JSON.stringify(selectedCurrencyId.split(',')));
        $('select#Currency').select2(
            {
                placeholder: "Select Currency..",
                allowClear: true,
                dropdownAdapter: $.fn.select2.amd.require('select2/selectAllAdapter') 
            }
        ).val(arrCur).trigger('change');
        var data = $('#Currency').select2('data');
        if (data) {
            for (var i = 0; i < data.length; i++) {
                if (i < data.length-1) { selectedCurrencyText += data[i].title + "/"; }
                if (i == data.length-1) { selectedCurrencyText += data[i].title; }
            }
        }
      
        GetFinancialProjectionYear(_selectedProjectStartDate);
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetCurrencyListError(x, y, z) {
    toastr.error(ErrorMessage);
}

//#region Get DosageForm List
function GetDosageFormList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllDosageForm, 'GET', GetDosageFormListSuccess, GetDosageFormListError);
}
function GetDosageFormListSuccess(data) {
    try {
        $('#DosageFrom').html('')
        let optionhtml = '<option value = "0">--Select--</option>';
        $.each(data._object, function (index, object) {
            optionhtml +='<option value="' +
                object.dosageFormId + '">' + object.dosageFormName + '</option>';
        });
        $("#DosageFrom").append(optionhtml);

        $("select#DosageFrom option").each(function (index,value) {
            if (this.value === selectedDosageFormId) {
                $("select#DosageFrom").prop('selectedIndex', index);
                return;
            }
        });
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetDosageFormListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion
function addRowFinanceDetails(j) {
    //if (validateDuplicateSKUs()) {
        j = $('.Skus').length;//+ 1;
        var table = $('#FinanceTableBoy');
        var node = $('#financeDetailsRow_0').clone(true);
        table.find('tr:last').after(node);
        table.find('tr:last').find("input").val("");
    table.find('tr:last').find("input").val("");
    table.find('tr:last').find('input[type = "number"]').val(0);
    SetChildRows();
    SetChildRowDeleteIcon();
    //}
}
function SaveClick() {
    $('.readOnlyUpdate').prop('required', true);
    $("#ForecastDate").prop('required', true);
    //if ($('.readOnlyUpdate').val() !== null && $('.readOnlyUpdate').val()!=="") {
    var isValidFinanaceForm = ValidateSKU_dropdown();
    if (isValidFinanaceForm) {

        $('#SaveType').val('submit');
        $("#Currencyid").val($("#Currency").val().join(','));
        /*$("#HfStatusRemark").val("Submitted");*/
        SetChildRows();
        $('#hdnSelectedBussinesUnit').val(_encBuid);
        SetProjectionDynamicValues();
        //}
        //else {
        //    preventSubmit();
        //}
    }
    return isValidFinanaceForm;
}
function SetProjectionDynamicValues() {
    var Project_txtbox_ControlArray = [];
    for (var i = 0; i < 10; i++) {
        var controlExpiries = $('#ProjectionExpiries_' + i);
        var controlAnnualConfirmatory = $('#ProjectionAnnualConfirmatoryRelease_' + i);

        var _Project_txtbox_Object = new Object();
        _Project_txtbox_Object.AnnualConfirmatoryRelease = controlAnnualConfirmatory.val();
        _Project_txtbox_Object.Expiries = controlExpiries.val();
        _Project_txtbox_Object.Year = Global_Projection_Year_data[i];
        _Project_txtbox_Object.BusinessUnitId = _encBuid;
        _Project_txtbox_Object.PIDFID = pidfId;
        Project_txtbox_ControlArray.push(_Project_txtbox_Object);
    }
    $('#hdnDynamicControlData').val(JSON.stringify(Project_txtbox_ControlArray));
}
function SaveDraftClick() {
    //if ($('.readOnlyUpdate').val() !== null && $('.readOnlyUpdate').val() !== "") {
    $('.readOnlyUpdate').prop('required', false);
    $("#ForecastDate").prop('required', true);
    $('#SaveType').val('draft');
    //let selectedCurrencyId = $("#Currency").val().join(',');
    $("#Currencyid").val($("#Currency").val().join(','));
    /*$("#HfStatusRemark").val("SavedAsDraft");*/
    SetChildRows();
    $('#hdnSelectedBussinesUnit').val(_encBuid);
    SetProjectionDynamicValues();    
    //}
    //else {
    //    preventSubmit();
    //}
   // $('.readOnlyUpdate').removeAttr('required');
   // grantSubmit();
}
function ApproveClick() {
    //if ($('.readOnlyUpdate').val() !== null && $('.readOnlyUpdate').val() !== "") {
    $('#SaveType').val('approved');
    $("#Currencyid").val($("#Currency").val().join(','));
    $("#HfStatusRemark").val($("#textApproveStatusRemark").val());
        SetChildRows();
    //}
    //else {
    //    preventSubmit();
    //}
    grantSubmit();
}
function RejectClick() {
    //if ($('.readOnlyUpdate').val() !== null && $('.readOnlyUpdate').val() !== "") {
    $('#SaveType').val('rejected');
    $("#Currencyid").val($("#Currency").val().join(','));
    $("#HfStatusRemark").val($("#textRejectStatusRemark").val());
        SetChildRows();
    //}
    //else {
    //    preventSubmit();
    //}
    grantSubmit();
}
function SetChildRows() {
    $.each($('#FinanceTableBoy tr'), function (index, value) {
        
        $(this).find("td:eq(0) select").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Skus");
        $(this).find("td:eq(0) select").attr("id", "Skus" + index.toString());
        $(this).find("td:eq(0) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PidffinaceId");
        $(this).find("td:eq(0) input").attr("id", "PidffinaceId" + index.toString());
        $(this).find("td:eq(1) select").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PakeSize");
        $(this).find("td:eq(1) select").attr("id", "PakeSize" + index.toString());
        $(this).find("td:eq(2) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PackSizeValue");
        $(this).find("td:eq(2) input").attr("id", "PackSizeValue" + index.toString());
        $(this).find("td:eq(3) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].BrandPrice");
        $(this).find("td:eq(3) input").attr("id", "BrandPrice" + index.toString());
        $(this).find("td:eq(4) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].GenericListprice");
        $(this).find("td:eq(4) input").attr("id", "GenericListprice" + index.toString());
        $(this).find("td:eq(5) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].NetRealisation");
        $(this).find("td:eq(5) input").attr("id", "NetRealisation" + index.toString());
        
        $(this).find("td:eq(6) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].EstMat2020By12units");
        $(this).find("td:eq(6) input").attr("id", "EstMat2020By12units" + index.toString());
       
        $(this).find("td:eq(7) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Marketinpacks");
        $(this).find("td:eq(7) input").attr("id", "Marketinpacks" + index.toString());
        $(this).find("td:eq(8) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].BatchsizeinLtrTabs");
        $(this).find("td:eq(8) input").attr("id", "BatchsizeinLtrTabs" + index.toString());
        $(this).find("td:eq(9) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Batchsize");
        $(this).find("td:eq(9) input").attr("id", "Batchsize" + index.toString());
        $(this).find("td:eq(10) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Yield");
        $(this).find("td:eq(10) input").attr("id", "Yield" + index.toString());
        $(this).find("td:eq(11) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Batchoutput");
        $(this).find("td:eq(11) input").attr("id", "Batchoutput" + index.toString());
        $(this).find("td:eq(12) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].ApiCad");
        $(this).find("td:eq(13) input").attr("id", "ApiCad" + index.toString());
        $(this).find("td:eq(13) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].ExcipientsCad");
        $(this).find("td:eq(13) input").attr("id", "ExcipientsCad" + index.toString());
        $(this).find("td:eq(14) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PmCad");
        $(this).find("td:eq(14) input").attr("id", "PmCad" + index.toString());
        $(this).find("td:eq(15) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].CcpcCad");
        $(this).find("td:eq(15) input").attr("id", "CcpcCad" + index.toString());
        $(this).find("td:eq(16) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].FreightCad");
        $(this).find("td:eq(16) input").attr("id", "FreightCad" + index.toString());
        $(this).find("td:eq(17) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].EmcureCogsPack");
        $(this).find("td:eq(17) input").attr("id", "EmcureCogsPack" + index.toString());
        $(this).find("td:eq(18) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PidffinaceBatchSizeCoatingId");
        $(this).find("td:eq(18) input").attr("id", "PidffinaceBatchSizeCoatingId" + index.toString());
       
       // console.log($(this).find("td:eq(0) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PidffinaceBatchSizeCoatingId").value())
    });
}
function SetChildRowDeleteIcon() {
    if ($('tbody#FinanceTableBoy tr').length > 1) {
        $('.del-rows').show();
    } else {
        $('.del-rows').hide();
    }
    //$("#FinanceTable tbody tr:first").find('.del-rows').hide();
}
function deleteRowFinance(j, element, id) {
    let deleteableId = id;
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
    SetChildRows();
    if (deleteableId > 0) {
        let deletehtml = `<input type="hidden" id="DeletePidffinaceBatchSizeCoatingId${j}" name="lsPidfFinanceBatchSizeCoating[${j}].DeletePidffinaceBatchSizeCoatingId" value="${deleteableId}">`;
        $("#FinanceTableBoy").append(deletehtml);
    }
    UpdateProjectionCommercial();
}
function preventSubmit() {
    $(document).on('submit', 'form', function (e) {
        e.preventDefault();
        //your code goes here
        //toastr.error('Error:required input fields could not be empty');
        //100% works
        return;
    });
}
function grantSubmit() {
    $(document).on('submit', 'form', function (e) {
        //e.preventDefault();
        //your code goes here
        //toastr.error('Error:required input fields could not be empty');
        //100% works
        //return;
        
    });
}

function calculateDealTerm(id) {
    let totaldealterm = 0;
    $('.deal-term').each(function () {
        const inputValue = $(this).val();
        const numericValue = parseInt(inputValue.replace(/,/g, ''));
        if (!isNaN(numericValue) && numericValue > 0) {
            totaldealterm += numericValue;
        }
    });
    $("#spanTotal").text(totaldealterm);
    formatCurrencyInElements('format-currency');
}

function calculateBatchSizeCaoting(ele) {
    let netRealisation = 0;
    let EstMat2020By12units = 0;
    let BatchsizeinLtrTabs = 0;
    let Batchsize = 0;
    let Yield = 0;
    let Batchoutput = 0;
    let sumforEmcureCogsPack = 0;
    let ApiCad = 0;
    let ExcipientsCad = 0;
    let PmCad = 0;
    let CcpcCad = 0;
    let FreightCad = 0;
    let packSize = 0;
    let PackSizeValue = 0;    
    let strengthId = 0;
    $.each($('#FinanceTableBoy tr'), function (index, value) {

        strengthId = $(this).find("select.Skus").val();
        PackSizeValue = $(this).find("input.PackSizeValue").val();
        BatchsizeinLtrTabs = $(this).find("input.BatchsizeinLtrTabs").val();

        $(this).find("input.Batchsize").val((parseFloat(BatchsizeinLtrTabs) / parseFloat(PackSizeValue)).toFixed(2));

        Batchsize = $(this).find("input.Batchsize").val();
        Yield = $(this).find("input.Yield").val();

        $(this).find("input.Batchoutput").val(((parseFloat(Batchsize == "" ? 0 : Batchsize) * parseFloat(Yield == "" ? 0 : Yield)) / 100).toFixed(2));
        ApiCad = $(this).find("input.API_CAD").val();
        ExcipientsCad = $(this).find("input.Excipients_CAD").val();
        PmCad = $(this).find("input.PM_CAD").val();
        CcpcCad = $(this).find("input.CCPC_CAD").val();
        FreightCad = $(this).find("input.Freight_CAD").val();

        sumforEmcureCogsPack = parseFloat(ApiCad == "" ? 0 : ApiCad) + parseFloat(ExcipientsCad == "" ? 0 : ExcipientsCad) + parseFloat(PmCad == "" ? 0 : PmCad) + parseFloat(CcpcCad == "" ? 0 : CcpcCad) + parseFloat(FreightCad == "" ? 0 : FreightCad);

        $(this).find("input.EmcureCOGs_pack").val(sumforEmcureCogsPack.toFixed(2));
        UpdateProjectionCommercial();
        

        //strengthId = $(this).find("td:eq(0) select option:selected").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Skus").val();
        ////getPackSize(strengthId);
        //packSize = $(this).find("td:eq(1) select option:selected").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PakeSize").val();
        ////$(this).find("td:eq(1) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PakeSize").val(packSize);
        //if (ele.valueAsNumber >= 0 && $(this).find("td:eq(3) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].GenericListprice").val() == ele.value) {
        //    netRealisation = (ele.valueAsNumber * 40) / 100;
        //    let textnetRealisation = netRealisation.toLocaleString("en");
        //    $(this).find("td:eq(4) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].NetRealisation").val(textnetRealisation);
        //}
        //if ($(this).find("td:eq(5) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].EstMat2020By12units").val() > 0) {
        //    EstMat2020By12units = parseFloat($(this).find("td:eq(5) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].EstMat2020By12units").val());
        //    let textEstMat2020By12units = EstMat2020By12units.toLocaleString("en");
        //    $(this).find("td:eq(6) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Marketinpacks").val(textEstMat2020By12units);
        //}
        //if ($(this).find("td:eq(7) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].BatchsizeinLtrTabs").val() > 0) {
        //    BatchsizeinLtrTabs = $(this).find("td:eq(7) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].BatchsizeinLtrTabs").val();
        //    Batchsize = parseInt(BatchsizeinLtrTabs) / parseInt(packSize); //parseFloat($(this).find("td:eq(0) select option:selected").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Skus").text().replace("mg", "").trim())
        //    $(this).find("td:eq(8) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Batchsize").val(parseInt(Batchsize));
        //}
        //if ($(this).find("td:eq(9) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Yield").val() > 0) {
        //    Yield = $(this).find("td:eq(9) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Yield").val();
        //    Batchoutput = parseFloat(Yield) * parseFloat(packSize); //parseFloat($(this).find("td:eq(0) select option:selected").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Skus").text().replace("mg", "").trim())
        //    $(this).find("td:eq(10) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Batchoutput").val(Batchoutput);
        //}
        //    ApiCad = $(this).find("td:eq(11) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].ApiCad").val();
        //    ExcipientsCad = $(this).find("td:eq(12) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].ExcipientsCad").val();
        //    PmCad = $(this).find("td:eq(13) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PmCad").val();
        //    CcpcCad = $(this).find("td:eq(14) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].CcpcCad").val();
        //    FreightCad = $(this).find("td:eq(15) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].FreightCad").val();
        //    sumforEmcureCogsPack = parseInt(Batchsize) + parseInt(Yield) + parseInt(Batchoutput) + parseInt(ApiCad) + parseInt(ExcipientsCad) + parseInt(PmCad) + parseInt(CcpcCad) + parseInt(FreightCad);
        //    $(this).find("td:eq(16) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].EmcureCogsPack").val(sumforEmcureCogsPack);
       // });
    })
}
function GetSkus(pidfId) {
    
    ajaxServiceMethod($('#hdnBaseURL').val() + `api/PidfFinance/GetStrengthByPIDFAnddBuId/${pidfId}/${_encBuid}`, 'GET', GetSkusListSuccess, GetSkusListError);
    function GetSkusListSuccess(data) {
        try {
            let arrselectedSKUs = selectedSKUs.split(',');
            if (arrselectedSKUs[0]!='' && arrselectedSKUs.length > 0) {
                $('select.DbSkus').html('')
                let optionhtml = '<option value = "0" selected="selected">--Select--</option>';
                $.each(data.table, function (index, object) {
                    optionhtml += '<option value="' +
                        object.pidfProductStrengthId + '">' + object.strength + object.unitofMeasurementName + '</option>';
                    
                });
                $("select.DbSkus").append(optionhtml);
                arrselectedSKUs.forEach(function (val, i) {
                    if (val == arrselectedSKUs[i]) {
                        $(`select#DbSkus${i}.Skus.DbSkus`).val(val);
                        getEditPackSize(val, i);
                        $(`input#BrandPrice${i}.BrandPrice`).attr("readonly", true);
                        $(`input#GenericListprice${i}.GenericListprice`).attr("readonly", true);
                        $(`input#NetRealisation${i}.NetRealisation`).attr("readonly", true);
                        $(`input#EstMat2020By12units${i}.EstMat2020By12units`).attr("readonly", true);
                        $(`input#Marketinpacks${i}.Marketinpacks`).attr("readonly", true);
                        $(`input#PackSizeValue${i}.PackSizeValue`).attr("readonly", true);
                        $(`input#BatchsizeinLtrTabs${i}.BatchsizeinLtrTabs`).attr("readonly", true);
                        
                    }
                });
                
            }
            else {
                $('select#Skus').html('')
                let optionhtml = '<option value = "0">--Select--</option>';
                $.each(data.table, function (index, object) {
                    optionhtml += '<option value="' +
                        object.pidfProductStrengthId + '" data=' + object.packSizeId + '>' + object.strength + 'mg </option>';
                });
                $("select#Skus").append(optionhtml);
            }
           
        } catch (e) {
            toastr.error('Error:' + e.message);
        }
    }
    function GetSkusListError() {
        toastr.error("Error");
    }
   
}
function getPackSize(ele) {
    if (ele.value == undefined) {
        toastr.error("Please select SKU first", "ERROR:")
        return false;
    }
    ajaxServiceMethod($('#hdnBaseURL').val() + `api/PidfFinance/GetPackSizeByStrengthId/${pidfId}/${_encBuid}/${ele.value}`, 'GET', getPackSizeSuccess, getPackSizeError);
    function getPackSizeSuccess(data) {
        try {
            
            var row_index = $(ele).closest('tr').index();
         
                $(`select#PakeSize${row_index}.PakeSize`).html('')
                let optionhtml = '<option value = "0">--Select--</option>';
                $.each(data.table, function (index, object) {
                    optionhtml += '<option value="' +
                        object.packSizeId + '" data=' + object.packSize+ '>' + object.packSizeName + ' </option>';
                    
                    //calculateBatchSizeCaoting(ele);
                   
                });
            let arrselectedPackSize = _selectedPackSize.split(',');
            if (arrselectedPackSize[0] != '' && arrselectedPackSize.length > 0) {
                arrselectedPackSize.forEach(function (val, i) {
                    if (val == arrselectedPackSize[i]) {
                        $(`select#PakeSize${i}.PakeSize option:selected`).val(val);

                    }
                });
            }
            $(`select#PakeSize${row_index}.PakeSize`).append(optionhtml);
            //validateDuplicateSKUs();
            RenderCommercialPerPack();
            RenderFinanceProjection();
        }
         catch (e) {
            toastr.error('Error:' + e.message);
        }
    }
    function getPackSizeError() {
        toastr.error("Error");
    }
}

function getEditPackSize(strengthId,rowIndex) {
   
    ajaxServiceMethod($('#hdnBaseURL').val() + `api/PidfFinance/GetPackSizeByStrengthId/${pidfId}/${_encBuid}/${strengthId}`, 'GET', getPackSizeSuccess, getPackSizeError);
    function getPackSizeSuccess(data) {
        try {

            /*var row_index = $(ele).closest('tr').index();*/

            $(`select#PakeSize${rowIndex}.PakeSize`).html('')
            let optionhtml = '<option value = "0">--Select--</option>';
            $.each(data.table, function (index, object) {
                optionhtml += '<option value="' +
                    object.packSizeId + '" data=' + object.packSize+ '>' + object.packSizeName + ' </option>';
            });
            
            $(`select#PakeSize${rowIndex}.PakeSize`).append(optionhtml);
            let arrselectedPackSize = _selectedPackSize.split(',');
            if (arrselectedPackSize[0] != '' && arrselectedPackSize.length > 0) {
                arrselectedPackSize.forEach(function (val, i) {
                    if (val == arrselectedPackSize[i]) {
                        $(`select#PakeSize${i}.PakeSize`).val(val);
                        $(`select#PakeSize${i}.PakeSize`).find('option[value="' + val + '"]').prop('selected',true).trigger("change");
                    }
                });
            }

        }
        catch (e) {
            toastr.error('Error:' + e.message);
        }
    }
    function getPackSizeError() {
        toastr.error("Error");
    }
}

function GetSUIMSVolumeYearWiseByPackSize(ele) {
    var row_index = $(ele).closest('tr').index();
    //skuElements = row_index == 0 ? $(ele).val() : $(`select#Skus${row_index}.Skus`).val();
    //let packSizeId = $(`select#PakeSize${row_index}.PakeSize option:selected`).attr('value');
    //let strengthId = $(`select#Skus${row_index}.Skus.DbSkus option:selected`).val() == undefined ? skuElements : $(`select#Skus${row_index}.Skus.DbSkus option:selected`).val();

    let packSizeId = $(ele).closest('tr').find("select.PakeSize option:selected").attr("value");
    let strengthId =$(ele).closest('tr').find("select.Skus").val();
    validateDuplicateSKUs();    
    if (isValidSku) {
      if (packSizeId > 0 && strengthId > 0) {
            ajaxServiceMethod($('#hdnBaseURL').val() + `api/PidfFinance/GetSUIMSVolumeYearWiseByPackSize/${pidfId}/${_encBuid}/${strengthId}/${packSizeId}`, 'GET', SUIMSVolumeYearWiseByPackSizeSuccess, SUIMSVolumeYearWiseByPackSizeError);
            function SUIMSVolumeYearWiseByPackSizeSuccess(data) {
                try {
                    $(`input#BrandPrice${row_index}.BrandPrice`).val((data.table.length > 0 ? parseFloat(data.table[0].brandPrice) : 0).toFixed(2));
                    $(`input#GenericListprice${row_index}.GenericListprice`).val((data.table.length > 0 ? parseFloat(data.table[0].genericPrice) : 0).toFixed(2));
                    let netRealisation = parseFloat((data.table.length > 0 ? data.table[0].genericPrice : 0)) * parseFloat((data.table.length > 0 ? data.table[0].priceDiscounting : 0)) / 100;
                    $(`input#NetRealisation${row_index}.NetRealisation`).val(netRealisation.toFixed(2));
                    $(`input#EstMat2020By12units${row_index}.EstMat2020By12units`).val(parseFloat((data.table.length > 0 ? data.table[0].suimsVolume : 0)).toFixed(2));
                    $(`input#Marketinpacks${row_index}.Marketinpacks`).val(parseFloat((data.table.length > 0 ? data.table[0].suimsVolume : 0)).toFixed(2));
                    $(`input#BatchsizeinLtrTabs${row_index}.BatchsizeinLtrTabs`).val(parseFloat((data.table.length > 0 ? data.table[0].commercialBatchSize : 0)).toFixed(2));  
                      
                    $(`input#PackSizeValue${row_index}.PackSizeValue`).val((data.table.length > 0 ? data.table[0].packSizeValue : 0));
                    
                    calculateBatchSizeCaoting(ele);

                    $(ele).closest('tr').find("#hdnMSLow").val((data.table.length > 0 ? data.table[0].marketSharePercentageLow : 0).toFixed(2));
                    $(ele).closest('tr').find("#hdnMSMid").val((data.table.length > 0 ? data.table[0].marketSharePercentageMedium : 0).toFixed(2));
                    $(ele).closest('tr').find("#hdnMSHigh").val((data.table.length > 0 ? data.table[0].marketSharePercentageHigh : 0).toFixed(2));
                    $(ele).closest('tr').find("#hdnNSPLow").val((data.table.length > 0 ? data.table[0].nspUnitsLow : 0).toFixed(2));
                    $(ele).closest('tr').find("#hdnNSPMid").val((data.table.length > 0 ? data.table[0].nspUnitsMedium : 0).toFixed(2));
                    $(ele).closest('tr').find("#hdnNSPHigh").val((data.table.length > 0 ? data.table[0].nspUnitsHigh : 0).toFixed(2));

                    if (data.table2.length > 0) {
                        CommcercialBU_NPS_MS_Data = data.table2;
                    }
                    if (data.table1.length>0)
                        UpdateDynamicTextBoxValues(data.table1);

                    RenderCommercialPerPack();
                    RenderFinanceProjection();
                }
                catch (e) {
                    toastr.error('Error:' + e.message);
                }
            }
            function SUIMSVolumeYearWiseByPackSizeError() {
                toastr.error("Error");
            }
        }
    }
}


//$('select#Currency').change(function () {
//    var data = $('#Currency').select2('data');
//    if (data) {
//        for (var i = 0; i < data.length; i++) {
//            if (i < data.length - 1) { selectedCurrencyText += data[i].title + "/"; }
//            if (i == data.length - 1) { selectedCurrencyText += data[i].title; }
//        }
//    }

//    GetFinancialProjectionYear(_selectedProjectStartDate);
//})
$("select#Currency").on("select2:select select2:unselecting", function (e) {
    selectedCurrencyText = ""; 
    $(".tdCurrency").text(selectedCurrencyText);
    let event = e;
    if (event.params._type == "unselecting") {
       
        var data = $('#Currency').select2('data');
        if (data) {
            data.pop(event.params.args.data.id);
            for (var i = 0; i < data.length; i++) {
                if (i < data.length - 1) { selectedCurrencyText += data[i].title + "/"; }
                if (i == data.length - 1) { selectedCurrencyText += data[i].title; }
            }
        }
        else {
            $(".tdCurrency").text("");
        }
    }
    else if (event.params._type == "select") {
       
        var data = $('#Currency').select2('data');
        if (data) {
            for (var i = 0; i < data.length; i++) {
                if (i < data.length - 1) { selectedCurrencyText += data[i].title + "/"; }
                if (i == data.length - 1) { selectedCurrencyText += data[i].title; }
            }
        }
        else {
            $(".tdCurrency").text("");
        }
    }
    GetFinancialProjectionYear(_selectedProjectStartDate);
});


function validateDuplicateSKUs() {
    let packSize = [];
    let strengthId = [];
    $.each($('#FinanceTableBoy tr'), function (index, value) {
        if ($('#FinanceTableBoy tr').length > 1) {
            strengthId.push($(this).find("td:eq(0) select option:selected").attr("name", "lsPidfFinanceBatchSizeCoating[" + parseInt(index).toString() + "].Skus").val());
            packSize.push($(this).find("td:eq(1) select option:selected").attr("name", "lsPidfFinanceBatchSizeCoating[" + parseInt(index).toString() + "].PakeSize").val())
            if (strengthId.includes($(`select#Skus${index + 1}`).val()) && packSize.includes($(`select#PakeSize${index + 1}`).val())) {
                $(`select#Skus${index + 1}`).prop('selectedIndex', 0);
                $(`select#PakeSize${index + 1}`).val(0);
                $(`#BrandPrice${index + 1}`).val(null);
                $(`#GenericListprice${index + 1}`).val(null);
                $(`#NetRealisation${index + 1}`).val(null);
                $(`#EstMat2020By12units${index + 1}`).val(null);
                $(`#Marketinpacks${index + 1}`).val(null);
                toastr.error("duplicate SKU and PackSize not allowed", "Error:");
                isValidSku = false;
                return false;
            }
        }
        isValidSku = true;
        return isValidSku;
    });
   // return false;
}
//$("i.fas.fa-plus").click(function () {
//    let count = 1;
//    for (let i = 0; i < count; i++) {
//        if (i == 0) { this.click(); }
//    }
//});
//$(".readOnlyUpdate").keyup(function () {
//    $(this).val($(this).val().replace(/ /g, ''));
//    if ($("#Entity").val().length == 1 && $(this).val()=="") {
//        $(this).attr("required", true)
//        $(this).removeClass("valid");
//    }

//});

function SetExpectedFillingBatchManufacturingDate(_projStartDate){
    var DateType = new Date(_projStartDate);
    var _StratDate = DateType; //new Date(DateType.getFullYear() + '-' + DateType.getDate() + '-' + (DateType.getMonth() + 1));
    var _BatchManufacturing = new Date(_StratDate.setDate(_StratDate.getDate() + 120));
    var _ExpectedFilling = new Date(_StratDate.setDate(_StratDate.getDate() + 90));
    $("#ExpectedFilling").val(moment(_ExpectedFilling).format("YYYY-MM-DD"));
    $("#BatchManufacturing").val(moment(_BatchManufacturing).format("YYYY-MM-DD"));
    GetFinancialProjectionYear(_projStartDate)
    UpdateProjectionCommercial();
}

function GetFinancialProjectionYear(dates) {
    $(".trProjectionYear").empty();
    //selectedCurrencyText = $(`#Currency option:selected`).text().split('-')[0];       new Date('2023-05-11')
  
    let td = `<td class="tdCurrency">${selectedCurrencyText}</td>`;
    for (var i = 0; i < 10; i++) {
        if (i == 0) {
            td += `<td>Mar-${getYearByLast3Months(dates)}</td>`;
        }
        else {
            td += `<td>Mar-${parseInt(getYearByLast3Months(dates)) + i
        }</td >`;
        }
    }
    //alert(getYearByLast3Months(ele.value))
    $(".trProjectionYear").append(td);
}
function getYearByLast3Months(date) {

    const today = new Date(date);
    let lastSixMonths = []

    for (var i = 10; i > 0; i -= 1) {
        if (i == 3) {
            const date = new Date(today.getFullYear(), today.getMonth() - i, 1);
            lastSixMonths.push(moment(date).format("YY"))
        }
    }

    return lastSixMonths.reverse() 
}

$(el).focus(function () {
    $(this).attr("step","any");
    let num = this.value;
    
    if (num.toString() == "E" || num.toString() == "e") {
        this.value = null;
    }
    if (num.toString() != "") {
        this.value = parseFloat(num).toFixed(2);
    }
});
$(el).focusout(function () {
    $(this).attr("step", "any");
    let num = this.value;
    if (num.toString() == "E" || num.toString() == "e") {
        this.value = null;
    }
    if (num.toString() == "") {
        this.value = null;
        $(this).attr("required", true);
    }
    if (num.toString()!="") {
        this.value = parseFloat(num).toFixed(2);
    }
});
(function () {
    'use strict'

    //// Fetch all the forms we want to apply custom Bootstrap validation styles to
   var forms = document.querySelectorAll('.needs-validation')

   // Loop over them and prevent submission
   Array.prototype.slice.call(forms)
       .forEach(function (form) {
           form.addEventListener('submit', function (event) {
               $(".readOnlyUpdate").removeClass("valid");
               if (!form.checkValidity()) {
                   event.preventDefault()
                   event.stopPropagation()
               }
               else if ($("select#Currency").val() == "") {
                   event.preventDefault()
                   event.stopPropagation()
                  // toastr.error("please select currency", "Required");
                   $("select#Currency").siblings(".select2-container").css('border', '0.5px solid red');
                   $("select#Currency").focus();
               }
               else if ($("select#Currency").val() != "") {
                   $("select#Currency").siblings(".select2-container").css('border', '0px');
               }
                if ($("select#DosageFrom.form-control.readOnlyUpdate").val() == 0) {
                    $("#DvDosageFrom").css("border", "0.5px solid red");
                    $("#select#DosageFrom.form-control.readOnlyUpdate").focus();
                    event.preventDefault()
                    event.stopPropagation()
                    //toastr.error("please select DosageFrom", "Required");

               }
               
               $(".valid-feedback").hide();
               form.classList.add('was-validated')
           }, false)
       })
})()


function loadFinanceProjectionData(pidfid, encBUId, buid){
    _encBuid = encBUId;
    SelectedTabBU_Finance = buid;
    BussinesUnitInterestedFinance(pidfid, buid, 'Finance');

    RenderCommercialPerPack();
    RenderFinanceProjection();

}
function GetBatchSizeCostingTRValues() {
    var Arr_FinanceTable_tr = [];
    $('#FinanceTableBoy tr').each(function (index, value) {
        var trObj =
        {
            SKU: $(this).find("select.Skus option:selected").text(),
            PackSize: $(this).find("select.PakeSize option:selected").text(),

            hdnMSLow: $(this).find("#hdnMSLow").val(),
            hdnMSMid: $(this).find("#hdnMSMid").val(),
            hdnMSHigh: $(this).find("#hdnMSHigh").val(),

            marketInPacks: $(this).find('.Marketinpacks').val(),

            hdnNSPLow: $(this).find("#hdnNSPLow").val(),
            hdnNSPMid: $(this).find("#hdnNSPMid").val(),
            hdnNSPHigh: $(this).find("#hdnNSPHigh").val(),

            emcureCOGs_pack: $(this).find(".EmcureCOGs_pack").val(),
            //netRealisation: $(this).find(".NetRealisation").val()

        }
        Arr_FinanceTable_tr.push(trObj);

    });
    return Arr_FinanceTable_tr;
}

$('.clsProjectTimeline').change(function () {

    
    let _appPeriodDays = isNaN($('#ApprovalPeriodinDays').val()) ? 0 : parseInt($('#ApprovalPeriodinDays').val());

    let _expDate = $('#ExpectedFilling').val();
    let _ExpectedFillingDate = new Date(_expDate);
    _expDate = _ExpectedFillingDate;
    _expDate.setDate(_ExpectedFillingDate.getDate() + _appPeriodDays);

    var resultDate = _expDate.toISOString().split('T')[0];
    $('#ApprovalDate').val(resultDate);


    _expDate.setDate(_ExpectedFillingDate.getDate() + _appPeriodDays+60);

    resultDate = _expDate.toISOString().split('T')[0];
    $('#ProductLaunchDate').val(resultDate);
    


});



