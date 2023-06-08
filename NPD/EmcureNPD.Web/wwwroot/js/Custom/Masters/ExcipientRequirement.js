$(document).ready(function () {
    GetExcipientRequirementList();
});

// #region Get ExcipientRequirement List
function GetExcipientRequirementList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllExcipientRequirement, 'GET', GetExcipientRequirementListSuccess, GetExcipientRequirementListError);
}
function GetExcipientRequirementListSuccess(data) {
    try {
        destoryStaticDataTable('#ExcipientRequirementTable');
        $('#ExcipientRequirementTable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#ExcipientRequirementTable tbody').append('<tr><td>' + object.excipientRequirementName + '</td><td>' + (object.excipientRequirementCost == null ? "" : object.excipientRequirementCost) + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveExcipientRequirementModel" data-backdrop="static" data-keyboard="false"  onclick="GetExcipientRequirementById(' + object.excipientRequirementId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow + '" href="" title="Delete" data-toggle="modal" data-target="#DeleteExcipientRequirementModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteExcipientRequirement(' + object.excipientRequirementId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#ExcipientRequirementTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetExcipientRequirementListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get ExcipientRequirement By Id
function GetExcipientRequirementById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetExcipientRequirementByIdUrl + "/" + id, 'GET', GetExcipientRequirementByIdSuccess, GetExcipientRequirementByIdError);
}
function GetExcipientRequirementByIdSuccess(data) {
    try {
        CleareExcipientRequirementFields();
        $('#ExcipientRequirementModel').modal('show');
        $('#ExcipientRequirementModel #ExcipientRequirementId').val(data._object.excipientRequirementId);
        $('#ExcipientRequirementModel #ExcipientRequirementName').val(data._object.excipientRequirementName);
        $('#ExcipientRequirementModel #ExcipientRequirementCost').val(data._object.excipientRequirementCost);
        $('#ExcipientRequirementModel #ExcipientRequirementTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#ExcipientRequirementModel #IsActive').prop('checked', false);
        }
        else {
            $('#ExcipientRequirementModel #IsActive').prop('checked', true);
        }
       
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetExcipientRequirementByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update ExcipientRequirement
function AddExcipientRequirement() {
    CleareExcipientRequirementFields();
    $('#ExcipientRequirementModel #ExcipientRequirementTitle').html(AddLabel);
}
function SaveMasterExcipientRequirement(form) {

    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveExcipientRequirement, 'POST', SaveExcipientRequirementSuccess, SaveExcipientRequirementError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveExcipientRequirementSuccess(data) {
    try {
        $('#ExcipientRequirementModel').modal('hide');
        if (data._Success === true) {
            CleareExcipientRequirementFields();
            toastr.success(RecordInsertUpdate);
            GetExcipientRequirementList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveExcipientRequirementError(x, y, z) {
    ErrorMessage = x.responseJSON._Message;
    toastr.error(ErrorMessage);
}
function CleareExcipientRequirementFields() {
    $('#ExcipientRequirementModel #IsActive').prop('checked', true);
    $('#ExcipientRequirementModel #ExcipientRequirementId').val("0");
    $('#ExcipientRequirementModel #ExcipientRequirementName').val("");
    $('#ExcipientRequirementModel #ExcipientRequirementCost').val("");
    $('#DeleteExcipientRequirementModel #ExcipientRequirementId').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete ExcipientRequirement
function ConfirmationDeleteExcipientRequirement(id) {
    $('#DeleteExcipientRequirementModel #ExcipientRequirementId').val(id);
}
function DeleteExcipientRequirement() {
    var tempInAtiveID = $('#DeleteExcipientRequirementModel #ExcipientRequirementId').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteExcipientRequirementByIdUrl + "/" + tempInAtiveID, 'POST', DeleteExcipientRequirementByIdSuccess, DeleteExcipientRequirementByIdError);
}
function DeleteExcipientRequirementByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareExcipientRequirementFields();
            toastr.success(RecordDelete);
            GetExcipientRequirementList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteExcipientRequirementByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion