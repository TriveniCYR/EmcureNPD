$(document).ready(function () {
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
    HideSaveAsDraft();
    getPIDFAccordion(_PIDFAccordionURL, _PIDFID, "dvPIDFAccrdion");
    getIPDAccordion(_IPDAccordionURL, _EncPIDFID, _PIDFBusinessUnitId, "dvIPDAccrdion");
    getCommercialAccordion(_CommercialAccordionURL, _EncPIDFID, _PIDFBusinessUnitId, "dvCommercialAccrdion");
});
function HideSaveAsDraft() {
    if ($('#StatusId').val() == 15)     //[APIInProgress = 14, APISubmitted = 15]
        $('#SaveDraft').hide();
    else
        $('#SaveDraft').show();
}

function InitializeProductTypeDropdown() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllProductType, 'GET', GetProductTypeListSuccess, GetProductTypeListError);
}
function GetProductTypeListSuccess(data) {
    try {
        var _emptyOption = '<option value="">-- Select --</option>';
        $('#ProductTypeId').append(_emptyOption);
        $.each(data._object, function (index, object) {
            $('#ProductTypeId').append($('<option>').text(object.productTypeName).attr('value', object.productTypeId));
        });

        if ($("#APIIPDDetailsFormID").val() > 0) {
            $("#ProductTypeId").val(DBProductTypeId);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetProductTypeListError(x, y, z) {
    toastr.error(ErrorMessage);
}
function IsFileTypeImage() {
    var $input = $("#MarketDetailsNewPortCGIDetails");
    var file = $input[0].files[0];
    if (file != null && file != undefined) {
        var fileType = file["type"];
        var validImageTypes = ["image/gif", "image/jpeg", "image/png", "image/jpg"];
        if ($.inArray(fileType, validImageTypes) < 0) {
            $("#valmsgMarketDetailsNewPortCGIDetails").text('Required Image!')
            return false;
        } else {
            $("#valmsgMarketDetailsNewPortCGIDetails").text('');
            return true;
        }
    }
}

$('#MarketDetailsNewPortCGIDetails').change(function () {
    if ($("#MarketDetailsNewPortCGIDetails")[0].files[0] != null) {
        if (IsFileTypeImage()) {
            $("#imgPreviewMarketdetails").show();
            var $input = $("#MarketDetailsNewPortCGIDetails");
            var reader = new FileReader();
            reader.onload = function () {
                $("#imgPreviewMarketdetails").attr("src", reader.result);
            }
            reader.readAsDataURL($input[0].files[0]);
        }
        else {
            $("#imgPreviewMarketdetails").hide();
            $("#imgPreviewMarketdetails").attr("src", "");
        }
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
    $("#APIIPD_SaveType").val('Save');
});
$('#SaveDraft').click(function () {
    $('#frmAPIIPDDetailsForm').validate().settings.ignore = "*";
    $("#APIIPD_SaveType").val('SaveDraft');
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
    var IsInvalid = false; var IsInvalidImage = false;
    var IsPrevImageExist = true;
    var ImageUrl = $('#MarketDetailsFileName').val();
    if (ImageUrl == "" || ImageUrl == undefined) {
        IsPrevImageExist = false;
    }
    if (($("#MarketDetailsNewPortCGIDetails")[0].files.length <= 0) && !IsPrevImageExist) {
        /*$("#valmsgMarketDetailsNewPortCGIDetails").text('Required');*/
        $("#MarketDetailsNewPortCGIDetails").addClass('InvalidBox');
        IsInvalid = true;
        $("#MarketDetailsNewPortCGIDetails").focus();
    }
    else if (!IsFileTypeImage() && !IsPrevImageExist) {
        IsInvalid = true;
        IsInvalidImage = true;
        $("#MarketDetailsNewPortCGIDetails").addClass('InvalidBox');
        $("#MarketDetailsNewPortCGIDetails").focus();
    }
    else {
        $("#valmsgMarketDetailsNewPortCGIDetails").text('');
        $("#MarketDetailsNewPortCGIDetails").removeClass('InvalidBox');
    }
    if ($("#DrugsCategory").val() == '') {
        /*$("#valmsgDrugsCategory").text('Required')*/
        $("#DrugsCategory").addClass('InvalidBox');
        IsInvalid = true;
        $("#DrugsCategory").focus();
    }
    else {
        $("#valmsgDrugsCategory").text('');
        $("#DrugsCategory").removeClass('InvalidBox');
    }
    if ($("#ProductStrength").val() == '') {
        /*$("#valmsgProductStrength").text('Required')*/
        $("#ProductStrength").addClass('InvalidBox');
        IsInvalid = true;
        $("#ProductStrength").focus();
    }
    else {
        $("#valmsgProductStrength").text('');
        $("#ProductStrength").removeClass('InvalidBox');
    }
    if ($("#ProductTypeId").val() == '') {
        $("#ProductTypeId").addClass('InvalidBox').focus();
        IsInvalid = true;
    }
    else {
        $("#ProductTypeId").removeClass('InvalidBox');
    }
    if (IsInvalid) {
        if (IsInvalidImage) { }
        else {
            $("#IsModelValid").val('')
            toastr.error('Some fields are missing !');
        }
    }
    else {
        $("#IsModelValid").val('Valid')
    }
    return IsInvalid;
}

function OpenImageInNewTab(url, FileName) {
    var win = window.open(url, FileName);
    if (win) {
        //Browser has allowed it to be opened
        win.focus();
    } else {
        //Browser has blocked it
        alert('Please allow popups for this website');
    }
}