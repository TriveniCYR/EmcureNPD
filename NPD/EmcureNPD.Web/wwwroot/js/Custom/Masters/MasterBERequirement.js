$(document).ready(function () {
    GetBERequirementList();
});

// #region Get BERequirement List
function GetBERequirementList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllBERequirement, 'GET', GetBERequirementListSuccess, GetBERequirementListError);
}
function GetBERequirementListSuccess(data) {
    try {
        $('#BERequirementTable tbody').html('')
        $.each(data._object, function (index, object) {
            $('#BERequirementTable tbody').append('<tr><td>' + object.beRequirementName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveBERequirementModel" data-backdrop="static" data-keyboard="false"  onclick="GetBERequirementById(' + object.beRequirementId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteBERequirementModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteBERequirement(' + object.beRequirementId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#BERequirementTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetBERequirementListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get BERequirement By Id
function GetBERequirementById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetBERequirementByIdUrl + "/" + id, 'GET', GetBERequirementByIdSuccess, GetBERequirementByIdError);
}
function GetBERequirementByIdSuccess(data) {
    try {
        CleareBERequirementFields();
        $('#SaveBERequirementModel #BERequirementID').val(data._object.beRequirementId);
        $('#SaveBERequirementModel #BERequirementName').val(data._object.beRequirementName);
        $('#SaveBERequirementModel #BERequirementTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveBERequirementModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveBERequirementModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetBERequirementByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update BERequirement
function AddBERequirement() {
    CleareBERequirementFields();
    $('#SaveBERequirementModel #BERequirementTitle').html(AddLabel);
}
function SaveBERequirementForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveBERequirement, 'POST', SaveBERequirementFormSuccess, SaveBERequirementFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveBERequirementFormSuccess(data) {
    try {
        $('#SaveBERequirementModel').modal('hide');
        if (data._Success === true) {
            CleareBERequirementFields();
            toastr.success(RecordInsertUpdate);
            GetBERequirementList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveBERequirementFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareBERequirementFields() {
    $('#SaveBERequirementModel #IsActive').prop('checked', true);
    $('#SaveBERequirementModel #BERequirementID').val("0");
    $('#SaveBERequirementModel #BERequirementName').val("");
    $('#DeleteBERequirementModel #BERequirementID').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete BERequirement
function ConfirmationDeleteBERequirement(id) {
    $('#DeleteBERequirementModel #BERequirementID').val(id);
}
function DeleteBERequirement() {
    var tempInAtiveID = $('#DeleteBERequirementModel #BERequirementID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteBERequirementByIdUrl + "/" + tempInAtiveID, 'POST', DeleteBERequirementByIdSuccess, DeleteBERequirementByIdError);
}
function DeleteBERequirementByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareBERequirementFields();
            toastr.success(RecordDelete);
            GetBERequirementList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteBERequirementByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion