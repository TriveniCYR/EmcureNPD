$(document).ready(function () {
    GetPackSizeList();
});

// #region Get PackSize List
function GetPackSizeList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllPackSize, 'GET', GetPackSizeListSuccess, GetPackSizeListError);
}
function GetPackSizeListSuccess(data) {
    try {
        destoryStaticDataTable('#PackSizeTable');
        $('#PackSizeTable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#PackSizeTable tbody').append('<tr><td>' + object.packSizeName + '</td><td>' + (object.packSize == null ? "" : object.packSize) + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SavePackSizeModel" data-backdrop="static" data-keyboard="false"  onclick="GetPackSizeById(' + object.packSizeId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeletePackSizeModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeletePackSize(' + object.packSizeId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#PackSizeTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPackSizeListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get PackSize By Id
function GetPackSizeById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetPackSizeByIdUrl + "/" + id, 'GET', GetPackSizeByIdSuccess, GetPackSizeByIdError);
}
function GetPackSizeByIdSuccess(data) {
    try {
        ClearePackSizeFields();
        $('#SavePackSizeModel #PackSizeID').val(data._object.packSizeId);
        $('#SavePackSizeModel #PackSizeName').val(data._object.packSizeName);
        $('#SavePackSizeModel #PackSize').val(data._object.packSize);
        $('#SavePackSizeModel #PackSizeTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SavePackSizeModel #IsActive').prop('checked', false);
        }
        else {
            $('#SavePackSizeModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetPackSizeByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update PackSize
function AddPackSize() {
    ClearePackSizeFields();
    $('#SavePackSizeModel #PackSizeTitle').html(AddLabel);
}
function SaveMasterPackSize(form) {
    
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SavePackSize, 'POST', SavePackSizeSuccess, SavePackSizeError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SavePackSizeSuccess(data) {
    try {
        $('#SavePackSizeModel').modal('hide');
        if (data._Success === true) {
            ClearePackSizeFields();
            toastr.success(RecordInsertUpdate);
            GetPackSizeList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SavePackSizeError(x, y, z) {
    toastr.error(ErrorMessage);
}
function ClearePackSizeFields() {
    $('#SavePackSizeModel #IsActive').prop('checked', true);
    $('#SavePackSizeModel #PackSizeID').val("0");
    $('#SavePackSizeModel #PackSizeName').val("");
    $('#SavePackSizeModel #PackSize').val("");
    $('#DeletePackSizeModel #PackSizeID').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete PackSize
function ConfirmationDeletePackSize(id) {
    $('#DeletePackSizeModel #PackSizeID').val(id);
}
function DeletePackSize() {
    var tempInAtiveID = $('#DeletePackSizeModel #PackSizeID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeletePackSizeByIdUrl + "/" + tempInAtiveID, 'POST', DeletePackSizeByIdSuccess, DeletePackSizeByIdError);
}
function DeletePackSizeByIdSuccess(data) {
    try {
        if (data._Success === true) {
            ClearePackSizeFields();
            toastr.success(RecordDelete);
            GetPackSizeList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeletePackSizeByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion