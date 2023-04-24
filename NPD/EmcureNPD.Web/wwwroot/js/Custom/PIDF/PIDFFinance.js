﻿let rowcount = 0;

$(document).ready(function () {
    console.log("selectedSKUs" + selectedSKUs);
   
    GetCurrencyList();
    calculateDealTerm();
    GetDosageFormList();
    GetSelectedMSPercent();
    GetSelectedTargetPriceScanario();
    GetSkus(pidfId);
    fnGetActiveBusinessUnit();
    GetFinancialProjectionYear(_selectedProjectStartDate);
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
    }

    $("#Currency").select2({
        placeholder: "Select Currency..",
        allowClear: true
    });
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
  // ajaxServiceMethod($('#hdnBaseURL').val() + AllCurrency, 'GET', GetCurrencyListSuccess, GetCurrencyListError);
     ajaxServiceMethod($('#hdnBaseURL').val() + "api/Currency/GetCurrencyByLoggedInUser", 'GET', GetCurrencyListSuccess, GetCurrencyListError);
}

function GetCurrencyListSuccess(data) {
    try {
        $('#Currency').html('')
        let optionhtml = ''; //'<option value = "0">--Select--</option>';
        $.each(data._object, function (index, object) {
            let currencyText = object.currencyCode == null ? object.currencyName : object.currencyCode + "-" + object.currencyName;
            optionhtml += '<option value="' +
                object.currencyId + '">' + currencyText + '</option>';
        });
        $("#Currency").append(optionhtml);

        let arrCur = JSON.parse(JSON.stringify(selectedCurrencyId.split(',')));
        $('select#Currency').select2(
            {
                placeholder: "Select Currency..",
                allowClear: true
            }
        ).val(arrCur).trigger('change'); 
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
    j = $('.Skus').length + 1;
    var table = $('#FinanceTableBoy');
    var node = $('#financeDetailsRow_0').clone(true);
    table.find('tr:last').after(node);
    table.find('tr:last').find("input").val("");
    SetChildRowDeleteIcon();
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
        $(this).find("td:eq(0) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PidffinaceId");
        $(this).find("td:eq(1) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PakeSize");

        $(this).find("td:eq(2) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].BrandPrice");
        $(this).find("td:eq(3) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].GenericListprice");
        $(this).find("td:eq(4) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].NetRealisation");
        $(this).find("td:eq(5) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].EstMat2016By12units");
        $(this).find("td:eq(6) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].EstMat2020By12units");
        $(this).find("td:eq(7) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Cagrover2016By12estMatunits");
        $(this).find("td:eq(8) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Marketinpacks");
        $(this).find("td:eq(9) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].BatchsizeinLtrTabs");

        $(this).find("td:eq(10) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Batchsize");
        $(this).find("td:eq(11) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Yield");
        $(this).find("td:eq(12) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Batchoutput");
        $(this).find("td:eq(13) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].ApiCad");
        $(this).find("td:eq(14) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].ExcipientsCad");
        $(this).find("td:eq(15) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PmCad");
        $(this).find("td:eq(16) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].CcpcCad");
        $(this).find("td:eq(17) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].FreightCad");
        $(this).find("td:eq(18) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].EmcureCogsPack");
        $(this).find("td:eq(19) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PidffinaceBatchSizeCoatingId");
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
function deleteRowFinance(j, element) {
    $(element).closest("tr").remove();
    SetChildRowDeleteIcon();
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
    $.each($('#FinanceTableBoy tr'), function (index, value) {
        packSize = $(this).find("td:eq(1) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PakeSize").val();
        if (ele.valueAsNumber >= 0 && $(this).find("td:eq(3) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].GenericListprice").val() == ele.value) {
            netRealisation = (ele.valueAsNumber * 40) / 100;
            let textnetRealisation = netRealisation.toLocaleString("en");
            $(this).find("td:eq(4) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].NetRealisation").val(textnetRealisation);
        }
        if ($(this).find("td:eq(6) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].EstMat2020By12units").val() > 0) {
            EstMat2020By12units = parseFloat($(this).find("td:eq(6) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].EstMat2020By12units").val());
            let textEstMat2020By12units = EstMat2020By12units.toLocaleString("en");
            $(this).find("td:eq(8) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Marketinpacks").val(textEstMat2020By12units);
        }
        if ($(this).find("td:eq(9) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].BatchsizeinLtrTabs").val() > 0) {
            BatchsizeinLtrTabs = $(this).find("td:eq(9) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].BatchsizeinLtrTabs").val();
            Batchsize = parseInt(BatchsizeinLtrTabs) / parseInt(packSize); //parseFloat($(this).find("td:eq(0) select option:selected").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Skus").text().replace("mg", "").trim())
            $(this).find("td:eq(10) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Batchsize").val(parseInt(Batchsize));
        }
        if ($(this).find("td:eq(11) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Yield").val() > 0) {
             Yield = $(this).find("td:eq(11) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Yield").val();
            Batchoutput = parseFloat(Yield) * parseFloat($(this).find("td:eq(0) select option:selected").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Skus").text().replace("mg", "").trim())
            $(this).find("td:eq(12) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].Batchoutput").val(Batchoutput);
        }
            ApiCad = $(this).find("td:eq(13) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].ApiCad").val();
            ExcipientsCad = $(this).find("td:eq(14) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].ExcipientsCad").val();
            PmCad = $(this).find("td:eq(15) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].PmCad").val();
            CcpcCad = $(this).find("td:eq(16) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].CcpcCad").val();
            FreightCad = $(this).find("td:eq(17) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].FreightCad").val();
            sumforEmcureCogsPack = parseInt(Batchsize) + parseInt(Yield) + parseInt(Batchoutput) + parseInt(ApiCad) + parseInt(ExcipientsCad) + parseInt(PmCad) + parseInt(CcpcCad) + parseInt(FreightCad);
            $(this).find("td:eq(18) input").attr("name", "lsPidfFinanceBatchSizeCoating[" + index.toString() + "].EmcureCogsPack").val(sumforEmcureCogsPack);
       // });
    })
}
function GetSkus(pidfId) {
    ajaxServiceMethod($('#hdnBaseURL').val() + "api/PidfFinance/GetStrengthByPIDFId" + "/" + pidfId, 'GET', GetSkusListSuccess, GetSkusListError);
    function GetSkusListSuccess(data) {
        try {
            let arrselectedSKUs = selectedSKUs.split(',');
            if (arrselectedSKUs[0]!='' && arrselectedSKUs.length > 0) {
                $('select.DbSkus').html('')
                let optionhtml = '<option value = "0">--Select--</option>';
                $.each(data, function (index, object) {
                    optionhtml += '<option value="' +
                        object.pidfproductStrengthId + '">' + object.strength + 'mg </option>';
                    
                });
                $("select.DbSkus").append(optionhtml);
                arrselectedSKUs.forEach(function (val, i) {
                    if (val == arrselectedSKUs[i]) {
                        $(`select#DbSkus${i}.Skus.DbSkus`).val(val);
                    }
                });
            }
            else {
                $('select#Skus').html('')
                let optionhtml = '<option value = "0">--Select--</option>';
                $.each(data, function (index, object) {
                    optionhtml += '<option value="' +
                        object.pidfproductStrengthId + '">' + object.strength + 'mg </option>';
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
    let selectedCurrency = $(`#Currency option:selected`).text().split('-')[0];
    let td = `<td>${selectedCurrency}</td>`;
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

    return lastSixMonths.reverse() // Result
    
    //var today = new Date(date);
    //for (i = 0; i < 4; i++) {
    //    if (i == 4) {
    //        if (i > today.getMonth())
    //            today = i - today.getMonth;
    //        else
    //            today = today.getMonth() - i;
    //    }
    //}
    //return today.getFullYear();
}

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