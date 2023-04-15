$(document).ready(function () {
    GetMarketExtensionList();
});

// #region Get MarketExtension List
function GetMarketExtensionList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllMarketExtension, 'GET', GetMarketExtensionListSuccess, GetMarketExtensionListError);
}
function GetMarketExtensionListSuccess(data) {
    try {
        destoryStaticDataTable('#MarketExtensionTable');
        $('#MarketExtensionTable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#MarketExtensionTable tbody').append('<tr><td>' + object.marketExtenstionName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveMarketExtensionModel" data-backdrop="static" data-keyboard="false"  onclick="GetMarketExtensionById(' + object.marketExtenstionId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow + '" href="" title="Delete" data-toggle="modal" data-target="#DeleteMarketExtensionModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteMarketExtension(' + object.marketExtenstionId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#MarketExtensionTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetMarketExtensionListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get MarketExtension By Id
function GetMarketExtensionById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetMarketExtensionByIdUrl + "/" + id, 'GET', GetMarketExtensionByIdSuccess, GetMarketExtensionByIdError);
}
function GetMarketExtensionByIdSuccess(data) {
    try {
        CleareMarketExtensionFields();
        $('#SaveMarketExtensionModel #MarketExtenstionId').val(data._object.marketExtenstionId);
        $('#SaveMarketExtensionModel #MarketExtenstionName').val(data._object.marketExtenstionName);
        $('#SaveMarketExtensionModel #MarketExtensionTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveMarketExtensionModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveMarketExtensionModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetMarketExtensionByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update MarketExtension
function AddMarketExtension() {
    CleareMarketExtensionFields();
    $('#SaveMarketExtensionModel #MarketExtensionTitle').html(AddLabel);
}
function SaveMarketExtensionForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveMarketExtension, 'POST', SaveMarketExtensionFormSuccess, SaveMarketExtensionFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveMarketExtensionFormSuccess(data) {
    try {
        $('#SaveMarketExtensionModel').modal('hide');
        if (data._Success === true) {
            CleareMarketExtensionFields();
            toastr.success(RecordInsertUpdate);
            GetMarketExtensionList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveMarketExtensionFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareMarketExtensionFields() {
    $('#SaveMarketExtensionModel #IsActive').prop('checked', true);
    $('#SaveMarketExtensionModel #MarketExtenstionId').val("0");
    $('#SaveMarketExtensionModel #MarketExtenstionName').val("");
    $('#DeleteMarketExtensionModel #MarketExtenstionId').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete MarketExtension
function ConfirmationDeleteMarketExtension(id) {
    $('#DeleteMarketExtensionModel #MarketExtenstionId').val(id);
}
function DeleteMarketExtension() {
    var tempInAtiveID = $('#DeleteMarketExtensionModel #MarketExtenstionId').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteMarketExtensionByIdUrl + "/" + tempInAtiveID, 'POST', DeleteMarketExtensionByIdSuccess, DeleteMarketExtensionByIdError);
}
function DeleteMarketExtensionByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareMarketExtensionFields();
            toastr.success(RecordDelete);
            GetMarketExtensionList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteMarketExtensionByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion