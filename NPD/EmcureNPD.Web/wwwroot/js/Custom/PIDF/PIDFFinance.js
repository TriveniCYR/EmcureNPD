﻿let rowcount = 0;
let selectedCurrencyText = "";
let el = document.querySelectorAll('input[type="number"]');
let isValidSku = false;
$(document).ready(function () {
 /*   console.log("selectedSKUs" + selectedSKUs);*/
  
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
    }

    $("#Currency").select2({
        placeholder: "Select Currency..",
        allowClear: true
    });
    $(el).attr("step", "any");
   
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
        let optionhtml = ''; //'<option value = "0">--Select--</option>';
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
                allowClear: true
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
    //if ($('.readOnlyUpdate').val() !== null && $('.readOnlyUpdate').val()!=="") {
    $('#SaveType').val('submit');
    $("#Currencyid").val($("#Currency").val().join(','));
    /*$("#HfStatusRemark").val("Submitted");*/
        SetChildRows();
    //}
    //else {
    //    preventSubmit();
    //}
}
function SaveDraftClick() {
    //if ($('.readOnlyUpdate').val() !== null && $('.readOnlyUpdate').val() !== "") {
    $('#SaveType').val('draft');
    //let selectedCurrencyId = $("#Currency").val().join(',');
    $("#Currencyid").val($("#Currency").val().join(','));
    /*$("#HfStatusRemark").val("SavedAsDraft");*/
        SetChildRows();
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
        $(this).find("td:eq(2) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].BrandPrice");
        $(this).find("td:eq(2) input").attr("id", "BrandPrice" + index.toString());
        $(this).find("td:eq(3) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].GenericListprice");
        $(this).find("td:eq(3) input").attr("id", "GenericListprice" + index.toString());
        $(this).find("td:eq(4) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].NetRealisation");
        $(this).find("td:eq(4) input").attr("id", "NetRealisation" + index.toString());
        
        $(this).find("td:eq(5) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].EstMat2020By12units");
        $(this).find("td:eq(5) input").attr("id", "EstMat2020By12units" + index.toString());
       
        $(this).find("td:eq(6) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Marketinpacks");
        $(this).find("td:eq(6) input").attr("id", "Marketinpacks" + index.toString());
        $(this).find("td:eq(7) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].BatchsizeinLtrTabs");
        $(this).find("td:eq(7) input").attr("id", "BatchsizeinLtrTabs" + index.toString());
        $(this).find("td:eq(8) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Batchsize");
        $(this).find("td:eq(8) input").attr("id", "Batchsize" + index.toString());
        $(this).find("td:eq(9) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Yield");
        $(this).find("td:eq(9) input").attr("id", "Yield" + index.toString());
        $(this).find("td:eq(10) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Batchoutput");
        $(this).find("td:eq(10) input").attr("id", "Batchoutput" + index.toString());
        $(this).find("td:eq(11) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].ApiCad");
        $(this).find("td:eq(11) input").attr("id", "ApiCad" + index.toString());
        $(this).find("td:eq(12) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].ExcipientsCad");
        $(this).find("td:eq(12) input").attr("id", "ExcipientsCad" + index.toString());
        $(this).find("td:eq(13) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PmCad");
        $(this).find("td:eq(13) input").attr("id", "PmCad" + index.toString());
        $(this).find("td:eq(14) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].CcpcCad");
        $(this).find("td:eq(14) input").attr("id", "CcpcCad" + index.toString());
        $(this).find("td:eq(15) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].FreightCad");
        $(this).find("td:eq(15) input").attr("id", "FreightCad" + index.toString());
        $(this).find("td:eq(16) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].EmcureCogsPack");
        $(this).find("td:eq(16) input").attr("id", "EmcureCogsPack" + index.toString());
        $(this).find("td:eq(17) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PidffinaceBatchSizeCoatingId");
        $(this).find("td:eq(17) input").attr("id", "PidffinaceBatchSizeCoatingId" + index.toString());
       
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
        if ($(this).val() >0) {
            totaldealterm += parseInt($(this).val());
        }
    });
    $("#spanTotal").text(totaldealterm)
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
    let strengthId = 0;
    $.each($('#FinanceTableBoy tr'), function (index, value) {

        strengthId = $(this).find("select.Skus").val();
        packSize = $(this).find("select.PakeSize").val();
        BatchsizeinLtrTabs = $(this).find("input.BatchsizeinLtrTabs").val();

        $(this).find("input.Batchsize").val((parseFloat(BatchsizeinLtrTabs) / parseFloat(packSize)).toFixed(2));

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
                        object.packSize + '" data=' + object.packSizeId+ '>' + object.packSizeName + ' </option>';
                    
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
                    object.packSize + '" data=' + object.packSizeId+ '>' + object.packSizeName + ' </option>';
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
    //let packSizeId = $(`select#PakeSize${row_index}.PakeSize option:selected`).attr('data');
    //let strengthId = $(`select#Skus${row_index}.Skus.DbSkus option:selected`).val() == undefined ? skuElements : $(`select#Skus${row_index}.Skus.DbSkus option:selected`).val();

    let packSizeId = $(ele).closest('tr').find("select.PakeSize option:selected").attr("data");
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
                    $(`input#BatchsizeinLtrTabs${row_index}.BatchsizeinLtrTabs`).val((data.table.length > 0 ? data.table[0].commercialBatchSize : 0).toFixed(2));
                    calculateBatchSizeCaoting(ele);

                    $(ele).closest('tr').find("#hdnMSLow").val((data.table.length > 0 ? data.table[0].marketSharePercentageLow : 0).toFixed(2));
                    $(ele).closest('tr').find("#hdnMSMid").val((data.table.length > 0 ? data.table[0].marketSharePercentageMedium : 0).toFixed(2));
                    $(ele).closest('tr').find("#hdnMSHigh").val((data.table.length > 0 ? data.table[0].marketSharePercentageHigh : 0).toFixed(2));
                    $(ele).closest('tr').find("#hdnNSPLow").val((data.table.length > 0 ? data.table[0].nspUnitsLow : 0).toFixed(2));
                    $(ele).closest('tr').find("#hdnNSPMid").val((data.table.length > 0 ? data.table[0].nspUnitsMedium : 0).toFixed(2));
                    $(ele).closest('tr').find("#hdnNSPHigh").val((data.table.length > 0 ? data.table[0].nspUnitsHigh : 0).toFixed(2));

                    RenderCommercialPerPack();
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
function fnGetActiveBusinessUnit() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetActiveBusinessUnit, 'GET', GetActiveBusinessUnitSuccess, GetActiveBusinessUnitError);
}
function GetActiveBusinessUnitSuccess(data) {
    var businessUnitHTML = "";
    var businessUnitPanel = "";
    $.each(data._object, function (index, item) {
        let buClassName = item.businessUnitName.toLowerCase() ==='india'?'in': item.businessUnitName.toLowerCase();
        businessUnitHTML += '<li class="nav-item p-0">\
            <a class="nav-link '+ (item.businessUnitId == _selectBusinessUnit ? "active" : "") + ' px-2" href="#custom-tabs-' + buClassName + '" data-toggle="pill" aria-selected="true" onclick="loadFInanceProjectionData(' + _selectedPidfId + ',' + item.businessUnitId + ')" id="custom-tabs-two-' + item.businessUnitId + '-tab">' + item.businessUnitName + '</a></li>';
        businessUnitPanel += '<div class="tab-pane ' + ((item.businessUnitId == _selectBusinessUnit ? "fade show active" : "")) + '" id="custom-tabs-' + item.businessUnitId + '" role="tabpanel" aria-labelledby="custom-tabs-two-' + item.businessUnitId + '-tab"></div>';
    });
    $('#custom-tabs-business-tab').html(businessUnitHTML);
    
   // $('#custom-tabs-business-tabContent').html(businessUnitPanel);

   // LoadIPDForm(_PIDFID, _selectBusinessUnit);
}
function GetActiveBusinessUnitError(x, y, z) {
    toastr.error(ErrorMessage);
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
function GetFinancialProjectionYear(dates) {
    $(".trProjectionYear").empty();
    //selectedCurrencyText = $(`#Currency option:selected`).text().split('-')[0];
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
                   toastr.error("please select currency", "Required");
               }
               else if ($("select#DosageFrom.form-control.readOnlyUpdate").val() == 0) {
                   event.preventDefault()
                   event.stopPropagation()
                   toastr.error("please select DosageFrom", "Required");
               }
               $(".valid-feedback").hide();
               form.classList.add('was-validated')
           }, false)
       })
})()



function RenderCommercialPerPack() {
    $('#tblCommercialPerPack').html('');

    let html = "";
    let _startDate = $('#ProjectStartDate').val();
    let _launchDate = $('#ProductLaunchDate').val();
    if (_startDate != "" && _launchDate != "") {
        let _ProjectStartDate = new Date(_startDate);
        let _ProjectLaunchDate = new Date(_launchDate);
        let tentativeStartDate = _ProjectStartDate;

        tentativeStartDate.setMonth(_ProjectStartDate.getMonth() - 3);

        let _FinancialYearStartDate = new Date(tentativeStartDate.getFullYear() + "/03/31");
        let _BeginYear = tentativeStartDate.getFullYear();
        if (_FinancialYearStartDate > tentativeStartDate) {
            _BeginYear = tentativeStartDate.getFullYear() - 1;
        }

        var _financialYearLaunchDate = new Date(_ProjectLaunchDate.getFullYear() + "/03/31");
        var _SalesBeginYear = _financialYearLaunchDate.getFullYear();
        if (_financialYearLaunchDate < _ProjectLaunchDate) {
            _SalesBeginYear = _ProjectLaunchDate.getFullYear() + 1;
        }

        html += '<thead class="bg-light">';
        html += '<tr><th colspan="4">Commercials per pack</th><th></th>';
        var counter = 0;
        for (var i = 0; i < 10; i++) {
            if ((_BeginYear + i) >= _SalesBeginYear) {
                counter++;
            }
            html += "<th class='thYearCounter'>" + (counter > 0 ? counter : "-") + "</th>";
        }
        html += "</tr>";

        html += "<tr><th>Scenarios</th><th class='scenario1'>Low</th><th class='scenario2'>Medium</th><th class='scenario3'>High</th><th></th>";
        for (var i = 0; i < 10; i++) {
            html += "<th>" + "Mar-" + (_BeginYear + i).toString().substr(-2) + "</th>";
        }
        html += "</tr>";
        html += "</thead><tbody>";

        html += '<tr><td colspan="4"></td><td>Revenue Months</td>';
        for (var i = 0; i < 10; i++) {
            var _projectionDate = new Date((_BeginYear + i) + "/03/31");
            const diffTime = Math.abs(_projectionDate - _ProjectLaunchDate);
            const diffMonths = Math.round(diffTime / (1000 * 60 * 60 * 24 * 30)); 
            html += "<td class='trRevenueMonths'>" + ((_projectionDate < _ProjectLaunchDate) ? "0" : (diffMonths > 12 ? 12 : diffMonths)) + "</td>";
        }
        html += "</tr>";
        $('#tblCommercialPerPack').html(html);
        var SumOfSales =    [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
        var SumOfCOGS =     [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
        var SumOfGC =       [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];      

        $('#FinanceTableBoy tr').each(function (index, value) {

            let SKU = $(this).find("select.Skus option:selected").text();
            let PackSize = $(this).find("select.PakeSize option:selected").text();

            html += '<tr class="bg-light"><td class="text-left" colspan="15"><b>' + SKU + " - " + PackSize + '</b></td></tr>';

            var _uniqueClass = "tr_" + $(this).find("select.Skus option:selected").val() + "_" + $(this).find("select.PakeSize option:selected").val();
            var MS_td_data = [];
            var NSP_td_data = [];
            var COGS_td_data = [];
             //---------------------Start-MS%_Row-------------------------------------------------------
            html += "<tr class='" + _uniqueClass + "'><td>MS%</td><td>" + $(this).find("#hdnMSLow").val() + "</td><td>" + $(this).find("#hdnMSMid").val() + "</td><td>" + $(this).find("#hdnMSHigh").val() + "</td><td>Units</td>";

            var marketSharePercentage =  GetMarketSharePercentage($(this).find("#hdnMSLow").val(), $(this).find("#hdnMSMid").val(), $(this).find("#hdnMSHigh").val());
            let marketInPacks = formatToNumber($(this).find('.Marketinpacks').val());
            let msErosion = formatToNumber($("#MarketShareErosionrate").val(), true);

            for (var i = 0; i < 10; i++) {
                var Row_th_Index = $('#tblCommercialPerPack').find('.thYearCounter:eq(' + i + ')').text();                
                var trYearIndex = formatToNumber((Row_th_Index == '-') ? '0' : Row_th_Index);
                var trRevenueMonths =  formatToNumber($('#tblCommercialPerPack').find('.trRevenueMonths:eq(' + i + ')').text());
                let _units = ((marketInPacks * (marketSharePercentage / 100)) * (Math.pow((1 - (msErosion / 100)),  trYearIndex))) * (trRevenueMonths / 12);
                html += "<td>" + _units.toFixed(3) + "</td>";
                MS_td_data.push(_units);
            }
            html += "</tr>";
             //---------------------End-MS%_Row-------------------------------------------------------           
            //---------------------Start-NSP_Row-------------------------------------------------------
            html += "<tr class='" + _uniqueClass +"'><td>NSP</td><td>" + $(this).find("#hdnNSPLow").val() + "</td><td>" + $(this).find("#hdnNSPMid").val() + "</td><td>" + $(this).find("#hdnNSPHigh").val() +"</td><td>NSP</td>";
            var nsp_Percentage = GetNSPPercentage($(this).find("#hdnNSPLow").val(), $(this).find("#hdnNSPMid").val(), $(this).find("#hdnNSPHigh").val());
           // let marketInPacks = formatToNumber($(this).find('.Marketinpacks').val());
            let nsp_Erosion = formatToNumber($("#PriceErosion").val(), true);   

            for (var i = 0; i < 10; i++) {
                var Row_th_Index = $('#tblCommercialPerPack').find('.thYearCounter:eq(' + i + ')').text();
                var trYearIndex = formatToNumber((Row_th_Index == '-') ? '0' : Row_th_Index);              
                let _units = (nsp_Percentage / 100) * (Math.pow((1 - (nsp_Erosion / 100)), trYearIndex))               
                html += "<td>" + _units.toFixed(3) + "</td>";
                NSP_td_data.push(_units);
            }
            html += "</tr>";
            //---------------------End-NSP_Row-------------------------------------------------------
            //---------------------Start-COGS_Row-------------------------------------------------------
            html += "<tr class='" + _uniqueClass +"'><td>COGS</td><td>" + $(this).find(".EmcureCOGs_pack").val() + "</td><td>" + $(this).find(".EmcureCOGs_pack").val() + "</td><td>" + $(this).find(".EmcureCOGs_pack").val() + "</td><td>COGS/Unit</td>";
            let COGS_Escalation = formatToNumber($("#EscalationinCOGS").val(), true);
            var COGS_Percentage = GetCOGSPercentage($(this).find(".EmcureCOGs_pack").val(), $(this).find(".EmcureCOGs_pack").val(), $(this).find(".EmcureCOGs_pack").val());
            for (var i = 0; i < 10; i++) {
                var Row_th_Index = $('#tblCommercialPerPack').find('.thYearCounter:eq(' + i + ')').text();
                var trYearIndex = formatToNumber((Row_th_Index == '-') ? '0' : Row_th_Index);
                
                let _units = (COGS_Percentage / 100) * (Math.pow((1 + (COGS_Escalation / 100)), trYearIndex))
                html += "<td>" + _units.toFixed(3) + "</td>";
                COGS_td_data.push(_units);
            }
            html += "</tr>";
            //---------------------END-COGS_Row-------------------------------------------------------
            //--------------------Sales-------------------------------------------------------
            var Sales_data = [];
            html += "<tr class='" + _uniqueClass +"'><td></td><td></td><td></td><td></td><td>Sales</td>";
            for (var i = 0; i < 10; i++) {
                let result = MS_td_data[i] * NSP_td_data[i];
                html += "<td>" + result.toFixed(3) + "</td>";
                Sales_data.push(result);
                SumOfSales[i] = SumOfSales[i] + result;               
            }
            html += "</tr>";
            //--------------------COGS-------------------------------------------------------
            var COGS_data = [];
            html += "<tr class='" + _uniqueClass +"'><td></td><td></td><td></td><td></td><td>COGS</td>";
            for (var i = 0; i < 10; i++) {
                let result = MS_td_data[i] * COGS_td_data[i];
                html += "<td>" + result.toFixed(3) + "</td>";
                COGS_data.push(result);
                SumOfCOGS[i] = SumOfCOGS[i] + result;                 
            }
            html += "</tr>";
            //--------------------GC-------------------------------------------------------
            var GC_data = [];
            html += "<tr class='" + _uniqueClass +"'><td></td><td></td><td></td><td></td><td>GC</td>";
            for (var i = 0; i < 10; i++) {
                let result = Sales_data[i] - COGS_data[i];
                html += "<td>" + result.toFixed(3) + "</td>";
                GC_data.push(result);
                SumOfGC[i] = SumOfGC[i] + result;
            }
            html += "</tr>";
            //--------------------GC%-------------------------------------------------------
            html += "<tr class='" + _uniqueClass + "'><td></td><td></td><td></td><td></td><td>GC%</td>";
            for (var i = 0; i < 10; i++) {
                let result = GC_data[i] / Sales_data[i];
                result = (result == Infinity || isNaN(result)) ? 0 : result;
                html += "<td>" + result.toFixed(3) + "</td>";
               
            }
            html += "</tr>";            
        });
        /* -----------Grand SUM-Rows--------------------*/
        html += "<tr><td></td></tr>"; 
       /*---------------Sales------------------------------*/
        html += "<tr class='SumOfSales bg-light'><td></td><td></td><td></td><td></td><td>Sales</td>";
        for (var i = 0; i < 10; i++) {
            let result = SumOfSales[i];
            html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";
        /*---------------COGS--------------------------*/
        html += "<tr class='SumOfCOGS bg-light'><td></td><td></td><td></td><td></td><td>COGS</td>";
        for (var i = 0; i < 10; i++) {
            let result = SumOfCOGS[i];
            html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";
        /*-----------------GC------------------------*/
        html += "<tr class='SumOfGC bg-light'><td></td><td></td><td></td><td></td><td>GC</td>";
        for (var i = 0; i < 10; i++) {
            let result = SumOfGC[i];
            html += "<td>" + result.toFixed(3) + "</td>";
        }
        html += "</tr>";

        if ($('#FinanceTableBoy tr').length > 0) {

        }
        html += "</tbody>";
    }
    $('#tblCommercialPerPack').html(html);
}
function GetMarketSharePercentage(low, mid, high) {
    let marketSharePercentage = 0;
    try {
        if ($('#MSPersentage').val() == "1") {
            marketSharePercentage = parseInt(low);
        } else if ($('#MSPersentage').val() == "2") {
            marketSharePercentage = parseInt(mid);
        } else if ($('#MSPersentage').val() == "3") {
            marketSharePercentage = parseInt(high);
        }
    } catch (e) {
    }
    return marketSharePercentage;
}
function GetNSPPercentage(low, mid, high) {
    let NSPPercentage = 0;
    try {
        if ($('#TargetPriceScenario').val() == "1") {
            NSPPercentage = parseInt(low);
        } else if ($('#TargetPriceScenario').val() == "2") {
            NSPPercentage = parseInt(mid);
        } else if ($('#TargetPriceScenario').val() == "3") {
            NSPPercentage = parseInt(high);
        }
    } catch (e) {
    }
    return NSPPercentage;
}
function GetCOGSPercentage(low, mid, high) {
    let COGSPercentage = 0;
    try {
        if ($('#TargetPriceScenario').val() == "1") {
            COGSPercentage = parseInt(low);
        } else if ($('#TargetPriceScenario').val() == "2") {
            COGSPercentage = parseInt(mid);
        } else if ($('#TargetPriceScenario').val() == "3") {
            COGSPercentage = parseInt(high);
        }
    } catch (e) {
    }
    return COGSPercentage;
}
function formatToNumber(input, isFloat) {
    try {
        if (isFloat) {
            return parseFloat(input);
        } else {
            return parseInt(input);
        }
    } catch (e) {
        return 0;
    }
}