$(document).ready(function () {
    GetAPISourcingList();
});

// #region Get APISourcing List
function GetAPISourcingList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllAPISourcing, 'GET', GetAPISourcingListSuccess, GetAPISourcingListError);
}
function GetAPISourcingListSuccess(data) {
    try {
        $('#APISourcingTable tbody').html('')
        $.each(data._object, function (index, object) {
            $('#APISourcingTable tbody').append('<tr><td>' + object.apiSourcingName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="btn btn-primary" data-toggle="modal" data-target="#SaveAPISourcingModel" data-backdrop="static" data-keyboard="false"  onclick="GetAPISourcingById(' + object.apiSourcingId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + EditLabel + '</a> <a class="btn btn-danger" data-toggle="modal" data-target="#DeleteAPISourcingModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteAPISourcing(' + object.apiSourcingId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + DeleteLabel + '</a>  </td></tr>');
        });
        StaticDataTable("#APISourcingTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetAPISourcingListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get APISourcing By Id
function GetAPISourcingById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetAPISourcingByIdUrl + "/" + id, 'GET', GetAPISourcingByIdSuccess, GetAPISourcingByIdError);
}
function GetAPISourcingByIdSuccess(data) {
    try {
        $('#SaveAPISourcingModel #APISourcingID').val(data._object.apiSourcingId);
        $('#SaveAPISourcingModel #APISourcingName').val(data._object.apiSourcingName);
        $('#SaveAPISourcingModel #APISourcingTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveAPISourcingModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveAPISourcingModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetAPISourcingByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update APISourcing
function AddAPISourcing() {
    CleareAPISourcingFields();
    $('#SaveAPISourcingModel #APISourcingTitle').html(AddLabel);
}
function SaveAPISourcingForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveAPISourcing, 'POST', SaveAPISourcingFormSuccess, SaveAPISourcingFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveAPISourcingFormSuccess(data) {
    try {
        $('#SaveAPISourcingModel').modal('hide');
        if (data._Success === true) {
            CleareAPISourcingFields();
            toastr.success(RecordInsertUpdate);
            GetAPISourcingList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveAPISourcingFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareAPISourcingFields() {
    $('#SaveAPISourcingModel #IsActive').prop('checked', true);
    $('#SaveAPISourcingModel #APISourcingID').val("0");
    $('#SaveAPISourcingModel #APISourcingName').val("");
    $('#DeleteAPISourcingModel #APISourcingID').val("0");
}
// #endregion

//#region Delete APISourcing
function ConfirmationDeleteAPISourcing(id) {
    $('#DeleteAPISourcingModel #APISourcingID').val(id);
}
function DeleteAPISourcing() {
    var tempInAtiveID = $('#DeleteAPISourcingModel #APISourcingID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteAPISourcingByIdUrl + "/" + tempInAtiveID, 'POST', DeleteAPISourcingByIdSuccess, DeleteAPISourcingByIdError);
}
function DeleteAPISourcingByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareAPISourcingFields();
            toastr.success(RecordDelete);
            GetAPISourcingList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteAPISourcingByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion