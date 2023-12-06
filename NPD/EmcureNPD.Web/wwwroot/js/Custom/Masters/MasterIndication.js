﻿$(document).ready(function () {
    GetDIAList();
});

// #region Get DIA List
function GetDIAList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllDIA, 'GET', GetDIAListSuccess, GetDIAListError);
}
function GetDIAListSuccess(data) {
    try {
        destoryStaticDataTable('#DIATable');
        $('#DIATable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#DIATable tbody').append('<tr><td>' + object.indicationName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveDIAModel" data-backdrop="static" data-keyboard="false"  onclick="GetDIAById(' + object.indicationId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow + '" href="" title="Delete" data-toggle="modal" data-target="#DeleteDIAModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteDIA(' + object.indicationId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#DIATable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetDIAListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get DIA By Id
function GetDIAById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetDIAByIdUrl + "/" + id, 'GET', GetDIAByIdSuccess, GetDIAByIdError);
}
function GetDIAByIdSuccess(data) {
    try {
        CleareDIAFields();
        $('#SaveDIAModel #IndicationId').val(data._object.indicationId);
        $('#SaveDIAModel #IndicationName').val(data._object.indicationName);
        $('#SaveDIAModel #DIATitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveDIAModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveDIAModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetDIAByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update DIA
function AddDIA() {
    CleareDIAFields();
    $('#SaveDIAModel #DIATitle').html(AddLabel);
}
function SaveDIAForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveDIA, 'POST', SaveDIAFormSuccess, SaveDIAFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveDIAFormSuccess(data) {
    try {
        $('#SaveDIAModel').modal('hide');
        if (data._Success === true) {
            CleareDIAFields();
            toastr.success(RecordInsertUpdate);
            GetDIAList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveDIAFormError(x, y, z) {
    ErrorMessage = x.responseJSON._Message;
    toastr.error(ErrorMessage);
}
function CleareDIAFields() {
    $('#SaveDIAModel #IsActive').prop('checked', true);
    $('#SaveDIAModel #IndicationId').val("0");
    $('#SaveDIAModel #IndicationName').val("");
    $('#DeleteDIAModel #IndicationId').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete DIA
function ConfirmationDeleteDIA(id) {
    $('#DeleteDIAModel #IndicationId').val(id);
}
function DeleteDIA() {
    var tempInAtiveID = $('#DeleteDIAModel #IndicationId').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteDIAByIdUrl + "/" + tempInAtiveID, 'POST', DeleteDIAByIdSuccess, DeleteDIAByIdError);
}
function DeleteDIAByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareDIAFields();
            toastr.success(RecordDelete);
            GetDIAList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteDIAByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion