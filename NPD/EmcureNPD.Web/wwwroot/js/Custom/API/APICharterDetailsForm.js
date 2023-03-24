﻿var ControlsToValidate = ['APIGroupLeader','ManHourRates'];
var DivsToValidate = ['dvTimelineInMonths', 'dvManhourEstimates', 'dvAnalyticalDepartment', 'dvPRDDepartment', 'dvCapitalOtherExpenditure', 'dvManhourEstimates','dvHeadwiseBudget'];


$(document).ready(function () {
    debugger;
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
});

$('#Save').click(function () {
    ValidateForm();
    $("#SaveType").val('Save');
});
$('#SaveDraft').click(function () {
    ValidateForm();// $("#IsModelValid").val('Valid')
    $("#SaveType").val('SaveDraft');
});

function ValidateForm() {
    var ArrofInvalid = []
    //var IsValid = true;;
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
            var ControlID = $(this).attr('id');
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
    var IsValid = (ArrofInvalid.length == 0) ? true : false;
    if (!IsValid) {
        toastr.error('Some fields are missing !');
        $("#IsModelValid").val('')
    }
    else {
        $("#IsModelValid").val('Valid')
    }
    return IsValid;
}


//--------------Calculation of Readoinly Feilds------------------------------------------
//--------------Calculation of TimelineInMonthsValue----------------------
$("[id*='TimelineInMonthsValue']").change(function () {
    var sum = 0;
    $("[id*='TimelineInMonthsValue']").each(function () {
        var Value = ($(this).val() == '') ? 0 : $(this).val();
        if (($(this).attr('id') != 'TimelineInMonths_6__TimelineInMonthsValue') && !isNaN(Value))
            sum = parseFloat(sum) + parseFloat(Value);
    });
    $("#TimelineInMonths_6__TimelineInMonthsValue").val(sum);
    CalculateTotalHeadwiseAnalytical();
});
//--------------Calculation of PRDDepartmentRawMaterialValue----------------------
$("[id*='PRDDepartmentRawMaterialValue']").change(function () {
    var sum = 0;
    $("[id*='PRDDepartmentRawMaterialValue']").each(function () {
        var Value = ($(this).val() == '') ? 0 : $(this).val();
        if (($(this).attr('id') != 'PRDDepartment_4__PRDDepartmentRawMaterialValue') && !isNaN(Value))
            sum = parseFloat(sum) + parseFloat(Value);
    });
    $("#PRDDepartment_4__PRDDepartmentRawMaterialValue").val(sum);
    $("#HeadwiseBudget_1__HeadwiseBudgetValue").val(sum); 
    $("[id*='HeadwiseBudgetValue']").change();    
    CalculateTotalHeadwiseAnalytical();
});
//----------Impurity------------------------------

$("[id*='AnalyticalDepartmentImpurityValue']").change(function () {
    var sum = 0;
    $("[id*='AnalyticalDepartmentImpurityValue']").each(function () {
        var Value = ($(this).val() == '') ? 0 : $(this).val();
        if (($(this).attr('id') != 'AnalyticalDepartment_5__AnalyticalDepartmentImpurityValue') && !isNaN(Value))
            sum = parseFloat(sum) + parseFloat(Value);
    });
    $("#AnalyticalDepartment_5__AnalyticalDepartmentImpurityValue").val(sum);
    CalculateTotalHeadwiseAnalytical();
});
//----------Stability------------------------------ AnalyticalDepartment_0__AnalyticalDepartmentStabilityValue
$("[id*='AnalyticalDepartmentStabilityValue']").change(function () {
    var sum = 0;
    $("[id*='AnalyticalDepartmentStabilityValue']").each(function () {
        var Value = ($(this).val() == '') ? 0 : $(this).val();
        if (($(this).attr('id') != 'AnalyticalDepartment_5__AnalyticalDepartmentStabilityValue') && !isNaN(Value))
            sum = parseFloat(sum) + parseFloat(Value);
    });
    $("#AnalyticalDepartment_5__AnalyticalDepartmentStabilityValue").val(sum);
    CalculateTotalHeadwiseAnalytical();
});
//----------AMV------------------------------ AnalyticalDepartment_0__AnalyticalDepartmentAMVValue
$("[id*='AnalyticalDepartmentAMVValue']").change(function () {
    var sum = 0;
    $("[id*='AnalyticalDepartmentAMVValue']").each(function () {
        var Value = ($(this).val() == '') ? 0 : $(this).val();
        if (($(this).attr('id') != 'AnalyticalDepartment_5__AnalyticalDepartmentAMVValue') && !isNaN(Value))
            sum = parseFloat(sum) + parseFloat(Value);
    });
    $("#AnalyticalDepartment_5__AnalyticalDepartmentAMVValue").val(sum);
    CalculateTotalHeadwiseAnalytical();
});

//----------AMT------------------------------ AnalyticalDepartment_0__AnalyticalDepartmentAMTValue
$("[id*='AnalyticalDepartmentAMTValue']").change(function () {
    var sum = 0;
    $("[id*='AnalyticalDepartmentAMTValue']").each(function () {
        var Value = ($(this).val() == '') ? 0 : $(this).val();
        if (($(this).attr('id') != 'AnalyticalDepartment_5__AnalyticalDepartmentAMTValue') && !isNaN(Value))
            sum = parseFloat(sum) + parseFloat(Value);
    });
    $("#AnalyticalDepartment_5__AnalyticalDepartmentAMTValue").val(sum);
    CalculateTotalHeadwiseAnalytical();
});
//----------ARD------------------------------ AnalyticalDepartment_0__AnalyticalDepartmentARDValue
$("[id*='AnalyticalDepartmentARDValue']").change(function () {
    var sum = 0;
    $("[id*='AnalyticalDepartmentARDValue']").each(function () {
        var Value = ($(this).val() == '') ? 0 : $(this).val();
        if (($(this).attr('id') != 'AnalyticalDepartment_3__AnalyticalDepartmentARDValue') &&
            ($(this).attr('id') != 'AnalyticalDepartment_4__AnalyticalDepartmentARDValue') &&
            ($(this).attr('id') != 'AnalyticalDepartment_5__AnalyticalDepartmentARDValue') && !isNaN(Value))
            sum = parseFloat(sum) + parseFloat(Value);
    });
    $("#AnalyticalDepartment_3__AnalyticalDepartmentARDValue").val(sum);
    cal_TotalcostofAnalyticalRawMaterial();
    CalculateTotalHeadwiseAnalytical();
});
$("#AnalyticalDepartment_4__AnalyticalDepartmentARDValue").change(function () {
    cal_TotalcostofAnalyticalRawMaterial();
});
function cal_TotalcostofAnalyticalRawMaterial() {
    var sum = 0;
    var Value = ($("#AnalyticalDepartment_4__AnalyticalDepartmentARDValue").val() == '') ? 0 : $("#AnalyticalDepartment_4__AnalyticalDepartmentARDValue").val();
    var SumValue = ($("#AnalyticalDepartment_3__AnalyticalDepartmentARDValue").val() == '') ? 0 : $("#AnalyticalDepartment_3__AnalyticalDepartmentARDValue").val();
    if (!isNaN(Value) && !isNaN(SumValue))
        sum = parseFloat(SumValue) + parseFloat(Value);

    $("#AnalyticalDepartment_5__AnalyticalDepartmentARDValue").val(sum);
    CalculateTotalHeadwiseAnalytical();
}

function CalculateTotalHeadwiseAnalytical () {
    var arrAnalyticalCol = ['ARD', 'Impurity', 'Stability', 'AMV', 'AMT'];
    var sum = 0;
    $.each(arrAnalyticalCol, function (index, value) {
        var ControlId = '#AnalyticalDepartment_5__AnalyticalDepartment' + value + 'Value';
        var valueOfControl = ($(ControlId).val() == '') ? 0 : $(ControlId).val();
        if (!isNaN(valueOfControl))
            sum = parseFloat(sum) + parseFloat(valueOfControl);        
    });
    $("#HeadwiseBudget_0__HeadwiseBudgetValue").val(sum);    // Headwise -Total Cost Of Analytical Raw Material
}
//--------------ManhourEstimates--------------------------------------------------------------

$("[id*='ManhourEstimatesNoOfEmployeeValue']").change(function () { 
    var ControlID = $(this).attr('id');
    Calculate_ManhourEstimates(ControlID);
});
$("[id*='ManhourEstimatesMonthsValue']").change(function () {
    var ControlID = $(this).attr('id');
    Calculate_ManhourEstimates(ControlID);
});

function Calculate_ManhourEstimates(ControlID) {
    var SpliID = ControlID.split("__");
    var MonthControlID = "#" + SpliID[0] + "__ManhourEstimatesMonthsValue";
    var NoofEmployeeControlID = "#" + SpliID[0] + "__ManhourEstimatesNoOfEmployeeValue";
    var MonthValue = ($(MonthControlID).val() == '') ? 0 : $(MonthControlID).val();
    var NoofEmployeeValue = ($(NoofEmployeeControlID).val() == '') ? 0 : $(NoofEmployeeControlID).val();

    var ManHourRates = ($("#ManHourRates").val() == '') ? 0 : $("#ManHourRates").val();

    var HoursInMonth = parseFloat(730);    // Asuumed , later may be change
    var HourValue = parseFloat(MonthValue) * HoursInMonth * NoofEmployeeValue;
    var CostValue = parseFloat(ManHourRates) * parseFloat(HourValue);

    var HoursControlID = "#" + SpliID[0] + "__ManhourEstimatesHoursValue";
    var CostControlID = "#" + SpliID[0] + "__ManhourEstimatesCostValue";

    $(HoursControlID).val(HourValue)
    $(CostControlID).val(CostValue);
    $(HoursControlID).change();
    $(CostControlID).change();
  
}
$("#ManHourRates").change(function () {
    $("[id*='ManhourEstimatesNoOfEmployeeValue']").change();
});

//--------------Calculation of Total Hours------------------------------------------

$("[id*='ManhourEstimatesHoursValue']").change(function () {
    var sum = 0;
    $("[id*='ManhourEstimatesHoursValue']").each(function () {
        var Value = ($(this).val() == '') ? 0 : $(this).val();
        if (($(this).attr('id') != 'ManhourEstimates_7__ManhourEstimatesHoursValue') && !isNaN(Value))
            sum = parseFloat(sum) + parseFloat(Value);
    });
    $("#ManhourEstimates_7__ManhourEstimatesHoursValue").val(sum);
});
//--------------Calculation of Total Cost------------------------------------------
$("[id*='ManhourEstimatesCostValue']").change(function () {
    var sum = 0;
    $("[id*='ManhourEstimatesCostValue']").each(function () {
        var Value = ($(this).val() == '') ? 0 : $(this).val();
        if (($(this).attr('id') != 'ManhourEstimates_7__ManhourEstimatesCostValue') && !isNaN(Value))
            sum = parseFloat(sum) + parseFloat(Value);
    });
    $("#ManhourEstimates_7__ManhourEstimatesCostValue").val(sum);
    $("#HeadwiseBudget_2__HeadwiseBudgetValue").val(sum);
    $("[id*='HeadwiseBudgetValue']").change();
});
//--------------Calculation of CapitalOtherExpenditureAmountValue------------------------------------------

$("[id*='CapitalOtherExpenditureAmountValue']").change(function () {
    var Value = ($(this).val() == '') ? 0 : $(this).val();
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
        var sum = 0;
        $("[id*='HeadwiseBudgetValue']").each(function () {
            var Value = ($(this).val() == '') ? 0 : $(this).val();
            if (($(this).attr('id') != 'HeadwiseBudget_8__HeadwiseBudgetValue') && !isNaN(Value))
                sum = parseFloat(sum) + parseFloat(Value);
        });
        $("#HeadwiseBudget_8__HeadwiseBudgetValue").val(sum);
    });
});
HeadwiseBudget_0__HeadwiseBudgetValue


