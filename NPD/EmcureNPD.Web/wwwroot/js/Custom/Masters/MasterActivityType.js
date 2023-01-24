$(document).ready(function () {
    GetActivityTypeList();
});

// #region Get ActivityType List
function GetActivityTypeList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllActivityType, 'GET', GetActivityTypeListSuccess, GetActivityTypeListError);
}
function GetActivityTypeListSuccess(data) {
    try {
        $('#ActivityTypeTable tbody').html('')
        $.each(data._object, function (index, object) {
            $('#ActivityTypeTable tbody').append('<tr><td>' + object.activityTypeName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="btn btn-primary" data-toggle="modal" data-target="#SaveActivityTypeModel" data-backdrop="static" data-keyboard="false"  onclick="GetActivityTypeById(' + object.activityTypeId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + EditLabel + '</a> <a class="btn btn-danger" data-toggle="modal" data-target="#DeleteActivityTypeModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteActivityType(' + object.activityTypeId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + DeleteLabel + '</a>  </td></tr>');
        });
        StaticDataTable("#ActivityTypeTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetActivityTypeListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get ActivityType By Id
function GetActivityTypeById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetActivityTypeByIdUrl + "/" + id, 'GET', GetActivityTypeByIdSuccess, GetActivityTypeByIdError);
}
function GetActivityTypeByIdSuccess(data) {
    try {
        $('#SaveActivityTypeModel #ActivityTypeId').val(data._object.activityTypeId);
        $('#SaveActivityTypeModel #ActivityTypeName').val(data._object.activityTypeName);
        $('#SaveActivityTypeModel #ActivityTypeTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveActivityTypeModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveActivityTypeModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetActivityTypeByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update ActivityType
function AddActivityType() {
    CleareActivityTypeFields();
    $('#SaveActivityTypeModel #ActivityTypeTitle').html(AddLabel);
}
function SaveActivityTypeForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveActivityType, 'POST', SaveActivityTypeFormSuccess, SaveActivityTypeFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveActivityTypeFormSuccess(data) {
    try {
        $('#SaveActivityTypeModel').modal('hide');
        if (data._Success === true) {
            CleareActivityTypeFields();
            toastr.success(RecordInsertUpdate);
            GetActivityTypeList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveActivityTypeFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareActivityTypeFields() {
    $('#SaveActivityTypeModel #IsActive').prop('checked', true);
    $('#SaveActivityTypeModel #ActivityTypeId').val("0");
    $('#SaveActivityTypeModel #ActivityTypeName').val("");
    $('#DeleteActivityTypeModel #ActivityTypeId').val("0");
}
// #endregion

//#region Delete ActivityType
function ConfirmationDeleteActivityType(id) {
    $('#DeleteActivityTypeModel #ActivityTypeId').val(id);
}
function DeleteActivityType() {
    var tempInAtiveID = $('#DeleteActivityTypeModel #ActivityTypeId').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteActivityTypeByIdUrl + "/" + tempInAtiveID, 'POST', DeleteActivityTypeByIdSuccess, DeleteActivityTypeByIdError);
}
function DeleteActivityTypeByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareActivityTypeFields();
            toastr.success(RecordDelete);
            GetActivityTypeList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteActivityTypeByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion