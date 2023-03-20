

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
    InitializeProductTypeDropdown(); 

    var IsImageAvailable = $("#imgPreviewMarketdetails").attr("src");
    if (IsImageAvailable == undefined || IsImageAvailable == '') {
        $("#imgPreviewMarketdetails").hide();
    }

});

function InitializeProductTypeDropdown() {
    debugger;
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllProductType, 'GET', GetProductTypeListSuccess, GetProductTypeListError);
}
function GetProductTypeListSuccess(data) {  
    try {
        $.each(data._object, function (index, object) {
            $('#ProductTypeId').append($('<option>').text(object.productTypeName).attr('value', object.productTypeId));
        });

        if ($("#APIIPDDetailsFormID").val()>0) {
            $("#ProductTypeId").val(DBProductTypeId);  
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetProductTypeListError(x, y, z) {
    toastr.error(ErrorMessage);
}


$('#MarketDetailsNewPortCGIDetails').change(function () {
    debugger;
    if ($("#MarketDetailsNewPortCGIDetails")[0].files[0] != null) {
        $("#imgPreviewMarketdetails").show();
        var $input = $("#MarketDetailsNewPortCGIDetails");
        var reader = new FileReader();
        reader.onload = function () {
            $("#imgPreviewMarketdetails").attr("src", reader.result);
        }
        reader.readAsDataURL($input[0].files[0]);
    } else {
        $("#imgPreviewMarketdetails").hide();
        $("#imgPreviewMarketdetails").attr("src", "");
    }
       
    $("#MarketDetailsFileName").val('');
});
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
    var IsInvalid = false;
    if ($("#MarketDetailsNewPortCGIDetails")[0].files.length <= 0) {
        $("#valmsgMarketDetailsNewPortCGIDetails").text('Required')
        IsInvalid = true;
    }
    else {
        $("#valmsgMarketDetailsNewPortCGIDetails").text('')
    }
    if ($("#DrugsCategory").val() == '') {
        $("#valmsgDrugsCategory").text('Required')
        IsInvalid = true;
    }
    else {
        $("#valmsgDrugsCategory").text('')
    }
    if ($("#ProductStrength").val() == '') {
        $("#valmsgProductStrength").text('Required')
        IsInvalid = true;
    }
    else {
        $("#valmsgProductStrength").text('')
    }
    if (IsInvalid) {
        $("#IsModelValid").val('')
    }
    else {
        $("#IsModelValid").val('Valid')
    }
    return IsInvalid;
}

function OpenImageInNewTab(url,FileName) {
    var win = window.open(url, FileName);
    if (win) {
        //Browser has allowed it to be opened
        win.focus();
    } else {
        //Browser has blocked it
        alert('Please allow popups for this website');
    }
}



  


