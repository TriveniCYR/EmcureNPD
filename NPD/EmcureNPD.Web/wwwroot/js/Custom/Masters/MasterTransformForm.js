$(document).ready(function () {
    GetTransformList();
});

// #region Get Transform List
function GetTransformList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllTransform, 'GET', GetTransformListSuccess, GetTransformListError);
}
function GetTransformListSuccess(data) {
    try {
        $('#TransformTable tbody').html('')
        $.each(data._object, function (index, object) {
            $('#TransformTable tbody').append('<tr><td>' + object.transformName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="btn btn-primary" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveTransformModel" data-backdrop="static" data-keyboard="false"  onclick="GetTransformById(' + object.transformId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="btn btn-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteTransformModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteTransform(' + object.transformId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#TransformTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetTransformListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get Transform By Id
function GetTransformById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetTransformByIdUrl + "/" + id, 'GET', GetTransformByIdSuccess, GetTransformByIdError);
}
function GetTransformByIdSuccess(data) {
    try {
        $('#SaveTransformModel #TransformId').val(data._object.transformId);
        $('#SaveTransformModel #TransformName').val(data._object.transformName);
        $('#SaveTransformModel #TransformTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveTransformModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveTransformModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetTransformByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update Transform
function AddTransform() {
    CleareTransformFields();
    $('#SaveTransformModel #TransformTitle').html(AddLabel);
}
function SaveTransformfun(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveTransform, 'POST', SaveTransformSuccess, SaveTransformError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveTransformSuccess(data) {
    try {
        $('#SaveTransformModel').modal('hide');
        if (data._Success === true) {
            CleareTransformFields();
            toastr.success(RecordInsertUpdate);
            GetTransformList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveTransformError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareTransformFields() {
    $('#SaveTransformModel #IsActive').prop('checked', true);
    $('#SaveTransformModel #TransformId').val("0");
    $('#SaveTransformModel #TransformName').val("");
    $('#DeleteTransformModel #TransformId').val("0");
}
// #endregion

//#region Delete Transform
function ConfirmationDeleteTransform(id) {
    $('#DeleteTransformModel #TransformId').val(id);
}
function DeleteTransform() {
    var tempInAtiveID = $('#DeleteTransformModel #TransformId').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteTransformByIdUrl + "/" + tempInAtiveID, 'POST', DeleteTransformByIdSuccess, DeleteTransformByIdError);
}
function DeleteTransformByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareTransformFields();
            toastr.success(RecordDelete);
            GetTransformList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteTransformByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion