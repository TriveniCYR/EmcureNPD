$(document).ready(function () {
    GetExtensionApplicationList();
});

// #region Get ExtensionApplication List
function GetExtensionApplicationList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllExtensionApplication, 'GET', GetExtensionApplicationListSuccess, GetExtensionApplicationListError);
}
function GetExtensionApplicationListSuccess(data) {
    try {
        destoryStaticDataTable('#ExtensionApplicationTable');
        $('#ExtensionApplicationTable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#ExtensionApplicationTable tbody').append('<tr><td>' + object.extensionApplicationName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveExtensionApplicationModel" data-backdrop="static" data-keyboard="false"  onclick="GetExtensionApplicationById(' + object.extensionApplicationId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteExtensionApplicationModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteExtensionApplication(' + object.extensionApplicationId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#ExtensionApplicationTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetExtensionApplicationListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get ExtensionApplication By Id
function GetExtensionApplicationById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetExtensionApplicationByIdUrl + "/" + id, 'GET', GetExtensionApplicationByIdSuccess, GetExtensionApplicationByIdError);
}
function GetExtensionApplicationByIdSuccess(data) {
    try {
        CleareExtensionApplicationFields();
        $('#SaveExtensionApplicationModel #ExtensionApplicationId').val(data._object.extensionApplicationId);
        $('#SaveExtensionApplicationModel #ExtensionApplicationName').val(data._object.extensionApplicationName);
        $('#SaveExtensionApplicationModel #ExtensionApplicationTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveExtensionApplicationModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveExtensionApplicationModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetExtensionApplicationByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update ExtensionApplication
function AddExtensionApplication() {
    CleareExtensionApplicationFields();
    $('#SaveExtensionApplicationModel #ExtensionApplicationTitle').html(AddLabel);
}
function SaveExtensionApplicationForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveExtensionApplication, 'POST', SaveExtensionApplicationFormSuccess, SaveExtensionApplicationFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveExtensionApplicationFormSuccess(data) {
    try {
        $('#SaveExtensionApplicationModel').modal('hide');
        if (data._Success === true) {
            CleareExtensionApplicationFields();
            toastr.success(RecordInsertUpdate);
            GetExtensionApplicationList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveExtensionApplicationFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareExtensionApplicationFields() {
    $('#SaveExtensionApplicationModel #IsActive').prop('checked', true);
    $('#SaveExtensionApplicationModel #ExtensionApplicationId').val("0");
    $('#SaveExtensionApplicationModel #ExtensionApplicationName').val("");
    $('#DeleteExtensionApplicationModel #ExtensionApplicationId').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete ExtensionApplication
function ConfirmationDeleteExtensionApplication(id) {
    $('#DeleteExtensionApplicationModel #ExtensionApplicationId').val(id);
}
function DeleteExtensionApplication() {
    var tempInAtiveID = $('#DeleteExtensionApplicationModel #ExtensionApplicationId').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteExtensionApplicationByIdUrl + "/" + tempInAtiveID, 'POST', DeleteExtensionApplicationByIdSuccess, DeleteExtensionApplicationByIdError);
}
function DeleteExtensionApplicationByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareExtensionApplicationFields();
            toastr.success(RecordDelete);
            GetExtensionApplicationList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteExtensionApplicationByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion