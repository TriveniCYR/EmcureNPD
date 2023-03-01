$(document).ready(function () {
    GetAPISourcingList();
});

// #region Get APISourcing List
function GetAPISourcingList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllAPISourcing, 'GET', GetAPISourcingListSuccess, GetAPISourcingListError);
}
function GetAPISourcingListSuccess(data) {
    try {
        $('#APISourcingTable tbody').html('')
        $.each(data._object, function (index, object) {
            $('#APISourcingTable tbody').append('<tr><td>' + object.apiSourcingName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveAPISourcingModel" data-backdrop="static" data-keyboard="false"  onclick="GetAPISourcingById(' + object.apiSourcingId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteAPISourcingModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteAPISourcing(' + object.apiSourcingId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#APISourcingTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetAPISourcingListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get APISourcing By Id
function GetAPISourcingById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAPISourcingByIdUrl + "/" + id, 'GET', GetAPISourcingByIdSuccess, GetAPISourcingByIdError);
}
function GetAPISourcingByIdSuccess(data) {
    try {
        CleareAPISourcingFields();
        $('#SaveAPISourcingModel #APISourcingID').val(data._object.apiSourcingId);
        $('#SaveAPISourcingModel #APISourcingName').val(data._object.apiSourcingName);
        $('#SaveAPISourcingModel #APISourcingTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveAPISourcingModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveAPISourcingModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetAPISourcingByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update APISourcing
function AddAPISourcing() {
    CleareAPISourcingFields();
    $('#SaveAPISourcingModel #APISourcingTitle').html(AddLabel);
}
function SaveAPISourcingForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveAPISourcing, 'POST', SaveAPISourcingFormSuccess, SaveAPISourcingFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveAPISourcingFormSuccess(data) {
    try {
        $('#SaveAPISourcingModel').modal('hide');
        if (data._Success === true) {
            CleareAPISourcingFields();
            toastr.success(RecordInsertUpdate);
            GetAPISourcingList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveAPISourcingFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareAPISourcingFields() {
    $('#SaveAPISourcingModel #IsActive').prop('checked', true);
    $('#SaveAPISourcingModel #APISourcingID').val("0");
    $('#SaveAPISourcingModel #APISourcingName').val("");
    $('#DeleteAPISourcingModel #APISourcingID').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete APISourcing
function ConfirmationDeleteAPISourcing(id) {
    $('#DeleteAPISourcingModel #APISourcingID').val(id);
}
function DeleteAPISourcing() {
    var tempInAtiveID = $('#DeleteAPISourcingModel #APISourcingID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteAPISourcingByIdUrl + "/" + tempInAtiveID, 'POST', DeleteAPISourcingByIdSuccess, DeleteAPISourcingByIdError);
}
function DeleteAPISourcingByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareAPISourcingFields();
            toastr.success(RecordDelete);
            GetAPISourcingList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteAPISourcingByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion