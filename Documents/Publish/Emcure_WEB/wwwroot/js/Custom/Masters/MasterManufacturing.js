$(document).ready(function () {
    GetManufacturingList();
});

// #region Get Manufacturing List
function GetManufacturingList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllManufacturing, 'GET', GetManufacturingListSuccess, GetManufacturingListError);
}
function GetManufacturingListSuccess(data) {
    try {
        destoryStaticDataTable('#ManufacturingTable');
        $('#ManufacturingTable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#ManufacturingTable tbody').append('<tr><td>' + object.manufacturingName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveManufacturingModel" data-backdrop="static" data-keyboard="false"  onclick="GetManufacturingById(' + object.manufacturingId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteManufacturingModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteManufacturing(' + object.manufacturingId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#ManufacturingTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetManufacturingListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get Manufacturing By Id
function GetManufacturingById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetManufacturingByIdUrl + "/" + id, 'GET', GetManufacturingByIdSuccess, GetManufacturingByIdError);
}
function GetManufacturingByIdSuccess(data) {
    try {
        CleareManufacturingFields();
        $('#SaveManufacturingModel #ManufacturingID').val(data._object.manufacturingId);
        $('#SaveManufacturingModel #ManufacturingName').val(data._object.manufacturingName);
        $('#SaveManufacturingModel #ManufacturingTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveManufacturingModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveManufacturingModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetManufacturingByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update Manufacturing
function AddManufacturing() {
    CleareManufacturingFields();
    $('#SaveManufacturingModel #ManufacturingTitle').html(AddLabel);
}
function SaveMasterManufacturing(form) {
    
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveManufacturing, 'POST', SaveManufacturingSuccess, SaveManufacturingError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveManufacturingSuccess(data) {
    try {
        $('#SaveManufacturingModel').modal('hide');
        if (data._Success === true) {
            CleareManufacturingFields();
            toastr.success(RecordInsertUpdate);
            GetManufacturingList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveManufacturingError(x, y, z) {
    ErrorMessage = x.responseJSON._Message;
    toastr.error(ErrorMessage);
}
function CleareManufacturingFields() {
    $('#SaveManufacturingModel #IsActive').prop('checked', true);
    $('#SaveManufacturingModel #ManufacturingID').val("0");
    $('#SaveManufacturingModel #ManufacturingName').val("");
    $('#DeleteManufacturingModel #ManufacturingID').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete Manufacturing
function ConfirmationDeleteManufacturing(id) {
    $('#DeleteManufacturingModel #ManufacturingID').val(id);
}
function DeleteManufacturing() {
    var tempInAtiveID = $('#DeleteManufacturingModel #ManufacturingID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteManufacturingByIdUrl + "/" + tempInAtiveID, 'POST', DeleteManufacturingByIdSuccess, DeleteManufacturingByIdError);
}
function DeleteManufacturingByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareManufacturingFields();
            toastr.success(RecordDelete);
            GetManufacturingList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteManufacturingByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion