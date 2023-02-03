$(document).ready(function () {
    GetAnalyticalGLList();
});

// #region Get AnalyticalGL List
function GetAnalyticalGLList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllAnalyticalGL, 'GET', GetAnalyticalGLListSuccess, GetAnalyticalGLListError);
}
function GetAnalyticalGLListSuccess(data) {
    try {
        $('#AnalyticalGLTable tbody').html('')
        $.each(data._object, function (index, object) {
            $('#AnalyticalGLTable tbody').append('<tr><td>' + object.analyticalName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="btn btn-primary" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveAnalyticalGLModel" data-backdrop="static" data-keyboard="false"  onclick="GetAnalyticalGLById(' + object.analyticalId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="btn btn-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteAnalyticalGLModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteAnalyticalGL(' + object.analyticalId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#AnalyticalGLTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetAnalyticalGLListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get AnalyticalGL By Id
function GetAnalyticalGLById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAnalyticalGLByIdUrl + "/" + id, 'GET', GetAnalyticalGLByIdSuccess, GetAnalyticalGLByIdError);
}
function GetAnalyticalGLByIdSuccess(data) {
    try {
        $('#SaveAnalyticalGLModel #AnalyticalID').val(data._object.analyticalId);
        $('#SaveAnalyticalGLModel #AnalyticalName').val(data._object.analyticalName);
        $('#SaveAnalyticalGLModel #AnalyticalGLTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveAnalyticalGLModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveAnalyticalGLModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetAnalyticalGLByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update AnalyticalGL
function AddAnalyticalGL() {
    CleareAnalyticalGLFields();
    $('#SaveAnalyticalGLModel #AnalyticalGLTitle').html(AddLabel);
}
function SaveAnalyticalGLForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveAnalyticalGL, 'POST', SaveAnalyticalGLFormSuccess, SaveAnalyticalGLFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveAnalyticalGLFormSuccess(data) {
    try {
        $('#SaveAnalyticalGLModel').modal('hide');
        if (data._Success === true) {
            CleareAnalyticalGLFields();
            toastr.success(RecordInsertUpdate);
            GetAnalyticalGLList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveAnalyticalGLFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareAnalyticalGLFields() {
    $('#SaveAnalyticalGLModel #IsActive').prop('checked', true);
    $('#SaveAnalyticalGLModel #AnalyticalID').val("0");
    $('#SaveAnalyticalGLModel #AnalyticalName').val("");
    $('#DeleteAnalyticalGLModel #AnalyticalID').val("0");
}
// #endregion

//#region Delete AnalyticalGL
function ConfirmationDeleteAnalyticalGL(id) {
    $('#DeleteAnalyticalGLModel #AnalyticalID').val(id);
}
function DeleteAnalyticalGL() {
    var tempInAtiveID = $('#DeleteAnalyticalGLModel #AnalyticalID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteAnalyticalGLByIdUrl + "/" + tempInAtiveID, 'POST', DeleteAnalyticalGLByIdSuccess, DeleteAnalyticalGLByIdError);
}
function DeleteAnalyticalGLByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareAnalyticalGLFields();
            toastr.success(RecordDelete);
            GetAnalyticalGLList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteAnalyticalGLByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion