$(document).ready(function () {
    GetDosageFormList();
});

// #region Get DosageForm List
function GetDosageFormList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllDosageForm, 'GET', GetDosageFormListSuccess, GetDosageFormListError);
}
function GetDosageFormListSuccess(data) {
    try {
        $('#DosageFormTable tbody').html('')
        $.each(data._object, function (index, object) {
            $('#DosageFormTable tbody').append('<tr><td>' + object.dosageFormName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveDosageFormModel" data-backdrop="static" data-keyboard="false"  onclick="GetDosageFormById(' + object.dosageFormId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteDosageFormModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteDosageForm(' + object.dosageFormId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#DosageFormTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetDosageFormListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get DosageForm By Id
function GetDosageFormById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetDosageFormByIdUrl + "/" + id, 'GET', GetDosageFormByIdSuccess, GetDosageFormByIdError);
}
function GetDosageFormByIdSuccess(data) {
    try {
        CleareDosageFormFields();
        $('#SaveDosageFormModel #DosageFormID').val(data._object.dosageFormId);
        $('#SaveDosageFormModel #DosageFormName').val(data._object.dosageFormName);
        $('#SaveDosageFormModel #DosageFormTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveDosageFormModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveDosageFormModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetDosageFormByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update DosageForm
function AddDosageForm() {
    CleareDosageFormFields();
    $('#SaveDosageFormModel #DosageFormTitle').html(AddLabel);
}
function SaveDosageFormForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveDosageForm, 'POST', SaveDosageFormFormSuccess, SaveDosageFormFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveDosageFormFormSuccess(data) {
    try {
        $('#SaveDosageFormModel').modal('hide');
        if (data._Success === true) {
            CleareDosageFormFields();
            toastr.success(RecordInsertUpdate);
            GetDosageFormList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveDosageFormFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareDosageFormFields() {
    $('#SaveDosageFormModel #IsActive').prop('checked', true);
    $('#SaveDosageFormModel #DosageFormID').val("0");
    $('#SaveDosageFormModel #DosageFormName').val("");
    $('#DeleteDosageFormModel #DosageFormID').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete DosageForm
function ConfirmationDeleteDosageForm(id) {
    $('#DeleteDosageFormModel #DosageFormID').val(id);
}
function DeleteDosageForm() {
    var tempInAtiveID = $('#DeleteDosageFormModel #DosageFormID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteDosageFormByIdUrl + "/" + tempInAtiveID, 'POST', DeleteDosageFormByIdSuccess, DeleteDosageFormByIdError);
}
function DeleteDosageFormByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareDosageFormFields();
            toastr.success(RecordDelete);
            GetDosageFormList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteDosageFormByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion