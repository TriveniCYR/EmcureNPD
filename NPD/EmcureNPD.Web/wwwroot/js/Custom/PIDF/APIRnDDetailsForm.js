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
    InitializeMarketDropdown(); 
      
});



function InitializeMarketDropdown() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllMarketExtension, 'GET', GetMarketListSuccess, GetMarketListError);
}
function GetMarketListSuccess(data) {  
    try {
        $.each(data._object, function (index, object) {
            $('#MarketID').append($('<option>').text(object.businessUnitName).attr('value', object.businessUnitId));
        });

        if ($("#PIDFAPIRnDFormID").val()>0) {
            $("#MarketID").val(DBMarketID);  
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetMarketListError(x, y, z) {
    toastr.error(ErrorMessage);
}


$('#imgPreviewMarketdetails').click(function () {
    var ImageUrl = $('#MarketDetailsFileName').val();   
    if (ImageUrl == "" || ImageUrl == undefined) {

    }
    else {
        var win = window.open(ImageUrl, '');
        if (win) {
            //Browser has allowed it to be opened
            win.focus();
        } else {
            //Browser has blocked it
            alert('Please allow popups for this website');
        }
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
function SetDivReadonly() {
    $("#CommercialPIDFScreen").find("input, submit, textarea, a, select").attr("disabled", "disabled");
    $("#CommercialPIDFScreen").find("button, submit, a").hide();
    $("#PIDFScreen").find("#collapseButton").show();
    $("#PIDFScreen").find("#PIDFormcollapseButton").show();
    $("#CommercialPIDFScreen").find("#CommercialcollapseButton").show();
    
    $("#CommercialcollapseButton").click();
    $("#PIDFormcollapseButton").click();
    $("#collapseButton").click();    
}
function ShowPopUpAPIIPD() {
    $("#CancelModelAPIIPD").find("button, submit, a").show();
    $('#CancelModelAPIIPD').modal('show');
}

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




  


