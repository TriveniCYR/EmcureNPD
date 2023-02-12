
$(document).ready(function () {
    SetDivReadonly();
    $("#IsModelValid").val('');
    if (SaveStatus != '' && SaveStatus != undefined)
    {
        if (SaveStatus == 'Saved successfully.')
            toastr.success(SaveStatus);
        else
            toastr.error(SaveStatus);
    }
});

function InitializeProductTypeDropdown() {
    debugger;
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllProductType, 'GET', GetProductTypeListSuccess, GetProductTypeListError);
}
function GetProductTypeListSuccess(data) {
    console.log(data);
    try {
        $.each(data._object, function (index, object) {
            $('#ProductTypeId').append($('<option>').text(object.productTypeName).attr('value', object.productTypeId));
        });
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetProductTypeListError(x, y, z) {
    toastr.error(ErrorMessage);
}


$('#btnPreview').click(function () {
    debugger;
    var $input = $("#MarketDetailsNewPortCGIDetails");
    var reader = new FileReader();
    reader.onload = function () {
        $("#imgPreviewMarketdetails").attr("src", reader.result);
    }
    reader.readAsDataURL($input[0].files[0]);
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
    var IsInvalid = false;
    if ($("#DrugsCategory").val() == '') {
        $("#valmsgDrugsCategory").val('Required')
        IsInvalid = true;
    }
    if ($("#ProductStrength").val() == '') {
        $("#valmsgProductStrength").val('Required')
        IsInvalid = true;
    }
    if (IsInvalid) {
        $("#IsModelValid").val('')
    }
    else {
        $("#IsModelValid").val('Valid')
    }
    return IsInvalid;
}





  


