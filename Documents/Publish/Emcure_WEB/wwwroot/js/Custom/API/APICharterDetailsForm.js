let ControlsToValidate = ['APIGroupLeader','ManHourRates'];
let DivsToValidate = ['dvTimelineInMonths', 'dvManhourEstimates', 'dvAnalyticalDepartment', 'dvPRDDepartment', 'dvCapitalOtherExpenditure', 'dvManhourEstimates','dvHeadwiseBudget'];

$(document).ready(function () {
   // $("#ManhourEstimates_7__ManhourEstimatesMonthsValue").hide();
   // $("#ManhourEstimates_7__ManhourEstimatesNoOfEmployeeValue").hide();
  //  SetDivReadonly();
    $("#IsModelValid").val('');
    if (SaveStatus != '' && SaveStatus != undefined) {
        if (SaveStatus == 'Saved successfully.')
            toastr.success(SaveStatus);
        else
            toastr.error(SaveStatus);
    }
    HideSaveAsDraft();
});
function HideSaveAsDraft() {
    if ($('#StatusId').val() == 15)    //[APIInProgress = 14,   APISubmitted = 15]
        $('#SaveDraft').hide();
    else
        $('#SaveDraft').show();
}

$('#Save').click(function () {
    let IsValid = false;
    RoundUPReadonlyValues();
    IsValid = ValidateForm();
   
    $("#SaveType_APICharter").val('Save');
    if (IsValid) {
        removeCommasFromFormatCurrencyInputs();
        return true;

    } else {
        return false;
    }
});


$('#SaveDraft').click(function () {
    RoundUPReadonlyValues();
    $('#frmAPICharterDetails').validate().settings.ignore = "*";
    $("#SaveType_APICharter").val('SaveDraft');
    removeCommasFromFormatCurrencyInputs()
});
function removeCommasFromFormatCurrencyInputs() {
    var currencyInputs = document.querySelectorAll('.format-currency');
    currencyInputs.forEach(function (inputElement) {
        var value = inputElement.value;
        if (value) {
            var newValue = value.replace(/,/g, '');
            inputElement.value = newValue;
        }
    });
}



function ValidateForm() {
    let ArrofInvalid = []
    //let IsValid = true;;
    $.each(ControlsToValidate, function (_, kv) {
        if ($("#" + kv).val() == '') {
            $('#valmsg' + kv).text('Required');
            //IsValid = false;
            ArrofInvalid.push(kv);
        }
        else {
            $('#valmsg' + kv).text('');
        }
    });

    $.each(DivsToValidate, function (index, value) {
        $("#" + value +" :input[type=text]").each(function () {
            let ControlID = $(this).attr('id');
            if ($("#" + ControlID).val() == '') {
                $(this).next().text('Required');
                //IsValid = false;
                ArrofInvalid.push(ControlID);
            }
            else {
                $(this).next().text('');
            }
        });
    });
    let IsValid = (ArrofInvalid.length == 0) ? true : false;
    if (!IsValid) {
        let controltobeFocus = ArrofInvalid[ArrofInvalid.length - 1];
        $('#' + controltobeFocus).focus();
        toastr.error('Some fields are missing !');
        $("#IsModelValid").val('')
    }
    else {
        $("#IsModelValid").val('Valid')
    }
    return IsValid;
}
$('input[type="number"]').on("keypress keyup blur", function (event) {
    let patt = new RegExp(/[0-9]*[.]{1}[0-9]{2}/i);
    let matchedString = $(this).val().match(patt);
    if (matchedString) {
        $(this).val(matchedString);
    }
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});
function RoundUPReadonlyValues() {
    $('input[type=number][readonly]').each(function () {
        let Value = $(this).val();
        let floatval = parseFloat(Value);
        $(this).val(floatval.toFixed(2));
    });
}

//--------------Calculation of Readoinly Feilds------------------------------------------
//--------------Calculation of TimelineInMonthsValue----------------------
$("[id*='TimelineInMonthsValue']").change(function () {
    let sum = 0;
    $("[id*='TimelineInMonthsValue']").each(function () {
        let Value = ($(this).val() == '') ? 0 : GetIntfromControlValue($(this).val());
        if (($(this).attr('id') != 'TimelineInMonths_6__TimelineInMonthsValue') && !isNaN(Value))
            sum = parseFloat(sum) + parseFloat(Value);
    });
    $("#TimelineInMonths_6__TimelineInMonthsValue").val(formatNumber(sum));
    CalculateTotalHeadwiseAnalytical();
});
//--------------Calculation of PRDDepartmentRawMaterialValue----------------------
$("[id*='PRDDepartmentRawMaterialValue']").change(function () {
    let sum = 0;
    $("[id*='PRDDepartmentRawMaterialValue']").each(function () {
        let Value = ($(this).val() == '') ? 0 : GetIntfromControlValue($(this).val());
        if (($(this).attr('id') != 'PRDDepartment_4__PRDDepartmentRawMaterialValue') && !isNaN(Value))
            sum = parseFloat(sum) + parseFloat(Value);
    });
    $("#PRDDepartment_4__PRDDepartmentRawMaterialValue").val(formatNumber(sum));
    $("#HeadwiseBudget_1__HeadwiseBudgetValue").val(sum);
    $("[id*='HeadwiseBudgetValue']").change();
    CalculateTotalHeadwiseAnalytical();
});
//----------Impurity------------------------------

$("[id*='AnalyticalDepartmentImpurityValue']").change(function () {
    let NoOfCols = GetIntfromControlValue$("#AnalyticalDepartment_1__AnalyticalDepartmentImpurityValue").val();
    let CostPerCols = GetIntfromControlValue$("#AnalyticalDepartment_2__AnalyticalDepartmentImpurityValue").val();
    let TotalCostPerColumn = parseFloat((NoOfCols == "" ? 0 : NoOfCols)) * parseFloat((CostPerCols == "" ? 0 : CostPerCols));
    $("#AnalyticalDepartment_3__AnalyticalDepartmentImpurityValue").val(TotalCostPerColumn);

    let Chemical = GetIntfromControlValue($("#AnalyticalDepartment_0__AnalyticalDepartmentImpurityValue").val());
    let CostofSilica = GetIntfromControlValue($("#AnalyticalDepartment_4__AnalyticalDepartmentImpurityValue").val());
    let TotalCostAnalytical = parseFloat((Chemical == "" ? 0 : Chemical)) + parseFloat((CostofSilica == "" ? 0 : CostofSilica)) + TotalCostPerColumn;
    $("#AnalyticalDepartment_5__AnalyticalDepartmentImpurityValue").val(TotalCostAnalytical);

    //let sum = 0;
    //$("[id*='AnalyticalDepartmentImpurityValue']").each(function () {
    //    let Value = ($(this).val() == '') ? 0 : $(this).val();
    //    if (($(this).attr('id') != 'AnalyticalDepartment_5__AnalyticalDepartmentImpurityValue') && !isNaN(Value))
    //        sum = parseFloat(sum) + parseFloat(Value);
    //});
    //$("#AnalyticalDepartment_5__AnalyticalDepartmentImpurityValue").val(sum);
    CalculateTotalHeadwiseAnalytical();
});
//----------Stability------------------------------ AnalyticalDepartment_0__AnalyticalDepartmentStabilityValue
$("[id*='AnalyticalDepartmentStabilityValue']").change(function () {
    let NoOfCols = GetIntfromControlValue($("#AnalyticalDepartment_1__AnalyticalDepartmentStabilityValue").val());
    let CostPerCols = GetIntfromControlValue($("#AnalyticalDepartment_2__AnalyticalDepartmentStabilityValue").val());
    let TotalCostPerColumn = parseFloat((NoOfCols == "" ? 0 : NoOfCols)) * parseFloat((CostPerCols == "" ? 0 : CostPerCols));
    $("#AnalyticalDepartment_3__AnalyticalDepartmentStabilityValue").val(TotalCostPerColumn);

    let Chemical = GetIntfromControlValue($("#AnalyticalDepartment_0__AnalyticalDepartmentStabilityValue").val());
    let CostofSilica = GetIntfromControlValue($("#AnalyticalDepartment_4__AnalyticalDepartmentStabilityValue").val());
    let TotalCostAnalytical = parseFloat((Chemical == "" ? 0 : Chemical)) + parseFloat((CostofSilica == "" ? 0 : CostofSilica)) + TotalCostPerColumn;
    $("#AnalyticalDepartment_5__AnalyticalDepartmentStabilityValue").val(TotalCostAnalytical);
    //let sum = 0;
    //$("[id*='AnalyticalDepartmentStabilityValue']").each(function () {
    //    let Value = ($(this).val() == '') ? 0 : $(this).val();
    //    if (($(this).attr('id') != 'AnalyticalDepartment_5__AnalyticalDepartmentStabilityValue') && !isNaN(Value))
    //        sum = parseFloat(sum) + parseFloat(Value);
    //});
    //$("#AnalyticalDepartment_5__AnalyticalDepartmentStabilityValue").val(sum);
    CalculateTotalHeadwiseAnalytical();
});
//----------AMV------------------------------ AnalyticalDepartment_0__AnalyticalDepartmentAMVValue
$("[id*='AnalyticalDepartmentAMVValue']").change(function () {
    let NoOfCols = GetIntfromControlValue($("#AnalyticalDepartment_1__AnalyticalDepartmentAMVValue").val());
    let CostPerCols = GetIntfromControlValue($("#AnalyticalDepartment_2__AnalyticalDepartmentAMVValue").val());
    let TotalCostPerColumn = parseFloat((NoOfCols == "" ? 0 : NoOfCols)) * parseFloat((CostPerCols == "" ? 0 : CostPerCols));
    $("#AnalyticalDepartment_3__AnalyticalDepartmentAMVValue").val(TotalCostPerColumn);

    let Chemical = $("#AnalyticalDepartment_0__AnalyticalDepartmentAMVValue").val();
    let CostofSilica = $("#AnalyticalDepartment_4__AnalyticalDepartmentAMVValue").val();
    let TotalCostAnalytical = parseFloat((Chemical == "" ? 0 : Chemical)) + parseFloat((CostofSilica == "" ? 0 : CostofSilica)) + TotalCostPerColumn;
    $("#AnalyticalDepartment_5__AnalyticalDepartmentAMVValue").val(TotalCostAnalytical);
    //let sum = 0;
    //$("[id*='AnalyticalDepartmentAMVValue']").each(function () {
    //    let Value = ($(this).val() == '') ? 0 : $(this).val();
    //    if (($(this).attr('id') != 'AnalyticalDepartment_5__AnalyticalDepartmentAMVValue') && !isNaN(Value))
    //        sum = parseFloat(sum) + parseFloat(Value);
    //});
    //$("#AnalyticalDepartment_5__AnalyticalDepartmentAMVValue").val(sum);
    CalculateTotalHeadwiseAnalytical();
});

//----------AMT------------------------------ AnalyticalDepartment_0__AnalyticalDepartmentAMTValue
$("[id*='AnalyticalDepartmentAMTValue']").change(function () {
    let NoOfCols = GetIntfromControlValue($("#AnalyticalDepartment_1__AnalyticalDepartmentAMTValue").val());
    let CostPerCols = GetIntfromControlValue($("#AnalyticalDepartment_2__AnalyticalDepartmentAMTValue").val());
    let TotalCostPerColumn = parseFloat((NoOfCols == "" ? 0 : NoOfCols)) * parseFloat((CostPerCols == "" ? 0 : CostPerCols));
    $("#AnalyticalDepartment_3__AnalyticalDepartmentAMTValue").val(TotalCostPerColumn);

    let Chemical = GetIntfromControlValue($("#AnalyticalDepartment_0__AnalyticalDepartmentAMTValue").val());
    let CostofSilica = GetIntfromControlValue($("#AnalyticalDepartment_4__AnalyticalDepartmentAMTValue").val());
    let TotalCostAnalytical = parseFloat((Chemical == "" ? 0 : Chemical)) + parseFloat((CostofSilica == "" ? 0 : CostofSilica)) + TotalCostPerColumn;
    $("#AnalyticalDepartment_5__AnalyticalDepartmentAMTValue").val(TotalCostAnalytical);

    //let sum = 0;
    //$("[id*='AnalyticalDepartmentAMTValue']").each(function () {
    //    let Value = ($(this).val() == '') ? 0 : $(this).val();
    //    if (($(this).attr('id') != 'AnalyticalDepartment_5__AnalyticalDepartmentAMTValue') && !isNaN(Value))
    //        sum = parseFloat(sum) + parseFloat(Value);
    //});
    //$("#AnalyticalDepartment_5__AnalyticalDepartmentAMTValue").val(sum);
    CalculateTotalHeadwiseAnalytical();
});
//----------ARD------------------------------ AnalyticalDepartment_0__AnalyticalDepartmentARDValue
$("[id*='AnalyticalDepartmentARDValue']").change(function () {
    let NoOfCols = GetIntfromControlValue($("#AnalyticalDepartment_1__AnalyticalDepartmentARDValue").val());
    let CostPerCols = GetIntfromControlValue($("#AnalyticalDepartment_2__AnalyticalDepartmentARDValue").val());
    let TotalCostPerColumn = parseFloat((NoOfCols == "" ? 0 : NoOfCols)) * parseFloat((CostPerCols == "" ? 0 : CostPerCols));
    $("#AnalyticalDepartment_3__AnalyticalDepartmentARDValue").val(TotalCostPerColumn);

    let Chemical = GetIntfromControlValue($("#AnalyticalDepartment_0__AnalyticalDepartmentARDValue").val());
    let CostofSilica = GetIntfromControlValue($("#AnalyticalDepartment_4__AnalyticalDepartmentARDValue").val());
    let TotalCostAnalytical = parseFloat((Chemical == "" ? 0 : Chemical)) + parseFloat((CostofSilica == "" ? 0 : CostofSilica)) + TotalCostPerColumn;
    $("#AnalyticalDepartment_5__AnalyticalDepartmentARDValue").val(TotalCostAnalytical);

    //$("[id*='AnalyticalDepartmentARDValue']").each(function () {
    //    let Value = ($(this).val() == '') ? 0 : $(this).val();
    //    if (($(this).attr('id') != 'AnalyticalDepartment_3__AnalyticalDepartmentARDValue') &&
    //        ($(this).attr('id') != 'AnalyticalDepartment_0__AnalyticalDepartmentARDValue') &&
    //        ($(this).attr('id') != 'AnalyticalDepartment_4__AnalyticalDepartmentARDValue') &&
    //        ($(this).attr('id') != 'AnalyticalDepartment_5__AnalyticalDepartmentARDValue') && !isNaN(Value))
    //        sum = parseFloat(sum) + parseFloat(Value);
    //});
    //cal_TotalcostofAnalyticalRawMaterial();

    CalculateTotalHeadwiseAnalytical();
});
//$("#AnalyticalDepartment_4__AnalyticalDepartmentARDValue").change(function () {
//    cal_TotalcostofAnalyticalRawMaterial();
//});
//function cal_TotalcostofAnalyticalRawMaterial() {
//    let sum = 0;
//    let Value = ($("#AnalyticalDepartment_4__AnalyticalDepartmentARDValue").val() == '') ? 0 : $("#AnalyticalDepartment_4__AnalyticalDepartmentARDValue").val();
//    let SumValue = ($("#AnalyticalDepartment_3__AnalyticalDepartmentARDValue").val() == '') ? 0 : $("#AnalyticalDepartment_3__AnalyticalDepartmentARDValue").val();
//    if (!isNaN(Value) && !isNaN(SumValue))
//        sum = parseFloat(SumValue) + parseFloat(Value);

//    $("#AnalyticalDepartment_5__AnalyticalDepartmentARDValue").val(sum);
//    CalculateTotalHeadwiseAnalytical();
//}

function CalculateTotalHeadwiseAnalytical () {
    let arrAnalyticalCol = ['ARD', 'Impurity', 'Stability', 'AMV', 'AMT'];
    let sum = 0;
    $.each(arrAnalyticalCol, function (index, value) {
        let ControlId = '#AnalyticalDepartment_5__AnalyticalDepartment' + value + 'Value';
        let valueOfControl = ($(ControlId).val() == '') ? 0 : $(ControlId).val();
        if (!isNaN(valueOfControl))
            sum = parseFloat(sum) + parseFloat(valueOfControl);
    });
    $("#HeadwiseBudget_0__HeadwiseBudgetValue").val(sum);    // Headwise -Total Cost Of Analytical Raw Material
}
//--------------ManhourEstimates--------------------------------------------------------------

$("[id*='ManhourEstimatesNoOfEmployeeValue']").change(function () {
    let ControlID = $(this).attr('id');
    Calculate_ManhourEstimates(ControlID);
});
$("[id*='ManhourEstimatesMonthsValue']").change(function () {
    let ControlID = $(this).attr('id');
    Calculate_ManhourEstimates(ControlID);
});

function Calculate_ManhourEstimates(ControlID) {
    let SpliID = ControlID.split("__");
    let MonthControlID = "#" + SpliID[0] + "__ManhourEstimatesMonthsValue";
    let NoofEmployeeControlID = "#" + SpliID[0] + "__ManhourEstimatesNoOfEmployeeValue";
    let MonthValue = ($(MonthControlID).val() == '') ? 0 : $(MonthControlID).val();
    let NoofEmployeeValue = ($(NoofEmployeeControlID).val() == '') ? 0 : $(NoofEmployeeControlID).val();

    let ManHourRates = ($("#ManHourRates").val() == '') ? 0 : $("#ManHourRates").val();

    //let HoursInMonth = parseFloat(730);    // Asuumed , later may be change
    let HourValue = parseFloat(GetIntfromControlValue(MonthValue)) * (8 * 22) * GetIntfromControlValue(NoofEmployeeValue);
    let CostValue = parseFloat(ManHourRates) * parseFloat(HourValue);

    let HoursControlID = "#" + SpliID[0] + "__ManhourEstimatesHoursValue";
    let CostControlID = "#" + SpliID[0] + "__ManhourEstimatesCostValue";

    $(HoursControlID).val(formatNumber(HourValue))
    $(CostControlID).val(formatNumber(CostValue));
    $(HoursControlID).change();
    $(CostControlID).change();
}
$("#ManHourRates").change(function () {
    $("[id*='ManhourEstimatesNoOfEmployeeValue']").change();
});

//--------------Calculation of Total Hours------------------------------------------

$("[id*='ManhourEstimatesHoursValue']").change(function () {
    let sum = 0;
    $("[id*='ManhourEstimatesHoursValue']").each(function () {
        let Value = ($(this).val() == '') ? 0 : GetIntfromControlValue($(this).val());
        if (($(this).attr('id') != 'ManhourEstimates_7__ManhourEstimatesHoursValue') && !isNaN(Value))
            sum = parseFloat(sum) + parseFloat(Value);
    });
    $("#ManhourEstimates_7__ManhourEstimatesHoursValue").val(formatNumber(sum));
});
function formatNumber(value, round, code) {
    if (code == null || code == undefined || code == "") {
        code = "en-US"
    }
    if (round == null || round == undefined || round == "") {
        value = parseFloat(value).toFixed(2);
    } else {
        value = Math.round(parseFloat(value));
    }
    return new Intl.NumberFormat(code).format(value);
}

function GetIntfromControlValue(cntrlVal) {
    cntrlVal = cntrlVal.replace(/,/g, '');
    return (cntrlVal == '') ? 0 : (cntrlVal);
}
//--------------Calculation of Total Cost------------------------------------------
$("[id*='ManhourEstimatesCostValue']").change(function () {
    let sum = 0;
    $("[id*='ManhourEstimatesCostValue']").each(function () {
        let Value = ($(this).val() == '') ? 0 : GetIntfromControlValue($(this).val());
        if (($(this).attr('id') != 'ManhourEstimates_7__ManhourEstimatesCostValue') && !isNaN(Value))
            sum = parseFloat(sum) + parseFloat(Value);
    });
    $("#ManhourEstimates_7__ManhourEstimatesCostValue").val(formatNumber(sum));
    $("#HeadwiseBudget_2__HeadwiseBudgetValue").val(sum);
    $("[id*='HeadwiseBudgetValue']").change();
});
//--------------Calculation of CapitalOtherExpenditureAmountValue------------------------------------------

$("[id*='CapitalOtherExpenditureAmountValue']").change(function () {
    let Value = ($(this).val() == '') ? 0 : $(this).val();
    if (!isNaN(Value)) {
        if ($(this).attr('id').includes('0__CapitalOtherExpenditureAmountValue')) //capex value
            $("#HeadwiseBudget_3__HeadwiseBudgetValue").val(Value);

        else if ($(this).attr('id').includes('1__CapitalOtherExpenditureAmountValue')) //Other Expenses/Utility Cost
            $("#HeadwiseBudget_4__HeadwiseBudgetValue").val(Value);

        else if ($(this).attr('id').includes('2__CapitalOtherExpenditureAmountValue')) //DMF Filling Cost
            $("#HeadwiseBudget_6__HeadwiseBudgetValue").val(Value);

        else if ($(this).attr('id').includes('3__CapitalOtherExpenditureAmountValue')) //Litigation Cost / Patent Registration Cost
            $("#HeadwiseBudget_5__HeadwiseBudgetValue").val(Value);

        else if ($(this).attr('id').includes('4__CapitalOtherExpenditureAmountValue')) //Bio Cost
            $("#HeadwiseBudget_7__HeadwiseBudgetValue").val(Value);

        $("[id*='HeadwiseBudgetValue']").change();
    }
});
//---------------------Total of Headwise count value----------HeadwiseBudget_0__HeadwiseBudgetValue
$("[id*='HeadwiseBudgetValue']").change(function () {
    $("[id*='HeadwiseBudgetValue']").change(function () {
        let sum = 0;
        $("[id*='HeadwiseBudgetValue']").each(function () {
            let Value = ($(this).val() == '') ? 0 : $(this).val();
            if (($(this).attr('id') != 'HeadwiseBudget_8__HeadwiseBudgetValue') && !isNaN(Value))
                sum = parseFloat(sum) + parseFloat(Value);
        });
        $("#HeadwiseBudget_8__HeadwiseBudgetValue").val(sum);
    });
});
