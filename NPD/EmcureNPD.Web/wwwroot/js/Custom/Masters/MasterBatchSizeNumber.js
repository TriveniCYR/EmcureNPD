$(document).ready(function () {
    GetBatchSizeNumberList();
});

// #region Get BatchSizeNumber List
function GetBatchSizeNumberList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllBatchSizeNumber, 'GET', GetBatchSizeNumberListSuccess, GetBatchSizeNumberListError);
}
function GetBatchSizeNumberListSuccess(data) {
    try {
        $('#BatchSizeNumberTable tbody').html('')
        $.each(data._object, function (index, object) {
            $('#BatchSizeNumberTable tbody').append('<tr><td>' + object.batchSizeNumberName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="btn btn-primary" data-toggle="modal" data-target="#SaveBatchSizeNumberModel" data-backdrop="static" data-keyboard="false"  onclick="GetBatchSizeNumberById(' + object.batchSizeNumberId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + EditLabel + '</a> <a class="btn btn-danger" data-toggle="modal" data-target="#DeleteBatchSizeNumberModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteBatchSizeNumber(' + object.batchSizeNumberId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + DeleteLabel + '</a>  </td></tr>');
        });
        StaticDataTable("#BatchSizeNumberTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetBatchSizeNumberListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get BatchSizeNumber By Id
function GetBatchSizeNumberById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetBatchSizeNumberByIdUrl + "/" + id, 'GET', GetBatchSizeNumberByIdSuccess, GetBatchSizeNumberByIdError);
}
function GetBatchSizeNumberByIdSuccess(data) {
    try {
        $('#SaveBatchSizeNumberModel #BatchSizeNumberID').val(data._object.batchSizeNumberId);
        $('#SaveBatchSizeNumberModel #BatchSizeNumberName').val(data._object.batchSizeNumberName);
        $('#SaveBatchSizeNumberModel #BatchSizeNumberTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveBatchSizeNumberModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveBatchSizeNumberModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetBatchSizeNumberByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update BatchSizeNumber
function AddBatchSizeNumber() {
    CleareBatchSizeNumberFields();
    $('#SaveBatchSizeNumberModel #BatchSizeNumberTitle').html(AddLabel);
}
function SaveBatchSizeNumberForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveBatchSizeNumber, 'POST', SaveBatchSizeNumberFormSuccess, SaveBatchSizeNumberFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveBatchSizeNumberFormSuccess(data) {
    try {
        $('#SaveBatchSizeNumberModel').modal('hide');
        if (data._Success === true) {
            CleareBatchSizeNumberFields();
            toastr.success(RecordInsertUpdate);
            GetBatchSizeNumberList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveBatchSizeNumberFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareBatchSizeNumberFields() {
    $('#SaveBatchSizeNumberModel #IsActive').prop('checked', true);
    $('#SaveBatchSizeNumberModel #BatchSizeNumberID').val("0");
    $('#SaveBatchSizeNumberModel #BatchSizeNumberName').val("");
    $('#DeleteBatchSizeNumberModel #BatchSizeNumberID').val("0");
}
// #endregion

//#region Delete BatchSizeNumber
function ConfirmationDeleteBatchSizeNumber(id) {
    $('#DeleteBatchSizeNumberModel #BatchSizeNumberID').val(id);
}
function DeleteBatchSizeNumber() {
    var tempInAtiveID = $('#DeleteBatchSizeNumberModel #BatchSizeNumberID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteBatchSizeNumberByIdUrl + "/" + tempInAtiveID, 'POST', DeleteBatchSizeNumberByIdSuccess, DeleteBatchSizeNumberByIdError);
}
function DeleteBatchSizeNumberByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareBatchSizeNumberFields();
            toastr.success(RecordDelete);
            GetBatchSizeNumberList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteBatchSizeNumberByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion