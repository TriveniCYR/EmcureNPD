var ControlsToValidate = ['APIGroupLeader','ManHourRates'];
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
    $("#IsModelValid").val('Valid')
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
    CalculateTotalHeadwiseAnalytical();
});

$("#AnalyticalDepartment_4__AnalyticalDepartmentARDValue").change(function () {
    var sum = 0;
    var Value = ($("#AnalyticalDepartment_4__AnalyticalDepartmentARDValue").val() == '') ? 0 : $("#AnalyticalDepartment_4__AnalyticalDepartmentARDValue").val();
    var SumValue = ($("#AnalyticalDepartment_3__AnalyticalDepartmentARDValue").val() == '') ? 0 : $("#AnalyticalDepartment_3__AnalyticalDepartmentARDValue").val();
    if (!isNaN(Value) && !isNaN(SumValue))
        sum = parseFloat(SumValue) + parseFloat(Value);

    $("#AnalyticalDepartment_5__AnalyticalDepartmentARDValue").val(sum);
    CalculateTotalHeadwiseAnalytical();
});
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
});
//--------------Calculation of Analytical Aaw Material---------------------------AnalyticalDepartment_5__AnalyticalDepartmentARDValue
function CalculateTotalHeadwiseAnalytical () {
    var sum = 0;
    var i = 0;
        var Control_Id = "#AnalyticalDepartment_5__AnalyticalDepartmentARDValue";
        var Value = ($(Control_Id).val() == '') ? 0 : $(Control_Id).val();
        if (!isNaN(Value))
        sum = parseFloat(Value);

    $("#HeadwiseBudget_0__HeadwiseBudgetValue").val(sum);
}
//-----------------Calculatyion of HeadwiseBudgetValue---------------------------------------
function CalculateTotalHeadwiseAnalytical() {
    var sum = 0;
    var i = 0;
    var Control_Id = "#AnalyticalDepartment_5__AnalyticalDepartmentARDValue";
    var Value = ($(Control_Id).val() == '') ? 0 : $(Control_Id).val();
    if (!isNaN(Value))
        sum = parseFloat(Value);

    $("#HeadwiseBudget_0__HeadwiseBudgetValue").val(sum);
}



