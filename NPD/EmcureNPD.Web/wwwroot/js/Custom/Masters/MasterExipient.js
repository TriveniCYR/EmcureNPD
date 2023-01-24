$(document).ready(function () {
    GetExipientList();
});

// #region Get Exipient List
function GetExipientList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllExipient, 'GET', GetExipientListSuccess, GetExipientListError);
}
function GetExipientListSuccess(data) {
    try {
        $('#ExipientTable tbody').html('')
        $.each(data._object, function (index, object) {
            $('#ExipientTable tbody').append('<tr><td>' + object.exipientName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="btn btn-primary" data-toggle="modal" data-target="#SaveExipientModel" data-backdrop="static" data-keyboard="false"  onclick="GetExipientById(' + object.exipientId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + EditLabel + '</a> <a class="btn btn-danger" data-toggle="modal" data-target="#DeleteExipientModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteExipient(' + object.exipientId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + DeleteLabel + '</a>  </td></tr>');
        });
        StaticDataTable("#ExipientTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetExipientListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get Exipient By Id
function GetExipientById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetExipientByIdUrl + "/" + id, 'GET', GetExipientByIdSuccess, GetExipientByIdError);
}
function GetExipientByIdSuccess(data) {
    try {
        $('#SaveExipientModel #ExipientID').val(data._object.exipientId);
        $('#SaveExipientModel #ExipientName').val(data._object.exipientName);
        $('#SaveExipientModel #ExipientTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveExipientModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveExipientModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetExipientByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update Exipient
function AddExipient() {
    CleareExipientFields();
    $('#SaveExipientModel #ExipientTitle').html(AddLabel);
}
function SaveExipientForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveExipient, 'POST', SaveExipientFormSuccess, SaveExipientFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveExipientFormSuccess(data) {
    try {
        $('#SaveExipientModel').modal('hide');
        if (data._Success === true) {
            CleareExipientFields();
            toastr.success(RecordInsertUpdate);
            GetExipientList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveExipientFormError(x, y, z) {
    toastr.error(ErrorMessage);
}
function CleareExipientFields() {
    $('#SaveExipientModel #IsActive').prop('checked', true);
    $('#SaveExipientModel #ExipientID').val("0");
    $('#SaveExipientModel #ExipientName').val("");
    $('#DeleteExipientModel #ExipientID').val("0");
}
// #endregion

//#region Delete Exipient
function ConfirmationDeleteExipient(id) {
    $('#DeleteExipientModel #ExipientID').val(id);
}
function DeleteExipient() {
    var tempInAtiveID = $('#DeleteExipientModel #ExipientID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteExipientByIdUrl + "/" + tempInAtiveID, 'POST', DeleteExipientByIdSuccess, DeleteExipientByIdError);
}
function DeleteExipientByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareExipientFields();
            toastr.success(RecordDelete);
            GetExipientList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteExipientByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion