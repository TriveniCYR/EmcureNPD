var ControlsToValidate = ['Development', 'ScaleUp', 'Exhibit', 'PlantQC', 'Total', 'SponsorBusinessPartner', 'APIMarketPrice', 'APITargetRMC_CCPC'];
var IsValid = false;
$(document).ready(function () {
    SetDivReadonly();
    $("#IsModelValid").val('');
    if (SaveStatus != '' && SaveStatus != undefined) {
        if (SaveStatus == 'Saved successfully.')
            toastr.success(SaveStatus);
        else
            toastr.error(SaveStatus);
    }
    InitializeMarketDropdown();

    if ($('#MarketDetailsFileName').val() == '') {
        $('#imgPreviewMarketdetails').hide();
    }
    else {
        $('#imgPreviewMarketdetails').show();
    }
    HideSaveAsDraft();
    getPIDFAccordion(_PIDFAccordionURL, _PIDFID, "dvPIDFAccrdion");
    getIPDAccordion(_IPDAccordionURL, _EncPIDFID, _PIDFBusinessUnitId, "dvIPDAccrdion");
    getCommercialAccordion(_CommercialAccordionURL, _EncPIDFID, _PIDFBusinessUnitId, "dvCommercialAccrdion");
    GetMasterAPIInhouselabels();
});

function HideSaveAsDraft() {
    if ($('#StatusId').val() == 15)     //[APIInProgress = 14, APISubmitted = 15]
        $('#SaveDraft').hide();
    else
        $('#SaveDraft').show();
}

function InitializeMarketDropdown() {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAllMarketExtension, 'GET', GetMarketListSuccess, GetMarketListError);
}
function GetMarketListSuccess(data) {
    try {
        $.each(data._object, function (index, object) {
            $('#MarketID').append($('<option>').text(object.businessUnitName).attr('value', object.businessUnitId));
        });

        if ($("#PIDFAPIRnDFormID").val() > 0) {
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
     IsValid = false;
    IsValid = ValidateForm();
    IsValid = validateDynamicControldDetailsRND();
    $("#APIRnD_SaveType").val('Save');
    if (IsValid) {
        return true;
    } else {
        return false;
    }


});
$('#SaveDraft').click(function () {
    $('#frmAPIRnDDetails').validate().settings.ignore = "*";
    $("#APIRnD_SaveType").val('SaveDraft');
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
        var controltobeFocus = ArrofInvalid[ArrofInvalid.length - 1];
        $('#' + controltobeFocus).focus();
        toastr.error('Some fields are missing !');
        $("#IsModelValid").val('')
    }
    else {
        $("#IsModelValid").val('Valid')
    }
    return IsValid;
}
function validateDynamicControldDetailsRND() {
    var IsValid = true;
    $('#frmAPIRnDDetails').find('.customvalidateformcontrol').each(function () {
        if ($(this).val().trim() == '') {
            $(this).css("border-color", "red");
            $(this).focus();
            IsValid = false;
        }
        else {
            $(this).css("border-color", "");
        }
    });
    return IsValid
}
function GetMasterAPIInhouselabels() {
    ajaxServiceMethod($('#hdnBaseURL').val() + getMasterAPIInhouselabels, 'GET', GetMasterAPIInhouselabelsSuccess, GetMasterAPIInhouselabelsError);
}
function GetMasterAPIInhouselabelsSuccess(data) {
    try {
        var masterInhouseLabelTable = document.querySelector(".table.table-bordered.apiRnDTable tbody");
        masterInhouseLabelTable.innerHTML = "";

        if (data != null && data.length > 0) {
            data.forEach(function (item, index) {
                console.log(item);
                var row = masterInhouseLabelTable.insertRow();
                var labelCell = row.insertCell(0);
                labelCell.innerHTML = `<b>${item.apiInhouseName}</b>`;
                var inputCell = row.insertCell(1);
                inputCell.innerHTML = `<input type="text" class="form-control" id="input_${item.apiInhouseId}" name="PIDFAPIInhouseEntities[${index}].Primary"> <input type="hidden" id="${item.apiInhouseId}"name="PIDFAPIInhouseEntities[${index}].ApiInhouseId" value="${item.apiInhouseId}">`;

            });
        } else {
            // Show a message if there is no data
            var emptyRow = masterInhouseLabelTable.insertRow();
            var emptyCell = emptyRow.insertCell(0);
            emptyCell.colSpan = 4;
            emptyCell.innerText = "No data available.";
        }
    } catch (e) {
        toastr.error(e.message);
    }
}
function GetMasterAPIInhouselabelsError(x, y, z) {
    toastr.error(ErrorMessage);
}
