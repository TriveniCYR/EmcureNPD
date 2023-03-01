var ControlsToValidate = ['Development', 'ScaleUp', 'Exhibit', 'PlantQC', 'Total', 'SponsorBusinessPartner', 'APIMarketPrice', 'APITargetRMC_CCPC'];

$(document).ready(function () {
    debugger;
    SetDivReadonly();
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


//function ValidateForm() {
//    var IsInvalid = false;
//    if ($("#MarketDetailsNewPortCGIDetails")[0].files.length <= 0) {
//        $("#valmsgMarketDetailsNewPortCGIDetails").text('Required')
//        IsInvalid = true;
//    }
//    else {
//        $("#valmsgMarketDetailsNewPortCGIDetails").text('')
//    }
//    if ($("#DrugsCategory").val() == '') {
//        $("#valmsgDrugsCategory").text('Required')
//        IsInvalid = true;
//    }
//    else {
//        $("#valmsgDrugsCategory").text('')
//    }
//    if ($("#ProductStrength").val() == '') {
//        $("#valmsgProductStrength").text('Required')
//        IsInvalid = true;
//    }
//    else {
//        $("#valmsgProductStrength").text('')
//    }
//    if (IsInvalid) {
//        $("#IsModelValid").val('')
//    }
//    else {
//        $("#IsModelValid").val('Valid')
//    }
//    return IsInvalid;
//}




  


