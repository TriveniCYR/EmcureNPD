var ControlsToValidate = ['APIGroupLeader','ManHourRates'];
var DivsToValidate = ['dvTimelineInMonths', 'dvManhourEstimates', 'dvAnalyticalDepartment', 'dvPRDDepartment', 'dvCapitalOtherExpenditure', 'dvManhourEstimates','dvHeadwiseBudget'];


$(document).ready(function () {
    debugger;
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
    /*------------------- TimelineInMonthsValue--------------------*/
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






