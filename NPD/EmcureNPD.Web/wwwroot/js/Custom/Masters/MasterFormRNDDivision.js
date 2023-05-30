$(document).ready(function () {
    GetFormRNDDivisionList();
});

// #region Get FormRNDDivision List
function GetFormRNDDivisionList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllFormRNDDivision, 'GET', GetFormRNDDivisionListSuccess, GetFormRNDDivisionListError);
}
function GetFormRNDDivisionListSuccess(data) {
    try {
        destoryStaticDataTable('#FormRNDDivisionTable');
        $('#FormRNDDivisionTable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#FormRNDDivisionTable tbody').append('<tr><td>' + object.formRNDDivisionName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveFormRNDDivisionModel" data-backdrop="static" data-keyboard="false"  onclick="GetFormRNDDivisionById(' + object.formRNDDivisionId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteFormRNDDivisionModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteFormRNDDivision(' + object.formRNDDivisionId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#FormRNDDivisionTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetFormRNDDivisionListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get FormRNDDivision By Id
function GetFormRNDDivisionById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetFormRNDDivisionByIdUrl + "/" + id, 'GET', GetFormRNDDivisionByIdSuccess, GetFormRNDDivisionByIdError);
}
function GetFormRNDDivisionByIdSuccess(data) {
    try {
        CleareFormRNDDivisionFields();
        $('#SaveFormRNDDivisionModel #FormRNDDivisionID').val(data._object.formRNDDivisionId);
        $('#SaveFormRNDDivisionModel #FormRNDDivisionName').val(data._object.formRNDDivisionName);
        $('#SaveFormRNDDivisionModel #FormRNDDivisionTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveFormRNDDivisionModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveFormRNDDivisionModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetFormRNDDivisionByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update FormRNDDivision
function AddFormRNDDivision() {
    CleareFormRNDDivisionFields();
    $('#SaveFormRNDDivisionModel #FormRNDDivisionTitle').html(AddLabel);
}
function SaveFormRNDDivisionForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveFormRNDDivision, 'POST', SaveFormRNDDivisionFormSuccess, SaveFormRNDDivisionFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveFormRNDDivisionFormSuccess(data) {
    try {
        $('#SaveFormRNDDivisionModel').modal('hide');
        if (data._Success === true) {
            CleareFormRNDDivisionFields();
            toastr.success(RecordInsertUpdate);
            GetFormRNDDivisionList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveFormRNDDivisionFormError(x, y, z) {
    ErrorMessage = x.responseJSON._Message;
    toastr.error(ErrorMessage);
}
function CleareFormRNDDivisionFields() {
    $('#SaveFormRNDDivisionModel #IsActive').prop('checked', true);
    $('#SaveFormRNDDivisionModel #FormRNDDivisionID').val("0");
    $('#SaveFormRNDDivisionModel #FormRNDDivisionName').val("");
    $('#DeleteFormRNDDivisionModel #FormRNDDivisionID').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete FormRNDDivision
function ConfirmationDeleteFormRNDDivision(id) {
    $('#DeleteFormRNDDivisionModel #FormRNDDivisionID').val(id);
}
function DeleteFormRNDDivision() {
    var tempInAtiveID = $('#DeleteFormRNDDivisionModel #FormRNDDivisionID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteFormRNDDivisionByIdUrl + "/" + tempInAtiveID, 'POST', DeleteFormRNDDivisionByIdSuccess, DeleteFormRNDDivisionByIdError);
}
function DeleteFormRNDDivisionByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareFormRNDDivisionFields();
            toastr.success(RecordDelete);
            GetFormRNDDivisionList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteFormRNDDivisionByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion