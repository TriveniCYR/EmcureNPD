$(document).ready(function () {
    GetUnitofMeasurementList();
});

// #region Get UnitofMeasurement List
function GetUnitofMeasurementList() {
    ajaxServiceMethod($('#hdnBaseURL').val() + AllUnitofMeasurement, 'GET', GetUnitofMeasurementListSuccess, GetUnitofMeasurementListError);
}
function GetUnitofMeasurementListSuccess(data) {
    try {
        destoryStaticDataTable('#UnitofMeasurementTable');
        $('#UnitofMeasurementTable tbody').html('');
        $.each(data._object, function (index, object) {
            $('#UnitofMeasurementTable tbody').append('<tr><td>' + object.unitofMeasurementName + '</td><td><span style="color:' + (object.isActive ? "green" : "red") + '">' + (object.isActive ? "Active" : "InActive") + '</span></td><td>  <a class="large-font" style="' + IsEditAllow + '" href="" title="Edit" data-toggle="modal" data-target="#SaveUnitofMeasurementModel" data-backdrop="static" data-keyboard="false"  onclick="GetUnitofMeasurementById(' + object.unitofMeasurementId + '); return false;"><i class="fa fa-fw fa-edit mr-1"></i> ' + '</a> <a class="large-font text-danger" style="' + IsDeleteAllow +'" href="" title="Delete" data-toggle="modal" data-target="#DeleteUnitofMeasurementModel" data-backdrop="static" data-keyboard="false" onclick="ConfirmationDeleteUnitofMeasurement(' + object.unitofMeasurementId + '); return false;"><i class="fa fa-fw fa-trash mr-1"></i> ' + '</a>  </td></tr>');
        });
        StaticDataTable("#UnitofMeasurementTable");
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetUnitofMeasurementListError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Get UnitofMeasurement By Id
function GetUnitofMeasurementById(id) {
    ajaxServiceMethod($('#hdnBaseURL').val() + GetUnitofMeasurementByIdUrl + "/" + id, 'GET', GetUnitofMeasurementByIdSuccess, GetUnitofMeasurementByIdError);
}
function GetUnitofMeasurementByIdSuccess(data) {
    try {
        CleareUnitofMeasurementFields();
        $('#SaveUnitofMeasurementModel #UnitofMeasurementID').val(data._object.unitofMeasurementId);
        $('#SaveUnitofMeasurementModel #UnitofMeasurementName').val(data._object.unitofMeasurementName);
        $('#SaveUnitofMeasurementModel #UnitofMeasurementTitle').html(UpdateLabel);
        if (!data._object.isActive) {
            $('#SaveUnitofMeasurementModel #IsActive').prop('checked', false);
        }
        else {
            $('#SaveUnitofMeasurementModel #IsActive').prop('checked', true);
        }
    }
    catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function GetUnitofMeasurementByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
// #endregion

// #region Insert/Update UnitofMeasurement
function AddUnitofMeasurement() {
    CleareUnitofMeasurementFields();
    $('#SaveUnitofMeasurementModel #UnitofMeasurementTitle').html(AddLabel);
}
function SaveUnitofMeasurementForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        ajaxServiceMethod($('#hdnBaseURL').val() + SaveUnitofMeasurement, 'POST', SaveUnitofMeasurementFormSuccess, SaveUnitofMeasurementFormError, JSON.stringify(getFormData($(form))));
    }
    return false;
}
function SaveUnitofMeasurementFormSuccess(data) {
    try {
        $('#SaveUnitofMeasurementModel').modal('hide');
        if (data._Success === true) {
            CleareUnitofMeasurementFields();
            toastr.success(RecordInsertUpdate);
            GetUnitofMeasurementList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function SaveUnitofMeasurementFormError(x, y, z) {
    ErrorMessage = x.responseJSON._Message;
    toastr.error(ErrorMessage);
}
function CleareUnitofMeasurementFields() {
    $('#SaveUnitofMeasurementModel #IsActive').prop('checked', true);
    $('#SaveUnitofMeasurementModel #UnitofMeasurementID').val("0");
    $('#SaveUnitofMeasurementModel #UnitofMeasurementName').val("");
    $('#DeleteUnitofMeasurementModel #UnitofMeasurementID').val("0");
    var validationMessages = document.querySelectorAll(".field-validation-error");

    // Loop through the messages and clear them
    for (var i = 0; i < validationMessages.length; i++) {
        validationMessages[i].textContent = "";
    }
}
// #endregion

//#region Delete UnitofMeasurement
function ConfirmationDeleteUnitofMeasurement(id) {
    $('#DeleteUnitofMeasurementModel #UnitofMeasurementID').val(id);
}
function DeleteUnitofMeasurement() {
    var tempInAtiveID = $('#DeleteUnitofMeasurementModel #UnitofMeasurementID').val();
    ajaxServiceMethod($('#hdnBaseURL').val() + DeleteUnitofMeasurementByIdUrl + "/" + tempInAtiveID, 'POST', DeleteUnitofMeasurementByIdSuccess, DeleteUnitofMeasurementByIdError);
}
function DeleteUnitofMeasurementByIdSuccess(data) {
    try {
        if (data._Success === true) {
            CleareUnitofMeasurementFields();
            toastr.success(RecordDelete);
            GetUnitofMeasurementList();
        }
        else {
            toastr.error(data._Message);
        }
    } catch (e) {
        toastr.error('Error:' + e.message);
    }
}
function DeleteUnitofMeasurementByIdError(x, y, z) {
    toastr.error(ErrorMessage);
}
//#endregion