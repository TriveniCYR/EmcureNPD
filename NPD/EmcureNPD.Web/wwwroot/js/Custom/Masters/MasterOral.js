$(document).ready(function () {
    GetOralList();
});

// #region Get Oral List
function GetOralList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllOral, 'GET', GetOralListSuccess, GetOralListError);
}
function GetOralListSuccess(data) {
    try {
        destoryStaticDataTable('#OralTable');
        $('#OralTable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#OralTable tbody').append('<tr><td>' + object.oralName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveOralModel" data-backdrop="static" data-keyboard="false"  onclick="GetOralById(' + object.oralId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteOralModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteOral(' + object.oralId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#OralTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetOralListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get Oral By Id
function GetOralById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetOralByIdUrl + "/" + id, 'GET', GetOralByIdSuccess, GetOralByIdError);
}
function GetOralByIdSuccess(data) {
    try {
        CleareOralFields();
        $('#SaveOralModel #OralID').val(data._object.oralId);
        $('#SaveOralModel #OralName').val(data._object.oralName);
        $('#SaveOralModel #OralTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveOralModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveOralModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetOralByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update Oral
function AddOral() {
    CleareOralFields();
    $('#SaveOralModel #OralTitle').html(AddLabel);
}
function SaveOralForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveOral, 'POST', SaveOralFormSuccess, SaveOralFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveOralFormSuccess(data) {
    try {
        $('#SaveOralModel').modal('hide');
        if (data._Success === true) {
            CleareOralFields();
            toastr.success(RecordInsertUpdate);
            GetOralList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveOralFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareOralFields() {
    $('#SaveOralModel #IsActive').prop('checked', true);
    $('#SaveOralModel #OralID').val("0");
    $('#SaveOralModel #OralName').val("");
    $('#DeleteOralModel #OralID').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete Oral
function ConfirmationDeleteOral(id) {
    $('#DeleteOralModel #OralID').val(id);
}
function DeleteOral() {
    var tempInAtiveID = $('#DeleteOralModel #OralID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteOralByIdUrl + "/" + tempInAtiveID, 'POST', DeleteOralByIdSuccess, DeleteOralByIdError);
}
function DeleteOralByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareOralFields();
            toastr.success(RecordDelete);
            GetOralList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteOralByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion