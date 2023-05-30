$(document).ready(function () {
    GetPackagingTypeList();
});

// #region Get PackagingType List
function GetPackagingTypeList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllPackagingType, 'GET', GetPackagingTypeListSuccess, GetPackagingTypeListError);
}
function GetPackagingTypeListSuccess(data) {
    try {
        destoryStaticDataTable('#PackagingTypeTable');
        $('#PackagingTypeTable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#PackagingTypeTable tbody').append('<tr><td>' + object.packagingTypeName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SavePackagingTypeModel" data-backdrop="static" data-keyboard="false"  onclick="GetPackagingTypeById(' + object.packagingTypeId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeletePackagingTypeModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeletePackagingType(' + object.packagingTypeId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#PackagingTypeTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPackagingTypeListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get PackagingType By Id
function GetPackagingTypeById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetPackagingTypeByIdUrl + "/" + id, 'GET', GetPackagingTypeByIdSuccess, GetPackagingTypeByIdError);
}
function GetPackagingTypeByIdSuccess(data) {
    try {
        ClearePackagingTypeFields();
        $('#SavePackagingTypeModel #PackagingTypeID').val(data._object.packagingTypeId);
        $('#SavePackagingTypeModel #PackagingTypeName').val(data._object.packagingTypeName);
        $('#SavePackagingTypeModel #PackagingTypeTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SavePackagingTypeModel #IsActive').prop('checked', false);
        }
        else {
            $('#SavePackagingTypeModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPackagingTypeByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update PackagingType
function AddPackagingType() {
    ClearePackagingTypeFields();
    $('#SavePackagingTypeModel #PackagingTypeTitle').html(AddLabel);
}
function SavePackagingTypeForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SavePackagingType, 'POST', SavePackagingTypeFormSuccess, SavePackagingTypeFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SavePackagingTypeFormSuccess(data) {
    try {
        $('#SavePackagingTypeModel').modal('hide');
        if (data._Success === true) {
            ClearePackagingTypeFields();
            toastr.success(RecordInsertUpdate);
            GetPackagingTypeList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SavePackagingTypeFormError(x, y, z) {
    ErrorMessage = x.responseJSON._Message;
    toastr.error(ErrorMessage);
}
function ClearePackagingTypeFields() {
    $('#SavePackagingTypeModel #IsActive').prop('checked', true);
    $('#SavePackagingTypeModel #PackagingTypeID').val("0");
    $('#SavePackagingTypeModel #PackagingTypeName').val("");
    $('#DeletePackagingTypeModel #PackagingTypeID').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete PackagingType
function ConfirmationDeletePackagingType(id) {
    $('#DeletePackagingTypeModel #PackagingTypeID').val(id);
}
function DeletePackagingType() {
    var tempInAtiveID = $('#DeletePackagingTypeModel #PackagingTypeID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeletePackagingTypeByIdUrl + "/" + tempInAtiveID, 'POST', DeletePackagingTypeByIdSuccess, DeletePackagingTypeByIdError);
}
function DeletePackagingTypeByIdSuccess(data) {
    try {
        if (data._Success === true) {
            ClearePackagingTypeFields();
            toastr.success(RecordDelete);
            GetPackagingTypeList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeletePackagingTypeByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion